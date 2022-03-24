using FluentValidation.TestHelper;
using HallOfFame.BusinessLogic.Persons.Queries.GetPersonById;
using HallOfFame.BusinessLogic.Resources;
using HallOfFame.BusinessLogic.UnitTests.Stubs;
using Xunit;

namespace HallOfFame.BusinessLogic.UnitTests.Perons.Queries.GetPersonById;

public class GetPersonByIdQueryValidatorTests
{
    private readonly GetPersonByIdQueryValidator _validator;

    public GetPersonByIdQueryValidatorTests()
    {
        _validator = new GetPersonByIdQueryValidator(new PersonRepoStub());
    }

    [Fact]
    public void Should_have_error_when_person_with_specified_id_is_not_found()
    {
        var query = new GetPersonByIdQuery(5);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.PersonId)
            .WithErrorCode(TextResources.NotFoundErrorCode);
    }

    [Fact]
    public void Should_not_have_error_when_command_is_valid()
    {
        var query = new GetPersonByIdQuery(2);

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }
}