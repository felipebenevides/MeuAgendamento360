using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using myschedule360.Application.Features.Appointments.Commands;
using myschedule360.Domain.Entities;
using myschedule360.Infrastructure.Data;

namespace myschedule360.IntegrationTests;

public class AppointmentIntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory<Program> _factory;

    public AppointmentIntegrationTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CreateAppointment_ShouldReturnSuccess()
    {
        // Arrange - Setup test data
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var businessId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var serviceId = Guid.NewGuid();
        var staffId = Guid.NewGuid();

        // Create test entities
        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Customer",
            Email = "customer@test.com"
        };

        var customer = new Customer
        {
            Id = customerId,
            PersonId = person.Id,
            Person = person,
            BusinessId = businessId
        };

        var service = new Service
        {
            Id = serviceId,
            Name = "Test Service",
            Price = 100,
            DurationMinutes = 60,
            BusinessId = businessId
        };

        var staffPerson = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Staff",
            Email = "staff@test.com"
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "staff@test.com",
            PasswordHash = "hash",
            PersonId = staffPerson.Id,
            Person = staffPerson,
            BusinessId = businessId
        };

        var staff = new Staff
        {
            Id = staffId,
            UserId = user.Id,
            User = user,
            BusinessId = businessId,
            IsActive = true
        };

        context.Persons.AddRange(person, staffPerson);
        context.Users.Add(user);
        context.Customers.Add(customer);
        context.Services.Add(service);
        context.Staff.Add(staff);
        await context.SaveChangesAsync();

        var command = new CreateAppointmentCommand(
            DateTime.UtcNow.AddDays(1),
            customerId,
            serviceId,
            staffId,
            businessId,
            "Test appointment"
        );

        var json = JsonSerializer.Serialize(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/v1/appointments", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}