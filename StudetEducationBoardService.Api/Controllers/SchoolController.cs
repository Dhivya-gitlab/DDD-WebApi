using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentEducationBoardService.Api.AppModels;
using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;
        private readonly ILogger<SchoolController> _logger;

        public SchoolController(ISchoolService schoolService, IMapper mapper, ILogger<SchoolController> logger)
        {
            this._schoolService = schoolService;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET: api/School
        [HttpGet]
        public async Task<IEnumerable<SchoolDetailsDto>> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all school info");
                List<School> schoolDetailsCol = await _schoolService.GetSchoolList();
                List<SchoolDetailsDto> schoolDetail = _mapper.Map<List<SchoolDetailsDto>>(schoolDetailsCol);
                _logger.LogInformation( $"Returning {schoolDetail.Count} schools.");
                return schoolDetail;
            }
            catch (System.Exception)
            {
                _logger.LogError("Something went wrong");
                return Enumerable.Empty<SchoolDetailsDto>().ToList();
            }
        }

        // GET: api/School/5
        [HttpGet("{id}", Name = "Get")]
        public SchoolDetailsDto Get(int id)
        {
            try
            {
                _logger.LogInformation("Fetching school info");
                School schoolDetailsDto = _schoolService.GetSchool(id);
                SchoolDetailsDto school = _mapper.Map<SchoolDetailsDto>(schoolDetailsDto);
                _logger.LogInformation($"Returning school.");
                return school;
            }
            catch (System.Exception)
            {
                _logger.LogError("Something went wrong");
                return new SchoolDetailsDto();
            }
        }

        // POST: api/School
        [HttpPost]
        public IActionResult Post([FromBody] CreateSchoolDto schoolToBeCreated)
        {
            try
            {
                if (schoolToBeCreated == null)
                {
                    _logger.LogInformation("Invalid school to add");
                    return BadRequest("Invalid school to add");
                }
                else
                {
                    School createSchool = _mapper.Map<School>(schoolToBeCreated);
                    _schoolService.CreateSchool(createSchool);
                    _logger.LogInformation("School created successfully");
                    return Ok("School created successfully");
                }
            }
            catch (System.Exception)
            {
                _logger.LogError("Something went wrong");
                return BadRequest("Error creating school");
            }
        }

        // PUT: api/School/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateSchoolDto schoolToUpdate)
        {
            try
            {
                if (id < 1)
                {
                    _logger.LogInformation("Invalid School Id to update");
                    return BadRequest("Invalid School Id");
                }
                else
                {
                    School schoolToBeUpdated = _mapper.Map<School>(schoolToUpdate);
                    _schoolService.UpdateSchool(id, schoolToBeUpdated);
                    _logger.LogInformation("Schoolupdated successfully");
                    return Ok("School updated successfully");
                }
            }
            catch (System.Exception)
            {
                _logger.LogError("Something went wrong");
                return BadRequest("Error updating school");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    _logger.LogInformation("Invalid school id");
                    return BadRequest("Invalid School Id");
                }
                else
                {
                    _schoolService.DeleteSchool(id);
                    _logger.LogInformation("School deleted successfully");
                    return Ok("School successfully removed from database");
                }

            }
            catch (System.Exception)
            {
                _logger.LogError("Something went wrong");
                return BadRequest("Error updating school");
            }
        }
    }
}
