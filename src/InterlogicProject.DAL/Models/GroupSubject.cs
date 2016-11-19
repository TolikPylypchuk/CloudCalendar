using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.GroupSubjects))]
	public class GroupSubject : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть групу")]
		public int GroupId { get; set; }

		[Required(ErrorMessage = "Вкажіть викладача")]
		public int LecturerId { get; set; }

		[Required(ErrorMessage = "Вкажіть назву предмету")]
		public int SubjectId { get; set; }

		[ForeignKey(nameof(GroupId))]
		public Group Group { get; set; }

		[ForeignKey(nameof(LecturerId))]
		public Lecturer Lecturer { get; set; }

		[ForeignKey(nameof(SubjectId))]
		public Subject Subject { get; set; }
	}
}
