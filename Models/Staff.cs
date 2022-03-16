using Hotel.DTOs;

namespace Hotel.Models;



public record Staff
{
    public int StaffId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Gender { get; set; }
    public long Mobile { get; set; }
    public string Shift { get; set; }
    public StaffDTO asDto => new StaffDTO
    {
        Name = Name,
        DateOfBirth = DateOfBirth,
        Gender = Gender,
        Mobile = Mobile,
        Shift = Shift
    };
}