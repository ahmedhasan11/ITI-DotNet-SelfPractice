using System.ComponentModel.DataAnnotations;

namespace Day1APISolution.DTO
{
	public class RegisterDTO
	{
		[Required(ErrorMessage = "username can't be nullable")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Email can't be nullable")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage ="password can't be nullable")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage ="Confirm Password and Password is not the same")]

		public string ConfirmPassword { get; set; }


	}
}
