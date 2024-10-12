using AutoMapper;
using kts_travels.Core.Application.Dtos;

using kts_travels.Core.Application.Factories.Interfaces;
using kts_travels.Core.Application.Services.Interfaces;
using kts_travels.Core.Domain.Repositories;
using kts_travels.Core.Domain.Entities;


namespace kts_travels.Core.Application.Services
{
    public class TripLogService(ITripLogRepository tripLogRepository, IVehicleRepository vehicleRepository, ITripLogFactory tripLogFactory, IMapper mapper, IVehicleSummariesFactory vehicleSummaryFactory) : ITripLogService
    {
        private readonly ITripLogRepository _tripLogRepository = tripLogRepository;
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly ITripLogFactory _tripLogFactory = tripLogFactory;
        private readonly IMapper _mapper = mapper;
        private readonly IVehicleSummariesFactory _vehicleSummaryFactory = vehicleSummaryFactory;

        public async Task<ResponseDto> AddTripLogAsync(TripLogDto triplogDto)
        {
            try
            {
                triplogDto = triplogDto ?? throw new ArgumentNullException(nameof(triplogDto), "TripLogDto cannot be null");

                var latestLog = await _tripLogRepository.GetLatestTripLogBeforeDateAsync(triplogDto.VehicleNo, triplogDto.Date);

                var existingLogWithSameKm = await _tripLogRepository.GetTripLogByStartingKmAsync(triplogDto.VehicleNo, triplogDto.StartingKm);
                if (existingLogWithSameKm != null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = $"A trip log with StartingKm of {triplogDto.StartingKm} already exists for vehicle {triplogDto.VehicleNo}."
                    };
                }

                if (latestLog != null && triplogDto.StartingKm <= latestLog.StartingKm)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = $"Cannot add trip log for {triplogDto.Date.ToShortDateString()} with StartingKm of {triplogDto.StartingKm}. The last log's reading of the date before {triplogDto.Date} is {latestLog.StartingKm}."
                    };
                }

                var tripLog = await _tripLogFactory.CreateTripLogAsync(triplogDto, _vehicleRepository);
                var result = await _tripLogRepository.AddTripLogAsync(tripLog);

