using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
	/// An API for homeworks.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class HomeworksController : Controller
	{
		private IHostingEnvironment env;
		private IRepository<Homework> homeworks;

		/// <summary>
		/// Initializes a new instance of the HomeworksController class.
		/// </summary>
		/// <param name="env">
		/// The hosting environment that this instance will use.
		/// </param>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public HomeworksController(
			IHostingEnvironment env,
			IRepository<Homework> repo)
		{
			this.homeworks = repo;
			this.env = env;
		}

		/// <summary>
		/// Gets all homeworks from the database.
		/// </summary>
		/// <returns>All homeworks from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<HomeworkDto>))]
		public IEnumerable<HomeworkDto> Get()
			=> this.homeworks.GetAll()?.ProjectTo<HomeworkDto>();

		/// <summary>
		/// Gets a homework with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the homework to get.</param>
		/// <returns>A homework with the specified ID.</returns>
		[HttpGet("{id}", Name = "GetHomeworkById")]
		[SwaggerResponse(200, Type = typeof(HomeworkDto))]
		public HomeworkDto Get(int id)
			=> Mapper.Map<HomeworkDto>(this.homeworks.GetById(id));

		/// <summary>
		/// Gets all homeworks with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All homeworks with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<HomeworkDto>))]
		public IEnumerable<HomeworkDto> GetForClass(int id)
			=> this.homeworks.GetAll()
				  ?.Where(h => h.ClassId == id)
				   .ProjectTo<HomeworkDto>();

		/// <summary>
		/// Gets all homeworks of the specified student.
		/// </summary>
		/// <param name="id">The ID of the student.</param>
		/// <returns>All homeworks of the specified student.</returns>
		[HttpGet("studentId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<HomeworkDto>))]
		public IEnumerable<HomeworkDto> GetForStudent(int id)
			=> this.homeworks.GetAll()
				  ?.Where(h => h.StudentId == id)
				   .ProjectTo<HomeworkDto>();

		/// <summary>
		/// Gets all homeworks of the specified student
		/// with the specified class.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <param name="studentId">The ID of the student.</param>
		/// <returns>
		/// All homeworks of the specified student with the specified class.
		/// </returns>
		[HttpGet("classId/{classId}/studentId/{studentId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<HomeworkDto>))]
		public IEnumerable<HomeworkDto> GetForStudentAndClass(
			int classId,
			int studentId)
			=> this.homeworks.GetAll()
				?.Where(h => h.ClassId == classId && h.StudentId == studentId)
				.ProjectTo<HomeworkDto>();

		/// <summary>
		/// Adds a new homework to the database.
		/// </summary>
		/// <param name="file">The file to add.</param>
		/// <param name="classId">The class related to the homework.</param>
		/// <param name="studentId">The student that posted the homework.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost("classId/{classId}/studentId/{studentId}")]
		[SwaggerResponse(201)]
		public async Task<IActionResult> Post(
			IFormFile file,
			int classId,
			int studentId)
		{
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
				Program.HomeworksPath,
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

			return this.CreatedAtRoute(
				"GetHomeworkById",
				new { id = homeworkToAdd.Id },
				Mapper.Map<HomeworkDto>(homeworkToAdd));
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
		public IActionResult Delete(int id)
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
					Program.HomeworksPath,
					$"{homeworkToDelete.ClassId}_{homeworkToDelete.StudentId}_" +
					$"{homeworkToDelete.FileName}"));

			return this.NoContent();
		}
	}
}
