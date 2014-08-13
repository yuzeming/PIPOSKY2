using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIPOSKY2.Models;
namespace PIPOSKY2.FormModels
{
    public class LoginFormModel
    {
        [Required]
        [Display(Name = "用户名")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "用户名长度不能大于{1} 且要小于{2}")]
        [RegularExpression(@"^[^\s]*$",
            ErrorMessage = "用户名格式有误，不能有空格")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码不能大于{1} 且要小于{2}")]
        [RegularExpression(@"^[^\s]*$",
            ErrorMessage = "密码格式有误，不能有空格")]
        public string UserPwd { get; set; }
        [Required]
        [Display(Name = "保持登录")]
        public bool KeepLogin { get; set; }
    }

    public class RegFormModel
    {
        [Required]
        [Display(Name = "用户名")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "用户名长度不能大于{1} 且要小于{2}")]
        [RegularExpression(@"^[^\s]*$",
            ErrorMessage = "用户名格式有误，不能有空格")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
		[DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码不能大于{1} 且要小于{2}")]
        [RegularExpression(@"^[^\s]*$",
            ErrorMessage = "密码格式有误，不能有空格")]
        public string UserPwd { get; set; }

        [Required]
        [Display(Name = "确认密码")]
		[DataType(DataType.Password)]
        public string UserPwd2 { get; set; }

        [Required]
        [Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
    }

    public class EditPersonalInfoModel {
        [Required]
        [Display(Name = "用户名")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "用户名长度不能大于{1} 且要小于{2}")]
        [RegularExpression(@"^[^\s]*$",
            ErrorMessage = "用户名格式有误，不能有空格")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
    }

    public class ChangePasswordModel {
        [Required]
        [Display(Name = "原密码")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "新密码")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码不能大于{2} 且要小于{1}")]
        [RegularExpression(@"^[^\s]*$",
            ErrorMessage = "密码格式有误，不能有空格")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class ContestFormModel
    {
        [Required]
        [Display(Name = "比赛名称")]
        public string ContestName { get; set; }
        [Required]
        [Display(Name = "开始时间")]
        public DateTime StartTime { get; set; }
        [Required]
        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }
        [Display(Name = "公开注册")]
        public bool CanReg { get; set; }

        public string Problems { get; set; }

        public List<User> Users { get; set; }

        public string AddUsers { get; set; }
    }

    public class UploadProblemFormModel
    {
        [Required]
        [Display(Name = "题目名称")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "文件路径")]
        public HttpPostedFileBase File{ get; set; }
        [Display(Name = "所有人可见")]
        public bool visible { get; set; }
    }
	
	public class SubmitFormModel
	{
		[Required]
		[Display(Name = "语言")]
		public string Lang { get; set; }

		[Required]
		[Display(Name="题目ID")]
		public int? PID { get; set; }

		[Required]
		[Display(Name="提交代码")]
		public string Source { get; set; }
	}

}