using Day1APISolution.DTO;
using Day1APISolution.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Day1APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _usermanager;
		public AccountController(UserManager<ApplicationUser> usermanager)
		{ 
		_usermanager= usermanager;
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
			//generate token
		}
	}
}
