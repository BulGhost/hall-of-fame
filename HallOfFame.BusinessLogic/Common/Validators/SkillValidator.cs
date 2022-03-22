using FluentValidation;
using HallOfFame.Domain.Entities;

namespace HallOfFame.BusinessLogic.Common.Validators;

internal class SkillValidator : AbstractValidator<Skill>
{
    internal SkillValidator()
    {
        RuleFor(s => s.Name).NotEmpty()
            .WithMessage(Resources.TextResources.SkillNameNotSpecified);
        RuleFor(s => s.Level).InclusiveBetween<Skill, byte>(1, 10)
            .WithMessage(Resources.TextResources.SkillLevelOutOfRange);
    }
}