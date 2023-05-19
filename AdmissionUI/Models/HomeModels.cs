using System.ComponentModel.DataAnnotations;

namespace AdmissionUI.Models
{
	public class HomeModels
	{
		public int StateId { get; set; }	
		public int CityId { get; set; }
        public int  CatId { get; set; }
		public int ServiceId { get; set; }
        public string Aadhar { get; set; }

    }
}
