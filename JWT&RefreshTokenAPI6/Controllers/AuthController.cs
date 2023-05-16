using Dapper;
using JWT_RefreshTokenAPI6.Helper;
using JWT_RefreshTokenAPI6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;

namespace JWT_RefreshTokenAPI6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"select * from [User] where Email = @Email and Password= @Password";
                    var result = await connection.QueryAsync<User>(sql, new { Email, Password });
                    if (result.ToList().Count > 0)
                    {
                        var token = new JwtSecurityToken(
                              issuer: _configuration["JWT:Issuer"],
                              audience: _configuration["JWT:SigningKey"],
                              expires: DateTime.Now.AddMinutes(20),
                              signingCredentials: null
                         );
                        return Ok(new StandardResponseModel { Status = true, StatusCode = 200, Massage = "Login SuccessFul", Data = token });
                    }
                    else
                    {
                        return Ok(new StandardResponseModel { Status = false, StatusCode = 401, Massage = "Login UnSuccessFul" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new StandardResponseModel { Status = false, StatusCode = 400, Massage = "Something Went Wrong" });
            }
        }

    }
}
