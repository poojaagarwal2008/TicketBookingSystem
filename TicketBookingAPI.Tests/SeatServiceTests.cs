using Moq;

public class SeatServiceTests
{
    private readonly Mock<ISeatRepository> _seatRepoMock;
    private readonly SeatService _seatService;

    public SeatServiceTests()
    {
        _seatRepoMock = new Mock<ISeatRepository>();
        _seatService = new SeatService(_seatRepoMock.Object);
    }

    [Fact]
    public async Task BookSeatAsync_ShouldReturnSuccess_WhenSeatIsAvailable()
    {
        // Arrange
        var seat = new Seat { Id = 1, IsBooked = false };
        _seatRepoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync(seat);
        _seatRepoMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

        // Assert
        Assert.True(success);
        Assert.Equal("Seat successfully booked.", message);
    }

    [Fact]
    public async Task BookSeatAsync_ShouldReturnConflict_WhenSeatIsAlreadyBooked()
    {
        // Arrange
        var seat = new Seat { Id = 1, IsBooked = true };
        _seatRepoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync(seat);

        // Act
        var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

        // Assert
        Assert.False(success);
        Assert.Equal("Seat already booked", message);
    }

    [Fact]
    public async Task BookSeatAsync_ShouldReturnNotFound_WhenSeatDoesNotExist()
    {
        // Arrange
        _seatRepoMock.Setup(r => r.GetSeatByIdAsync(1)).ReturnsAsync((Seat?)null);

        // Act
        var (success, message) = await _seatService.BookSeatAsync(1, "Pooja");

        // Assert
        Assert.False(success);
        Assert.Equal("Seat not found", message);
    }
}
