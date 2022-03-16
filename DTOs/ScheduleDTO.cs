using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hotel.DTOs;

public record ScheduleDTO
{
    [JsonPropertyName("id")]
    public int ScheduleId { get; set; }

    [JsonPropertyName("check_in")]
    public DateTimeOffset CheckIn { get; set; }

    [JsonPropertyName("check_out")]
    public DateTimeOffset CheckOut { get; set; }

    [JsonPropertyName("guest_count")]
    public int GuestCount { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    // [JsonPropertyName("room")]
    // public RoomDTO Room { get; set; }

    [JsonPropertyName("room")]
    public List<RoomDTO> Room { get; set; }


    [JsonPropertyName("guest")]
    public List<GuestDTO> Guest { get; set; }


}

public record CreateScheduleDTO
{

    [Required]

    [JsonPropertyName("check_in")]
    public DateTimeOffset CheckIn { get; set; }
    [Required]

    [JsonPropertyName("check_out")]
    public DateTimeOffset CheckOut { get; set; }
    [Required]

    [JsonPropertyName("guest_count")]
    public int GuestCount { get; set; }


    [JsonPropertyName("price")]
    [Required]
    public double Price { get; set; }
    [JsonPropertyName("guest_id")]
    [Required]
    public int GuestId { get; set; }
    [JsonPropertyName("room_id")]
    [Required]
    public int RoomId { get; set; }


}


public record UpdateScheduleDTO
{

    [Required]

    [JsonPropertyName("check_in")]
    public DateTimeOffset CheckIn { get; set; }
    [Required]

    [JsonPropertyName("check_out")]
    public DateTimeOffset CheckOut { get; set; }
    [Required]

    [JsonPropertyName("guest_count")]
    public int GuestCount { get; set; }


    [JsonPropertyName("guest_id")]
    [Required]
    public int GuestId { get; set; }
    [JsonPropertyName("room_id")]
    [Required]
    public int RoomId { get; set; }


}