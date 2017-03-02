using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for students.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class StudentsController : Controller
	{
		private IRepository<Student> students;
		private UserManager<User> manager;

		/// <summary>
		/// Initializes a new instance of the StudentsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		/// <param name="manager">
		/// The user manager that this instance will use.
		/// </param>
		public StudentsController(
			IRepository<Student> repo,
			UserManager<User> manager)
		{
			this.students = repo;
			this.manager = manager;
		}

		/// <summary>
		/// Gets all students from the database.
		/// </summary>
		/// <returns>All students from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<StudentDto>))]
		public IEnumerable<StudentDto> Get()
			=> this.students.GetAll()?.ProjectTo<StudentDto>();

		/// <summary>
		/// Gets a student with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the student to get.</param>
		/// <returns>A student with the specified ID.</returns>
		[HttpGet("{id}", Name = "GetStudentById")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto Get(int id)
			=> Mapper.Map<StudentDto>(this.students.GetById(id));

		/// <summary>
		/// Gets a student with the specified user ID.
		/// </summary>
		/// <param name="id">The ID of the user.</param>
		/// <returns>A student with the specified user ID.</returns>
		[HttpGet("userId/{id}")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto GetByUser(string id)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll()
							?.FirstOrDefault(s => s.UserId == id));

		/// <summary>
		/// Gets a student with the specified email.
		/// </summary>
		/// <param name="email">The email of the student.</param>
		/// <returns>A student with the specified email.</returns>
		[HttpGet("email/{email}")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto GetByEmail(string email)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll()
							?.FirstOrDefault(s => s.User.Email == email));

		/// <summary>
		/// Gets a student with the specified transcript number.
		/// </summary>
		/// <param name="number">The transcript number.</param>
		/// <returns>A student with the specified transcript number.</returns>
		[HttpGet("transcript/{number}")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto GetByTranscript(string number)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll().FirstOrDefault(
					s => s.TranscriptNumber == number));

		/// <summary>
		/// Gets all students with the specified group.
		/// </summary>
		/// <param name="id">The ID of the group.</param>
		/// <returns>All students with the specified group.</returns>
		[HttpGet("groupId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<StudentDto>))]
		public IEnumerable<StudentDto> GetForGroup(int id)
			=> this.students.GetAll()
						   ?.Where(s => s.GroupId == id)
							.ProjectTo<StudentDto>();

		/// <summary>
		/// Adds a new student to the database.
		/// </summary>
		/// <param name="studentDto">The student to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		public async Task<IActionResult> Post([FromBody] StudentDto studentDto)
		{
			if (studentDto?.UserFirstName == null ||
				studentDto.UserMiddleName == null ||
				studentDto.UserLastName == null ||
				studentDto.UserEmail == null ||
				studentDto.GroupId == 0 ||
				studentDto.IsGroupLeader == null ||
				studentDto.TranscriptNumber == null)
			{
				return this.BadRequest();
			}

			var userToAdd = new User
			{
				FirstName = studentDto.UserFirstName,
				MiddleName = studentDto.UserMiddleName,
				LastName = studentDto.UserLastName,
				Email = studentDto.UserEmail,
				NormalizedEmail = studentDto.UserEmail.ToUpper(),
				UserName = studentDto.UserEmail,
				NormalizedUserName = studentDto.UserEmail.ToUpper()
			};

			await this.manager.CreateAsync(userToAdd);
			await this.manager.AddToRoleAsync(userToAdd, "Student");

			var studentToAdd = new Student
			{
				User = userToAdd,
				GroupId = studentDto.GroupId,
				IsGroupLeader = studentDto.IsGroupLeader ?? false,
				TranscriptNumber = studentDto.TranscriptNumber
			};

			this.students.Add(studentToAdd);

			studentDto.Id = studentToAdd.Id;

			return this.CreatedAtRoute(
				"GetStudentById", new { id = studentDto.Id }, studentDto);
		}

		/// <summary>
		/// Updates a student.
		/// </summary>
		/// <param name="id">The ID of the student to update.</param>
		/// <param name="studentDto">The student to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Put(
			int id,
			[FromBody] StudentDto studentDto)
		{
			if (studentDto == null)
			{
				return this.BadRequest();
			}

			var studentToUpdate = this.students.GetById(id);

			if (studentToUpdate == null)
			{
				return this.NotFound();
			}

			if (studentDto.GroupId != 0)
			{
				studentToUpdate.GroupId = studentDto.GroupId;
			}

			if (studentDto.IsGroupLeader != null)
			{
				studentToUpdate.IsGroupLeader =
					studentDto.IsGroupLeader ?? false;
			}

			if (studentDto.TranscriptNumber != null)
			{
				studentToUpdate.TranscriptNumber = studentDto.TranscriptNumber;
			}

			if (studentDto.UserFirstName != null ||
			    studentDto.UserMiddleName != null ||
			    studentDto.UserLastName != null ||
			    studentDto.UserEmail != null)
			{
				var userToUpdate = await this.manager.FindByIdAsync(
					studentToUpdate.UserId);

				if (studentDto.UserFirstName != null)
				{
					userToUpdate.FirstName = studentDto.UserFirstName;
				}

				if (studentDto.UserMiddleName != null)
				{
					userToUpdate.MiddleName = studentDto.UserMiddleName;
				}

				if (studentDto.UserLastName != null)
				{
					userToUpdate.LastName = studentDto.UserLastName;
				}

				if (studentDto.UserEmail != null)
				{
					userToUpdate.Email = studentDto.UserEmail;
				}

				await this.manager.UpdateAsync(userToUpdate);
			}

			this.students.Update(studentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a student.
		/// </summary>
		/// <param name="id">The ID of the student to update.</param>
		/// <param name="studentDto">The student to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Patch(
			int id,
			[FromBody] StudentDto studentDto)
		{
			if (studentDto == null)
			{
				return this.BadRequest();
			}

			var studentToUpdate = this.students.GetById(id);

			if (studentToUpdate == null)
			{
				return this.NotFound();
			}

			if (studentDto.GroupId != 0)
			{
				studentToUpdate.GroupId = studentDto.GroupId;
			}

			if (studentDto.IsGroupLeader != null)
			{
				studentToUpdate.IsGroupLeader =
					studentDto.IsGroupLeader ?? false;
			}

			if (studentDto.TranscriptNumber != null)
			{
				studentToUpdate.TranscriptNumber = studentDto.TranscriptNumber;
			}

			if (studentDto.UserFirstName != null ||
				studentDto.UserMiddleName != null ||
				studentDto.UserLastName != null ||
				studentDto.UserEmail != null)
			{
				var userToUpdate = await this.manager.FindByIdAsync(
					studentToUpdate.UserId);

				if (studentDto.UserFirstName != null)
				{
					userToUpdate.FirstName = studentDto.UserFirstName;
				}

				if (studentDto.UserMiddleName != null)
				{
					userToUpdate.MiddleName = studentDto.UserMiddleName;
				}

				if (studentDto.UserLastName != null)
				{
					userToUpdate.LastName = studentDto.UserLastName;
				}

				if (studentDto.UserEmail != null)
				{
					userToUpdate.Email = studentDto.UserEmail;
				}

				await this.manager.UpdateAsync(userToUpdate);
			}

			this.students.Update(studentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a student.
		/// </summary>
		/// <param name="id">The ID of the student to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Delete(int id)
		{
			var studentToDelete = this.students.GetById(id);

			if (studentToDelete == null)
			{
				return this.NotFound();
			}

			this.students.Delete(studentToDelete);

			await this.manager.DeleteAsync(studentToDelete.User);

			return this.NoContent();
		}
	}
}
