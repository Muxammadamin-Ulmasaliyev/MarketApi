using System.ComponentModel.DataAnnotations;

namespace EComerceApi.Models
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Username is required!")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Phone number is required!")]
		public string PhoneNumber { get; set; }
		[EmailAddress]
		[Required(ErrorMessage = "Email is required!")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required!")]
		public string Password { get; set; }
	}
}
