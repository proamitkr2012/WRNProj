using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionModel 
{
    public  class Subjects
    {

        public int SubjectId { get; set; }
        public string Subject  { get; set; }

        public int? ChoiceOrder { get; set; }
        public string? CoSubject { get; set; }
        public string? SkillSubject { get; set; }
    }
}
