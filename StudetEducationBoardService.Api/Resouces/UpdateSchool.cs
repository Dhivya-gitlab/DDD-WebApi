using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Api.AppModels
{
    public class UpdateSchool
    {
        [Required]
        public int SchoolId { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string SchoolName { get; set; }

        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }
    }
}
