using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AgileWebApi.DataTransferObjects.LoginDTOS;

namespace AgileWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                return Ok(new {id = user.Id, userName = user.UserName});
            }

            return NotFound("Username or password is incorrect");
        }

        private IdentityUser Authenticate(LoginDTO userLogin)
        {
            var currentUser = _userManager.FindByEmailAsync(userLogin.Username + "@otis.com").Result;

            if (currentUser == null) return null;
            // var currentUser = Users.UserLogins.FirstOrDefault(u =>
            //     u.Username.ToLower() == userLogin.Username.ToLower() && u.Password == userLogin.Password);

            if (currentUser != null) return currentUser;

            return null;
        }
    }
}
