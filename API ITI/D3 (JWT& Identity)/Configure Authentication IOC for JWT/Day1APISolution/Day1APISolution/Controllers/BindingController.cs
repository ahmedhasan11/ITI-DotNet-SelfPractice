using Day1APISolution.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day1APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BindingController : ControllerBase
	{
		[HttpGet("{name:alpha}/{age:int}")]
		public IActionResult testprimitive(int age , string name)
		{
			return Ok();
		}
		[HttpGet("{name}")]
		public IActionResult TestObj(Department department, string name)
		{
			return Ok();
		}
		[HttpGet("{id}/{name}/{managername}")]
		public IActionResult TestCustomObj(int id , string managername, string name)
		{
			//its ok but what if we want to et these deatils from route in a dept obj
			return Ok();
		}

		[HttpGet("{ID}/{Name}/{ManagerName}")]
		public IActionResult TestCustomObj([FromRoute] Department department)
		{
			//but why we did this? because by default model binding bind the 
			//complex types from body , so it will be null if you didnt do [FromRoute]
			
			return Ok();
		}
	}
}
