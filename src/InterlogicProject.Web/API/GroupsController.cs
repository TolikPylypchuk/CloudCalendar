using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject.API
{
	[Route("api/[controller]")]
	public class GroupsController : Controller
	{
		private IRepository<Group> groups;

		public GroupsController(IRepository<Group> repo)
		{
			this.groups = repo;
		}

		[HttpGet]
		public IEnumerable<Group> Get()
			=> this.groups.GetAll();

		[HttpGet("{id}")]
		public Group Get(int id)
			=> this.groups.GetById(id);
	}
}
