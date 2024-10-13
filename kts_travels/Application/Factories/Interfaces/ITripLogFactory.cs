using kts_travels.SharedServices.Application.Dtos;
using kts_travels.SharedServices.Domain.Repositories;
using kts_travels.SharedServices.Domain.Entities;

namespace kts_travels.SharedServices.Application.Factories.Interfaces
{
    public interface ITripLogFactory
    {
        Task<TripLog> CreateTripLogAsync(TripLogDto tripLogDto, IVehicleRepository vehicleRepository);
    }
}
