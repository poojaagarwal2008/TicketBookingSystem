public interface ISeatRepository
{
    Task<Seat?> GetSeatByIdAsync(int id);
    Task<bool> SaveChangesAsync();
}
