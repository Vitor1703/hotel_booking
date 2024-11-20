using Application.Bookings.Dtos;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class BookingManagerTests
    {
        private Mock<IBookingManager> _mockBookingManager;

        [SetUp]
        public void SetUp()
        {
            // Mock do BookingManager
            _mockBookingManager = new Mock<IBookingManager>();

            // Mock dos métodos
            _mockBookingManager.Setup(m => m.GetAllBookingsAsync())
                .ReturnsAsync(Enumerable.Range(1, 10).Select(i => new BookingDto
                {
                    Id = i,
                    GuestId = i,
                    RoomId = i,
                    Start = DateTime.Now.AddDays(-i),
                    End = DateTime.Now.AddDays(i)
                }));
        }

        [Test]
        public async Task ShouldCreateBookingWithSuccess()
        {
            // Arrange
            var request = new CreateBookingRequest
            {
                GuestId = 1,
                RoomId = 2,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(3)
            };

            var expectedBooking = new BookingDto
            {
                Id = 1,
                GuestId = request.GuestId,
                RoomId = request.RoomId,
                Start = request.CheckIn,
                End = request.CheckOut
            };

            _mockBookingManager.Setup(m => m.CreateBookingAsync(request))
                .ReturnsAsync(expectedBooking);

            // Act
            var response = await _mockBookingManager.Object.CreateBookingAsync(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Id, Is.EqualTo(expectedBooking.Id));
            Assert.That(response.GuestId, Is.EqualTo(request.GuestId));
            Assert.That(response.RoomId, Is.EqualTo(request.RoomId));
            Assert.That(response.Start, Is.EqualTo(request.CheckIn));
            Assert.That(response.End, Is.EqualTo(request.CheckOut));
        }

        [Test]
        public async Task ShouldGetBookingWithSuccess()
        {
            // Arrange
            var expectedBooking = new BookingDto
            {
                Id = 1,
                GuestId = 1,
                RoomId = 2,
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(3)
            };

            _mockBookingManager.Setup(m => m.GetBookingByIdAsync(1))
                .ReturnsAsync(expectedBooking);

            // Act
            var response = await _mockBookingManager.Object.GetBookingByIdAsync(1);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Id, Is.EqualTo(expectedBooking.Id));
            Assert.That(response.GuestId, Is.EqualTo(expectedBooking.GuestId));
            Assert.That(response.RoomId, Is.EqualTo(expectedBooking.RoomId));
            Assert.That(response.Start, Is.EqualTo(expectedBooking.Start));
            Assert.That(response.End, Is.EqualTo(expectedBooking.End));
        }

        [Test]
        public async Task ShouldGetAllBookings()
        {
            // Act
            var response = await _mockBookingManager.Object.GetAllBookingsAsync();

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task ShouldUpdateBookingWithSuccess()
        {
            // Arrange
            var updatedBooking = new BookingDto
            {
                Id = 1,
                GuestId = 2,
                RoomId = 3,
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now.AddDays(4)
            };

            _mockBookingManager.Setup(m => m.UpdateBookingAsync(1, updatedBooking))
                .ReturnsAsync(true);

            // Act
            var result = await _mockBookingManager.Object.UpdateBookingAsync(1, updatedBooking);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ShouldDeleteBookingWithSuccess()
        {
            // Arrange
            _mockBookingManager.Setup(m => m.DeleteBookingAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _mockBookingManager.Object.DeleteBookingAsync(1);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
