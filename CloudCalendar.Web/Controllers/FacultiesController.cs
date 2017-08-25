using System.Collections.Generic;
using System.Linq;

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
	/// An API for faculties.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class FacultiesController : Controller
	{
		private IRepository<Faculty> faculties;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="FacultiesController"/> class.
		/// </summary>
		/// <param name="repo">
		/// The repository of faculties that this instance will use.
		/// </param>
		public FacultiesController(IRepository<Faculty> repo)
		{
			this.faculties = repo;
		}

		/// <summary>
		/// Gets all faculties from the database.
		/// </summary>
		/// <returns>All faculties from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<FacultyDto>))]
		public IEnumerable<FacultyDto> GetAll()
			=> this.faculties.GetAll()?.ProjectTo<FacultyDto>();

		/// <summary>
		/// Gets a faculty with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the faculty to get.</param>
		/// <returns>A faculty with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(FacultyDto))]
		public FacultyDto GetById([FromRoute] int id)
			=> Mapper.Map<FacultyDto>(this.faculties.GetById(id));

		/// <summary>
		/// Gets all faculties with the specified building.
		/// </summary>
		/// <param name="buildingId">The ID of the building.</param>
		/// <returns>All faculties with the specified building.</returns>
		[HttpGet("buildingId/{buildingId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<FacultyDto>))]
		public IEnumerable<FacultyDto> GetForBuilding(
			[FromRoute] int buildingId)
			=> this.faculties.GetAll()
							?.Where(f => f.BuildingId == buildingId)
							 .ProjectTo<FacultyDto>();

		/// <summary>
		/// Adds a new faculty to the database.
		/// </summary>
		/// <param name="facultyDto">The faculty to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] FacultyDto facultyDto)
		{
			if (facultyDto?.Name == null ||
				facultyDto.BuildingId == 0)
			{
				return this.BadRequest();
			}

			var facultyToAdd = new Faculty
			{
				Name = facultyDto.Name,
				BuildingId = facultyDto.BuildingId
			};

			this.faculties.Add(facultyToAdd);

			facultyDto.Id = facultyToAdd.Id;

			return this.CreatedAtAction(
				nameof(this.GetById), new { id = facultyDto.Id }, facultyDto);
		}

		/// <summary>
		/// Updates a faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty to update.</param>
		/// <param name="facultyDto">The faculty to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] FacultyDto facultyDto)
		{
			if (facultyDto == null)
			{
				return this.BadRequest();
			}

			var facultyToUpdate = this.faculties.GetById(id);

			if (facultyToUpdate == null)
			{
				return this.NotFound();
			}

			if (facultyDto.Name != null)
			{
				facultyToUpdate.Name = facultyDto.Name;
			}

			if (facultyDto.BuildingId != 0)
			{
				facultyToUpdate.BuildingId = facultyDto.BuildingId;
			}

			this.faculties.Update(facultyToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty to update.</param>
		/// <param name="facultyDto">The faculty to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] FacultyDto facultyDto)
		{
			if (facultyDto == null)
			{
				return this.BadRequest();
			}

			var facultyToUpdate = this.faculties.GetById(id);

			if (facultyToUpdate == null)
			{
				return this.NotFound();
			}

			if (facultyDto.Name != null)
			{
				facultyToUpdate.Name = facultyDto.Name;
			}

			if (facultyDto.BuildingId != 0)
			{
				facultyToUpdate.BuildingId = facultyDto.BuildingId;
			}

			this.faculties.Update(facultyToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete([FromRoute] int id)
		{
			var facultyToDelete = this.faculties.GetById(id);

			if (facultyToDelete == null)
			{
				return this.NotFound();
			}

			this.faculties.Delete(facultyToDelete);

			return this.NoContent();
		}
	}
}
