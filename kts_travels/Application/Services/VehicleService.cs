using AutoMapper;
using kts_travels.SharedServices.Application.Dtos;
using kts_travels.SharedServices.Application.Services.Interfaces;
using kts_travels.SharedServices.Domain.Entities;
using kts_travels.SharedServices.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace kts_travels.SharedServices.Application.Services
{
    public class VehicleService(ILogger<VehicleService> logger, IVehicleRepository vehicleRepository, IMapper mapper) : IVehicleService
    {
        private readonly ILogger<VehicleService> _logger = logger;
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IMapper _mapper = mapper;


        public async Task<ResponseDto> AddVehicleAsync(VehicleDto vehicleDto)
        {
            try
            {
                var newVehicle = _mapper.Map<Vehicle>(vehicleDto);
                bool isSuccess = await _vehicleRepository.AddVehicleAsync(newVehicle);
                return new ResponseDto
                {
                    IsSuccess = isSuccess,
                    Message = isSuccess ? "New Vehicle has been added successfully" : "Failed to add the new Vehicle",
                    Result = isSuccess ? newVehicle : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("There has been an Error while adding a new vehicle");
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDto> DeleteVehicleAsync(int id)
        {
            try
            {
                bool isSuccess = await _vehicleRepository.DeleteVehicleAsync(id);
                return new ResponseDto
                {
                    IsSuccess = isSuccess,
                    Message = isSuccess ? "The Vehicle has been deleted successfully" : "Failed to delete the vehicle"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("There has been an Error While deleting the vehicle.");
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseDto> GetVehicleAsync(int id)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
                if (vehicle == null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Result = "The Vehicle is Unavialable"
                    };
                }
                VehicleDto vehicleDto = _mapper.Map<VehicleDto>(vehicle);
                return new ResponseDto
                {
                    IsSuccess = true,
                    Result = vehicleDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Thre has been an Error while fetching the vehicle.");
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDto> GetVehicleByNoAsync(string vehicleNo)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleByNoAsync(vehicleNo);
                if (vehicle == null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Result = "The Vehicle is Unavialable"
                    };
                }
                VehicleDto vehicleDto = _mapper.Map<VehicleDto>(vehicle);
                return new ResponseDto
                {
                    IsSuccess = true,
                    Result = vehicleDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Thre has been an Error while fetching the vehicle.");
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ResponseDto> GetVehiclesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var vehicles = await _vehicleRepository.GetAllVehicleAsync(pageNumber, pageSize);
                if (vehicles == null || !vehicles.Any())
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "No records of the vehicle are available at the moment."
                    };
                }

                var vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);
                return new ResponseDto
                {
                    IsSuccess = true,
                    Result = vehicleDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There has been an error while fetching the vehicles.");
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<ResponseDto> UpdateVehicleAsync(VehicleDto vehicleDto)
        {
            try
            {
                var editedVehicle = _mapper.Map<Vehicle>(vehicleDto);
                bool isSuccess = await _vehicleRepository.UpdateVehicleAsync(editedVehicle);
                return new ResponseDto
                {
                    IsSuccess = isSuccess,
                    Message = isSuccess ? "New Vehicle has been added successfully" : "Failed to add the new Vehicle",
                    Result = isSuccess ? editedVehicle : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("There has been an Error while adding a new vehicle");
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }
    }
}
