using AutoMapper;
using HallOfFame.DataAccess.DbContext;
using HallOfFame.DataAccess.Models;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.DataAccess.Repositories;

public class PersonRepo : IPersonRepo
{
    private readonly HallOfFameDbContext _context;
    private readonly IMapper _mapper;

    public DbSet<PersonModel> Persons { get; }
    public DbSet<SkillModel> PersonsSkills { get; }

    public PersonRepo(HallOfFameDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
        Persons = _context.Set<PersonModel>();
        PersonsSkills = _context.Set<SkillModel>();
    }

    public async Task<int> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        var personModel = _mapper.Map<Person, PersonModel>(person);
        Persons.Add(personModel);
        return await SaveAsync(cancellationToken);
    }

    public async Task<long> CreateAsync(Person person, CancellationToken cancellationToken = default)
    {
        var personModel = _mapper.Map<Person, PersonModel>(person);
        Persons.Add(personModel);
        await SaveAsync(cancellationToken);
        return personModel.Id;
    }

    public async Task<int> UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        var personModel = _mapper.Map<Person, PersonModel>(person);
        PersonsSkills.RemoveRange(PersonsSkills.Where(s => s.PersonId == personModel.Id));
        PersonsSkills.AddRange(personModel.Skills);
        Persons.Update(personModel);
        return await SaveAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        PersonModel personToDelete = Persons.FirstOrDefault(a => a.Id == id);
        if (personToDelete == null) return 0;

        Persons.Remove(personToDelete);
        return await SaveAsync(cancellationToken);
    }

    public async Task<Person> FindAsync(long id, CancellationToken cancellationToken = default)
    {
        PersonModel personModel = await Persons
            .Include(person => person.Skills)
            .SingleOrDefaultAsync(person => person.Id == id, cancellationToken);
        return _mapper.Map<PersonModel, Person>(personModel);
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<PersonModel> personModels = await Persons
            .Include(person => person.Skills)
            .ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<PersonModel>, IEnumerable<Person>>(personModels);
    }

    public async Task ExecuteQueryAsync(string sql, object[] sqlParametersObjects, CancellationToken cancellationToken = default) =>
        await _context.Database.ExecuteSqlRawAsync(sql, sqlParametersObjects, cancellationToken);

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
}