                if (result)
                {
                    // Get the month for VehicleSummary
                    var month = new DateTime(triplogDto.Date.Year, triplogDto.Date.Month, 1);
                    var tripLogs = await _tripLogRepository.GetTripLogsForVehicleAndMonthAsync(triplogDto.VehicleNo, month);

                    // Update or create the VehicleSummary
                    await _vehicleSummaryFactory.CreateOrUpdateVehicleSummaryAsync(
                        tripLog.VehicleId, // Assuming you have VehicleId in tripLog
                        tripLog.LocationId, // Assuming you have LocationId in tripLog
                        month,
                        tripLogs
                    );

                    return new ResponseDto { IsSuccess = true, Message = "Trip log added successfully." };
                }
                return new ResponseDto { IsSuccess = false, Message = "Failed to add trip log." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ResponseDto> GetTripLogByIdAsync(int id)
        {
            try
            {
                var tripLog = await _tripLogRepository.GetTripLogByIdAsync(id);
                if (tripLog != null)
                {
                    var tripLogDto = _mapper.Map<TripLogDto>(tripLog);
                    return new ResponseDto { IsSuccess = true, Result = tripLogDto };
                }
                return new ResponseDto { IsSuccess = false, Message = "Trip log not found." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ResponseDto> GetAllTripLogsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var tripLogs = await _tripLogRepository.GetAllTripLogsAsync(pageNumber, pageSize);
                var tripLogDtos = _mapper.Map<List<TripLogDto>>(tripLogs);
                return new ResponseDto { IsSuccess = true, Result = tripLogDtos };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ResponseDto> UpdateTripLogAsync(TripLogDto triplogDto)
        {
            try
            {
                var existingTripLog = await _tripLogRepository.GetTripLogByIdAsync(triplogDto.TripId);
                if (existingTripLog == null)
                {
                    return new ResponseDto { IsSuccess = false, Message = "Trip log not found." };
                }

                // Get the vehicle associated with the existing trip log
                var vehicle = await _vehicleRepository.GetVehicleByIdAsync(existingTripLog.VehicleId);
                if (vehicle == null)
                {
                    return new ResponseDto { IsSuccess = false, Message = "Associated vehicle not found." };
                }

                // Get the latest trip log for the vehicle before the specified date
                var latestLog = await _tripLogRepository.GetLatestTripLogBeforeDateAsync(vehicle.VehicleNo, triplogDto.Date);

                // Check if the latest log exists and compare the StartingKm
                if (latestLog != null && triplogDto.StartingKm <= latestLog.StartingKm)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = $"Cannot update trip log for {triplogDto.Date.ToShortDateString()} with StartingKm of {triplogDto.StartingKm}. The last log's reading before this date is {latestLog.StartingKm}."
                    };
                }

                // Use the factory to create/update the trip log
                var updatedTripLog = await _tripLogFactory.CreateTripLogAsync(triplogDto, _vehicleRepository);
                updatedTripLog.TripId = existingTripLog.TripId; // Preserve the original TripId

                var result = await _tripLogRepository.UpdateTripLogAsync(updatedTripLog);
                if (result)
                {
                    var month = new DateTime(triplogDto.Date.Year, triplogDto.Date.Month, 1);
                    var tripLogs = await _tripLogRepository.GetTripLogsForVehicleAndMonthAsync(triplogDto.VehicleNo, month);

                    // Update or create the VehicleSummary
                    await _vehicleSummaryFactory.CreateOrUpdateVehicleSummaryAsync(
                        updatedTripLog.VehicleId, // Assuming you have VehicleId in tripLog
                        updatedTripLog.LocationId, // Assuming you have LocationId in tripLog
                        month,
                        tripLogs
                    );

                    return new ResponseDto { IsSuccess = true, Message = "Trip log updated successfully." };
                }
                return new ResponseDto { IsSuccess = false, Message = "Failed to update trip log." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }


        public async Task<ResponseDto> DeleteTripLogAsync(int id)
        {
            try
            {
                // Fetch the trip log to be deleted
                var tripLogToDelete = await _tripLogRepository.GetTripLogByIdAsync(id);
                if (tripLogToDelete == null)
                {
                    return new ResponseDto { IsSuccess = false, Message = "Trip log not found." };
                }

                var vehicleNo = tripLogToDelete.Vehicle.VehicleNo;
                var month = new DateTime(tripLogToDelete.Date.Year, tripLogToDelete.Date.Month, 1);

                // Delete the trip log
                var result = await _tripLogRepository.DeleteTripLogAsync(id);
                if (result)
                {
                    // Get remaining trip logs for the vehicle for the specific month
                    var tripLogs = await _tripLogRepository.GetTripLogsForVehicleAndMonthAsync(vehicleNo, month);

                    // Update the VehicleSummary
                    await _vehicleSummaryFactory.CreateOrUpdateVehicleSummaryAsync(
                        tripLogToDelete.VehicleId, // Assuming you have VehicleId in tripLogToDelete
                        tripLogToDelete.LocationId, // Assuming you have LocationId in tripLogToDelete
                        month,
                        tripLogs
                    );

                    return new ResponseDto { IsSuccess = true, Message = "Trip log deleted successfully." };
                }

                return new ResponseDto { IsSuccess = false, Message = "Failed to delete trip log." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }


        public async Task<ResponseDto> SearchTripLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Start date cannot be date after the end date."
                    };
                }

                var tripLogs = await _tripLogRepository.SearchTripLogsByDateRangeAsync(startDate, endDate);
                var tripLogDtos = _mapper.Map<List<TripLogDto>>(tripLogs);
                return new ResponseDto { IsSuccess = true, Result = tripLogDtos };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }


        public async Task<ResponseDto> SearchTripLogsByVehicleNoAsync(string vehicleNo)
        {
            try
            {
                var tripLogs = await _tripLogRepository.SearchTripLogsByVehicleNoAsync(vehicleNo);
                var tripLogDtos = _mapper.Map<List<TripLogDto>>(tripLogs);
                return new ResponseDto { IsSuccess = true, Result = tripLogDtos };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}