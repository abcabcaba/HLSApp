using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
   public class Login
    {
        [Required]
        public string LoginID { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleID { get; set; }
    }

    public class MenuList
    {
        public Int32 REPORT_SRNO { get; set; }
        public string RHREF { get; set; }
        public string RANME { get; set; }
        public Int32 ROLEID { get; set; }
        public Int32 P_MENU { get; set; }
        public Int32 P_MENU_CD { get; set; }
    }
}
