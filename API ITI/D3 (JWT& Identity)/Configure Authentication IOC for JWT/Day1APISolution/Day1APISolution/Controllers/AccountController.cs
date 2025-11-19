using Day1APISolution.DTO;
using Day1APISolution.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Day1APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _usermanager;
		private readonly IConfiguration _configuration;
		public AccountController(UserManager<ApplicationUser> usermanager, IConfiguration configuration)
		{ 
		_usermanager= usermanager;
		_configuration= configuration;
		}
		[HttpPost("/Register")]
		public async Task<IActionResult> Register(RegisterDTO registerDTO)
		{
			if (ModelState.IsValid)
			{

				ApplicationUser user = new ApplicationUser();
				user.Email = registerDTO.Email;
				user.UserName = registerDTO.Username;
				IdentityResult result = await _usermanager.CreateAsync(user, registerDTO.Password);
				if (result.Succeeded)
				{
					return Ok("Create");
				}
					//return BadRequest(result.Errors.Select(e=>e.Description));
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("Password", item.Description);
					}
			}

				return BadRequest(ModelState);


		}
		[HttpPost("/Login")] //to send the user anme and password in body 
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ApplicationUser user=await _usermanager.FindByNameAsync(loginDTO.Username);
			if (user==null)
			{
				ModelState.AddModelError("Username", "Username OR Password Invalid");
			}
			bool istruepassword= await _usermanager.CheckPasswordAsync(user, loginDTO.Password);
			if (istruepassword==false)
			{
				ModelState.AddModelError("Username", "Username OR Password Invalid");
			}
				//claims
			List<Claim> Ourclaims = new List<Claim>();
			Ourclaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
			Ourclaims.Add(new Claim(ClaimTypes.Name, user.UserName));
			Ourclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
			var roles = await _usermanager.GetRolesAsync(user);
				foreach (var item in roles)
				{
					Ourclaims.Add( new Claim(ClaimTypes.Role, item));
				}

				//security key (encoding of secret key)
			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]));

				//signing credentials
			SigningCredentials OursigningCredentials= new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

				//token design
			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _configuration["JwtOptions:Issuer"],
				audience: _configuration["JwtOptions:Audience"],
				claims: Ourclaims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials:OursigningCredentials);

			return Ok(new
			{
				token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				expiration = DateTime.Now.AddHours(1)
			});

		}


	}
}

