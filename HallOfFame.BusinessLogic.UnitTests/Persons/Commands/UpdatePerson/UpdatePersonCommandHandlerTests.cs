using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using FluentAssertions;
using HallOfFame.BusinessLogic.Common;
using HallOfFame.BusinessLogic.Persons.Commands.UpdatePerson;
using HallOfFame.BusinessLogic.UnitTests.Stubs;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;
using Xunit;

namespace HallOfFame.BusinessLogic.UnitTests.Perons.Commands.UpdatePerson;

public class UpdatePersonCommandHandlerTests
{
    private readonly IPersonRepo _categoryRepo;
    private readonly UpdatePersonCommandHandler _commandHandler;

    public UpdatePersonCommandHandlerTests()
    {
        _categoryRepo = new PersonRepoStub();
        var configurationProvider = new MapperConfiguration(cfg =>
            cfg.AddProfile(new BusinessLogicMappingProfile()));
        IMapper mapper = configurationProvider.CreateMapper();
        _commandHandler = new UpdatePersonCommandHandler(_categoryRepo, mapper);
    }

    [Fact]
    public void Add_new_person()
    {
        var skills = new List<Skill>
        {
            new() { Name = "EF Core", Level = 4 },
            new() { Name = "SOLID", Level = 6 }
        };
        var command = new UpdatePersonCommand(2, "John Smith", "J_Smith", skills);
        var expectedResult = new Person { Id = 2, Name = command.Name, DisplayName = command.DisplayName, Skills = command.Skills };

        _commandHandler.Handle(command, CancellationToken.None).Wait();

        var persons = _categoryRepo.GetAllAsync().Result.ToList();
        persons.Should().HaveCount(3);
        persons.First(p => p.Id == expectedResult.Id).Should().BeEquivalentTo(expectedResult);
    }
}