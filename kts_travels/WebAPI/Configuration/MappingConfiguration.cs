using AutoMapper;
using kts_travels.Application.Dtos;
using kts_travels.Domain.Entities;

namespace kts_travels.WebAPI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TripLogDto, TripLog>().ReverseMap();
            CreateMap<VehicleDto, Vehicle>().ReverseMap();
        }
    }
}
