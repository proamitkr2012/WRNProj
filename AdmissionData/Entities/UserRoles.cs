using System;
using System.Collections.Generic;
using System.Text;

namespace AdmissionData.Entities
{
    public class UserRoles
    {
        public long StudId { get; set; }
        public int RoleId { get; set; }
        public virtual CollegeUsers Users { get; set; }
        public virtual Role Role { get; set; }
    }

    public class AdminMasterRoles
    {
        public int AdminId { get; set; }
        public int RoleId { get; set; }
        public virtual AdminMaster Admin { get; set; }
        public virtual Role Role { get; set; }
    }

}
