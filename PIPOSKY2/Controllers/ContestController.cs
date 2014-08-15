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
using PIPOSKY2.AuthHelper;

namespace PIPOSKY2.Controllers
{
    public class ContestController : Controller
    {
        PIPOSKY2DbContext db = new PIPOSKY2DbContext();
        public ActionResult Index()
        {
            return View(db.Contests.ToList());
        }

        public ActionResult Details(int id)
        {
            var tmp = db.Contests.Find(id);
            if (tmp == null) return HttpNotFound();
            int? uid = Session["_UserID"] as int? ?? -1;
            if (uid != -1)
            {
                var x = db.Record.Single(m => m.Belong.ContestID == id && m.User.UserID == uid);
                ViewBag.record = x;
                ViewBag.Details = x.Details.GroupBy(m => m.Prob.ProblemID).Select(m => m.OrderByDescending(m1 => m1.SubmitID).Last()).ToDictionary(m => m.Prob.ProblemID);
            }
            return View(tmp);
        }

        public ActionResult Edit(int? id)
        {
            var tmp =new ContestFormModel();
            tmp.StartTime = System.DateTime.Now;
            tmp.EndTime = System.DateTime.Now.AddHours(4);

            if (id!=null)
            {
                var x = db.Contests.Find(id);
                if (x == null) return HttpNotFound();
                tmp.ContestName = x.ContestName;
                tmp.StartTime = x.StartTime;
                tmp.EndTime = x.EndTime;
                foreach (var i in x.Problmems)
                    tmp.Problems += i.ProblemID + "\n";
                tmp.Users = new List<User>();
                foreach (var i in x.Record)
                    tmp.Users.Add(i.User);
            }
            return View(tmp);
        }

        [HttpPost]
        public ActionResult Edit(int? id, ContestFormModel form)
        {
            
            if (form.EndTime < form.StartTime)
            {
                ModelState.AddModelError("EndTime", "结束时间早于开始时间");
            }
            var tmpUser = new List<User>();
            foreach (var x in form.AddUsers.Split(new char[]{',','\r','\n'},StringSplitOptions.RemoveEmptyEntries))
            {
                int uid;
                var t =Int32.TryParse(x,out uid) ? db.Users.Find(uid) : null;
                if (t!=null)
                    tmpUser.Add(t);
                else
                    ModelState.AddModelError("Users","没有找到用户"+x);
            }
            var tmpProb = new List<Problem>();
            foreach (var x in form.Problems.Split(new char[] { ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int uid;
                var t =Int32.TryParse(x,out uid) ? db.Problems.Find(uid) : null;
                if (t!=null)
                    tmpProb.Add(t);
                else
                    ModelState.AddModelError("Problems","没有找到题目"+x);
            }
            if (ModelState.IsValid)
            {
                var tmp = id!=null ? db.Contests.Find(id) : new Contest();
                tmp.ContestName = form.ContestName;
                tmp.StartTime = form.StartTime;
                tmp.EndTime = form.EndTime;
                tmp.Problmems = tmpProb;
                db.Contests.AddOrUpdate(tmp);
                foreach (var u in tmpUser)
                {
                    if (!db.Record.Any(x => x.User.UserID == u.UserID && x.Belong.ContestID == tmp.ContestID))
                    {
                        var x = new ContestRecord();
                        x.Belong = tmp;
                        x.User = u;
                        x.Score = 0;
                        db.Record.AddOrUpdate(x);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
