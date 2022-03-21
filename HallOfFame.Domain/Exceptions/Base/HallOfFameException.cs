namespace HallOfFame.Domain.Exceptions.Base;

public abstract class HallOfFameException : Exception
{
    protected HallOfFameException()
    {
    }

    protected HallOfFameException(string message) : base(message)
    {
    }

    protected HallOfFameException(string message, Exception innerException) : base(message, innerException)
    {
    }
}