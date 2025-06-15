using System.ComponentModel.DataAnnotations;

public class Seat
{
    public int Id { get; set; }
    public bool IsBooked { get; set; }
    public string? BookedBy { get; set; }
    public DateTime? BookedAt { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
