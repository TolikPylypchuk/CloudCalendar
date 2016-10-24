using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject.API
{
	[Route("api/[controller]")]
	public class StudentsController : Controller
	{
		private IRepository<Student> students;

		public StudentsController(IRepository<Student> repo)
		{
			this.students = repo;
		}

		[HttpGet]
		public IEnumerable<Student> Get()
			=> this.students.GetAll();
		
		[HttpGet("{id}")]
		public Student Get(int id)
			=> this.students.GetById(id);
	}
}
