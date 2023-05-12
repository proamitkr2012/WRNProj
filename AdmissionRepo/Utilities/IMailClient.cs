using AdmissionModel;
using AdmissionData.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace AdmissionRepo.Utilities
{
    public interface IMailClient
    {
       // bool SendMail_UserDetail(LoginMasterModel model);
       // bool SendMail_UserDetailTeacher(LoginMasterModel model);
        
        //bool SendMail_Subscription(string email, string _subscribeUrl);


        bool SendMail(string to, string subject, string body);
        bool SendMulpMail(string to, string subject, string body);
        

    }
}