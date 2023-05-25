using System.Security.Policy;

namespace AdmissionModel
{
    public  class StudentWeightage
    {
        public string ApplicationNo { get; set; }
        public string Name { get; set; }
        public int  ID { get; set; }
        public int  StdWeightID { get; set; }
        public bool ctrlWeightage { get; set; }
        public int Weight { get; set; }

       public int  ParentId { get; set; }
    }
}
