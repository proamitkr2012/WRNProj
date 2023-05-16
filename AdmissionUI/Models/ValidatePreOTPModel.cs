namespace AdmissionUI.Models
{
    public class ValidatePreOTPModel:ResponseModel
    {
        public string OTP { get; set; }
        public string  InputOTP{ get; set; }
        public string EncriptData { get; set;}
        public string Mobile{ get; set; }
       
    }
}
