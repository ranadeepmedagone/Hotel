using Microsoft.AspNetCore.Mvc;
using Hotel.Models;
using Hotel.Repositories;
using Hotel.DTOs;

namespace Hotel.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly IStaffRepository _staff;
    private readonly IRoomRepository _room;

    public StaffController(ILogger<StaffController> logger, IStaffRepository staff,IRoomRepository room)
    {
        _logger = logger;
        _staff = staff;
        _room = room;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {
        var res = await _staff.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{staff_id}")]
    public async Task<ActionResult> GetById([FromRoute] int staff_id)
    {
         var res = await _staff.GetById(staff_id);
         if(res is null)
          return NotFound();

         var dto = res.asDto;
        dto.Room = (await _room.GetRoomByStaffId(staff_id)).Select(x => x.asDto).ToList();
        return Ok(dto); 
 

        
    }

    [HttpPost]
    
    public async Task<ActionResult<StaffDTO>> Create([FromBody] StaffCreateDTO Data)
    {
        var toCreateStaff = new Staff
        {
            Name = Data.Name.Trim(),
            Mobile = Data.Mobile,
            // Gender = Data.Gender,
            Shift = Data.Shift,
            
            
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Gender = Data.Gender
        
        };
        var createdStaff = await _staff.Create(toCreateStaff);

        return StatusCode(StatusCodes.Status201Created, createdStaff.asDto);
    }

    

    [HttpPut("{staff_id}")]
    public async Task<ActionResult> UpdateStaff([FromRoute] int staff_id,
    [FromBody] StaffUpdateDTO Data)
    {
        var existing = await _staff.GetById(staff_id);
        if (existing is null)
            return NotFound("No Staff found with given id");

        var toUpdateStaff = existing with
        {
            
            Name = Data.Name?.Trim() ?? existing.Name,
            Mobile = Data.Mobile,
            DateOfBirth = Data.DateOfBirth,

        
        
        };

        var didUpdate = await _staff.Update(toUpdateStaff);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }


    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete([FromRoute] int id)
    // {

    // }
}
