public class SeatRepository : ISeatRepository
{
    private readonly BookingDbContext _context;
    public SeatRepository(BookingDbContext context) => _context = context;

    public async Task<Seat?> GetSeatByIdAsync(int id) => await _context.Seats.FindAsync(id);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}
