using System.Collections.Generic;
using FluentValidation.TestHelper;
using HallOfFame.BusinessLogic.Persons.Commands.CreatePerson;
using HallOfFame.Domain.Entities;
using Xunit;

namespace HallOfFame.BusinessLogic.UnitTests.Perons.Commands.CreatePerson;

public class CreatePersonCommandValidatorTests
{
    private readonly CreatePersonCommandValidator _validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Should_have_error_when_person_name_is_invalid(string name)
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 6 }
        };
        var command = new CreatePersonCommand(name, "J_Doe", skills);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Should_have_error_when_person_display_name_is_invalid(string displayName)
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 6 }
        };
        var command = new CreatePersonCommand("John Doe", displayName, skills);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.DisplayName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Should_have_error_when_person_skill_name_is_invalid(string skillName)
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = skillName, Level = 6 }
        };
        var command = new CreatePersonCommand("John Doe", "J_Doe", skills);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor("Skills[1].Name");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(21)]
    public void Should_have_error_when_person_skill_level_is_invalid(byte skillLevel)
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = skillLevel }
        };
        var command = new CreatePersonCommand("John Doe", "J_Doe", skills);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor("Skills[1].Level");
    }

    [Fact]
    public void Should_have_error_when_person_has_duplicate_skill_names()
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 8 },
            new() { Name = "OOP", Level = 9}
        };
        var command = new CreatePersonCommand("John Doe", "J_Doe", skills);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c)
            .WithErrorMessage(Resources.TextResources.DuplicateSkills);
    }

    [Fact]
    public void Should_not_have_error_when_command_is_valid()
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 8 }
        };
        var command = new CreatePersonCommand("John Doe", "J_Doe", skills);

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}