using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Airline.Models;
using Airline.Repositories.Implementations;
using Airline.RequestBodies;
using Airline.Services.Implementations;
using Airline.Validators;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Airline.Tests.Services.Implementations;

public class CreateRouteServiceIntegrationTests : IntegrationTestBase
{
    private readonly CreateRouteService _createRouteService;
    private readonly RouteRepository _routeRepository;

    public CreateRouteServiceIntegrationTests()
    {
        _routeRepository = new RouteRepository(Context);
        _createRouteService = new CreateRouteService(_routeRepository);
    }

    [Fact]
    public async void CreateAsync_ShouldInsertRouteInDatabase()
    {
        // Arrange
        RouteInsertRequestBody routeData = new(
            From: "São Paulo",
            To: "Rio de Janeiro",
            Distance: 430.5
        );

        // Act 
        int routeId = await _createRouteService.CreateAsync(routeData);

        // Assert 
        List<Route> routesInDb = await Context.Routes.ToListAsync();
        Assert.Single(routesInDb);

        Route createdRoute = routesInDb.First();

        Assert.Equal("São Paulo", createdRoute.From);
        Assert.Equal("Rio de Janeiro", createdRoute.To);
        Assert.Equal(430.5, createdRoute.Distance);

        Assert.True(routeId > 0);
        Assert.Equal(createdRoute.RouteID, routeId);
    }

    [Fact]
    public async void CreateAsync_MultipleRoutes_ShouldInsertAllInDatabase()
    {
        // Arrange - Criar múltiplas rotas
        RouteInsertRequestBody route1 = new("São Paulo", "Rio de Janeiro", 430.5);
        RouteInsertRequestBody route2 = new("Brasília", "Salvador", 1050.0);
        RouteInsertRequestBody route3 = new("Fortaleza", "Recife", 630.0);

        // Act 
        int id1 = await _createRouteService.CreateAsync(route1);
        int id2 = await _createRouteService.CreateAsync(route2);
        int id3 = await _createRouteService.CreateAsync(route3);

        // Assert 
        List<Route> routesInDb = await Context.Routes.ToListAsync();
        Assert.Equal(3, routesInDb.Count);

        Assert.True(id1 > 0);
        Assert.True(id2 > 0);
        Assert.True(id3 > 0);
        Assert.NotEqual(id1, id2);
        Assert.NotEqual(id2, id3);
        Assert.NotEqual(id1, id3);
    }

    [Fact]
    public async void CreateAsync_ShouldBeRetrievableByGetByIdAsync()
    {
        // Arrange
        RouteInsertRequestBody routeData = new("Porto Alegre", "Curitiba", 560.0);

        // Act 
        int routeId = await _createRouteService.CreateAsync(routeData);
        Route retrievedRoute = await _routeRepository.GetByIdAsync(routeId);

        // Assert 
        Assert.NotNull(retrievedRoute);
        Assert.Equal(routeId, retrievedRoute.RouteID);
        Assert.Equal("Porto Alegre", retrievedRoute.From);
        Assert.Equal("Curitiba", retrievedRoute.To);
        Assert.Equal(560.0, retrievedRoute.Distance);
    }

    [Fact]
    public async void CreateAsync_WithZeroDistance_ShouldStillCreate()
    {
        // Arrange 
        RouteInsertRequestBody routeData = new("Aeroporto A", "Aeroporto B", 0.0);

        // Act
        int routeId = await _createRouteService.CreateAsync(routeData);

        // Assert
        Route route = await _routeRepository.GetByIdAsync(routeId);
        Assert.NotNull(route);
        Assert.Equal(0.0, route.Distance);
    }

    [Fact]
    public async void CreateAsync_WithLargeDistance_ShouldHandleCorrectly()
    {
        // Arrange 
        RouteInsertRequestBody routeData = new(
            From: "São Paulo",
            To: "Tokyo",
            Distance: 18500.0
        );

        // Act
        int routeId = await _createRouteService.CreateAsync(routeData);

        // Assert
        Route route = await _routeRepository.GetByIdAsync(routeId);
        Assert.NotNull(route);
        Assert.Equal(18500.0, route.Distance);
    }

    [Fact]
    public async void CreateAsync_DuplicateRoutes_ShouldThrowValidationException()
    {
        // Arrange 
        RouteInsertRequestBody route1 = new("São Paulo", "Brasília", 1000.0);
        RouteInsertRequestBody route2 = new("São Paulo", "Brasília", 1000.0);

        // Act & Assert
        await _createRouteService.CreateAsync(route1);
        await Assert.ThrowsAsync<ValidationException>(async () =>
            await _createRouteService.CreateAsync(route2)
        );

        List<Route> routesInDb = await Context.Routes.ToListAsync();
        Assert.Single(routesInDb);
    }
}
