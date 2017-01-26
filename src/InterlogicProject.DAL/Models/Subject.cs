using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Subjects))]
	public class Subject : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть назву")]
		[StringLength(100)]
		public string Name { get; set; }

		public override string ToString() => this.Name;
	}
}
