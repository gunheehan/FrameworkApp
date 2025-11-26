using FrameworkApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameworkApp.Services.Data
{
    public class SecurityDAO
    {
        internal bool FindByUser(UserModel user)
        {
            return user.Username == "Admin" && user.Password == "12345";
        }
    }
}