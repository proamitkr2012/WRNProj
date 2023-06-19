using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionModel
{
    public  class Colleges_Course: Faculties
    {
        public int IsNEP { get; set; } = 0;
        public string CCode { get; set; }   
        public string Name{ get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Seat { get; set; }
    }




}
