namespace myschedule360.Domain.ValueObjects;

public record CNPJ
{
    public string Value { get; }

    public CNPJ(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CNPJ cannot be empty");

        var cleanCnpj = value.Replace(".", "").Replace("/", "").Replace("-", "");
        
        if (!IsValid(cleanCnpj))
            throw new ArgumentException("Invalid CNPJ");

        Value = cleanCnpj;
    }

    private static bool IsValid(string cnpj)
    {
        if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0]))
            return false;

        var digits = cnpj.Select(c => int.Parse(c.ToString())).ToArray();
        
        // First verification digit
        var weights1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var sum = digits.Take(12).Select((d, i) => d * weights1[i]).Sum();
        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;
        
        if (digits[12] != digit1) return false;
        
        // Second verification digit
        var weights2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        sum = digits.Take(13).Select((d, i) => d * weights2[i]).Sum();
        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;
        
        return digits[13] == digit2;
    }

    public string Formatted => $"{Value[..2]}.{Value[2..5]}.{Value[5..8]}/{Value[8..12]}-{Value[12..]}";
}