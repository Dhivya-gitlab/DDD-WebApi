using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StudentEducationBoardService.Api.AppModels
{
    public class SchoolDetailsDto :IEquatable<SchoolDetailsDto>
    {
        public int SchoolId { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Program { get; set; }
        [Required]
        public string AssessmentPeriod { get; set; }

        public bool Equals([AllowNull] SchoolDetailsDto schoolDetail)
        {
            if ((schoolDetail == null) || !this.GetType().Equals(schoolDetail.GetType()))
                { return false; }
            else
            {
                return (SchoolId == schoolDetail.SchoolId) && (SchoolName == schoolDetail.SchoolName)
                    && (Country == schoolDetail.Country) && (CommunicationLanguage == schoolDetail.CommunicationLanguage)
                    && (User == schoolDetail.User) && (Program == schoolDetail.Program) && (AssessmentPeriod == schoolDetail.AssessmentPeriod);
            }
        }
    }
}
