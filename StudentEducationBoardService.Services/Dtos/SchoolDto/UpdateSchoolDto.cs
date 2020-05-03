using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Services.Dtos.SchoolDto
{
    public class UpdateSchoolDto
    {
        public int SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }
    }
}
