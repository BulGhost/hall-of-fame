using FluentValidation;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;

namespace HallOfFame.BusinessLogic.Persons.Queries.GetPersonById;

public class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
{
    public GetPersonByIdQueryValidator(IPersonRepo repo)
    {
        RuleFor(q => q.PersonId).MustAsync(async (id, cancellationToken) =>
        {
            Person person = await repo.FindAsync(id, cancellationToken);
            return person != null;
        }).WithMessage(Resources.TextResources.PersonDoesNotExist);
    }
}