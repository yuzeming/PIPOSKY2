using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Mvc;
using PIPOSKY2.Models;
using PIPOSKY2.FormModels;
using PIPOSKY2.AuthHelper;

namespace PIPOSKY2.Controllers
{
    public class JudgeController : ApiController
    {

        PIPOSKY2DbContext db = new PIPOSKY2DbContext();

        // GET api/judge
        public SubmitApiModels Get()
        {
            var ret = new SubmitApiModels();
            var tmp = db.Submits.FirstOrDefault(_ => _.State == SubmitState.Waiting);
            if (tmp == null)
                ret.SubmitID = 0;
            else
            {
                ret.SubmitID = tmp.SubmitID;
                ret.ProbID = tmp.Prob.ProblemID;
                ret.Source = tmp.Source;
                ret.Lang = tmp.Lang;
                tmp.State = SubmitState.Running;
				db.Submits.AddOrUpdate(tmp);
                SubmitController.UpdateSubmitState(db, tmp.Record);
	            db.SaveChanges();
            }
            return ret;
        }

        // POST api/judge
	    public void Post([FromBody] SubmitResultApiModels value)
	    {
		    var tmp = db.Submits.Find(value.SubmitID);
		    if (tmp != null)
		    {
			    tmp.Result = value.Result;
			    tmp.State = value.State;
                tmp.Score = value.Score;
                tmp.CompilerRes = value.CompilerRes;
			    db.Submits.AddOrUpdate(tmp);
                SubmitController.UpdateSubmitState(db, tmp.Record);
			    db.SaveChanges();
		    }
	    }

    }
}
