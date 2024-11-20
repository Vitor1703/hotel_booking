using Application.Rooms.Dtos;
using Application.Rooms.Ports;
using Application.Rooms.Requests;
using Moq;
using NUnit.Framework;

namespace ApplicationTest
{
    public class RoomManagerTests
    {
        private Mock<IRoomManager> _mockRoomManager;

        [SetUp]
        public void SetUp()
        {
            // Mock do RoomManager
            _mockRoomManager = new Mock<IRoomManager>();

            // Mock dos métodos
            _mockRoomManager.Setup(m => m.GetAllRoomsAsync())
                .ReturnsAsync(Enumerable.Range(1, 10).Select(i => new RoomDto
                {
                    Id = i,
                    Name = $"Room {i}",
                    Level = i,
                    IsInMaintenance = i % 2 == 0,
                    PriceValue = i * 50,
                    Currency = "USD"
                }));
        }

        [Test]
        public async Task ShouldCreateRoomWithSuccess()
        {
            // Arrange
            var request = new CreateRoomRequest
            {
                Name = "VIP Room",
                Level = 5,
                PriceValue = 300,
                Currency = "USD"
            };

            var expectedRoom = new RoomDto
            {
                Id = 1,
                Name = request.Name,
                Level = request.Level,
                PriceValue = request.PriceValue,
                Currency = request.Currency,
                IsInMaintenance = false
            };

            _mockRoomManager.Setup(m => m.CreateRoomAsync(request))
                .ReturnsAsync(expectedRoom);

            // Act
            var response = await _mockRoomManager.Object.CreateRoomAsync(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Id, Is.EqualTo(expectedRoom.Id));
            Assert.That(response.Name, Is.EqualTo(request.Name));
            Assert.That(response.Level, Is.EqualTo(request.Level));
            Assert.That(response.PriceValue, Is.EqualTo(request.PriceValue));
            Assert.That(response.Currency, Is.EqualTo(request.Currency));
        }

        [Test]
        public async Task ShouldGetRoomWithSuccess()
        {
            // Arrange
            var expectedRoom = new RoomDto
            {
                Id = 1,
                Name = "Room 1",
                Level = 1,
                IsInMaintenance = false,
                PriceValue = 100,
                Currency = "USD"
            };

            _mockRoomManager.Setup(m => m.GetRoomByIdAsync(1))
                .ReturnsAsync(expectedRoom);

            // Act
            var response = await _mockRoomManager.Object.GetRoomByIdAsync(1);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Id, Is.EqualTo(expectedRoom.Id));
            Assert.That(response.Name, Is.EqualTo(expectedRoom.Name));
            Assert.That(response.Level, Is.EqualTo(expectedRoom.Level));
            Assert.That(response.PriceValue, Is.EqualTo(expectedRoom.PriceValue));
            Assert.That(response.Currency, Is.EqualTo(expectedRoom.Currency));
        }

        [Test]
        public async Task ShouldGetAllRooms()
        {
            // Act
            var response = await _mockRoomManager.Object.GetAllRoomsAsync();

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task ShouldUpdateRoomWithSuccess()
        {
            // Arrange
            var updatedRoom = new RoomDto
            {
                Id = 1,
                Name = "Updated Room",
                Level = 3,
                IsInMaintenance = true,
                PriceValue = 250,
                Currency = "USD"
            };

            _mockRoomManager.Setup(m => m.UpdateRoomAsync(1, updatedRoom))
                .ReturnsAsync(true);

            // Act
            var result = await _mockRoomManager.Object.UpdateRoomAsync(1, updatedRoom);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ShouldDeleteRoomWithSuccess()
        {
            // Arrange
            _mockRoomManager.Setup(m => m.DeleteRoomAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _mockRoomManager.Object.DeleteRoomAsync(1);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
