using AdmissionData.Entities;
using AdmissionModel;

namespace AdmissionRepo 
{
    public interface IAdminDashBoardRepo  
    {
        public Task<DashBoardEntityCount>RegisterationDataCountReport();
        public Task<DashBoardEntityCount>BedRegisterationDataCountReport();
		public Task<IEnumerable <StudentAllData>>TotalStudentList(SearchStudent searchStudent);
        public Task<IEnumerable<StudentAllData>>RegisteredStudentList(SearchStudent searchStudent);
		public Task<IEnumerable<StudentAllData>>Student_Course_Fees_List(SearchStudent searchStudent);
	}
}