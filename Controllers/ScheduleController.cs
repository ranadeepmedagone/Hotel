using Microsoft.AspNetCore.Mvc;
using Hotel.Models;
using Hotel.Repositories;
using Hotel.DTOs;

namespace Hotel.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly IScheduleRepository _Schedule;
    private readonly IGuestRepository _guest;
    private readonly IRoomRepository _room;

    public ScheduleController(ILogger<ScheduleController> logger, IScheduleRepository Schedule,IGuestRepository guest,IRoomRepository room)
    {
        _logger = logger;
        _Schedule = Schedule;
        _guest = guest;
        _room = room;
    }

    // [HttpGet]
    // public async Task<ActionResult> GetList()
    // {
    //     var res = await _Schedule.GetList();

    //     return Ok(res.Select(x => x.asDto));
    // }

    [HttpGet("{schedule_id}")]
    public async Task<ActionResult> GetById([FromRoute] int schedule_id)
    {
         var res = await _Schedule.GetById(schedule_id);
         if(res is null)
          return NotFound();

         var dto = res.asDto;
        dto.Guest = (await _guest.GetGuestByScheduleId(res.GuestId)).Select(x => x.asDto).ToList(); 
        dto.Room = (await _room.GetRoomByScheduleId(res.RoomId)).Select(x => x.asDto).ToList(); 
        return Ok(dto);
        


        

    }

    [HttpPost]
    
    public async Task<ActionResult<ScheduleDTO>> Create([FromBody] CreateScheduleDTO Data)
    {
        var toCreateSchedule = new Schedule
        {
              CheckIn = Data.CheckIn,
              CheckOut = Data.CheckOut,
              GuestCount = Data.GuestCount,
              Price = Data.Price,
              GuestId = Data.GuestId,
              RoomId = Data.RoomId,




        
        };
        var createdSchedule = await _Schedule.Create(toCreateSchedule);

        return StatusCode(StatusCodes.Status201Created, createdSchedule.asDto);
    }

    

    [HttpPut("{schedule_id}")]
    public async Task<ActionResult> UpdateSchedule([FromRoute] int schedule_id,
    [FromBody] UpdateScheduleDTO Data)
    {
        var existing = await _Schedule.GetById(schedule_id);
        if (existing is null)
            return NotFound("No Schedule found with given id");

        var toUpdateSchedule = existing with
        {

            CheckIn = Data.CheckIn,
              CheckOut = Data.CheckOut,
              GuestCount = Data.GuestCount,
        
              GuestId = Data.GuestId,
              RoomId = Data.RoomId
        };

        var didUpdate = await _Schedule.Update(toUpdateSchedule);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }


    


    [HttpDelete("{schedule_id}")]
    public async Task<ActionResult> Delete([FromRoute] int schedule_id)
    {
        var existing = await _Schedule.GetById(schedule_id);
        if (existing is null)
            return NotFound("No Schedule found with given Schedule Id");

        var didDelete = await _Schedule.Delete(schedule_id);

        return NoContent();
    }
}
