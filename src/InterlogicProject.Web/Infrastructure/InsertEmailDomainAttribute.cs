using InterlogicProject.Web.Models.ViewModels;

using Microsoft.AspNetCore.Mvc.Filters;

namespace InterlogicProject.Web.Infrastructure
{
	public class InsertEmailDomainAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(
			ActionExecutingContext context)
		{
			var model = context.ActionArguments["model"] as LoginModel;
			if (model?.Username != null)
			{
				model.Username += $"@{Program.EmailDomain}";
			}
		}
	}
}
