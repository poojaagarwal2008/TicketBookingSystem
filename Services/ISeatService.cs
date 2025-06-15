public interface ISeatService
{
    Task<(bool success, string message)> BookSeatAsync(int seatId, string userName);
}