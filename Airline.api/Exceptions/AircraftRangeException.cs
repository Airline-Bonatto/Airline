
namespace Airline.Exceptions;

public class AircraftRangeException : Exception
{
    public AircraftRangeException()
    {
    }

    public AircraftRangeException(string message)
        : base(message)
    {
    }
}