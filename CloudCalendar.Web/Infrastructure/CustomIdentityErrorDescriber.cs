using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;

namespace CloudCalendar.Web.Infrastructure
{
	[ExcludeFromCodeCoverage]
	public class CustomIdentityErrorDescriber : IdentityErrorDescriber
	{
		public override IdentityError DefaultError()
			=> new IdentityError
			{
				Code = nameof(this.DefaultError),
				Description = "Виникла невідома помилка."
			};

		public override IdentityError ConcurrencyFailure()
			=> new IdentityError
			{
				Code = nameof(this.ConcurrencyFailure),
				Description = "Помилка збігу, об'єкт було модифіковано."
			};

		public override IdentityError PasswordMismatch()
			=> new IdentityError
			{
				Code = nameof(this.PasswordMismatch),
				Description = "Неправильний пароль."
			};

		public override IdentityError InvalidToken()
			=> new IdentityError
			{
				Code = nameof(this.InvalidToken),
				Description = "Неправильний знак."
			};

		public override IdentityError LoginAlreadyAssociated()
			=> new IdentityError
			{
				Code = nameof(this.LoginAlreadyAssociated),
				Description = "Користувач із вказаним логіном вже існує."
			};

		public override IdentityError InvalidUserName(string userName)
			=> new IdentityError
			{
				Code = nameof(this.InvalidUserName),
				Description = $"Ім'я користувача '{userName}' є неправильним."
			};

		public override IdentityError InvalidEmail(string email)
			=> new IdentityError
			{
				Code = nameof(this.InvalidEmail),
				Description = $"Email '{email}' є неправильним."
			};

		public override IdentityError DuplicateUserName(string userName)
			=> new IdentityError
			{
				Code = nameof(this.DuplicateUserName),
				Description = $"Ім'я користувача '{userName}' вже зайняте."
			};

		public override IdentityError DuplicateEmail(string email)
			=> new IdentityError
			{
				Code = nameof(this.DuplicateEmail),
				Description = $"Email '{email}' вже зайнятий."
			};

		public override IdentityError InvalidRoleName(string role)
			=> new IdentityError
			{
				Code = nameof(this.InvalidRoleName),
				Description = $"Назва ролі '{role}' є неправильною."
			};

		public override IdentityError DuplicateRoleName(string role)
			=> new IdentityError
			{
				Code = nameof(this.DuplicateRoleName),
				Description = $"Назва ролі '{role}' вже зайнята."
			};

		public override IdentityError UserAlreadyHasPassword()
			=> new IdentityError
			{
				Code = nameof(this.UserAlreadyHasPassword),
				Description = "У користувача вже встановлено пароль."
			};

		public override IdentityError UserLockoutNotEnabled()
			=> new IdentityError
			{
				Code = nameof(this.UserLockoutNotEnabled),
				Description = "Блокування не ввімкнуто для цього користувача."
			};

		public override IdentityError UserAlreadyInRole(string role)
			=> new IdentityError
			{
				Code = nameof(this.UserAlreadyInRole),
				Description = $"Користувач вже є в ролі '{role}'."
			};

		public override IdentityError UserNotInRole(string role)
			=> new IdentityError
			{
				Code = nameof(this.UserNotInRole),
				Description = $"Користувач не є в ролі '{role}'."
			};

		public override IdentityError PasswordTooShort(int length)
			=> new IdentityError
			{
				Code = nameof(this.PasswordTooShort),
				Description =
					$"Усі паролі мають мати як мінімум {length} символів."
			};
	}
}
