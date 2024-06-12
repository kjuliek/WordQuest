using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WordQuestAPI.Models;

namespace WordQuestAPI.Controllers
{
    [Route("account/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("check-login-status")]
        public IActionResult CheckLoginStatus()
        {
            // Vérifiez ici l'état de connexion de l'utilisateur
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(new {isLoggedIn = true, id = userId });
            }
            else
            {
                return Ok(new {isLoggedIn = false});
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //HttpContext.Session.SetString("UserEmail", email);
                    return Ok("Login successful");
                }
            }

            return Unauthorized("Invalid login attempt");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("UserEmail");
            return Ok("Logout successful");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                return Ok("Registration successful");
            } 
            else
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { Errors = errorMessages });
            }
        }
    }
}
