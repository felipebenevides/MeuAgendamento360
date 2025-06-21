using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using myschedule360.Application.Features.Authentication.Commands;
using myschedule360.Infrastructure.Data;

namespace myschedule360.IntegrationTests;

public class AuthenticationIntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory<Program> _factory;

    public AuthenticationIntegrationTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task RegisterBusinessOwner_ShouldReturnSuccess()
    {
        // Arrange
        var command = new RegisterBusinessOwnerCommand(
            "Test Business",
            "John",
            "Doe",
            "john@test.com",
            "password123",
            "12345678901",
            "11999999999",
            "Test Street",
            "123",
            "Test Neighborhood",
            "Test City",
            "SP",
            "01234567"
        );

        var json = JsonSerializer.Serialize(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/v1/auth/register-business-owner", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnToken()
    {
        // Arrange - First register a user
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Register user first
        var registerCommand = new RegisterBusinessOwnerCommand(
            "Test Business",
            "Jane",
            "Doe",
            "jane@test.com",
            "password123",
            "12345678902",
            "11888888888",
            "Test Street",
            "456",
            "Test Neighborhood",
            "Test City",
            "SP",
            "01234567"
        );

        var registerJson = JsonSerializer.Serialize(registerCommand);
        var registerContent = new StringContent(registerJson, Encoding.UTF8, "application/json");
        await _client.PostAsync("/api/v1/auth/register-business-owner", registerContent);

        // Now login
        var loginCommand = new LoginUserCommand("jane@test.com", "password123");
        var loginJson = JsonSerializer.Serialize(loginCommand);
        var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/v1/auth/login", loginContent);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("token", responseContent);
    }
}