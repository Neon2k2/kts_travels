using AutoMapper;
using kts_travels.Models;
using kts_travels.Models.Dto;


namespace kts_travels.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DieselEntryDto, DieselEntry>().ReverseMap();
            CreateMap<DieselAverageDto, DieselEntryDto>();

        }
    }
}
