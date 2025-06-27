
namespace AirlineAPI.DTO
{
    public record AircraftUpdateDTO
    (
        int AircraftId,
        int Capacity,
        double Range,
        double AvaregeFuelConsumption
    )
    {
        public AircraftUpdateDTO(AircraftUpdateRequestBody requestBody, int aircraftId)
            : this(aircraftId, requestBody.Capacity, requestBody.Range, requestBody.AvaregeFuelConsumption)
        {
        }
    }
}
