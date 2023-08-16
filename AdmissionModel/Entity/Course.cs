using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionModel 
{
    public class Course 
    {
     public int CourseId { get;set; }
     public string CourseName { get; set; }
     public  string CourseType { get; set; }
     public int CourseTypeId { get; set; }
     public int IsNEP { get; set; }
     public string CourseCode { get; set; }
     public bool IsProfessional { get; set; }
    }
}
