using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayBook.Content
{
    public static class ConstHelper
    {
        public static readonly string ADMINROLE = "admin";

        public static readonly string USERROLE = "user";

        public static readonly string RegistrationLinkTemplate = "/Account/Register?tokenId="; 
    }
}