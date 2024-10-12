using AutoMapper;
using kts_travels.SharedServices.Application.Dtos;
using kts_travels.SharedServices.Domain.Entities;

namespace kts_travels.SharedServices.Application.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TripLogDto, TripLog>().ReverseMap();
            CreateMap<VehicleDto, Vehicle>().ReverseMap();
            CreateMap<VehicleSummaryDto, VehicleSummary>().ReverseMap();
        }
    }
}
