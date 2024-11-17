namespace HTTPTest.Booking;

public class BookingApiTests
{
    private readonly HttpClient _client;

    public BookingApiTests()
    {
        var applicationFactory = new CustomWebApplicationFactory<Program>();
        _client = applicationFactory.CreateClient();
    }

    [Fact]
    public async Task CreateBooking_ReturnsBadRequest_WhenRoomIsOccupied()
    {
        // Arrange
        var newBooking = new
        {
            RoomId = 1,
            GuestId = 2,
            CheckIn = DateTime.UtcNow.AddHours(1),
            CheckOut = DateTime.UtcNow.AddHours(5)
        };

        // Act
        var response = await _client.PostAsJsonAsync("/booking", newBooking);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetBookingById_ReturnsBooking_WhenExists()
    {
        // Arrange
        var bookingId = 1;

        // Act
        var response = await _client.GetAsync($"/booking/{bookingId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var booking = await response.Content.ReadFromJsonAsync<BookingDto>();
        Assert.NotNull(booking);
        Assert.Equal(bookingId, booking.Id);
    }
}
