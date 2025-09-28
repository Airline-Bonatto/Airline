
using AirlineAPI.Airline.api.RequestBodies;

namespace AirlineAPI.Airline.api.DTO;

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
