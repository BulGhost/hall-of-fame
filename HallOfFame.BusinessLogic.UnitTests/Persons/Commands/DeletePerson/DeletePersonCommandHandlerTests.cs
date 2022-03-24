using System.Linq;
using System.Threading;
using FluentAssertions;
using HallOfFame.BusinessLogic.Persons.Commands.DeletePerson;
using HallOfFame.BusinessLogic.UnitTests.Stubs;
using HallOfFame.Domain.Repositories;
using Xunit;

namespace HallOfFame.BusinessLogic.UnitTests.Perons.Commands.DeletePerson;

public class DeletePersonCommandHandlerTests
{
    private readonly IPersonRepo _personRepo;
    private readonly DeletePersonCommandHandler _commandHandler;

    public DeletePersonCommandHandlerTests()
    {
        _personRepo = new PersonRepoStub();
        _commandHandler = new DeletePersonCommandHandler(_personRepo);
    }

    [Fact]
    public void Delete_person()
    {
        var command = new DeletePersonCommand(3);

        _commandHandler.Handle(command, CancellationToken.None).Wait();

        var persons = _personRepo.GetAllAsync().Result.ToList();
        persons.Should().HaveCount(2);
        persons.FirstOrDefault(p => p.Id == command.PersonId).Should().BeNull();
    }
}