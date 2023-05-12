﻿
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace AdmissionUI.Helpers
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {
        public virtual CustomPrincipalSerializeModel CurrentUser
        {
            get
            {
                if (User.Claims.Count() > 0)
                {
                    string userData = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                    var user = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(userData);
                    //check membership renewal and expiry
                    //if (user.MembershipId == 0 || user.MembershipExpiry.Date <= DateTime.Now.Date)
                    //{
                    //    var membership = UOF.IMembershipSubscription.GetMembershipDetails(user.UserId);
                    //    user.MembershipId = membership.MembershipId;
                    //    user.MembershipExpiry = membership.ExpiryDate;
                    //}
                    return user;
                }
                return null;
            }
        }
    }
}
