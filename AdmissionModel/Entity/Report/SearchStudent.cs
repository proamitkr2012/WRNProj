using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionModel 
{
	public  class SearchStudent
	{
		public string SearchText { get; set; } = "";
		public int CourseTypeId { get; set; } = 0;
        public int CourseID { get; set; } = 0;
        public int PgIndex { get; set; } = 1;
		public int PgSize { get; set; } = 50;
	}
}
