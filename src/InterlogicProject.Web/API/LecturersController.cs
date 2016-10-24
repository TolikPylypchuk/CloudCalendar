using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject.API
{
	[Route("api/[controller]")]
	public class LecturersController : Controller
	{
		private IRepository<Lecturer> lecturers;

		public LecturersController(IRepository<Lecturer> repo)
		{
			this.lecturers = repo;
		}

		[HttpGet]
		public IEnumerable<Lecturer> Get()
			=> this.lecturers.GetAll();

		[HttpGet("{id}")]
		public Lecturer Get(int id)
			=> this.lecturers.GetById(id);
	}
}
