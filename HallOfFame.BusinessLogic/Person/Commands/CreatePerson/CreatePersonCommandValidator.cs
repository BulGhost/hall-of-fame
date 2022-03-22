using FluentValidation;
using HallOfFame.BusinessLogic.Common.Validators;
using HallOfFame.Domain.Entities;

namespace HallOfFame.BusinessLogic.Person.Commands.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
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