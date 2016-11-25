using InterlogicProject.Models.ViewModels;

using Microsoft.AspNetCore.Mvc.Filters;

namespace InterlogicProject.Infrastructure
{
	public class InsertEmailDomainAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(
			ActionExecutingContext context)
		{
			var model = context.ActionArguments["model"] as LoginModel;
			if (model?.Email != null)
			{
				model.Email += $"@{Program.EmailDomain}";
			}
		}
	}
}
