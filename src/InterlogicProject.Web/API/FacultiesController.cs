using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject.API
{
	[Route("api/[controller]")]
	public class FacultiesController : Controller
	{
		private IRepository<Faculty> faculties;

		public FacultiesController(IRepository<Faculty> repo)
		{
			this.faculties = repo;
		}

		[HttpGet]
		public IEnumerable<Faculty> Get()
			=> this.faculties.GetAll();

		[HttpGet("{id}")]
		public Faculty Get(int id)
			=> this.faculties.GetById(id);
	}
}
