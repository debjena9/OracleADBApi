using Microsoft.AspNetCore.Mvc;

namespace OracleDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public UsersController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _databaseService.GetUsers();
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Post([FromQuery] string name, [FromQuery] string age)
        {
            await _databaseService.CreateUser(new User() { Name = name, Age = Convert.ToInt32(age) });
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
