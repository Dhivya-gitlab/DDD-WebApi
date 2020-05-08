using Microsoft.Extensions.Caching.Distributed;
using StudentEducationBoardService.Domain;
using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace StudentEducationBoardService.Services.Services
{
    public class SchoolService : ISchoolService
    {
        private IUnitOfWork schoolUnitOfWork;
        private readonly IDistributedCache _redisCache;

        public SchoolService(IUnitOfWork unitOfWork, IDistributedCache redisCache)
        {
            schoolUnitOfWork = unitOfWork;
            _redisCache = redisCache;
        }
        public void CreateSchool(School createSchool)
        {
            if (createSchool == null)
            {
                throw new ArgumentNullException(nameof(createSchool));
            }

            //School schoolToBeCreated = new School()
            //{
            //    SchoolName = createSchoolDto.SchoolName,
            //    Country = createSchoolDto.Country,
            //    CommunicationLanguage = createSchoolDto.CommunicationLanguage,
            //    User = createSchoolDto.User,
            //    Program = createSchoolDto.Program,
            //    AssessmentPeriod = createSchoolDto.AssessmentPeriod
            //};

            schoolUnitOfWork.Repository.Add(createSchool);
            schoolUnitOfWork.Complete();
        }

        public void DeleteSchool(int schoolId)
        {
            School toBeDeleted = schoolUnitOfWork.Repository.GetById(schoolId);

            if (toBeDeleted != null)
            {
                schoolUnitOfWork.Repository.Remove(toBeDeleted);
                schoolUnitOfWork.Complete();
            }
        }

        public School GetSchool(int schoolID)
        {
            var school = schoolUnitOfWork.Repository.GetById(schoolID);
            //School schoolRequested = new SchoolDetailsDto()
            //{
            //    SchoolId = school.SchoolId,
            //    SchoolName = school.SchoolName,
            //    Country = school.Country,
            //    CommunicationLanguage = school.CommunicationLanguage,
            //    User = school.User,
            //    Program = school.Program,
            //    AssessmentPeriod = school.AssessmentPeriod
            //};

            return school;
        }

        public async Task<List<School>> GetSchoolList()
        {
            string cachedSchoolsDetail = await _redisCache.GetStringAsync("schools");

            if (cachedSchoolsDetail == null || string.IsNullOrEmpty(cachedSchoolsDetail))
            {
                var schoolList = await schoolUnitOfWork.Repository.GetAll();
                cachedSchoolsDetail = JsonSerializer.Serialize<List<School>>(schoolList.ToList());
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(1));
                await _redisCache.SetStringAsync("schools", cachedSchoolsDetail);
            }
            JsonSerializerOptions opt = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var schoolListCol = JsonSerializer.Deserialize<List<School>>(cachedSchoolsDetail, opt);
            return schoolListCol.ToList();
            //var schoolList = await schoolUnitOfWork.Repository.GetAll();
            //return schoolList.ToList();
            //return schoolList.Select(s => new SchoolDetailsDto
            //{
            //    SchoolId = s.SchoolId,
            //    SchoolName = s.SchoolName,
            //    Country = s.Country,
            //    CommunicationLanguage = s.CommunicationLanguage,
            //    User = s.User,
            //    Program = s.Program,
            //    AssessmentPeriod = s.AssessmentPeriod
            //}).ToList();
        }

        public void UpdateSchool(int id, School updateSchool)
        {
            if (updateSchool == null)
            {
                throw new ArgumentNullException(nameof(updateSchool));
            }

            School schoolToBeUpdated = schoolUnitOfWork.Repository.GetById(id);

            if (schoolToBeUpdated != null)
            {
                schoolToBeUpdated.SchoolName = updateSchool.SchoolName;
                schoolToBeUpdated.Country = updateSchool.Country;
                schoolToBeUpdated.CommunicationLanguage = updateSchool.CommunicationLanguage;
                schoolToBeUpdated.User = updateSchool.User;
                schoolToBeUpdated.Program = updateSchool.Program;
                schoolToBeUpdated.AssessmentPeriod = updateSchool.AssessmentPeriod;

                schoolUnitOfWork.Repository.Update(schoolToBeUpdated);
                schoolUnitOfWork.Complete();
            }
        }
    }
}
