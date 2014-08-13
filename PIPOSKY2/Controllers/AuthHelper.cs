using PIPOSKY2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Specialized;
using PIPOSKY2.FormModels;

namespace PIPOSKY2.AuthHelper
{
    public class CheckinLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["User"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/User/Login");
            }
        }
    }

    public class CheckinLogOffAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["User"] != null)
            {
                filterContext.HttpContext.Response.Redirect("/User/info");
            }
        }
    }

    public class CheckAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["User"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/User/Login");
            }
            User tmp = filterContext.HttpContext.Session["User"] as User;
            if (tmp.UserType != "admin")
            {
                filterContext.HttpContext.Response.Redirect("/User/info");
            }
        }
    }

    public class CheckAdminOrEditorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["User"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/User/Login");
            }
            User tmp = filterContext.HttpContext.Session["User"] as User;
            if ((tmp.UserType != "admin") && (tmp.UserType != "editor"))
            {
                filterContext.HttpContext.Response.Redirect("/User/info");
            }
        }
    } 



}