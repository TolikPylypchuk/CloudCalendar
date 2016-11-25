using Microsoft.AspNetCore.Identity;

namespace InterlogicProject.Infrastructure
{
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
				Code = nameof(ConcurrencyFailure),
				Description = "Помилка збігу, об'єкт було модифіковано."
			};

		public override IdentityError PasswordMismatch()
			=> new IdentityError
			{
				Code = nameof(PasswordMismatch),
				Description = "Неправильний пароль."
			};

		public override IdentityError InvalidToken()
			=> new IdentityError
			{
				Code = nameof(InvalidToken),
				Description = "Неправильний знак."
			};

		public override IdentityError LoginAlreadyAssociated()
			=> new IdentityError
			{
				Code = nameof(LoginAlreadyAssociated),
				Description = "Користувач із вказаним логіном вже існує."
			};

		public override IdentityError InvalidUserName(string userName)
			=> new IdentityError
			{
				Code = nameof(InvalidUserName),
				Description = $"Ім'я користувача '{userName}' є неправильним."
			};

		public override IdentityError InvalidEmail(string email)
			=> new IdentityError
			{
				Code = nameof(InvalidEmail),
				Description = $"Email '{email}' є неправильним."
			};

		public override IdentityError DuplicateUserName(string userName)
			=> new IdentityError
			{
				Code = nameof(DuplicateUserName),
				Description = $"Ім'я користувача '{userName}' вже зайняте."
			};

		public override IdentityError DuplicateEmail(string email)
			=> new IdentityError
			{
				Code = nameof(DuplicateEmail),
				Description = $"Email '{email}' вже зайнятий."
			};

		public override IdentityError InvalidRoleName(string role)
			=> new IdentityError
			{
				Code = nameof(InvalidRoleName),
				Description = $"Назва ролі '{role}' є неправильною."
			};

		public override IdentityError DuplicateRoleName(string role)
			=> new IdentityError
			{
				Code = nameof(DuplicateRoleName),
				Description = $"Назва ролі '{role}' вже зайнята."
			};

		public override IdentityError UserAlreadyHasPassword()
			=> new IdentityError
			{
				Code = nameof(UserAlreadyHasPassword),
				Description = "У користувача вже встановлено пароль."
			};

		public override IdentityError UserLockoutNotEnabled()
			=> new IdentityError
			{
				Code = nameof(UserLockoutNotEnabled),
				Description = "Блокування не ввімкнуто для цього користувача."
			};

		public override IdentityError UserAlreadyInRole(string role)
			=> new IdentityError
			{
				Code = nameof(UserAlreadyInRole),
				Description = $"Користувач вже є в ролі '{role}'."
			};

		public override IdentityError UserNotInRole(string role)
			=> new IdentityError
			{
				Code = nameof(UserNotInRole),
				Description = $"Користувач не є в ролі '{role}'."
			};

		public override IdentityError PasswordTooShort(int length)
			=> new IdentityError
			{
				Code = nameof(PasswordTooShort),
				Description =
					$"Усі паролі мають мати як мінімум {length} символів."
			};
	}
}
