using KCMS.Domain.User;
using KCMS.Domain.ViewModel;
using KCMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public UsersController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // GET: api/Users/5
        [HttpGet()]
        public ActionResult<User> GetUser()
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            long userId = long.Parse(token.Issuer);
            var user = _userService.GetUser(userId);
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<User>> PostUser(UserInsertModel model)
        {
            var user = await _userService.AddUser(model);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost("Login")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Login(UserLoginModel model)
        {
            var jwt = _userService.Login(model);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "Success" });
        }

        [HttpPost("Logout")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Success" });
        }
    }
}
