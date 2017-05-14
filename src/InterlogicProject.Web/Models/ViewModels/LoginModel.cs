using System.ComponentModel.DataAnnotations;

namespace InterlogicProject.Web.Models.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Неправильний email або пароль")]
		[UIHint("email")]
		public string Username { get; set; }
		
		[Required(ErrorMessage = "Неправильний email або пароль")]
		[UIHint("password")]
		public string Password { get; set; }
	}
}
