using System.ComponentModel.DataAnnotations;

public class BookingRequest
{
    [Required(ErrorMessage = "SeatId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "SeatId must be a positive number")]
    public int SeatId { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 100 characters")]
    public string UserName { get; set; } = string.Empty;
}
