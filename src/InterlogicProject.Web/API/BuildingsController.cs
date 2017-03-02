using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for buildings.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class BuildingsController : Controller
	{
		private IRepository<Building> buildings;

		/// <summary>
		/// Initializes a new instance of the BuildingsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public BuildingsController(IRepository<Building> repo)
		{
			this.buildings = repo;
		}

		/// <summary>
		/// Gets all buildings from the database.
		/// </summary>
		/// <returns>All buildings from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<BuildingDto>))]
		public IEnumerable<BuildingDto> Get()
			=> this.buildings.GetAll()?.ProjectTo<BuildingDto>();

		/// <summary>
		/// Gets a building with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the building to get.</param>
		/// <returns>A building with the specified ID.</returns>
		[HttpGet("{id}", Name = "GetBuildingById")]
		[SwaggerResponse(200, Type = typeof(BuildingDto))]
		public BuildingDto Get(int id)
			=> Mapper.Map<BuildingDto>(this.buildings.GetById(id));

		/// <summary>
		/// Adds a new building to the database.
		/// </summary>
		/// <param name="buildingDto">The building to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		public IActionResult Post([FromBody] BuildingDto buildingDto)
		{
			if (buildingDto?.Name == null)
			{
				return this.BadRequest();
			}

			var buildingToAdd = new Building { Name = buildingDto.Name };

			this.buildings.Add(buildingToAdd);

			buildingDto.Id = buildingToAdd.Id;

			return this.CreatedAtRoute(
				"GetBuildingById", new { id = buildingDto.Id }, buildingDto);
		}

		/// <summary>
		/// Updates a building.
		/// </summary>
		/// <param name="id">The ID of the building to update.</param>
		/// <param name="buildingDto">The building to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Put(int id, [FromBody] BuildingDto buildingDto)
		{
			if (buildingDto?.Name == null)
			{
				return this.BadRequest();
			}

			var buildingToUpdate = this.buildings.GetById(id);

			if (buildingToUpdate == null)
			{
				return this.NotFound();
			}

			buildingToUpdate.Name = buildingDto.Name;
			this.buildings.Update(buildingToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a building.
		/// </summary>
		/// <param name="id">The ID of the building to update.</param>
		/// <param name="buildingDto">The building to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Patch(int id, [FromBody] BuildingDto buildingDto)
		{
			if (buildingDto?.Name == null)
			{
				return this.BadRequest();
			}

			var buildingToUpdate = this.buildings.GetById(id);

			if (buildingToUpdate == null)
			{
				return this.NotFound();
			}

			buildingToUpdate.Name = buildingDto.Name;
			this.buildings.Update(buildingToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a building.
		/// </summary>
		/// <param name="id">The ID of the building to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Delete(int id)
		{
			var buildingToDelete = this.buildings.GetById(id);

			if (buildingToDelete == null)
			{
				return this.NotFound();
			}

			this.buildings.Delete(buildingToDelete);

			return this.NoContent();
		}
	}
}
