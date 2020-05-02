using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentEducationBoardService.Api.AppModels;
using StudentEducationBoardService.Data;
using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Services.Dtos.SchoolDto;
using StudentEducationBoardService.Services.ServicesInterface;

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
        public IEnumerable<SchoolDetails> Get()
        {
            List<SchoolDetailsDto> schoolDetailsDto = _schoolService.GetSchoolList();
            List<SchoolDetails> schoolDetail = _mapper.Map<List<SchoolDetails>>(schoolDetailsDto);
            return schoolDetail;
        }

        // GET: api/School/5
        [HttpGet("{id}", Name = "Get")]
        public SchoolDetails Get(int id)
        {
            SchoolDetailsDto schoolDetailsDto = _schoolService.GetSchool(id);
            SchoolDetails school = _mapper.Map<SchoolDetails>(schoolDetailsDto);
            return school;
        }

        // POST: api/School
        [HttpPost]
        public void Post([FromBody] CreateSchool schoolToBeCreated)
        {
            CreateSchoolDto createSchool = _mapper.Map<CreateSchoolDto>(schoolToBeCreated);
            _schoolService.CreateSchool(createSchool);
        }

        // PUT: api/School/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateSchool schoolToUpdate)
        {
            UpdateSchoolDto schoolToBeUpdated = _mapper.Map<UpdateSchoolDto>(schoolToUpdate);
            _schoolService.UpdateSchool(schoolToBeUpdated);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _schoolService.DeleteSchool(id);
        }
    }

    //Backup working copy
    //public class SchoolController : ControllerBase
    //{
    //    private StudentEducationBoardDbContext educationBoardContext;

    //    public SchoolController(StudentEducationBoardDbContext educationBoardContext)
    //    {
    //        this.educationBoardContext = educationBoardContext;
    //    }

    //    // GET: api/School
    //    [HttpGet]
    //    public IEnumerable<School> Get()
    //    {
    //        return educationBoardContext.Schools;
    //    }

    //    // GET: api/School/5
    //    [HttpGet("{id}", Name = "Get")]
    //    public School Get(int id)
    //    {
    //        var school = educationBoardContext.Schools.Find(id);
    //        return school;
    //    }

    //    // POST: api/School
    //    [HttpPost]
    //    public void Post([FromBody] School school)
    //    {
    //        educationBoardContext.Schools.Add(school);
    //        educationBoardContext.SaveChanges();
    //    }

    //    // PUT: api/School/5
    //    [HttpPut("{id}")]
    //    public void Put(int id, [FromBody] School value)
    //    {
    //        var schoolToUpdate = educationBoardContext.Schools.Find(id);
    //        schoolToUpdate.SchoolName = value.SchoolName;
    //        schoolToUpdate.Country = value.Country;
    //        schoolToUpdate.CommunicationLanguage = value.CommunicationLanguage;
    //        educationBoardContext.SaveChanges();
    //    }

    //    // DELETE: api/ApiWithActions/5
    //    [HttpDelete("{id}")]
    //    public void Delete(int id)
    //    {
    //        var schoolToRemove = educationBoardContext.Schools.Find(id);
    //        educationBoardContext.Schools.Remove(schoolToRemove);
    //        educationBoardContext.SaveChanges();
    //    }
    //}
}
