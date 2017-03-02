using System;
using System.Collections.Generic;
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
		public IEnumerable<CommentDto> Get()
			=> this.comments.GetAll()?.ProjectTo<CommentDto>();

		/// <summary>
		/// Gets a comment with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the comment to get.</param>
		/// <returns>A comment with the specified ID.</returns>
		[HttpGet("{id}", Name = "GetCommentById")]
		[SwaggerResponse(200, Type = typeof(CommentDto))]
		public CommentDto Get(int id)
			=> Mapper.Map<CommentDto>(this.comments.GetById(id));

		/// <summary>
		/// Gets all comments with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All comments with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<CommentDto>))]
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

		/// <summary>
		/// Adds a new comment to the database.
		/// </summary>
		/// <param name="commentDto">The comment to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		public IActionResult Post([FromBody] CommentDto commentDto)
		{
			if (commentDto?.Text == null ||
				commentDto.UserId == null ||
				commentDto.ClassId == 0 ||
				commentDto.DateTime == default(DateTime))
			{
				return this.BadRequest();
			}

			var commentToAdd = new Comment
			{
				ClassId = commentDto.ClassId,
				UserId = commentDto.UserId,
				Text = commentDto.Text,
				DateTime = commentDto.DateTime
			};

			this.comments.Add(commentToAdd);

			commentDto.Id = commentToAdd.Id;

			return this.CreatedAtRoute(
				"GetCommentById", new { id = commentDto.Id }, commentDto);
		}

		/// <summary>
		/// Updates a comment.
		/// </summary>
		/// <param name="id">The ID of the comment to update.</param>
		/// <param name="commentDto">The comment to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Put(int id, [FromBody] CommentDto commentDto)
		{
			if (commentDto?.Text == null)
			{
				return this.BadRequest();
			}

			var commentToUpdate = this.comments.GetById(id);

			if (commentToUpdate == null)
			{
				return this.NotFound();
			}

			commentToUpdate.Text = commentDto.Text;
			this.comments.Update(commentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a comment.
		/// </summary>
		/// <param name="id">The ID of the comment to update.</param>
		/// <param name="commentDto">The comment to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Patch(int id, [FromBody] CommentDto commentDto)
		{
			if (commentDto?.Text == null)
			{
				return this.BadRequest();
			}

			var commentToUpdate = this.comments.GetById(id);

			if (commentToUpdate == null)
			{
				return this.NotFound();
			}

			commentToUpdate.Text = commentDto.Text;
			this.comments.Update(commentToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a comment.
		/// </summary>
		/// <param name="id">The ID of the comment to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		public IActionResult Delete(int id)
		{
			var commentToDelete = this.comments.GetById(id);

			if (commentToDelete == null)
			{
				return this.NotFound();
			}

			this.comments.Delete(commentToDelete);

			return this.NoContent();
		}
	}
}
