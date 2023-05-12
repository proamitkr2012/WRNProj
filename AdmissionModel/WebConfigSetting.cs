using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace AdmissionModel
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Config;
        public static void Initialize(IConfiguration Configuration)
        {
            Config = Configuration;
        }
    }
    public class WebConfigSetting
    {
        public static string BaseURL { get { return ConfigurationManager.AppSettings["baseUrl"].ToString(); } }

        public static string SmsUrl { get { return ConfigurationManager.AppSettings["SmsUrl"].ToString(); } }
        public static string SmsUser { get { return ConfigurationManager.AppSettings["SmsUser"].ToString(); } }
        public static string SmsKey { get { return ConfigurationManager.AppSettings["SmsKey"].ToString(); } }
        public static string SmsSender { get { return ConfigurationManager.AppSettings["SmsSender"].ToString(); } }

        public static string MailHost { get { return ConfigurationManager.AppSettings["mailHost"].ToString(); } }
        public static string PortNo { get { return ConfigurationManager.AppSettings["port"].ToString(); } }
        public static string FromEmail { get { return ConfigurationManager.AppSettings["fromEmail"].ToString(); } }
        public static string UserName { get { return ConfigurationManager.AppSettings["username"].ToString(); } }
        public static string Password { get { return ConfigurationManager.AppSettings["password"].ToString(); } }
        public static string ReplyEmail { get { return ConfigurationManager.AppSettings["ReplyEmail"].ToString(); } }

        public static string GEmailFrom { get { return ConfigurationManager.AppSettings["GEmailFrom"].ToString(); } }
        public static string GEmailUsername { get { return ConfigurationManager.AppSettings["GEmailUsername"].ToString(); } }
        public static string GEmailPassword { get { return ConfigurationManager.AppSettings["GEmailPassword"].ToString(); } }

        public static string DbConnection { get { return ConfigurationManager.AppSettings["DefaultConnectionMGKVP"].ToString(); } }
        public static string DbConnectionAdm { get { return ConfigurationManager.AppSettings["DefaultConnectionMGKVPAdm"].ToString(); } }
        public static string DefaultConnectionPhd { get { return ConfigurationManager.AppSettings["DefaultConnectionPhd"].ToString(); } }
        public static string DbConnection1 { get { return ConfigurationManager.AppSettings["DbConnection1"].ToString(); } }
        public static string univCode { get { return ConfigurationManager.AppSettings["univCode"].ToString(); } }


        public static string PhotoPath { get { return ConfigurationManager.AppSettings["PhotoPath"].ToString(); } }
        public static string SignPath { get { return ConfigurationManager.AppSettings["SignPath"].ToString(); } }
        public static string DocPath { get { return ConfigurationManager.AppSettings["DocPath"].ToString(); } }
        public static string ImgCloudPath { get { return ConfigurationManager.AppSettings["ImgCloudPath"].ToString(); } }

        public static string univName { get { return ConfigurationManager.AppSettings["univName"].ToString(); } }
        public static string univAddress { get { return ConfigurationManager.AppSettings["univAddress"].ToString(); } }

        ///////////////////
        public static string MERCHANT_KEY { get { return ConfigurationManager.AppSettings["MERCHANT_KEY"].ToString(); } }
        public static string SALT { get { return ConfigurationManager.AppSettings["SALT"].ToString(); } }
        public static string Gateway_URL { get { return ConfigurationManager.AppSettings["Gateway_URL"].ToString(); } }
        public static string action { get { return ConfigurationManager.AppSettings["action"].ToString(); } }
        public static string surl { get { return ConfigurationManager.AppSettings["surl"].ToString(); } }
        public static string furl { get { return ConfigurationManager.AppSettings["furl"].ToString(); } }
        public static string curl { get { return ConfigurationManager.AppSettings["curl"].ToString(); } }
        public static string hashSequence { get { return ConfigurationManager.AppSettings["hashSequence"].ToString(); } }
        public static string scurl { get { return ConfigurationManager.AppSettings["scurl"].ToString(); } }


    }
}
