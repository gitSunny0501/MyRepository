using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DapperDemo.Models
{
    public class User
    {
        [Display(Name = "编号")]
        public int AutoId { set; get; }

        [Display(Name = "用户ID")]
        [Required(ErrorMessage ="不能为空")]
        public string UserId { set; get; }

        [Display(Name = "密码")]
        [Required(ErrorMessage ="不能为空")]
        public string UserPwd { set; get; }
    }
}