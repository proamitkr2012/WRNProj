

using AdmissionData.Entities;
using AdmissionModel;

namespace AdmissionRepo
{
    public interface IstudentPreRepo : IGenericRepository<StudentMasters>
    {
        public Task<StudentPreRegistratiuon> GetByPreIdAsync(string id);
        public Task<StudentPreRegistratiuon> GetByMobileAsync(string mobileNo);
        public Task<int> IsMobileExistsAsync(string mobileNo);
        public Task<sqlResponse> VerifyOTP_InsertStudentData(StudentMasters studentMasters);
        public Task<int> ChangePassword(StudentMasters studentMasters);
        public Task<StudentMasters> CheckAuthuntication (StudentMasters studentMasters);
        public Task<int> IsAadharExists(string aadhar);

        Task<int> UploadDocData(string appno, string path);

        Task<StudentMasters> GetByMobileNoAsync(string mobileNo);
    }  
}
