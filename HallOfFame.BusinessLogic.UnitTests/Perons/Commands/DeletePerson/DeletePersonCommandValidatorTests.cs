using FluentValidation.TestHelper;
using HallOfFame.BusinessLogic.Persons.Commands.DeletePerson;
using HallOfFame.BusinessLogic.Resources;
using HallOfFame.BusinessLogic.UnitTests.Stubs;
using Xunit;

namespace HallOfFame.BusinessLogic.UnitTests.Perons.Commands.DeletePerson;

public class DeletePersonCommandValidatorTests
{
    private readonly DeletePersonCommandValidator _validator;

    public DeletePersonCommandValidatorTests()
    {
        _validator = new DeletePersonCommandValidator(new PersonRepoStub());
    }

    [Fact]
    public void Should_have_error_when_person_with_specified_id_is_not_found()
    {
        var command = new DeletePersonCommand(5);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.PersonId)
            .WithErrorCode(TextResources.NotFoundErrorCode);
    }

    [Fact]
    public void Should_not_have_error_when_command_is_valid()
    {
        var command = new DeletePersonCommand(2);

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}