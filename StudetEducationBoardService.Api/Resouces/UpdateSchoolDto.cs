using System.ComponentModel.DataAnnotations;

namespace StudentEducationBoardService.Api.AppModels
{
    public class UpdateSchoolDto
    {
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string Country { get; set; }
         [Required]
        public string CommunicationLanguage { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Program { get; set; }
        [Required]
        public string AssessmentPeriod { get; set; }
    }
}
