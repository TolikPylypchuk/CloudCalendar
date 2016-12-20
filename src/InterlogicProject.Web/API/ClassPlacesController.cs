﻿using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Models.Dto;

namespace InterlogicProject.API
{
	/// <summary>
	/// An API for class places.
	/// </summary>
	[Route("api/[controller]")]
	public class ClassPlacesController : Controller
	{
		private IRepository<ClassPlace> places;

		/// <summary>
		/// Initializes a new instance of the ClassPlacesController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public ClassPlacesController(IRepository<ClassPlace> repo)
		{
			this.places = repo;
		}

		/// <summary>
		/// Gets all class places from the database.
		/// </summary>
		/// <returns>All class places from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassPlaceDto>))]
		public IEnumerable<ClassPlaceDto> Get()
			=> this.places.GetAll()?.ProjectTo<ClassPlaceDto>();

		/// <summary>
		/// Gets a class place with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the class place to get.</param>
		/// <returns>A class place with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ClassPlaceDto))]
		public ClassPlaceDto Get(int id)
			=> Mapper.Map<ClassPlaceDto>(this.places.GetById(id));

		/// <summary>
		/// Gets all class places with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All class places with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassPlaceDto>))]
		public IEnumerable<ClassPlaceDto> GetByClass(int id)
			=> this.places.GetAll()
						 ?.Where(p => p.ClassId == id)
						  .ProjectTo<ClassPlaceDto>();
	}
}