namespace AdmissionUI.Models
{
    public class StdWeightageEligbilty:ResponseModel
    {
        public string ApplicationNo { get; set; }
        public string? EncrptedData { get; set; }
        public int Newdata { get; set; } = 1;
        public List<stdWeightageModel>  stdweightageList { get; set; }


    }
}
