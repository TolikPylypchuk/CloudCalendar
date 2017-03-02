using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace InterlogicProject.Web.Infrastructure
{
	public static class MvcOptionsExtensions
	{
		public static void UseCommaDelimitedArrayModelBinding(this MvcOptions opts)
		{
			var binderToFind = opts.ModelBinderProviders.FirstOrDefault(
				x => x.GetType() == typeof(SimpleTypeModelBinderProvider));

			if (binderToFind == null)
			{
				return;
			}

			int index = opts.ModelBinderProviders.IndexOf(binderToFind);

			opts.ModelBinderProviders.Insert(
				index, new CommaDelimitedArrayModelBinderProvider());
		}
	}
}
