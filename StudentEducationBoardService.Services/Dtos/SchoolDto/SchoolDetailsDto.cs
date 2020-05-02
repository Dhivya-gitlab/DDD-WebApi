using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Services.Dtos.SchoolDto
{
   public class SchoolDetailsDto
    {
        public int SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }
    }
}
