using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using PIPOSKY2.Controllers;

namespace PIPOSKY2.Models
{

    public class PIPOSKY2DbContext : DbContext
    {
        public PIPOSKY2DbContext()
            : base("PIPOSKY2DbContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<User> Users { set; get; }
        public DbSet<Problem> Problems { set; get; }
        public DbSet<Contest> Contests { set; get; }
		public DbSet<Submit> Submits { get; set; }
        public DbSet<ContestRecord> Record { get; set; }

    }

	public class DBInitializer : DropCreateDatabaseIfModelChanges<PIPOSKY2DbContext>
	{
		protected override void Seed(PIPOSKY2DbContext context)
		{
			var user = new User {UserEmail = "test@test.com", UserName = "root", UserPwd = "admin123", UserType = "admin", StudentNumber = ""};
			context.Users.AddOrUpdate(user);
            context.SaveChanges();
            var tmpCont = new Contest();
            var tmpRecord = new ContestRecord();
            tmpRecord.User = user;
            tmpRecord.Belong = tmpCont;
            tmpCont.Record = new List<ContestRecord> {tmpRecord};
            tmpCont.Problmems = new List<Problem>();
            tmpCont.ContestName = "TEST!!";
            tmpCont.StartTime = DateTime.Now;
            tmpCont.EndTime = DateTime.Now.AddDays(1);

            for (var i = 1; i <= 10; ++i)
            {
                var tmp = new Problem { Creator = user, ProblemName = "test " + i.ToString(), Description = "test", Config = "[]", Visible = true, Downloadable = true };
                context.Problems.AddOrUpdate(tmp);
                tmpCont.Problmems.Add(tmp);
                for (var j = 1; j <= i; ++j)
                {
                    var x = new Submit();
                    x.Record = tmpRecord;
                    x.Prob = tmp;
                    x.User = user;
                    x.Source = "aaa";
                    x.State = SubmitState.Waiting;
                    x.Time = DateTime.Now;
                    x.Lang = "cpp";
                    context.Submits.AddOrUpdate(x);
                    
                }
            }
            context.Contests.AddOrUpdate(tmpCont);
            context.Record.AddOrUpdate(tmpRecord);
            context.SaveChanges();
            SubmitController.UpdateSubmitState(context, tmpRecord);
			base.Seed(context);
		}


	}

    public class User
    {
        [Key]
        public int UserID { set; get; }
        [Required]
        public string UserName { set; get; }
        [Required]
        public string UserPwd { set; get; }
        [Required]
        public string UserEmail { set; get; }
        public string StudentNumber { set; get; }
        public string UserType { set; get; }

    }

    public class Problem
    {
        [Key]
        public int ProblemID { set; get; }
        [Required]
        public string ProblemName { set; get; }
        public string ProblemPath { set; get; }
        [Required]
        public bool Visible { set; get; }
        [Required]
        public bool Downloadable { set; get; }
        [Required]
        public virtual User Creator { set; get; }
        [Required]
        public string Description { set; get; }
        public string Solution { set; get; }
        [Required]
        public string Config { set; get; }
    }

    public class Contest
    {
        [Key]
        public int ContestID { set; get; }
        [Required]
        public string ContestName { set; get; }
        [Required]
        public DateTime StartTime { set; get; }
        [Required]
        public DateTime EndTime { set; get; }
        public virtual ICollection<ContestRecord> Record { set; get; }
        public virtual ICollection<Problem> Problmems { set; get; }
    }

    public class ContestRecord
    {
        [Key]
        public int RecordID { set; get; }
        [Required]
        public virtual Contest Belong { get; set; }
        [Required]
        public virtual User User { set; get; }
        public int Score { get; set; }

        public virtual ICollection<Submit> Details { get; set; } 
    }

    public enum SubmitState
    {
        Waiting,
        Running,

        Accepted,
        TimeLimitExceeded,
        MemoryLimitExceeded,
        WrongAnswer,
        RuntimeError,
        OutputLimitExceeded,
        CompileError,
        SystemError,
        ValidatorError,
    }

	public class Submit
	{
		[Key]
		public int SubmitID { get; set; }
		[Required]
		public string Lang { get; set; }
		public virtual Problem Prob { get; set; }
		public virtual User User { get; set; }

        public virtual ContestRecord Record { get; set; }
		[Required]
		public DateTime Time { get; set; }

		[Required]
		public string Source { get; set; }
        public SubmitState State { get; set; }
        public int Score { get; set; }
		public string Result { get; set; } //Json
        public string CompilerRes { get; set; }
	}
}