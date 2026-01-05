
using System;

using Airline.Database;

using Microsoft.EntityFrameworkCore;

namespace Airline.Tests;

public class IntegrationTestBase : IDisposable
{
    protected readonly AirlineContext Context;

    public IntegrationTestBase()
    {
        DbContextOptions<AirlineContext> options = new DbContextOptionsBuilder<AirlineContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new AirlineContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
