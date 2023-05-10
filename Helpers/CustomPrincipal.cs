using System;
using System.Linq;
using System.Security.Principal;

namespace AdmissionUI.Helpers
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string _role)
        {
            string[] roleArray = _role.Split(',');
            foreach (string role in roleArray)
            {
                if (Roles.Any(r => r.Contains(role.Trim())))
                {
                    return true;
                }
            }
            return false;
        }

        //public CustomPrincipal(string Username)
        //{
        //    this.Identity = new GenericIdentity(Username);
        //}

        public int UserId { get; set; }
        public string UserLoginId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string[] Roles { get; set; }
        public string ProfilePic { get; set; }
        public int LoginStatus { get; set; }
        public bool IsPaid { get; set; }

        ///

    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string UserLoginId { get; set; }
        
        public string Name { get; set; }        
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string[] Roles { get; set; }
        public string ProfilePic { get; set; }
        public int LoginStatus { get; set; }
        public bool IsPaid { get; set; }
        
    }
}