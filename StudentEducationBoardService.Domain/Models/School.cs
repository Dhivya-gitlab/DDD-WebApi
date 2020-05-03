﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Domain.Models
{
    public class School
    {
        public int SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string Country { get; set; }

        public string CommunicationLanguage { get; set; }

        public string User { get; set; }

        public string Program { get; set; }

        public string AssessmentPeriod { get; set; }
    }
}
