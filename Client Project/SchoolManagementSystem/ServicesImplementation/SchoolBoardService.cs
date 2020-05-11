using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolManagementSystem.ServicesImplementation
{
    public class SchoolBoardService : ISchoolService
    {
        private readonly HttpClient _httpClient;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not applicable for constructor")]

        public SchoolBoardService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SchoolBoardApi");
        }
        private static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task CreateSchoolAsync(CreateSchoolViewModel createSchoolViewModel)
        {
            var schoolPostTask = await _httpClient.PostAsJsonAsync<CreateSchoolViewModel>("school", createSchoolViewModel);

            if (!schoolPostTask.IsSuccessStatusCode)
            {
                throw new ApplicationException($"{schoolPostTask.ReasonPhrase}: The status code is: {(int)schoolPostTask.StatusCode}");
            }
        }

        public async Task DeleteSchoolAsync(int schoolId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"school/{schoolId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"{response.ReasonPhrase}: The status code is: {(int)response.StatusCode}");
            }
        }

        public async Task<SchoolDetailsViewModel> GetSchoolAsync(int schoolId)
        {
            var response = _httpClient.GetAsync($"school/{schoolId}");
            response.Wait();

            var requestedSchool = response.Result;

            if (!requestedSchool.IsSuccessStatusCode)
            {
                throw new ApplicationException($"{requestedSchool.ReasonPhrase}: The status code is: {(int)requestedSchool.StatusCode}");
            }
            return await requestedSchool.Content.ReadAsAsync<SchoolDetailsViewModel>();
        }

        public async Task<List<SchoolDetailsViewModel>> GetSchoolListAsync()
        {
            //Http get call
            var apiResponseTask = _httpClient.GetAsync("school");
            apiResponseTask.Wait();

            var schoolApiResult = apiResponseTask.Result;

            if (schoolApiResult.IsSuccessStatusCode)
            {
                var readSchoolDetailsTask = await schoolApiResult.Content.ReadAsAsync<List<SchoolDetailsViewModel>>();

                return readSchoolDetailsTask;
            }
            else //web api sent an error message
            {
                throw new ApplicationException($"{schoolApiResult.ReasonPhrase}: The status code is: {(int)schoolApiResult.StatusCode}");
            }

        }

        public async Task UpdateSchoolAsync(UpdateSchoolViewModel updateDepartmentViewModel)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<UpdateSchoolViewModel>($"school/{updateDepartmentViewModel.SchoolId}",updateDepartmentViewModel);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"{response.ReasonPhrase}: The status code is: {(int)response.StatusCode}");
            }
        }
    }
}
