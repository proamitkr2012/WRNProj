using AdmissionData.Entities;
using AdmissionModel;

namespace AdmissionUI.Models
{
	public class SearchStudentModel:ResponseModel
	{
		public string SearchText { get; set; } = "";
        public int CourseTypeId { get; set; } = 0;
		public int CourseID { get; set; } = 0;
		public int PgIndex { get; set; } = 1;
        public int PgSize { get; set; } = 50;
	    public List<StudentAllData> studentList { get; set; }
        public List<DashBoardEntityCount> listcourseWise { get; set; }
    }
}
