using kts_travels.Application.Dtos;
using kts_travels.Domain.Repositories;
using kts_travels.Domain.Entities;

namespace kts_travels.Application.Factories.Interfaces
{
    public interface ITripLogFactory
    {
        Task<TripLog> CreateTripLogAsync(TripLogDto tripLogDto, IVehicleRepository vehicleRepository);
    }
}
