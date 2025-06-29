using AirlineAPI.DTO;

namespace AirlineAPI;

public interface IRouteRepository
{
    Task Insert(RouteMergeDTO createData);
}
