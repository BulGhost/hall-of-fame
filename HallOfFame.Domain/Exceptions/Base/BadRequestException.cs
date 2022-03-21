namespace HallOfFame.Domain.Exceptions.Base;

public abstract class BadRequestException : HallOfFameException
{
    protected BadRequestException()
    {
    }

    protected BadRequestException(string message) : base(message)
    {
    }

    protected BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}