using AutoMapper;
using kts_travels.Data;
using kts_travels.Models;
using kts_travels.Models.Dto;
using kts_travels.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace kts_travels.Services
{
    public class DieselEntryService : IDieselEntryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DieselEntryService> _logger;

        public DieselEntryService(AppDbContext context, IMapper mapper, ILogger<DieselEntryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseDto> GetAllEntriesAsync()
        {
            try
            {
                var entries = await _context.DieselEntries.ToListAsync();
                var entriesDto = _mapper.Map<List<DieselEntryDto>>(entries);
                return new ResponseDto { Result = entriesDto, IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all diesel entries.");
                return new ResponseDto { IsSuccess = false, Message = "An error occurred while fetching the entries." };
            }
        }

        public async Task<ResponseDto> GetEntriesByLocationAndVehicleNoAsync(string location, string vehicleNo)
        {
            try
            {
                var entries = await _context.DieselEntries
                    .Where(e => e.Location == location && e.VehicleNo == vehicleNo)
                    .ToListAsync();

                if (!entries.Any())
                {
                    return new ResponseDto { IsSuccess = false, Message = "No entries found for the given criteria" };
                }

                var entriesDto = _mapper.Map<List<DieselEntryDto>>(entries);
                return new ResponseDto { Result = entriesDto, IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving diesel entries for location: {location} and vehicleNo: {vehicleNo}", location, vehicleNo);
                return new ResponseDto { IsSuccess = false, Message = "An error occurred while fetching the entries." };
            }
        }

        public async Task<ResponseDto> GetAveragesByMonthAsync(string month, string year, string location = null, string vehicleNo = null)
        {
            try
            {
                if (!int.TryParse(month, out int monthNum) || !int.TryParse(year, out int yearNum))
                {
                    return new ResponseDto { IsSuccess = false, Message = "Invalid month or year" };
                }

                var query = _context.DieselEntries.AsQueryable();

                if (!string.IsNullOrEmpty(location))
                {
                    query = query.Where(e => e.Location == location);
                }

                if (!string.IsNullOrEmpty(vehicleNo))
                {
                    query = query.Where(e => e.VehicleNo == vehicleNo);
                }

                query = query.Where(e => e.Date.Month == monthNum && e.Date.Year == yearNum);

                var entries = await query.ToListAsync();
                if (!entries.Any())
                {
                    return new ResponseDto { IsSuccess = false, Message = "No entries found for the given criteria" };
                }

                // Find the earliest and latest entries for the given month and vehicle
                var earliestEntry = entries.OrderBy(e => e.Date).First();
                var latestEntry = entries.OrderBy(e => e.Date).Last();

                // Calculate the total diesel used and the distance covered
                var totalDiesel = entries.Sum(e => e.DieselLiters);
                var totalKm = latestEntry.StartingKm - earliestEntry.StartingKm;
                var average = totalDiesel > 0 ? totalKm / totalDiesel : 0;

                var averagesDto = new DieselAverageDto
                {
                    VehicleNo = vehicleNo,
                    TotalDieselLiters = totalDiesel,
                    OpeningKms = earliestEntry.StartingKm,
                    ClosingKms = latestEntry.StartingKm,
                    TotalKmRun = totalKm,
                    Average = average
                };

                return new ResponseDto { Result = averagesDto, IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating averages for month: {month}, year: {year}, location: {location}, vehicleNo: {vehicleNo}", month, year, location, vehicleNo);
                return new ResponseDto { IsSuccess = false, Message = "An error occurred while calculating the averages." };
            }
        }


        public async Task<ResponseDto> AddDieselEntryAsync(DieselEntryDto entryDto)
        {
            try
            {
                if (entryDto == null)
                {
                    return new ResponseDto { IsSuccess = false, Message = "Diesel entry data is null" };
                }

                var entry = _mapper.Map<DieselEntry>(entryDto);
                _context.DieselEntries.Add(entry);
                await _context.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Message = "Entry added successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding diesel entry.");
                return new ResponseDto { IsSuccess = false, Message = "An error occurred while adding the entry." };
            }
        }
    }


}
