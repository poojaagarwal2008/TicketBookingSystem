using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _seatService;
    public SeatsController(ISeatService seatService) => _seatService = seatService;

    [HttpPost("book")]
    public async Task<IActionResult> BookSeat([FromBody] BookingRequest request)
    {
        var (success, message) = await _seatService.BookSeatAsync(request.SeatId, request.UserName);
        return success ? Ok(new { success, message }) : Conflict(new { success, message });
    }
}