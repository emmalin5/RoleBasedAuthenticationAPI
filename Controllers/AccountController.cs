using auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration=configuration;
            _roleManager=roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerUser, string role)
        {
            //Check User Exists
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status="Error", Message="User already Exist!" }
                    );
            }

            //Add User to Database
            var newUser = new ApplicationUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

           

            //if (result.Succeeded)
            //{
            //    return StatusCode(StatusCodes.Status201Created,
            //       new Response { Status="Success", Message="User Created Success" }
            //       );

            //}
            //else
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //       new Response { Status="Error", Message="User Failed to Create" }
            //       );
            //} 

           //Assign a role
           if(await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(newUser, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response {Status="Error", Message="User Failed to Create" });
                }
                await _userManager.AddToRoleAsync(newUser, role);
                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status="Success", Message="User Created Successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message="Use is not Created."}
                    );
            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginUser)
        {
            //checking the user
            var user = await _userManager.FindByNameAsync(loginUser.Email);
            if(user!= null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                var authClaims = new List<Claim>
                {
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach(var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));

                }
                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });


            }

            return Unauthorized();
            //checking the password

            //climList creation
            //we add role to the list
            //generate the token with the claims..
            //returning the token
        }

        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Ok(new { Message = "Logout successful" });
        //}


        private JwtSecurityToken GetToken(List<Claim> AuthClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)

                ) ;

            return token;
        }
    }
}
