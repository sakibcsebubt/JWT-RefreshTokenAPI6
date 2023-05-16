using JWT_RefreshTokenAPI6.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_RefreshTokenAPI6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DemoController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData() 
        {

            return Ok(new StandardResponseModel { Status = true, StatusCode = 200, Massage = "GETData"});
        }

    }
}
