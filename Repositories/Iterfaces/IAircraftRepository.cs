
using AirlineAPI.DTO;

namespace AirlineAPI;

public interface IAircraftRepository
{
    public IEnumerable<AircraftListDataView> GetAircrafts();
    public void Insert(AircraftCreateDTO createData);
}
