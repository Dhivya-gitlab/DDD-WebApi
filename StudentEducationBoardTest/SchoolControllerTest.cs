using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;
using StudentEducationBoardService.Api.AppModels;
using StudentEducationBoardService.Api.Controllers;
using StudentEducationBoardService.Domain;
using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Domain.Repositories;
using StudentEducationBoardService.Domain.Services;
using StudentEducationBoardService.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEducationBoardTest
{
    [TestFixture]
    public class SchoolControllerTest
    {
        #region "Variable"
        ISchoolService _schoolService;
        ISchoolRepository _schoolRepository;
        IUnitOfWork _unitOfWork;
        Task<IQueryable<School>> _schoolDetails;
        SchoolController _schoolController;
        Mock<IDistributedCache> _redisCache;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _schoolDetails = SetUpSchoolDetails();
            _schoolRepository = SetSchoolRepository();
            _redisCache = new Mock<IDistributedCache>();

            //we can't set property to mock object directly. To achive that implemented below code
            var unitOfData = new Mock<IUnitOfWork>();
            unitOfData.SetupGet(u => u.Repository).Returns(_schoolRepository);
            _unitOfWork = unitOfData.Object;

            _unitOfWork.Repository = _schoolRepository;

            _schoolService = new SchoolService(_unitOfWork, _redisCache.Object);

            _schoolController = new SchoolController(_schoolService, SchoolAutoMapper.Mapper());
        }
        private Task<IQueryable<School>> SetUpSchoolDetails()
        {
            List<School> schools = new List<School>(){
                new School()
            {
                SchoolId = 1,
                SchoolName = "Test School 1",
                Country = "India",
                CommunicationLanguage = "English",
                User = "Teacher",
                AssessmentPeriod = "Summer"
                }
            ,
           new School()
            {
                SchoolId = 2,
                SchoolName = "Test School 2",
                Country = "India",
                CommunicationLanguage = "English",
                User = "Principal",
                AssessmentPeriod = "Winter"
            } };
            return Task.Run(() => schools.ToList().AsQueryable());
        }

        public ISchoolRepository SetSchoolRepository()
        {
            //init repository
            var mockSchoolRepository = new Mock<ISchoolRepository>();

            //Setup mocking behaviour
            mockSchoolRepository.Setup(r => r.GetAll()).Returns(_schoolDetails);
            mockSchoolRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(new Func<int, School>(id => _schoolDetails.Result.ToList().Find(a => a.SchoolId.Equals(id))));
            mockSchoolRepository.Setup(r => r.Add(It.IsAny<School>())).Callback(new Action<School>(newSchool =>
            {
                newSchool.SchoolName = string.Format("Test School {0}", (_schoolDetails.Result.ToList().Count + 1).ToString());
            }));
            mockSchoolRepository.Setup(r => r.Update(It.IsAny<School>())).Callback(new Action<School>(updatedSchool =>
           {
               var oldSchool = _schoolDetails.Result.ToList().Find(s => s.SchoolId == (It.IsAny<int>()));
               oldSchool = updatedSchool;
           }));

            mockSchoolRepository.Setup(r => r.Remove(It.IsAny<School>())).Callback(new Action<School>(deleteSchool =>
            {
                var toBeDelete = _schoolDetails.Result.ToList().Find(s => s.SchoolId == (It.IsAny<int>()));
                if (toBeDelete != null)
                    _schoolDetails.Result.ToList().Remove(toBeDelete);
            }));

            return mockSchoolRepository.Object;
        }

        [Test]
        public void GetAllSchoolTest()
        {
            List<SchoolDetailsDto> schools = _schoolController.Get().Result.ToList();
            List<SchoolDetailsDto> schoolDetail = SchoolAutoMapper.Mapper().Map<List<SchoolDetailsDto>>(_schoolDetails.Result.ToList());
            Assert.IsTrue(schools.GetType().Equals(schoolDetail.GetType()));
        }
        [Test]
        [TestCase(1)]
        public void GetSchoolByID(int schoolId)
        {
            SchoolDetailsDto school = _schoolController.Get(schoolId);
            var mockSchoolsName = SchoolAutoMapper.Mapper().Map<SchoolDetailsDto>(_schoolDetails.Result.Where(s => s.SchoolId == schoolId).ToList().First()).SchoolName;
            Assert.AreEqual(school.SchoolName, mockSchoolsName);
        }

        [Test]
        [TestCase(1)]
        public void DeleteSchoolByID_ReturnsHttpOkResult(int schoolId)
        {
            var result = _schoolController.Delete(schoolId);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        [TestCase(-1)]
        public void DeleteSchoolByID_ReturnsHttpBadResult(int schoolId)
        {
            var result = _schoolController.Delete(schoolId);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        [TestCase(1)]
        public void PutSchoolByID_ReturnsHttpOkResult(int id)
        {
            School school = _schoolDetails.Result.Where(s => s.SchoolId == id).First();
            UpdateSchoolDto detailSchool = SchoolAutoMapper.Mapper().Map<UpdateSchoolDto>(school);
            detailSchool.SchoolName = "Test Modified";
            var result = _schoolController.Put(school.SchoolId, detailSchool);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        [TestCase(-1)]
        public void PutSchoolByID_ReturnsHttpBadResult(int id)
        {
            School school = _schoolDetails.Result.Where(s => s.SchoolId == 1).First();
            UpdateSchoolDto detailSchool = SchoolAutoMapper.Mapper().Map<UpdateSchoolDto>(school);
            detailSchool.SchoolName = "Test Modified";
            var result = _schoolController.Put(id, detailSchool);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void PostSchool_ReturnsHttpOkResult()
        {
            School school = _schoolDetails.Result.Where(s => s.SchoolId == 1).First();
            CreateSchoolDto detailSchool = SchoolAutoMapper.Mapper().Map<CreateSchoolDto>(school);
            detailSchool.SchoolName = "Test Modified";
            var result = _schoolController.Post(detailSchool);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void PostSchool_ReturnsHttpBadResult()
        {
            CreateSchoolDto detailSchool = null;
            var result = _schoolController.Post(detailSchool);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
