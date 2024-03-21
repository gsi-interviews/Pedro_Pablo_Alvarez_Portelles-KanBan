using FastEndpoints;
using FluentValidation;

namespace KanBanApi.Application.Commands.UpdateTodo;

public class UpdateTodoCommandValidator : Validator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Title)
            .Length(5, 20).WithMessage("Todo title must have between 5 and 20 characters");

        RuleFor(x => x.Message)
            .MaximumLength(200).WithMessage("Todo message must have at most 200 characters");

        RuleFor(x => x.Status)
            .Must(x => x == null || new[] { "Todo", "Doing", "Review", "Done" }.Contains(x))
                .WithMessage("Valid statuses are only: 'Todo', 'Doing', 'Review' and 'Done'");
    }
}