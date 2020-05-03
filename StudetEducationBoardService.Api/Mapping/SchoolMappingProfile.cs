using AutoMapper;
using StudentEducationBoardService.Api.AppModels;
using StudentEducationBoardService.Services.Dtos.SchoolDto;

namespace StudentEducationBoardService.Api.Mapping
{
    public class SchoolMappingProfile :Profile
    {
        public SchoolMappingProfile()
        {
            CreateMap<SchoolDetailsDto, SchoolDetails>();
            CreateMap<CreateSchool, CreateSchoolDto>();
            CreateMap<UpdateSchool, UpdateSchoolDto>();
        }
    }
}
