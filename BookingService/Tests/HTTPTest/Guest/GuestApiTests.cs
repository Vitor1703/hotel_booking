namespace DomainTests.HTTPTest.Guest;

using System.Net;
using System.Net.Http.Json;
using Xunit;

public class GuestApiTests
{
    private readonly HttpClient _client;

    public GuestApiTests()
    {
        // Configure o HttpClient com o endpoint correto do seu servidor
        var applicationFactory = new CustomWebApplicationFactory<Program>(); // Use sua factory para iniciar o app
        _client = applicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetGuestById_ReturnsGuest_WhenGuestExists()
    {
        // Arrange
        var guestId = 1;

        // Act
        var response = await _client.GetAsync($"/guest/{guestId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var guest = await response.Content.ReadFromJsonAsync<GuestDto>();
        Assert.NotNull(guest);
        Assert.Equal(guestId, guest.Id);
    }

    [Fact]
    public async Task PostGuest_CreatesGuest_WhenValid()
    {
        // Arrange
        var newGuest = new
        {
            Name = "John",
            Surname = "Doe",
            Email = "john.doe@example.com",
            DocumentId = new { IdNumber = "123456789", DocumentType = 1 }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/guest", newGuest);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdGuest = await response.Content.ReadFromJsonAsync<GuestDto>();
        Assert.NotNull(createdGuest);
        Assert.Equal("John", createdGuest.Name);
    }

    [Fact]
    public async Task DeleteGuest_ReturnsBadRequest_WhenGuestHasBookings()
    {
        // Arrange
        var guestId = 2; // Assume que este ID está associado a uma reserva

        // Act
        var response = await _client.DeleteAsync($"/guest/{guestId}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
