
namespace AirlineAPI;

public interface IAircraftRepository
{
    public IEnumerable<AircraftListDataView> GetAircraftsByCapacity();
}
