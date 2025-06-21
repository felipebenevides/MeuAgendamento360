using System.Net;

namespace myschedule360.IntegrationTests;

public class HealthCheckTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthCheckTests(TestWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HealthCheck_ShouldReturnHealthy()
    {
        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task HealthController_ShouldReturnHealthy()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("healthy", content);
    }

    [Fact]
    public async Task ReadyCheck_ShouldReturnReady()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/health/ready");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("ready", content);
    }
}