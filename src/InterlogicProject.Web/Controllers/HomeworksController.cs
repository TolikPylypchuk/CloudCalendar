using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;
using InterlogicProject.Web.Services;

namespace InterlogicProject.Web.Controllers
{
	/// <summary>
	/// An API for homeworks.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class HomeworksController : Controller
	{
		private IHostingEnvironment env;
		private IRepository<Homework> homeworks;
		private Settings settings;

		/// <summary>
		/// Initializes a new instance of the HomeworksController class.
		/// </summary>
		/// <param name="env">
		/// The hosting environment that this instance will use.
		/// </param>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		/// <param name="options">
		/// The application settings that this instance will use.
		/// </param>
		public HomeworksController(
			IHostingEnvironment env,
			IRepository<Homework> repo,
			IOptionsSnapshot<Settings> options)
		{
			this.homeworks = repo;
			this.env = env;
			this.settings = options.Value;
		}

		/// <summary>
		/// Gets all homeworks from the database.
		/// </summary>
		/// <returns>All homeworks from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<HomeworkDto>))]
		public IEnumerable<HomeworkDto> GetAll()
			=> this.homeworks.GetAll()?.ProjectTo<HomeworkDto>();

		/// <summary>
		/// Gets a homework with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the homework to get.</param>
		/// <returns>A homework with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(HomeworkDto))]
		public HomeworkDto GetById([FromRoute] int id)
			=> Mapper.Map<HomeworkDto>(this.homeworks.GetById(id));

		/// <summary>
		/// Gets all homeworks with the specified class.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <returns>All homeworks with the specified class.</returns>
		[HttpGet("classId/{classId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<HomeworkDto>))]
		public IEnumerable<HomeworkDto> GetForClass([FromRoute] int classId)
			=> this.homeworks.GetAll()
				  ?.Where(h => h.ClassId == classId)
				   .ProjectTo<HomeworkDto>();

		/// <summary>
		/// Gets all homeworks of the specified student.
		/// </summary>
		/// <param name="studentId">The ID of the student.</param>
		/// <returns>All homeworks of the specified student.</returns>
		[HttpGet("studentId/{studentId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<HomeworkDto>))]
		public IEnumerable<HomeworkDto> GetForStudent(
			[FromRoute] int studentId)
			=> this.homeworks.GetAll()
				  ?.Where(h => h.StudentId == studentId)
				   .ProjectTo<HomeworkDto>();

		/// <summary>
		/// Gets a homework of the specified student
		/// for the specified class.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <param name="studentId">The ID of the student.</param>
		/// <returns>
		/// A homework of the specified student for the specified class.
		/// </returns>
		[HttpGet("classId/{classId}/studentId/{studentId}")]
		[SwaggerResponse(200, Type = typeof(HomeworkDto))]
		public HomeworkDto GetForStudentAndClass(
			[FromRoute] int classId,
			[FromRoute] int studentId)
			=> Mapper.Map<HomeworkDto>(this.homeworks.GetAll()
				?.FirstOrDefault(
					h => h.ClassId == classId && h.StudentId == studentId));

		/// <summary>
		/// Adds a new homework to the database.
		/// </summary>
		/// <param name="classId">The class related to the homework.</param>
		/// <param name="studentId">The student that posted the homework.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost("classId/{classId}/studentId/{studentId}")]
		[SwaggerResponse(201)]
		[Authorize(Roles = "Student")]
		public async Task<IActionResult> Post(
			[FromRoute] int classId,
			[FromRoute] int studentId)
		{
			var file = this.Request.Form.Files.FirstOrDefault();

			if (file == null || classId <= 0 || studentId <= 0)
			{
				return this.BadRequest();
			}

			var homework = this.homeworks.GetAll().FirstOrDefault(
				h => h.FileName == file.FileName);

			if (homework != null)
			{
				return this.Forbid();
			}

			string filePath = Path.Combine(
				this.env.WebRootPath,
				this.settings.HomeworksPath,
				$"{classId}_{studentId}_{file.FileName}");

			using (var stream = System.IO.File.Open(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			var homeworkToAdd = new Homework
			{
				ClassId = classId,
				StudentId = studentId,
				FileName = file.FileName,
				DateTime = DateTime.Now
			};

			this.homeworks.Add(homeworkToAdd);

			return this.CreatedAtAction(
				nameof(this.GetById),
				new { id = homeworkToAdd.Id },
				Mapper.Map<HomeworkDto>(homeworkToAdd));
		}

		/// <summary>
		/// Updates a homework.
		/// </summary>
		/// <param name="id">The ID of the homework.</param>
		/// <param name="homeworkDto">The homework to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] HomeworkDto homeworkDto)
		{
			if (homeworkDto == null)
			{
				return this.BadRequest();
			}

			var homeworkToUpdate = this.homeworks.GetById(id);

			if (homeworkToUpdate == null)
			{
				return this.NotFound();
			}

			homeworkToUpdate.Accepted = homeworkDto.Accepted;

			this.homeworks.Update(homeworkToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a homework.
		/// </summary>
		/// <param name="id">The ID of the homework.</param>
		/// <param name="homeworkDto">The homework to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] HomeworkDto homeworkDto)
		{
			if (homeworkDto == null)
			{
				return this.BadRequest();
			}

			var homeworkToUpdate = this.homeworks.GetById(id);

			if (homeworkToUpdate == null)
			{
				return this.NotFound();
			}

			homeworkToUpdate.Accepted = homeworkDto.Accepted;

			this.homeworks.Update(homeworkToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a homework.
		/// </summary>
		/// <param name="id">The ID of the homework to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Delete([FromRoute] int id)
		{
			var homeworkToDelete = this.homeworks.GetById(id);

			if (homeworkToDelete == null)
			{
				return this.NotFound();
			}

			this.homeworks.Delete(homeworkToDelete);

			System.IO.File.Delete(
				Path.Combine(
					this.env.WebRootPath,
					this.settings.HomeworksPath,
					$"{homeworkToDelete.ClassId}_{homeworkToDelete.StudentId}_" +
					$"{homeworkToDelete.FileName}"));

			return this.NoContent();
		}
	}
}
