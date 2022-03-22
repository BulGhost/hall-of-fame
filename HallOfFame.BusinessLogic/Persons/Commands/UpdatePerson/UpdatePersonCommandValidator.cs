using FluentValidation;
using HallOfFame.BusinessLogic.Common.Validators;
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
        }).WithMessage(Resources.TextResources.PersonDoesNotExist);

        RuleFor(cmd => cmd.Name).NotEmpty()
            .WithMessage(Resources.TextResources.NameNotSpecified);
        RuleFor(cmd => cmd.DisplayName).NotEmpty()
            .WithMessage(Resources.TextResources.DisplayNameNotSpecified);
        RuleForEach(cmd => cmd.Skills).SetValidator(new SkillValidator());
        RuleFor(cmd => cmd).Must(cmd => HasNoDuplicateSkills(cmd.Skills))
            .WithMessage(Resources.TextResources.DuplicateSkills);
    }

    private bool HasNoDuplicateSkills(IEnumerable<Skill> skills)
    {
        return skills.GroupBy(skill => skill.Name)
            .All(g => g.Count() == 1);
    }
}