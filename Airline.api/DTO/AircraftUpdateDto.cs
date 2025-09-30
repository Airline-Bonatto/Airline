
using Airline.RequestBodies;

namespace Airline.DTO;

public record AircraftUpdateDTO
(
    int AircraftId,
    int? Capacity,
    double? Range,
    double? AverageFuelConsumption,
    DateTime? FinalDate = null
)
{
    public AircraftUpdateDTO(AircraftUpdateRequestBody requestBody, int aircraftId)
        : this(aircraftId, requestBody.Capacity, requestBody.Range, requestBody.AverageFuelConsumption)
    {
    }
}
