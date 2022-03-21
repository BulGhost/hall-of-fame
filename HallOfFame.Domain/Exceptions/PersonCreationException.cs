using HallOfFame.Domain.Exceptions.Base;

namespace HallOfFame.Domain.Exceptions;

public sealed class PersonCreationException : BadRequestException
{
    public PersonCreationException()
    {
    }

    public PersonCreationException(string message) : base(message)
    {
    }

    public PersonCreationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}