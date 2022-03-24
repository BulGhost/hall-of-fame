using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using FluentAssertions;
using HallOfFame.BusinessLogic.Common;
using HallOfFame.BusinessLogic.Persons.Commands.CreatePerson;
using HallOfFame.BusinessLogic.UnitTests.Stubs;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;
using Xunit;

namespace HallOfFame.BusinessLogic.UnitTests.Persons.Commands.CreatePerson;

public class CreatePersonCommandHandlerTests
{
    private readonly IPersonRepo _categoryRepo;
    private readonly CreatePersonCommandHandler _commandHandler;

    public CreatePersonCommandHandlerTests()
    {
        _categoryRepo = new PersonRepoStub();
        var configurationProvider = new MapperConfiguration(cfg =>
            cfg.AddProfile(new BusinessLogicMappingProfile()));
        IMapper mapper = configurationProvider.CreateMapper();
        _commandHandler = new CreatePersonCommandHandler(_categoryRepo, mapper);
    }

    [Fact]
    public void Add_new_person()
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 6 }
        };
        var command = new CreatePersonCommand("John Doe", "J_Doe", skills);
        var expectedResult = new Person { Id = 4, Name = command.Name, DisplayName = command.DisplayName, Skills = command.Skills};

        var actualResult = _commandHandler.Handle(command, CancellationToken.None).Result;

        var persons = _categoryRepo.GetAllAsync().Result.ToList();
        persons.Should().HaveCount(4);
        persons.First(p => p.Id == expectedResult.Id).Should().BeEquivalentTo(expectedResult);
        actualResult.Should().Be(expectedResult.Id);
    }
}