using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Lecturers))]
	public class Lecturer : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть інформацію про користувача")]
		public string UserId { get; set; }

		[Required(ErrorMessage = "Вкажіть кафедру, до якої належить викладач")]
		public int DepartmentId { get; set; }

		[Required(ErrorMessage =
			"Вкажіть чи цей викладач є завідувачем кафедри")]
		public bool IsHead { get; set; }

		[Required(ErrorMessage =
			"Вкажіть чи цей викладач є деканом факультету")]
		public bool IsDean { get; set; }

		[Required(ErrorMessage =
			"Вкажіть чи цей викладач має права адміністатора")]
		public bool IsAdmin { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public Department Department { get; set; }

		public override string ToString() => this.User.FullName;
	}
}
