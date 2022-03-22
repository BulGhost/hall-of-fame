using FluentValidation;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;

namespace HallOfFame.BusinessLogic.Persons.Commands.DeletePerson;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator(IPersonRepo repo)
    {
        RuleFor(cmd => cmd.PersonId).MustAsync(async (id, cancellationToken) =>
        {
            Person person = await repo.FindAsync(id, cancellationToken);
            return person != null;
        }).WithMessage(Resources.TextResources.PersonDoesNotExist);
    }
}