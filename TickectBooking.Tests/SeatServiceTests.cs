using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace TicketBookingSystem.Tests
{
    public class SeatServiceTests
    {
        private readonly Mock<ISeatRepository> _repoMock;
        private readonly SeatService _seatService;

        public SeatServiceTests()
        {
            _repoMock = new Mock<ISeatRepository>();
            _seatService = new SeatService(_repoMock.Object);
        }

        [Fact]
        public async Task BookSeatAsync_ShouldReturnSuccess_WhenSeatIsAvailable()
        {
            _repoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync(new Seat { Id = 1, IsBooked = false });
            _repoMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

            Assert.True(success);
            Assert.Equal("Seat successfully booked.", message);
        }

        [Fact]
        public async Task BookSeatAsync_ShouldReturnConflict_WhenSeatAlreadyBooked()
        {
            _repoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync(new Seat { Id = 1, IsBooked = true });

            var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

            Assert.False(success);
            Assert.Equal("Seat already booked", message);
        }

        [Fact]
        public async Task BookSeatAsync_ShouldReturnNotFound_WhenSeatDoesNotExist()
        {
            _repoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync((Seat?)null);

            var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

            Assert.False(success);
            Assert.Equal("Seat not found", message);
        }

        [Fact]
        public async Task BookSeatAsync_ShouldReturnFailure_WhenSaveFails()
        {
            _repoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync(new Seat { Id = 1, IsBooked = false });
            _repoMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(false);

            var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

            Assert.False(success);
            Assert.Equal("Booking failed.", message);
        }

        [Fact]
        public async Task BookSeatAsync_ShouldHandleConcurrencyException_Gracefully()
        {
            _repoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync(new Seat { Id = 1, IsBooked = false });
            _repoMock.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());

            var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

            Assert.False(success);
            Assert.Equal("Seat booking failed due to concurrent update.", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public async Task BookSeatAsync_ShouldFail_ForInvalidSeatIds(int invalidSeatId)
        {
            _repoMock.Setup(r => r.GetSeatByIdAsync(invalidSeatId)).ReturnsAsync((Seat?)null);

            var (success, message) = await _seatService.BookSeatAsync(invalidSeatId, "Pooja");

            Assert.False(success);
            Assert.Equal("Seat not found", message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task BookSeatAsync_ShouldStillWork_WithEmptyUsername(string? name)
        {
            _repoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync(new Seat { Id = 1, IsBooked = false });
            _repoMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            var (success, message) = await _seatService.BookSeatAsync(1, name ?? "");

            Assert.True(success);
            Assert.Equal("Seat successfully booked.", message);
        }
    }
}
