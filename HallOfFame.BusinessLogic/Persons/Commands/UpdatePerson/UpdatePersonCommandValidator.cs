using FluentValidation;
using HallOfFame.BusinessLogic.Common.Validators;
using HallOfFame.BusinessLogic.Resources;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;

namespace HallOfFame.BusinessLogic.Persons.Commands.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator(IPersonRepo repo)
    {
        RuleFor(cmd => cmd.Id).MustAsync(async (id, cancellationToken) =>
            {
                Person person = await repo.FindAsync(id, cancellationToken);
                return person != null;
            }).WithErrorCode(TextResources.NotFoundErrorCode)
            .WithMessage(TextResources.PersonDoesNotExist);

        RuleFor(cmd => cmd.Name).NotEmpty()
            .WithMessage(TextResources.NameNotSpecified);
        RuleFor(cmd => cmd.DisplayName).NotEmpty()
            .WithMessage(TextResources.DisplayNameNotSpecified);
        RuleForEach(cmd => cmd.Skills).SetValidator(new SkillValidator());
        RuleFor(cmd => cmd).Must(cmd => HasNoDuplicateSkills(cmd.Skills))
            .WithMessage(TextResources.DuplicateSkills);
    }

    private bool HasNoDuplicateSkills(IEnumerable<Skill> skills)
    {
        return skills.GroupBy(skill => skill.Name)
            .All(g => g.Count() == 1);
    }
}