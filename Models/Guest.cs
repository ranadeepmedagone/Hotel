using Hotel.DTOs;

namespace Hotel.Models;



public record Guest
{
    public int GuestId { get; set; }
    public string Name { get; set; }
    public long Mobile { get; set; }
    public string Email { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }

    public GuestDTO asDto => new GuestDTO
    {
        Email = Email,
        GuestId = GuestId,
        Mobile = Mobile,
        Name = Name,
    };
}