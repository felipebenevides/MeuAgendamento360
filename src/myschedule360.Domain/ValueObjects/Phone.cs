namespace myschedule360.Domain.ValueObjects;

public record Phone
{
    public string Value { get; }

    public Phone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone cannot be empty");

        var cleanPhone = value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        
        if (!IsValid(cleanPhone))
            throw new ArgumentException("Invalid Brazilian phone number");

        Value = cleanPhone;
    }

    private static bool IsValid(string phone)
    {
        // Brazilian phone: 11 digits (with 9) or 10 digits (without 9)
        return phone.Length is 10 or 11 && phone.All(char.IsDigit);
    }

    public string Formatted => Value.Length == 11 
        ? $"({Value[..2]}) {Value[2]} {Value[3..7]}-{Value[7..]}"
        : $"({Value[..2]}) {Value[2..6]}-{Value[6..]}";
}