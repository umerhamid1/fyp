using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentMonitoringSystem.Areas.Security.Controllers
{
    public class RecoverPasswordController : Controller
    {
        // GET: Security/RecoverPassword
        public ActionResult Index()
        {
            return View();
        }
    }
}