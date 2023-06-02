namespace AdmissionUI.Models
{
    public class ResponseModel
    {
        public int Status { get; set; } = 0;
        public string Msg { get; set; } = "";
		public string Roles { get; set; } = "";
		public int isPaidCourseFees { get;set; }
    }
}
