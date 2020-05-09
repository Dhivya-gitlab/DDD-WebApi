using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentEducationBoardService.Domain.Models
{
    public class School
    {
        public int SchoolId { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
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
    }
}
