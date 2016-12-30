using System.ComponentModel.DataAnnotations;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.Web.Models.ViewModels
{
	public class EditStudentModel
	{
		public Student CurrentStudent { get; set; }
		public bool IsEditing { get; set; }

		public int Id { get; set; } = -1;

		[Required(ErrorMessage = "Вкажіть ім'я")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Вкажіть по-батькові")]
		public string MiddleName { get; set; }

		[Required(ErrorMessage = "Вкажіть прізвище")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Вкажіть email")]
		[EmailAddress(ErrorMessage = "Вкажіть правильний email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Вкажіть групу")]
		public string GroupName { get; set; }

		[Required(ErrorMessage = "Вкажіть номер заліковки")]
		public string TranscriptNumber { get; set; }

		public bool IsGroupLeader { get; set; }
	}
}
