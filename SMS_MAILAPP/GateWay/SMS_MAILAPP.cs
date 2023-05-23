﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace SMS_MAIL.GateWay
{
    public class SMS_MAILAPP : ISMS_MAIL
    {

        private readonly IConfiguration _config;
        private readonly string uname;
        private readonly string password;
        private readonly string senderid;
        private string smsapi;
        private readonly string peid;

        private readonly ILogger<SMS_MAILAPP> _logger;
        private readonly IConfigurationBuilder configurationBuilder;
        public SMS_MAILAPP(ILogger<SMS_MAILAPP> logger, IConfiguration config )
        {
            _config = config;
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
        }





        public async   Task<int>  sendMail(string emailId, string msg)
        {
            try
            {
                string _username = configurationBuilder.Build().GetSection("EMAIL:UserName").Value;
                string _password = configurationBuilder.Build().GetSection("EMAIL:Password").Value;
                MailMessage mm = new MailMessage();
                string _fromEmail = configurationBuilder.Build().GetSection("EMAIL:FromMail").Value;
                mm.From = new MailAddress(_fromEmail);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = configurationBuilder.Build().GetSection("EMAIL:Host").Value;
                smtp.EnableSsl = true;
                smtp.Port =Convert.ToInt32(configurationBuilder.Build().GetSection("EMAIL:Port").Value);
                
                NetworkCredential NetworkCred = new NetworkCredential(_username, _password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Send(mm);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
                return -1;
            }


        }

        public Task<int>  sendSMS(string mobile, string msg, string tempID)
        {
            try 
            {
                string uname = configurationBuilder.Build().GetSection("SMS:SMSUSERNAME").Value;
                string password = configurationBuilder.Build().GetSection("SMS:SMSUSERPWD").Value;
                string senderid = configurationBuilder.Build().GetSection("SMS:SMSSENDER").Value;
                string smsapi = configurationBuilder.Build().GetSection("SMS:SMSAPI").Value;
                string peid = configurationBuilder.Build().GetSection("SMS:PEID").Value;



                msg = msg + senderid;
                smsapi = smsapi.Replace("[AND]", "&");
                smsapi = smsapi.Replace("[USERNAME]", uname);
                smsapi = smsapi.Replace("[PWD]", password);
                smsapi = smsapi.Replace("[SENDERID]", senderid);
                smsapi = smsapi.Replace("[MOBILE]", mobile);
                smsapi = smsapi.Replace("[MESSAGE]", msg);
                smsapi = smsapi.Replace("[TempateId]", tempID);
                smsapi = smsapi.Replace("[PED]", peid);
                //"http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=" + uname + "&password=" + password + "&sender=" + senderid + "&to=" + mobileno + "&message=" + message + "&reqid=1&format={json|text}&pe_id=" + PE_ID + "&template_id=1207165466872211158&route_id=39"
                string createdURL = smsapi;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();

                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
            }
            return Task.FromResult(-1);
        }
    }
}