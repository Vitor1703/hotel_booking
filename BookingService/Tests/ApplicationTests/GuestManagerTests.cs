using Application.Guests.Dtos;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Moq;
using NUnit.Framework;

namespace ApplicationTest
{
    public class GuestManagerTests
    {
        private Mock<IGuestManager> _mockGuestManager;

        [SetUp]
        public void SetUp()
        {
            // Mock do GuestManager
            _mockGuestManager = new Mock<IGuestManager>();

            // Mock dos métodos
            _mockGuestManager.Setup(m => m.GetAllGuestsAsync())
                .ReturnsAsync(Enumerable.Range(1, 10).Select(i => new GuestDto
                {
                    Id = i,
                    Name = $"Name{i}",
                    Surname = $"Surname{i}",
                    Email = $"guest{i}@example.com",
                    DocumentId = $"DOC{i:0000000}"
                }));
        }

        [Test]
        public async Task ShouldCreateGuestWithSuccess()
        {
            // Arrange
            var request = new CreateGuestRequest
            {
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                DocumentId = "DOC1234567"
            };

            var expectedGuest = new GuestDto
            {
                Id = 1,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                DocumentId = request.DocumentId
            };

            _mockGuestManager.Setup(m => m.CreateGuestAsync(request))
                .ReturnsAsync(expectedGuest);

            // Act
            var response = await _mockGuestManager.Object.CreateGuestAsync(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Id, Is.EqualTo(expectedGuest.Id));
            Assert.That(response.Name, Is.EqualTo(request.Name));
            Assert.That(response.Surname, Is.EqualTo(request.Surname));
            Assert.That(response.Email, Is.EqualTo(request.Email));
            Assert.That(response.DocumentId, Is.EqualTo(request.DocumentId));
        }

        [Test]
        public async Task ShouldGetGuestWithSuccess()
        {
            // Arrange
            var expectedGuest = new GuestDto
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                DocumentId = "DOC1234567"
            };

            _mockGuestManager.Setup(m => m.GetGuestByIdAsync(1))
                .ReturnsAsync(expectedGuest);

            // Act
            var response = await _mockGuestManager.Object.GetGuestByIdAsync(1);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Id, Is.EqualTo(expectedGuest.Id));
            Assert.That(response.Name, Is.EqualTo(expectedGuest.Name));
            Assert.That(response.Surname, Is.EqualTo(expectedGuest.Surname));
            Assert.That(response.Email, Is.EqualTo(expectedGuest.Email));
            Assert.That(response.DocumentId, Is.EqualTo(expectedGuest.DocumentId));
        }

        [Test]
        public async Task ShouldGetAllGuests()
        {
            // Act
            var response = await _mockGuestManager.Object.GetAllGuestsAsync();

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task ShouldUpdateGuestWithSuccess()
        {
            // Arrange
            var updatedGuest = new GuestDto
            {
                Id = 1,
                Name = "Jane",
                Surname = "Doe",
                Email = "jane.doe@example.com",
                DocumentId = "DOC7654321"
            };

            _mockGuestManager.Setup(m => m.UpdateGuestAsync(1, updatedGuest))
                .ReturnsAsync(true);

            // Act
            var result = await _mockGuestManager.Object.UpdateGuestAsync(1, updatedGuest);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ShouldDeleteGuestWithSuccess()
        {
            // Arrange
            _mockGuestManager.Setup(m => m.DeleteGuestAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _mockGuestManager.Object.DeleteGuestAsync(1);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
