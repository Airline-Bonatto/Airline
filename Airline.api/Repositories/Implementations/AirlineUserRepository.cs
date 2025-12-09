
using Airline.Database;
using Airline.Models;

namespace Airline.Repositories.Implementations;

public class AirlineUserRepository(AirlineContext context)
{
    private readonly AirlineContext _context = context;


    public async Task<AirlineUser?> GetUserByIdAsync(int userId)
    {
        return await _context.AirlineUsers.FindAsync(userId);
    }
}