using System.ComponentModel.DataAnnotations;

namespace StudentEducationBoardService.Api.AppModels
{
    public class CreateSchoolDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string SchoolName { get; set; }

        [Required]
        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }

        public string User { get; set; }

        public string Program { get; set; }

        public string AssessmentPeriod { get; set; }
    }
}
