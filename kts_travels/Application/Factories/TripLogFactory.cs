using AutoMapper;
using kts_travels.Application.Dtos;
using kts_travels.Application.Factories.Interfaces;
using kts_travels.Domain.Repositories;
using kts_travels.Domain.Entities;

namespace kts_travels.Application.Factories
{
    public class TripLogFactory(IMapper mapper) : ITripLogFactory
    {
        private readonly IMapper _mapper = mapper;

        public async Task<TripLog> CreateTripLogAsync(TripLogDto tripLogDto, IVehicleRepository vehicleRepository)
        {
            tripLogDto = tripLogDto ?? throw new ArgumentNullException(nameof(tripLogDto), "TripLogDto cannot be null");

            var vehicle = await vehicleRepository.GetVehicleByNoAsync(tripLogDto.VehicleNo);
            vehicle = vehicle ?? throw new InvalidOperationException($"Vehicle with number {tripLogDto.VehicleNo} not found.");

            var tripLog = _mapper.Map<TripLog>(tripLogDto);
            tripLog.VehicleId = vehicle.VehicleId;

            return tripLog;
        }
    }


}
