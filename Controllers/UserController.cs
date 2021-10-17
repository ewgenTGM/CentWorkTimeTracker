using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
       
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users.Where(user => user.UserType == 0).ToList());
        }
    }
}
