using AdmissionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionUI.Models
{
    public class StudentSelectSubjectModel :ResponseModel
    {
        public string Ccode { get; set; }
        public int CourseID { get; set; }
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public int IsNewData { get; set; } = 1;
        public List<StudentAppliedCollegesSubject> stdapplycolsubList { get; set; }
    }
}
