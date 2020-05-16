using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services;

namespace SchoolManagementSystem.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolService _schoolService;
        private readonly ServiceBusSender _serviceBusSender;
        public List<SchoolDetailsViewModel> SchoolDetailsList { get; private set; }
        public SchoolController(ISchoolService schoolService, ServiceBusSender serviceSender)
        {
            _schoolService = schoolService;
            _serviceBusSender = serviceSender;
        }

        public async void OnGetAsync()
        {
            SchoolDetailsList = await _schoolService.GetSchoolListAsync();
        }
        // GET: School
        public ActionResult Index()
        {
            try
            {
                OnGetAsync();
            }
            catch (Exception)
            {
                SchoolDetailsList = Enumerable.Empty<SchoolDetailsViewModel>().ToList();
                ModelState.AddModelError(string.Empty, "Server error.");
            }

            return View(SchoolDetailsList);
        }

        // GET: School/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: School/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: School/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSchoolViewModel schoolModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // _schoolService.CreateSchoolAsync(schoolModel);

                    // Send this to the bus for the other services
                    await _serviceBusSender.SendMessage(new SchoolDetailsViewModel
                    {
                        SchoolName = schoolModel.SchoolName,
                        Country = schoolModel.Country,
                        CommunicationLanguage = schoolModel.CommunicationLanguage,
                        Program = schoolModel.Program,
                        User = schoolModel.User,
                        AssessmentPeriod = schoolModel.AssessmentPeriod
                    });
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error.");

                return View(schoolModel);
            }
            catch
            {
                return View(schoolModel);
            }
        }

        // GET: School/Edit/5
        public ActionResult Edit(int id)
        {
            var schoolDetails = _schoolService.GetSchoolAsync(id);
            return View(schoolDetails.Result);
        }

        // POST: School/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                UpdateSchoolViewModel schoolToUpdate = new UpdateSchoolViewModel();
                schoolToUpdate.SchoolId = id;
                schoolToUpdate.SchoolName = collection["SchoolName"];
                schoolToUpdate.AssessmentPeriod = collection["AssessmentPeriod"];
                schoolToUpdate.CommunicationLanguage = collection["CommunicationLanguage"];
                schoolToUpdate.Country = collection["Country"];
                schoolToUpdate.Program = collection["Program"];
                schoolToUpdate.User = collection["User"];
                _schoolService.UpdateSchoolAsync(schoolToUpdate);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: School/Delete/5
        public ActionResult Delete(int id)
        {
            var deleteSchool = _schoolService.GetSchoolAsync(id);
            return View(deleteSchool.Result);
        }

        // POST: School/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _schoolService.DeleteSchoolAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}