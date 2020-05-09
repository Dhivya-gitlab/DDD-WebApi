using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentEducationBoardService.Api.AppModels;
using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;

        public SchoolController(ISchoolService schoolService, IMapper mapper)
        {
            this._schoolService = schoolService;
            this._mapper = mapper;
        }

        // GET: api/School
        [HttpGet]
        public async Task<IEnumerable<SchoolDetailsDto>> Get()
        {
            List<School> schoolDetailsCol = await _schoolService.GetSchoolList();
            List<SchoolDetailsDto> schoolDetail = _mapper.Map<List<SchoolDetailsDto>>(schoolDetailsCol);
            return schoolDetail;
        }

        // GET: api/School/5
        [HttpGet("{id}", Name = "Get")]
        public SchoolDetailsDto Get(int id)
        {
            School schoolDetailsDto = _schoolService.GetSchool(id);
            SchoolDetailsDto school = _mapper.Map<SchoolDetailsDto>(schoolDetailsDto);
            return school;
        }

        // POST: api/School
        [HttpPost]
        public IActionResult Post([FromBody] CreateSchoolDto schoolToBeCreated)
        {
            if (schoolToBeCreated == null)
            {
                return BadRequest("Invalid school to add");
            }
            else
            {
                School createSchool = _mapper.Map<School>(schoolToBeCreated);
                _schoolService.CreateSchool(createSchool);
                return Ok("School created successfully");
            }
        }

        // PUT: api/School/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateSchoolDto schoolToUpdate)
        {
            if (id < 1)
            {
                return BadRequest("Invalid School Id");
            }
            else
            {
                School schoolToBeUpdated = _mapper.Map<School>(schoolToUpdate);
                _schoolService.UpdateSchool(id, schoolToBeUpdated);
                return Ok("School updated successfully");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid School Id");
            }
            else
            {
                _schoolService.DeleteSchool(id);
                return Ok("School successfully removed from database");
            }
        }
    }
}
