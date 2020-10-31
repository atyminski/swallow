using FluentValidation;
using Gevlee.Swallow.Api.Contract.Tasks;


namespace Gevlee.Swallow.Server.Validators
{
	public class TaskModelValidator : AbstractValidator<TaskModel>
	{
		public TaskModelValidator()
		{
			RuleFor(x => x.Name).MaximumLength(255).MinimumLength(1);
			RuleFor(x => x.Date).NotEmpty();
		}
	}
}
