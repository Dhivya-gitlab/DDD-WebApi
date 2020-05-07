using System.ComponentModel.DataAnnotations;

namespace StudentEducationBoardService.Api.AppModels
{
    public class UpdateSchoolDto
    {
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string SchoolName { get; set; }

        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }
        public string User { get; set; }

        public string Program { get; set; }

        public string AssessmentPeriod { get; set; }
    }
}
