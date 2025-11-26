using FrameworkApp.Models;
using FrameworkApp.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrameworkApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        public string Login(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            Boolean success = securityService.Authenticate(userModel);
            if (success)
            {
                return "Result : Success Login";
            }
            return "Result : Fail Login";
        }
    }
}