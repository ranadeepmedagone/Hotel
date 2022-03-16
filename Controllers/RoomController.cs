using Microsoft.AspNetCore.Mvc;
using Hotel.Models;
using Hotel.Repositories;
using Hotel.DTOs;

namespace Hotel.Controllers;

[ApiController]
[Route("api/Room")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomRepository _Room;
    private readonly IStaffRepository _staff;
    private readonly IScheduleRepository _schedule;

    public RoomController(ILogger<RoomController> logger, IRoomRepository Room,IStaffRepository staff,IScheduleRepository schedule)
    {
        _logger = logger;
        _Room = Room;
        _staff = staff;
        _schedule = schedule;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {
        var res = await _Room.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{room_id}")]
    public async Task<ActionResult> GetById([FromRoute] int room_id)
    {
         var res = await _Room.GetById(room_id);
         if(res is null)
          return NotFound();

         var dto = res.asDto;
        dto.Staff = (await _staff.GetStaffByRoomId(room_id)).Select(x => x.asDto).ToList(); 
        dto.Schedule = (await _schedule.GetScheduleByRoomId(room_id)).Select(x => x.asDto).ToList(); 

        return Ok(dto);
    }

    [HttpPost]
    
    public async Task<ActionResult<RoomDTO>> Create([FromBody] CreateRoomDTO Data)
    {
        var toCreateRoom = new Room
        {
            Type = Data.Type,
            Size = Data.Size,
            Price = Data.Price,
            StaffId = Data.StaffId,
        
        };
        var createdRoom = await _Room.Create(toCreateRoom);

        return StatusCode(StatusCodes.Status201Created, createdRoom.asDto);
    }

    

    [HttpPut("{room_id}")]
    public async Task<ActionResult> UpdateRoom([FromRoute] int room_id,
    [FromBody] UpdateRoomDTO Data)
    {
        var existing = await _Room.GetById(room_id);
        if (existing is null)
            return NotFound("No Room found with given id");

        var toUpdateRoom = existing with
        {
            
            Type = Data.Type?.Trim() ?? existing.Type,
            Size = Data.Size,
            Price = Data.Price,
            
            // Mobile = Data.Mobile,
            // DateOfBirth = Data.DateOfBirth,
        };

        var didUpdate = await _Room.Update(toUpdateRoom);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }


    


    [HttpDelete("{room_id}")]
    public async Task<ActionResult> Delete([FromRoute] int room_id)
    {
        var existing = await _Room.GetById(room_id);
        if (existing is null)
            return NotFound("No Room found with given Room Id");

        var didDelete = await _Room.Delete(room_id);

        return NoContent();
    }
}
