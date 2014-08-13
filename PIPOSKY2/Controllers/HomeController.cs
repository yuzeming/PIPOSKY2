using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIPOSKY2.Models;
using PIPOSKY2.FormModels;
using PIPOSKY2.AuthHelper;

namespace PIPOSKY2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "User");
            else
                return View();
        }
    }
}
