using HallOfFame.Domain.Entities;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Queries.GetAllPersons;

public record GetPersonsQuery : IRequest<IEnumerable<Person>>;