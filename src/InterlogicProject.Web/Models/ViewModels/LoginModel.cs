using System.ComponentModel.DataAnnotations;

namespace InterlogicProject.Models.ViewModels
{
	/// <summary>
	/// A simple model used for logging in.
	/// </summary>
	public class LoginModel
	{
		/// <summary>
		/// The user's email.
		/// </summary>
		[Required]
		[UIHint("email")]
		public string Email { get; set; }

		/// <summary>
		/// The user's password.
		/// </summary>
		[Required]
		[UIHint("password")]
		public string Password { get; set; }
	}
}
