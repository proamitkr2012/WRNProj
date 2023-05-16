using AdmissionData;
using AdmissionData.Entities;
using AdmissionModel;
using AdmissionModel.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdmissionRepo
{
    public interface IAdminRepository : IRepository<USR_INF>
    {
         AdminMasterDTO AuthenticateAdmin(string userName, string password);
        StudentMastersDTO CheckStudentPreData(LoginDTO data);
        List<tblState> GetStateList();
        List<tblDistrict> GetDistrictList(int Id);
        StudentMastersDTO CheckBedStudentDATA(string Enrollment);
        Task<int> AddRegistration(StudentMastersDTO model);
        List<DocSetting> GetDocRequiredList(int DegreeID);
        List<QualifationMasters> GetQualificationList(string Roll);
        Task<bool> UpdateQualification(QualifationMasters model);
        bool UploadDocData(StudentDocUploaded data); StudentMastersDTO GetStudentPreData(string Enrollment);
        StudentMastersDTO GetStudentMasterData(string Enrollment);

        List<CollegeMasters> GetCollegeList();
        Task<int> AddPreRegistration(StudentMastersDTO model);
    }
}
