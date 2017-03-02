﻿using System.Collections.Generic;
using System.Linq;

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
	/// An API for departments.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class DepartmentsController : Controller
	{
		private IRepository<Department> departments;

		/// <summary>
		/// Initializes a new instance of the DepartmentsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public DepartmentsController(IRepository<Department> repo)
		{
			this.departments = repo;
		}

		/// <summary>
		/// Gets all departments from the database.
		/// </summary>
		/// <returns>All departments from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<DepartmentDto>))]
		public IEnumerable<DepartmentDto> Get()
			=> this.departments.GetAll()?.ProjectTo<DepartmentDto>();

		/// <summary>
		/// Gets a department with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the department to get.</param>
		/// <returns>A department with the specified ID.</returns>
		[HttpGet("{id}", Name = "GetDepartmentById")]
		[SwaggerResponse(200, Type = typeof(DepartmentDto))]
		public DepartmentDto Get(int id)
			=> Mapper.Map<DepartmentDto>(this.departments.GetById(id));

		/// <summary>
		/// Gets all departments with the specified faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty.</param>
		/// <returns>All departments with the specified faculty.</returns>
		[HttpGet("facultyId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<DepartmentDto>))]
		public IEnumerable<DepartmentDto> GetForFaculty(int id)
			=> this.departments.GetAll()
							  ?.Where(d => d.FacultyId == id)
							   .ProjectTo<DepartmentDto>();

		/// <summary>
		/// Adds a new department to the database.
		/// </summary>
		/// <param name="departmentDto">The department to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		public IActionResult Post([FromBody] DepartmentDto departmentDto)
		{
			if (departmentDto?.Name == null ||
				departmentDto.FacultyId == 0)
			{
				return this.BadRequest();
			}

			var departmentToAdd = new Department
			{
				Name = departmentDto.Name,
				FacultyId = departmentDto.FacultyId
			};

			this.departments.Add(departmentToAdd);

			departmentDto.Id = departmentToAdd.Id;

			return this.CreatedAtRoute(
				"GetDepartmentById", new { id = departmentDto.Id }, departmentDto);
		}

		/// <summary>
		/// Updates a department.
		/// </summary>
		/// <param name="id">The ID of the department to update.</param>
		/// <param name="departmentDto">The department to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Put(int id, [FromBody] DepartmentDto departmentDto)
		{
			if (departmentDto == null)
			{
				return this.BadRequest();
			}

			var departmentToUpdate = this.departments.GetById(id);

			if (departmentToUpdate == null)
			{
				return this.NotFound();
			}

			if (departmentDto.Name != null)
			{
				departmentToUpdate.Name = departmentDto.Name;
			}

			if (departmentDto.FacultyId != 0)
			{
				departmentToUpdate.FacultyId = departmentDto.FacultyId;
			}

			this.departments.Update(departmentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a department.
		/// </summary>
		/// <param name="id">The ID of the department to update.</param>
		/// <param name="departmentDto">The department to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Patch(int id, [FromBody] DepartmentDto departmentDto)
		{
			if (departmentDto == null)
			{
				return this.BadRequest();
			}

			var departmentToUpdate = this.departments.GetById(id);

			if (departmentToUpdate == null)
			{
				return this.NotFound();
			}

			if (departmentDto.Name != null)
			{
				departmentToUpdate.Name = departmentDto.Name;
			}

			if (departmentDto.FacultyId != 0)
			{
				departmentToUpdate.FacultyId = departmentDto.FacultyId;
			}

			this.departments.Update(departmentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a department.
		/// </summary>
		/// <param name="id">The ID of the department to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Delete(int id)
		{
			var departmentToDelete = this.departments.GetById(id);

			if (departmentToDelete == null)
			{
				return this.NotFound();
			}

			this.departments.Delete(departmentToDelete);

			return this.NoContent();
		}
	}
}
