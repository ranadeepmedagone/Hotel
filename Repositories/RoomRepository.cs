using Dapper;
using Hotel.Models;
using Hotel.Utilities;

namespace Hotel.Repositories;

public interface IRoomRepository
{
    Task<Room> Create(Room Item);
    Task<bool> Update(Room Item);
    Task<bool> Delete(int RoomId);
    Task<List<Room>> GetList();
    Task<Room> GetById(int RoomId);
    Task<List<Room>> GetListByGuestId(int GuestId);
    Task<List<Room>> GetRoomByScheduleId(int ScheduleId);
    Task<List<Room>> GetRoomByStaffId(int StaffId);
}

public class RoomRepository : BaseRepository, IRoomRepository
{
    public RoomRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Room> Create(Room Item)
    {
        var query = $@"INSERT INTO {TableNames.room} 
        (type, size, price, staff_id) 
        VALUES (@Type, @Size, @Price, @StaffId) 
        RETURNING *";
        using (var con = NewConnection)
            return await con.QuerySingleAsync<Room>(query, Item);

    }

    public async Task<bool> Delete(int RoomId)
    {
        var query = $@"DELETE FROM {TableNames.room} WHERE room_id = @RoomId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { RoomId }) > 0;

    }

    public async Task<Room> GetById(int RoomId)
    {
        var query = $@"SELECT r.* FROM {TableNames.room} r
        LEFT JOIN {TableNames.room} s ON s.room_id = r.Room_id 
        WHERE r.room_id = @RoomId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Room>(query, new { RoomId });
    }

    public async Task<List<Room>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.room}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Room>(query)).AsList();

    }

    public async Task<List<Room>> GetListByGuestId(int GuestId)
    {
        var query = $@"SELECT r.* FROM {TableNames.schedule} s 
        LEFT JOIN {TableNames.room} r ON r.room_id = s.room_id 
        WHERE s.guest_id = @GuestId";

        // LEFT JOIN {TableNames.guest} g ON g.id = s.guest_id 

        using (var con = NewConnection)
            return (await con.QueryAsync<Room>(query, new { GuestId })).AsList();
    }

    public async Task<List<Room>> GetRoomByScheduleId(int ScheduleId)
    {
       var query = $@"SELECT * FROM ""{TableNames.room}""
        WHERE room_id = @ScheduleId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Room>(query,new{ScheduleId})).AsList();
           return res;
        }
    }

    public async Task<List<Room>> GetRoomByStaffId(int StaffId)
    {
        var query = $@"SELECT * FROM ""{TableNames.room}""
        WHERE staff_id = @StaffId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Room>(query,new{StaffId})).AsList();
           return res;
        }
    }

    public async Task<bool> Update(Room Item)
    {
        var query = $@"UPDATE {TableNames.room} 
        SET type = @Type, size = @Size, price = @Price WHERE room_id = @RoomId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }
}