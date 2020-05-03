using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Api.AppModels
{
    public class SchoolDetails
    {
        public int SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }
    }
}
