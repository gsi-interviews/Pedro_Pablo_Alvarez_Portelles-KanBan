using FastEndpoints;
using FluentValidation;

namespace KanBanApi.Application.Commands.CreateTodo;

public class CreateTodoCommandValidator : Validator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty().WithMessage("Todo title must not be empty or null")
            .Length(5, 20).WithMessage("Todo title must have between 5 and 20 characters");

        RuleFor(x => x.Message)
            .NotEmpty().NotNull().WithMessage("Todo message must not be empty")
            .MaximumLength(200).WithMessage("Todo message must have at most 200 characters");
    }
}