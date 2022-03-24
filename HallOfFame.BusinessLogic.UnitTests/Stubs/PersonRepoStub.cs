using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;

namespace HallOfFame.BusinessLogic.UnitTests.Stubs;

public class PersonRepoStub : IPersonRepo
{
    private readonly List<Person> _persons = new()
    {
        new Person
        {
            Id = 1, Name = "August Haynes", DisplayName = "A_Haynes", Skills = new List<Skill>
            {
                new() { Name = "C#", Level = 9 },
                new() { Name = ".NET", Level = 8 },
                new() { Name = "EF Core", Level = 6 },
                new() { Name = "Git", Level = 5 }
            }
        },
        new Person
        {
            Id = 2, Name = "Javier Hardin", DisplayName = "J_Hardin", Skills = new List<Skill>
            {
                new() { Name = "C#", Level = 7 },
                new() { Name = "EF Core", Level = 5 },
                new() { Name = "WinForms", Level = 3 }
            }
        },
        new Person
        {
            Id = 3, Name = "Kristopher Black", DisplayName = "K_Black", Skills = new List<Skill>
            {
                new() { Name = "C#", Level = 5 },
                new() { Name = ".NET", Level = 4 }
            }
        }
    };

    private long _identity;

    public PersonRepoStub()
    {
        _identity = _persons.Count;
    }

    public async Task<int> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        person.Id = ++_identity;
        _persons.Add(person);
        return await Task.FromResult(1);
    }

    public async Task<int> UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        Person personToUpdate = _persons.Find(p => p.Id == person.Id);
        if (personToUpdate == null) return await Task.FromResult(0);

        personToUpdate.Id = person.Id;
        personToUpdate.Name = person.Name;
        personToUpdate.DisplayName = person.DisplayName;
        personToUpdate.Skills = person.Skills;
        return await Task.FromResult(1);
    }

    public async Task<int> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        Person personToDelete = _persons.Find(p => p.Id == id);
        if (personToDelete == null) return await Task.FromResult(0);

        _persons.Remove(personToDelete);
        return await Task.FromResult(1);
    }

    public async Task<Person> FindAsync(long id, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_persons.Find(p => p.Id == id));
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_persons);
    }

    public async Task ExecuteQueryAsync(string sql, object[] sqlParametersObjects, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(0);
    }

    public async Task<long> CreateAsync(Person person, CancellationToken cancellationToken = default)
    {
        person.Id = ++_identity;
        _persons.Add(person);
        return await Task.FromResult(_identity);
    }
}