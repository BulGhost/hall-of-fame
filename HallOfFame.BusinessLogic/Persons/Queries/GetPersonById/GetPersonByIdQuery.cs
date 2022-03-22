using HallOfFame.Domain.Entities;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Queries.GetPersonById;

public record GetPersonByIdQuery(long PersonId) : IRequest<Person>;