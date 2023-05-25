using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionModel 
{
    public  class StudentAppliedCourse:Course
    {

     public long STD_COUSEID { get;set; }
     public string ApllicationNo { get;set; }
     public string CRN { get;set; }
     public int IsPaid { get;set; }
     public string PaidON { get;set; }
     public string TransactionID { get;set; }
     public string CCODE { get;set; }
     public double Fees { get; set; }
     public bool IsFor_Paid { get;set; }
     public bool Status { get; set; }
     public string SabTranId { get; set; }
    }
}
