using AutoMapper;
using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Domain.Entities;

namespace kts_travels.Core.WebAPI.Configuration
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
