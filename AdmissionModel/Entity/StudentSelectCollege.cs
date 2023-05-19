using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionModel.Entity
{
    public class StudentSelectCollege: Colleges_Course
    {
        public string ApplicationNo { get; set; }
        public long  SRNO { get; set; }

        public List<StudentSelectCollege>studentSelectColleges { get; set; }
    }
}
