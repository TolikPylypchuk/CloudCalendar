using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for comments.
	/// </summary>
	[Route("api/[controller]")]
	public class CommentsController : Controller
	{
		private IRepository<Comment> comments;

		/// <summary>
		/// Initializes a new instance of the CommentsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public CommentsController(IRepository<Comment> repo)
		{
			this.comments = repo;
		}

		/// <summary>
		/// Gets all comments from the database.
		/// </summary>
		/// <returns>All comments from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> Get()
			=> this.comments.GetAll()?.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets a comment with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the comment to get.</param>
		/// <returns>A comment with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(CommentDto))]
		public CommentDto Get(int id)
			=> Mapper.Map<CommentDto>(this.comments.GetById(id));

		/// <summary>
		/// Gets all comments with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All comments with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClass(int id)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == id)
							.OrderBy(c => c.DateTime)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets the specified amount of comments with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <param name="num">The amount of comments.</param>
		/// <returns>
		/// The specified amount of comments with the specified class.
		/// </returns>
		[HttpGet("classId/{id}/take/{num}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClass(int id, int num)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == id)
							.OrderBy(c => c.DateTime)
							.Take(num)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets all comments with the specified user.
		/// </summary>
		/// <param name="id">The ID of the user.</param>
		/// <returns>All comments with the specified user.</returns>
		[HttpGet("userId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForUser(string id)
			=> this.comments.GetAll()
						   ?.Where(c => c.UserId == id)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets all comments with the specified class and user.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>All comments with the specified class and user.</returns>
		[HttpGet("classId/{classId}/userId/{userId}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClassAndUser(
			int classId,
			string userId)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == classId)
							.Where(c => c.UserId == userId)
							.OrderBy(c => c.DateTime)
							.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets the specified amount of comments
		/// with the specified class and user.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <param name="userId">The ID of the user.</param>
		/// <param name="num">The amount of comments.</param>
		/// <returns>
		/// The specified amount of comments with the specified class and user.
		/// </returns>
		[HttpGet("classId/{classId}/userId/{userId}/take/{num}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> GetForClassAndUser(
			int classId,
			string userId,
			int num)
			=> this.comments.GetAll()
						   ?.Where(c => c.ClassId == classId)
							.Where(c => c.UserId == userId)
							.OrderBy(c => c.DateTime)
							.Take(num)
							.ProjectTo<CommentDto>();
	}
}
