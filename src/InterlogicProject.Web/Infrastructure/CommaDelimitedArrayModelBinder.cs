using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InterlogicProject.Web.Infrastructure
{
	[ModelBinder]
	public class CommaDelimitedArrayModelBinder : IModelBinder
	{
		private readonly IModelBinder fallbackBinder;

		public CommaDelimitedArrayModelBinder(IModelBinder fallbackBinder)
		{
			this.fallbackBinder = fallbackBinder ??
				throw new ArgumentNullException(nameof(fallbackBinder));
		}

		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (this.fallbackBinder == null)
			{
				throw new ArgumentNullException(nameof(fallbackBinder));
			}
			var value = bindingContext.ValueProvider.GetValue(
				bindingContext.ModelName);

			string str = value.FirstValue;

			if (str != null)
			{
				var elementType = bindingContext.ModelType.GetElementType();
				var converter = TypeDescriptor.GetConverter(elementType);

				var values = str.Split(
						new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
						.Select(x => converter.ConvertFromString(x?.Trim()))
						.ToArray();

				var result = Array.CreateInstance(elementType, values.Length);

				values.CopyTo(result, 0);

				bindingContext.Result = ModelBindingResult.Success(result);
			} else
			{
				return this.fallbackBinder.BindModelAsync(bindingContext);
			}

			return Task.CompletedTask;
		}
	}
}
