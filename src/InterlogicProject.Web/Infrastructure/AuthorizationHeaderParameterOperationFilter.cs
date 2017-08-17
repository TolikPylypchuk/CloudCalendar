using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Authorization;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InterlogicProject.Web.Infrastructure
{
	[ExcludeFromCodeCoverage]
	public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
	{
		public void Apply(Operation operation, OperationFilterContext context)
		{
			var filterPipeline = context.ApiDescription
				.ActionDescriptor.FilterDescriptors;

			bool isAuthorized = filterPipeline
				.Select(filterInfo => filterInfo.Filter)
				.Any(filter => filter is AuthorizeFilter);

			bool allowAnonymous = filterPipeline
				.Select(filterInfo => filterInfo.Filter)
				.Any(filter => filter is IAllowAnonymousFilter);

			if (isAuthorized && !allowAnonymous)
			{
				if (operation.Parameters == null)
					operation.Parameters = new List<IParameter>();

				operation.Parameters.Add(new NonBodyParameter
				{
					Name = "Authorization",
					In = "header",
					Description = "Access token",
					Required = true,
					Type = "string"
				});
			}
		}
	}
}
