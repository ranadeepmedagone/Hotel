using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Hotel.Models;

namespace Hotel.DTOs;

public record RoomDTO
{
    [JsonPropertyName("id")]
    public int RoomId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("staff_id")]
    public int StaffId { get; set; }


    [JsonPropertyName("staff")]
    public List<StaffDTO> Staff { get; set; }

    [JsonPropertyName("schedule")]
    public List<ScheduleDTO> Schedule { get; set; }

}


public record CreateRoomDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(30)]


    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    [Required]
    public int Size { get; set; }
    [JsonPropertyName("price")]
    [Required]
    public double Price { get; set; }

    [JsonPropertyName("staff_id")]
    [Required]
    public int StaffId { get; set; }
}


public record UpdateRoomDTO
{
    


    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    [Required]
    public int Size { get; set; }
    [JsonPropertyName("price")]
    [Required]
    public double Price { get; set; }

    
}