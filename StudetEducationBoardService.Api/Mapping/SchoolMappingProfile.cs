using AutoMapper;
using StudentEducationBoardService.Api.AppModels;
using StudentEducationBoardService.Domain.Models;

namespace StudentEducationBoardService.Api.Mapping
{
    public class SchoolMappingProfile :Profile
    {
        public SchoolMappingProfile()
        {
            CreateMap<School, SchoolDetailsDto>();
            CreateMap<CreateSchoolDto, School>();
            CreateMap<School, CreateSchoolDto>();
            CreateMap<UpdateSchoolDto, School>();
            CreateMap<School, UpdateSchoolDto>();
        }
    }
}
