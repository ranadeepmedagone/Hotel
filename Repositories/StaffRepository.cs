using Dapper;
using Hotel.Models;
using Hotel.Utilities;

namespace Hotel.Repositories;

public interface IStaffRepository
{
    Task<Staff> Create(Staff Item);
    Task<bool> Update(Staff Item);
    Task<bool> Delete(int StaffId);
    Task<List<Staff>> GetList();
    Task<List<Staff>> GetStaffByRoomId(int RoomId);
    Task<Staff> GetById(int StaffId);
}

public class StaffRepository : BaseRepository, IStaffRepository
{
    public StaffRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Staff> Create(Staff Item)
    {
        var query = $@"INSERT INTO {TableNames.staff} 
        (name, mobile, date_of_birth, gender, shift) 
        VALUES (@Name, @Mobile, @DateOfBirth, @Gender, @Shift) 
        RETURNING *";
        using (var con = NewConnection)
            return await con.QuerySingleAsync<Staff>(query, Item);

    }

    public async Task<bool> Delete(int StaffId)
    {
        var query = $@"DELETE FROM {TableNames.staff} WHERE staff_id = @Id";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { StaffId }) > 0;

    }
    
        public async Task<Staff> GetById(int StaffId)
    {
        var query = $@"SELECT * FROM ""{TableNames.staff}""
        WHERE staff_id = @StaffId ";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Staff>(query,
        new{
            StaffId
        });
         
    

    }

    public async Task<List<Staff>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.staff}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Staff>(query)).AsList();

    }

    public async Task<List<Staff>> GetStaffByRoomId(int RoomId)
    {
        var query = $@"SELECT * FROM ""{TableNames.room}""
        WHERE room_id = @RoomId";

        using(var con = NewConnection){
           var res = (await con.QueryAsync<Staff>(query,new{RoomId})).AsList();
           return res;
        }
    }

    public async Task<bool> Update(Staff item)
    {

        var query = $@"UPDATE ""{TableNames.staff}"" SET name = @Name, mobile = @Mobile, date_of_birth = @DateOfBirth
        WHERE staff_id = @StaffId";

        using(var con = NewConnection){

            var rowCount = await con.ExecuteAsync(query,item);
            return rowCount == 1;

        }
        
    }
}