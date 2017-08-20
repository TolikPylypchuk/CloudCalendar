using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;
using CloudCalendar.Web.Models.Dto;

namespace CloudCalendar.Web.Controllers
{
	/// <summary>
	/// An API for subjects.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class SubjectsController : Controller
    {
		private IRepository<Subject> subjects;

		/// <summary>
		/// Initializes a new instance of the SubjectsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public SubjectsController(IRepository<Subject> repo)
		{
			this.subjects = repo;
		}

		/// <summary>
		/// Gets all subjects from the database.
		/// </summary>
		/// <returns>All subjects from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<SubjectDto>))]
		public IEnumerable<SubjectDto> GetAll()
			=> this.subjects.GetAll()?.ProjectTo<SubjectDto>();

		/// <summary>
		/// Gets a subject with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the subject to get.</param>
		/// <returns>A subject with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(SubjectDto))]
		public SubjectDto GetById([FromRoute] int id)
			=> Mapper.Map<SubjectDto>(this.subjects.GetById(id));

		/// <summary>
		/// Adds a new subject to the database.
		/// </summary>
		/// <param name="subjectDto">The subject to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] SubjectDto subjectDto)
		{
			if (subjectDto?.Name == null)
			{
				return this.BadRequest();
			}

			var subjectToAdd = new Subject { Name = subjectDto.Name };

			this.subjects.Add(subjectToAdd);

			subjectDto.Id = subjectToAdd.Id;

			return this.CreatedAtAction(
				nameof(this.GetById), new { id = subjectDto.Id }, subjectDto);
		}

		/// <summary>
		/// Updates a subject.
		/// </summary>
		/// <param name="id">The ID of the subject to update.</param>
		/// <param name="subjectDto">The subject to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] SubjectDto subjectDto)
		{
			if (subjectDto?.Name == null)
			{
				return this.BadRequest();
			}

			var subjectToUpdate = this.subjects.GetById(id);

			if (subjectToUpdate == null)
			{
				return this.NotFound();
			}

			subjectToUpdate.Name = subjectDto.Name;
			this.subjects.Update(subjectToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a subject.
		/// </summary>
		/// <param name="id">The ID of the subject to update.</param>
		/// <param name="subjectDto">The subject to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] SubjectDto subjectDto)
		{
			if (subjectDto?.Name == null)
			{
				return this.BadRequest();
			}

			var subjectToUpdate = this.subjects.GetById(id);

			if (subjectToUpdate == null)
			{
				return this.NotFound();
			}

			subjectToUpdate.Name = subjectDto.Name;
			this.subjects.Update(subjectToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a subject.
		/// </summary>
		/// <param name="id">The ID of the subject to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete([FromRoute] int id)
		{
			var subjectToDelete = this.subjects.GetById(id);

			if (subjectToDelete == null)
			{
				return this.NotFound();
			}

			this.subjects.Delete(subjectToDelete);

			return this.NoContent();
		}
	}
}
