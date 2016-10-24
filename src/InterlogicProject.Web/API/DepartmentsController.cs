using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject.API
{
	[Route("api/[controller]")]
	public class DepartmentsController : Controller
	{
		private IRepository<Department> departments;

		public DepartmentsController(IRepository<Department> repo)
		{
			this.departments = repo;
		}

		[HttpGet]
		public IEnumerable<Department> Get()
			=> this.departments.GetAll();

		[HttpGet("{id}")]
		public Department Get(int id)
			=> this.departments.GetById(id);
	}
}
