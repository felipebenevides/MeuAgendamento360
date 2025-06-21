namespace myschedule360.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiVersionAttribute : Attribute
{
    public string Version { get; }

    public ApiVersionAttribute(string version)
    {
        Version = version;
    }
}