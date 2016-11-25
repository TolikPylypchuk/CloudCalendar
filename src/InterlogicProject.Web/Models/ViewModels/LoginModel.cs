using System.ComponentModel.DataAnnotations;

namespace InterlogicProject.Models.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Неправильний email або пароль")]
		[UIHint("email")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "Неправильний email або пароль")]
		[UIHint("password")]
		public string Password { get; set; }
	}
}
