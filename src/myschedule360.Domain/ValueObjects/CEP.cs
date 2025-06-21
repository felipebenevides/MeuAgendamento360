namespace myschedule360.Domain.ValueObjects;

public record CEP
{
    public string Value { get; }

    public CEP(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CEP cannot be empty");

        var cleanCep = value.Replace("-", "");
        
        if (!IsValid(cleanCep))
            throw new ArgumentException("Invalid CEP");

        Value = cleanCep;
    }

    private static bool IsValid(string cep)
    {
        return cep.Length == 8 && cep.All(char.IsDigit);
    }

    public string Formatted => $"{Value[..5]}-{Value[5..]}";
}