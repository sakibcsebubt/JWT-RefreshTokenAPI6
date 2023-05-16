using Dapper;
using JWT_RefreshTokenAPI6.Helper;
using JWT_RefreshTokenAPI6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace JWT_RefreshTokenAPI6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"select * from [User]";
                    var result = await connection.QueryAsync<User>(sql, new { Id = 1 });

                    return Ok(new StandardResponseModel { Status = true, StatusCode = 200, Data = result });
                }
            }
            catch (Exception ex) {
                return Ok(new StandardResponseModel { Status = false, StatusCode = 400, Massage = "Something Went Wrong" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(User model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"Insert into [User] ( Email, FirstName, LastName, Password, PasswordSalt, TS, Active)
                                Values (@Email, @FirstName, @LastName, @Password, @PasswordSalt, @TS, @Active);";
                    var result = await connection.ExecuteAsync(sql, model);
                    if(result > 0)
                    {
                        return Ok(new StandardResponseModel { Status = true, StatusCode = 200, Massage = "Succesfully Inserted" });

                    }
                    else
                    {
                        return Ok(new StandardResponseModel { Status = false, StatusCode = 400, Massage = "No Data Insert" });

                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new StandardResponseModel { Status = false, StatusCode = 400, Massage = "Something Went Wrong",Data =ex.Message });
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByNameAsync( string Name)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"select * from [User] where FirstName = @Name";
                    var result = await connection.QueryAsync<User>(sql, new { Name });

                    return Ok(new StandardResponseModel { Status = true, StatusCode = 200, Data = result });
                }
            }
            catch (Exception ex)
            {
                return Ok(new StandardResponseModel { Status = false, StatusCode = 400, Massage = "Something Went Wrong" });
            }
        }
    }
}
