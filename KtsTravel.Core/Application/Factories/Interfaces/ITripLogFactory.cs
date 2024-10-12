using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Domain.Repositories;
using kts_travels.Core.Domain.Entities;

namespace kts_travels.Core.Application.Factories.Interfaces
{
    public interface ITripLogFactory
    {
        Task<TripLog> CreateTripLogAsync(TripLogDto tripLogDto, IVehicleRepository vehicleRepository);
    }
}
