namespace HallOfFame.Domain.Exceptions.Base;

public abstract class NotFoundException : HallOfFameException
{
    protected NotFoundException()
    {
    }

    protected NotFoundException(string message) : base(message)
    {
    }

    protected NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}