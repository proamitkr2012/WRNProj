using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdmissionData.Entities;

namespace AdmissionModel 
{
    public  class StudentAllData:StudentMasters
    {
        public string? CourseTypeName { get; set; }
		public string? Courses { get; set; }
		public string? SabTranId { get; set; }
        public string? Amount { get; set; }
		public string? CRN { get; set; }

	}
}
