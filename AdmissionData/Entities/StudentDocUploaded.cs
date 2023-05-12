using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdmissionData.Entities
{
    public class StudentDocUploaded
    {
        [Key]
        public long EntryID { get; set; }
        public int DocID { get; set; }   
        public string Roll { get; set; }   
        public string? FilePath { get; set; }   
        public string? VirtualFilePath { get; set; }   
        public bool IsActive { get; set; }   
        public DateTime? CreateDate { get; set; }
        [NotMapped]
        public string DocName { get; set; }
        [NotMapped]
        public string DocDisplayName { get; set; }

    }
}
