using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for lecturers.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class LecturersController : Controller
	{
		private IRepository<Lecturer> lecturers;
		private UserManager<User> manager;

		/// <summary>
		/// Initializes a new instance of the LecturersController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		/// <param name="manager">
		/// The user manager that this instance will use.
		/// </param>
		public LecturersController(
			IRepository<Lecturer> repo,
			UserManager<User> manager)
		{
			this.lecturers = repo;
			this.manager = manager;
		}

		/// <summary>
		/// Gets all lecturers from the database.
		/// </summary>
		/// <returns>All lecturers from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> Get()
			=> this.lecturers.GetAll()?.ProjectTo<LecturerDto>();

		/// <summary>
		/// Gets a lecturer with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the lecturer to get.</param>
		/// <returns>A lecturer with the specified ID.</returns>
		[HttpGet("{id}", Name = "GetLecturerById")]
		[SwaggerResponse(200, Type = typeof(LecturerDto))]
		public LecturerDto Get(int id)
			=> Mapper.Map<LecturerDto>(this.lecturers.GetById(id));

		/// <summary>
		/// Gets a lecturer with the specified user ID.
		/// </summary>
		/// <param name="id">The ID of the user.</param>
		/// <returns>A lecturer with the specified user ID.</returns>
		[HttpGet("userId/{id}")]
		[SwaggerResponse(200, Type = typeof(LecturerDto))]
		public LecturerDto GetByUser(string id)
			=> Mapper.Map<LecturerDto>(
				this.lecturers.GetAll()
							 ?.FirstOrDefault(l => l.UserId == id));

		/// <summary>
		/// Gets a lecturer with the specified email.
		/// </summary>
		/// <param name="email">The email of the lecturer.</param>
		/// <returns>A lecturer with the specified email.</returns>
		[HttpGet("email/{email}")]
		[SwaggerResponse(200, Type = typeof(LecturerDto))]
		public LecturerDto GetByEmail(string email)
			=> Mapper.Map<LecturerDto>(
				this.lecturers.GetAll()
							 ?.FirstOrDefault(l => l.User.Email == email));

		/// <summary>
		/// Gets all lecturers with the specified department.
		/// </summary>
		/// <param name="id">The ID of the department.</param>
		/// <returns>All lecturers with the specified department.</returns>
		[HttpGet("departmentId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> GetForDepartment(int id)
			=> this.lecturers.GetAll()
							?.Where(l => l.DepartmentId == id)
							 .ProjectTo<LecturerDto>();

		/// <summary>
		/// Gets all lecturers with the specified faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty.</param>
		/// <returns>All lecturers with the specified faculty.</returns>
		[HttpGet("facultyId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> GetForFaculty(int id)
			=> this.lecturers.GetAll()
							?.Where(l => l.Department.FacultyId == id)
							 .ProjectTo<LecturerDto>();

		/// <summary>
		/// Gets all lecturers with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All lecturers with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> GetForClass(int id)
		=> this.lecturers.GetAll()
						?.Include(l => l.Classes)
						 .Where(l => l.Classes.Any(c => c.ClassId == id))
						 .ProjectTo<LecturerDto>();

		/// <summary>
		/// Adds a new lecturer to the database.
		/// </summary>
		/// <param name="lecturerDto">The lecturer to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		public async Task<IActionResult> Post([FromBody] LecturerDto lecturerDto)
		{
			if (lecturerDto?.FirstName == null ||
				lecturerDto.MiddleName == null ||
				lecturerDto.LastName == null ||
				lecturerDto.Email == null ||
				lecturerDto.DepartmentId == 0 ||
				lecturerDto.IsAdmin == null ||
				lecturerDto.IsDean == null ||
				lecturerDto.IsHead == null)
			{
				return this.BadRequest();
			}

			var userToAdd = new User
			{
				FirstName = lecturerDto.FirstName,
				MiddleName = lecturerDto.MiddleName,
				LastName = lecturerDto.LastName,
				Email = lecturerDto.Email,
				NormalizedEmail = lecturerDto.Email.ToUpper(),
				UserName = lecturerDto.Email,
				NormalizedUserName = lecturerDto.Email.ToUpper()
			};

			await this.manager.CreateAsync(userToAdd);
			await this.manager.AddToRoleAsync(userToAdd, "Lecturer");

			var lecturerToAdd = new Lecturer
			{
				User = userToAdd,
				DepartmentId = lecturerDto.DepartmentId,
				IsAdmin = lecturerDto.IsAdmin ?? false,
				IsDean = lecturerDto.IsDean ?? false,
				IsHead = lecturerDto.IsHead ?? false
			};

			this.lecturers.Add(lecturerToAdd);

			lecturerDto.Id = lecturerToAdd.Id;

			return this.CreatedAtRoute(
				"GetLecturerById", new { id = lecturerDto.Id }, lecturerDto);
		}

		/// <summary>
		/// Updates a lecturer.
		/// </summary>
		/// <param name="id">The ID of the lecturer to update.</param>
		/// <param name="lecturerDto">The lecturer to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Put(
			int id,
			[FromBody] LecturerDto lecturerDto)
		{
			if (lecturerDto == null)
			{
				return this.BadRequest();
			}

			var lecturerToUpdate = this.lecturers.GetById(id);

			if (lecturerToUpdate == null)
			{
				return this.NotFound();
			}

			if (lecturerDto.DepartmentId != 0)
			{
				lecturerToUpdate.DepartmentId = lecturerDto.DepartmentId;
			}

			if (lecturerDto.IsAdmin != null)
			{
				lecturerToUpdate.IsAdmin = lecturerDto.IsAdmin ?? false;
			}

			if (lecturerDto.IsDean != null)
			{
				lecturerToUpdate.IsDean = lecturerDto.IsDean ?? false;
			}

			if (lecturerDto.IsHead != null)
			{
				lecturerToUpdate.IsHead = lecturerDto.IsHead ?? false;
			}

			if (lecturerDto.FirstName != null ||
				lecturerDto.MiddleName != null ||
				lecturerDto.LastName != null ||
				lecturerDto.Email != null)
			{
				var userToUpdate = await this.manager.FindByIdAsync(
					lecturerToUpdate.UserId);

				if (lecturerDto.FirstName != null)
				{
					userToUpdate.FirstName = lecturerDto.FirstName;
				}

				if (lecturerDto.MiddleName != null)
				{
					userToUpdate.MiddleName = lecturerDto.MiddleName;
				}

				if (lecturerDto.LastName != null)
				{
					userToUpdate.LastName = lecturerDto.LastName;
				}

				if (lecturerDto.Email != null)
				{
					userToUpdate.Email = lecturerDto.Email;
				}

				await this.manager.UpdateAsync(userToUpdate);
			}

			this.lecturers.Update(lecturerToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a lecturer.
		/// </summary>
		/// <param name="id">The ID of the lecturer to update.</param>
		/// <param name="lecturerDto">The lecturer to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Patch(
			int id,
			[FromBody] LecturerDto lecturerDto)
		{
			if (lecturerDto == null)
			{
				return this.BadRequest();
			}

			var lecturerToUpdate = this.lecturers.GetById(id);

			if (lecturerToUpdate == null)
			{
				return this.NotFound();
			}

			if (lecturerDto.DepartmentId != 0)
			{
				lecturerToUpdate.DepartmentId = lecturerDto.DepartmentId;
			}

			if (lecturerDto.IsAdmin != null)
			{
				lecturerToUpdate.IsAdmin = lecturerDto.IsAdmin ?? false;
			}

			if (lecturerDto.IsDean != null)
			{
				lecturerToUpdate.IsDean = lecturerDto.IsDean ?? false;
			}

			if (lecturerDto.IsHead != null)
			{
				lecturerToUpdate.IsHead = lecturerDto.IsHead ?? false;
			}

			if (lecturerDto.FirstName != null ||
				lecturerDto.MiddleName != null ||
				lecturerDto.LastName != null ||
				lecturerDto.Email != null)
			{
				var userToUpdate = await this.manager.FindByIdAsync(
					lecturerToUpdate.UserId);

				if (lecturerDto.FirstName != null)
				{
					userToUpdate.FirstName = lecturerDto.FirstName;
				}

				if (lecturerDto.MiddleName != null)
				{
					userToUpdate.MiddleName = lecturerDto.MiddleName;
				}

				if (lecturerDto.LastName != null)
				{
					userToUpdate.LastName = lecturerDto.LastName;
				}

				if (lecturerDto.Email != null)
				{
					userToUpdate.Email = lecturerDto.Email;
				}

				await this.manager.UpdateAsync(userToUpdate);
			}

			this.lecturers.Update(lecturerToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a lecturer.
		/// </summary>
		/// <param name="id">The ID of the lecturer to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Delete(int id)
		{
			var lecturerToDelete = this.lecturers.GetById(id);

			if (lecturerToDelete == null)
			{
				return this.NotFound();
			}

			this.lecturers.Delete(lecturerToDelete);

			await this.manager.DeleteAsync(lecturerToDelete.User);

			return this.NoContent();
		}
	}
}
