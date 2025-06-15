using Microsoft.EntityFrameworkCore;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _repository;
    public SeatService(ISeatRepository repository) => _repository = repository;

    public async Task<(bool success, string message)> BookSeatAsync(int seatId, string userName)
    {
        var seat = await _repository.GetSeatByIdAsync(seatId);
        if (seat == null)
            return (false, "Seat not found");

        if (seat.IsBooked)
            return (false, "Seat already booked");

        seat.IsBooked = true;
        seat.BookedBy = userName;
        seat.BookedAt = DateTime.UtcNow;

        try
        {
            var saved = await _repository.SaveChangesAsync();
            return (saved, saved ? "Seat successfully booked." : "Booking failed.");
        }
        catch (DbUpdateConcurrencyException)
        {
            return (false, "Seat booking failed due to concurrent update.");
        }
    }
}
