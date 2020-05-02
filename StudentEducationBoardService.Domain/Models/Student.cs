using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Domain.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public char Gender { get; set; }

        public string Citizenship { get; set; }

        public string Countries { get; set; }

        public string Languages {get;set;}

        public School School { get; set; }

        //public Program Program { get; set; }
    }
}
