using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Groups))]
	public class Group : EntityBase
	{
		[Required(ErrorMessage = "Введіть назву групи")]
		[StringLength(10)]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Введіть рік вступу групи")]
		public int Year { get; set; }

		[Required(ErrorMessage = "Вкажіть куротора групи")]
		public int CuratorId { get; set; }
		
		[ForeignKey(nameof(CuratorId))]
		public Lecturer Curator { get; set; }
		
		public virtual ICollection<Student> Students { get; set; } =
			new HashSet<Student>();

		public override string ToString() => this.Name;
	}
}
