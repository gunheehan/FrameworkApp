using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrameworkApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestView() 
        {
            // return a 'testview.cshtml' view
            return View();
        }

        public string Welcome(string Name, int numTimes = 1)
        {
            return HttpUtility.HtmlEncode($"Hello {Name}, NumTimes is: {numTimes}");
        }

        public string Welcome2(string Name, int ID = 1)
        {
            return HttpUtility.HtmlEncode($"Hello {Name}, ID is: {ID}");
        }

        public ActionResult ErrorMessage()
        {
            return View();
        }

        public string PrintMessage()
        {
            return "<h1>Welcome</h1>";
        }
        // 병합이 되나
        // 같은 라인 테스트
    }
}