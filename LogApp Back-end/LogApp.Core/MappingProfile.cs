using AutoMapper;
using LogApp.Core.DTO;
using LogApp.Core.Models;

namespace LogApp.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Record, RecordOverallDTO>().ReverseMap();
            CreateMap<Record, RecordDetailsDTO>().ReverseMap();
        }
    }
}
