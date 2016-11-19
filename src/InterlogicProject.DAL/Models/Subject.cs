using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Subjects))]
	public class Subject : EntityBase
	{
		[Required(ErrorMessage = "Введіть назву предмету")]
		public string Name { get; set; }
	}
}
