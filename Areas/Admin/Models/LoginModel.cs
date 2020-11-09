using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLMS.Areas.Admin.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage ="Mời nhập tên tài khoản:")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời nhập mật khẩu:")]
        public string PassWord { set; get; }

        public bool RememberMe{ set; get; }
    }
}