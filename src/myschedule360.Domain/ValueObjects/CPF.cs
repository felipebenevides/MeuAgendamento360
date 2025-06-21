namespace myschedule360.Domain.ValueObjects;

public record CPF
{
    public string Value { get; }

    public CPF(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CPF cannot be empty");

        var cleanCpf = value.Replace(".", "").Replace("-", "");
        
        if (!IsValid(cleanCpf))
            throw new ArgumentException("Invalid CPF");

        Value = cleanCpf;
    }

    private static bool IsValid(string cpf)
    {
        if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
            return false;

        var digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();
        
        // First verification digit
        var sum = 0;
        for (int i = 0; i < 9; i++)
            sum += digits[i] * (10 - i);
        
        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;
        
        if (digits[9] != digit1) return false;
        
        // Second verification digit
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += digits[i] * (11 - i);
        
        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;
        
        return digits[10] == digit2;
    }

    public string Formatted => $"{Value[..3]}.{Value[3..6]}.{Value[6..9]}-{Value[9..]}";
}