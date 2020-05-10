using Abp.Extensions;
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
            schoolUnitOfWork.Repository.Add(createSchool);
            schoolUnitOfWork.Complete();
            var response = AddSchoolDetailsToRedisCache();
            response.Wait();
        }

        public void DeleteSchool(int schoolId)
        {
            School toBeDeleted = schoolUnitOfWork.Repository.GetById(schoolId);

            if (toBeDeleted != null)
            {
                schoolUnitOfWork.Repository.Remove(toBeDeleted);
                schoolUnitOfWork.Complete();
                var response = AddSchoolDetailsToRedisCache();
                response.Wait();
            }
        }

        public School GetSchool(int schoolID)
        {
            var school = schoolUnitOfWork.Repository.GetById(schoolID);
            return school;
        }

        public async Task<List<School>> GetSchoolList()
        {
            string cachedSchoolsDetail = await _redisCache.GetStringAsync("schools");

            if (cachedSchoolsDetail == null || string.IsNullOrEmpty(cachedSchoolsDetail))
            {
                cachedSchoolsDetail = await AddSchoolDetailsToRedisCache();
            }
            JsonSerializerOptions opt = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var schoolListCol = JsonSerializer.Deserialize<List<School>>(cachedSchoolsDetail, opt);
            return schoolListCol.ToList();
        }

        private async Task<string> AddSchoolDetailsToRedisCache()
        {
            string cachedSchoolsDetail;
            var schoolList = await schoolUnitOfWork.Repository.GetAll();
            var options = new DistributedCacheEntryOptions();
            cachedSchoolsDetail = JsonSerializer.Serialize<List<School>>(schoolList.ToList());

            if (cachedSchoolsDetail != null)
            {
                options.SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(1));
                await _redisCache.SetStringAsync("schools", cachedSchoolsDetail, options);
            }
            return cachedSchoolsDetail;
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
                var response = AddSchoolDetailsToRedisCache();
                response.Wait();
            }
        }
    }
}
