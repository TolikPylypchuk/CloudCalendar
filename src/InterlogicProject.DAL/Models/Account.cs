using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(ApplicationDbContext.Accounts))]
	public class Account : EntityBase
	{
		[Required(ErrorMessage = "Please enter the first name")]
		[StringLength(30)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter the middle name")]
		[StringLength(30)]
		public string MiddleName { get; set; }

		[Required(ErrorMessage = "Please enter the last name")]
		[StringLength(30)]
		public string LastName { get; set; }

		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		[Required(ErrorMessage = "Please enter the email")]
		[StringLength(30)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Please enter the password")]
		[StringLength(30)]
		public string Password { get; set; }
	}
}
