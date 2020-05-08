using AutoMapper;
using StudentEducationBoardService.Api.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardTest
{
    //Auto mapper is singleton in unit test, so created helper class for Automapper object
    public class SchoolAutoMapper
    {
        public static IMapper _mapper;

        public static IMapper Mapper()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new SchoolMappingProfile());
                    }
                );
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            return _mapper;
        }
    }
}
