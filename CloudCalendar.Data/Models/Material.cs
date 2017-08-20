using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCalendar.Data.Models
{
	[Table(nameof(AppDbContext.Materials))]
	public class Material : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть назву файлу")]
		[StringLength(100)]
		public string FileName { get; set; }

		[Required(ErrorMessage = "Вкажіть пару")]
		public int ClassId { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }
	}
}
