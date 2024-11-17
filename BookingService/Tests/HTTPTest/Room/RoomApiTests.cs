namespace HTTPTest.Room;

public class RoomApiTests
{
    private readonly HttpClient _client;

    public RoomApiTests()
    {
        var applicationFactory = new CustomWebApplicationFactory<Program>();
        _client = applicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetRoomById_ReturnsRoom_WhenExists()
    {
        // Arrange
        var roomId = 1;

        // Act
        var response = await _client.GetAsync($"/room/{roomId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var room = await response.Content.ReadFromJsonAsync<RoomDto>();
        Assert.NotNull(room);
        Assert.Equal(roomId, room.Id);
    }

    [Fact]
    public async Task PostRoom_CreatesRoom_WhenValid()
    {
        // Arrange
        var newRoom = new
        {
            Name = "Suite",
            Level = 5,
            IsInMaintenance = false,
            PriceValue = 250,
            Currency = "DOLLAR"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/room", newRoom);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdRoom = await response.Content.ReadFromJsonAsync<RoomDto>();
        Assert.NotNull(createdRoom);
        Assert.Equal("Suite", createdRoom.Name);
    }

    [Fact]
    public async Task DeleteRoom_ReturnsBadRequest_WhenRoomIsOccupied()
    {
        // Arrange
        var roomId = 1; // Assume que este ID está associado a uma reserva ativa

        // Act
        var response = await _client.DeleteAsync($"/room/{roomId}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
