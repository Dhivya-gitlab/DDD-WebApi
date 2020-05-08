using System;
using System.Diagnostics.CodeAnalysis;

namespace StudentEducationBoardService.Api.AppModels
{
    public class SchoolDetailsDto :IEquatable<SchoolDetailsDto>
    {
        public int SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }
        public string User { get; set; }

        public string Program { get; set; }

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
