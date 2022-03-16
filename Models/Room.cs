using Hotel.DTOs;
namespace Hotel.Models;



public record Room
{
    public int RoomId { get; set; }
    public string Type { get; set; }
    public int Size { get; set; }
    public double Price { get; set; }
    public int StaffId { get; set; }


    public RoomDTO asDto => new RoomDTO
    {
        RoomId = RoomId,
        Size = Size,
        Type = Type.ToString()
        
    };
}