using AutoMapper;
using StudentEducationBoardService.Api.AppModels;
using StudentEducationBoardService.Services.Dtos.SchoolDto;

namespace StudentEducationBoardService.Api.Mapping
{
    public class SchoolMappingProfile :Profile
    {
        public SchoolMappingProfile()
        {
            CreateMap<SchoolDetails, SchoolDetailsDto>();
            CreateMap<CreateSchool, CreateSchoolDto>();
            CreateMap<UpdateSchool, UpdateSchoolDto>();
        }
    }
}
