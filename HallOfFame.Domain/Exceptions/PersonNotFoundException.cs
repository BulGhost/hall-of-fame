using HallOfFame.Domain.Exceptions.Base;

namespace HallOfFame.Domain.Exceptions;

public sealed class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException()
    {
    }

    public PersonNotFoundException(long personId)
        : base($"Person with the id={personId} was not found.")
    {
    }

    public PersonNotFoundException(long personId, Exception innerException)
        : base($"Person with the id={personId} was not found.", innerException)
    {
    }
}