using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Homeworks))]
	public class Homework: EntityBase
	{
		[Required(ErrorMessage = "Вкажіть файл")]
		[StringLength(100)]
		public string FileName { get; set; }

		[Required(ErrorMessage = "Вкажіть студента")]
		public int StudentId { get; set; }

		[Required(ErrorMessage = "Вкажіть пару")]
		public int ClassId { get; set; }

		[Required(ErrorMessage = "Вкажіть дату та час")]
		public DateTime DateTime { get; set; }

		public bool? Accepted { get; set; }

		[ForeignKey(nameof(StudentId))]
		public Student Student { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }
	}
}
