using StudentEducationBoardService.Domain;
using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Domain.SchoolDto.Dtos.SchoolDto;
using StudentEducationBoardService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Services.Services
{
    public class SchoolService : ISchoolService
    {
        private IUnitOfWork schoolUnitOfWork;
        public SchoolService(IUnitOfWork unitOfWork)
        {
            schoolUnitOfWork = unitOfWork;
        }
        public void CreateSchool(CreateSchoolDto createSchoolDto)
        {
            if (createSchoolDto == null)
            {
                throw new ArgumentNullException(nameof(createSchoolDto));
            }

            School schoolToBeCreated = new School()
            {
                SchoolName = createSchoolDto.SchoolName,
                Country = createSchoolDto.Country,
                CommunicationLanguage = createSchoolDto.CommunicationLanguage,
                User = createSchoolDto.User,
                Program = createSchoolDto.Program,
                AssessmentPeriod = createSchoolDto.AssessmentPeriod
            };

            schoolUnitOfWork.Repository.Add(schoolToBeCreated);
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

        public  SchoolDetailsDto GetSchool(int schoolID)
        {
            var school =  schoolUnitOfWork.Repository.GetById(schoolID);
            SchoolDetailsDto schoolRequested = new SchoolDetailsDto()
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                Country = school.Country,
                CommunicationLanguage = school.CommunicationLanguage,
                User = school.User,
                Program = school.Program,
                AssessmentPeriod = school.AssessmentPeriod
            };

            return schoolRequested;
        }

        public async Task<List<SchoolDetailsDto>> GetSchoolList()
        {
            var schoolList = await schoolUnitOfWork.Repository.GetAll();
            return schoolList.Select(s => new SchoolDetailsDto
            {
                SchoolId = s.SchoolId,
                SchoolName = s.SchoolName,
                Country = s.Country,
                CommunicationLanguage = s.CommunicationLanguage,
                User = s.User,
                Program = s.Program,
                AssessmentPeriod = s.AssessmentPeriod
            }).ToList();
        }

        public void UpdateSchool(int id, UpdateSchoolDto updateSchoolDto)
        {
            if (updateSchoolDto == null)
            {
                throw new ArgumentNullException(nameof(updateSchoolDto));
            }

            School schoolToBeUpdated = schoolUnitOfWork.Repository.GetById(id);

            if (schoolToBeUpdated != null)
            {
                schoolToBeUpdated.SchoolName = updateSchoolDto.SchoolName;
                schoolToBeUpdated.Country = updateSchoolDto.Country;
                schoolToBeUpdated.CommunicationLanguage = updateSchoolDto.CommunicationLanguage;
                schoolToBeUpdated.User = updateSchoolDto.User;
                schoolToBeUpdated.Program = updateSchoolDto.Program;
                schoolToBeUpdated.AssessmentPeriod = updateSchoolDto.AssessmentPeriod;

                schoolUnitOfWork.Repository.Update(schoolToBeUpdated);
                schoolUnitOfWork.Complete();
            }
        }
    }
}
