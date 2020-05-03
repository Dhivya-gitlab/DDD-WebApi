using Abp.Domain.Uow;
using StudentEducationBoardService.Data;
using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Services.Dtos.SchoolDto;
using StudentEducationBoardService.Services.ServicesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IUnitOfWork = StudentEducationBoardService.Data.IUnitOfWork;

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
                CommunicationLanguage = createSchoolDto.CommunicationLanguage
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

        public SchoolDetailsDto GetSchool(int schoolID)
        {
            School school = schoolUnitOfWork.Repository.GetById(schoolID);
            SchoolDetailsDto schoolRequested = new SchoolDetailsDto()
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                Country = school.Country,
                CommunicationLanguage = school.CommunicationLanguage
            };

            return schoolRequested;
        }

        public List<SchoolDetailsDto> GetSchoolList()
        {
            List<SchoolDetailsDto> schoolList = schoolUnitOfWork.Repository.GetAll()
            .Select(s => new SchoolDetailsDto
            {
                SchoolId = s.SchoolId,
                SchoolName = s.SchoolName,
                Country = s.Country,
                CommunicationLanguage = s.CommunicationLanguage
            }).ToList();

            return schoolList;
        }

        public void UpdateSchool(UpdateSchoolDto updateSchoolDto)
        {
            if (updateSchoolDto == null)
            {
                throw new ArgumentNullException(nameof(updateSchoolDto));
            }

            School schoolToBeUpdated = schoolUnitOfWork.Repository.GetById(updateSchoolDto.SchoolId);
            if (schoolToBeUpdated != null)
            {
                schoolToBeUpdated.SchoolName = updateSchoolDto.SchoolName;
                schoolToBeUpdated.Country = updateSchoolDto.Country;
                schoolToBeUpdated.CommunicationLanguage = updateSchoolDto.CommunicationLanguage;

                schoolUnitOfWork.Repository.Update(schoolToBeUpdated);
                schoolUnitOfWork.Complete();
            }
        }
    }
}
