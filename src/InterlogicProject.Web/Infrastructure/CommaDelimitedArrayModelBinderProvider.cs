using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace InterlogicProject.Web.Infrastructure
{
	[ExcludeFromCodeCoverage]
	public class CommaDelimitedArrayModelBinderProvider : IModelBinderProvider
	{
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (context.Metadata.ModelType == typeof(int[]))
			{
				return new CommaDelimitedArrayModelBinder(
					new SimpleTypeModelBinder(context.Metadata.ModelType));
			}

			return null;
		}
	}
}
