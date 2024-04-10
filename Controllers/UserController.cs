using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly StoreContext _context;
        public UserController(StoreContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsValidEmail(model.Email))
            {
                ModelState.AddModelError("Email", "Invalid Email address.");
                return BadRequest(ModelState);
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, 14);
            var username_lower = model.Username.ToLower();
            var email_lower = model.Email.ToLower();
            var userData = new User { Username = username_lower, Email = email_lower, Password = hashedPassword };
            await _context.Users.AddAsync(userData);
            await _context.SaveChangesAsync();
            return Ok("User Successfully Registered");
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Email_lower = model.Email.ToLower();
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == Email_lower);
            if (user == null)
            {
                return BadRequest("User Not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return Unauthorized("Incorrect password");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_herekjhsdkfksdjfkljsdklfjklsdjklfjklasj"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name , user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var sectoken = new JwtSecurityToken(

                issuer: "http://localhost:5286",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(sectoken);
            return Ok(token);

        }
        [Authorize]
        [HttpPost("profile")]
        public ActionResult profile()
        {
            var name = User.Identity.Name;
            var user = _context.Users.SingleOrDefault(u => u.Username == name);
            return Ok(user);
        }


    }










    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
