using BAL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class MenuDataController : Controller
    {
        public ActionResult GetMenuList()
        {
            //List<BAL.Models.MenuList> objMenuList = new LoginBAL().GetMenuDetails(Convert.ToInt32(Session["OFFICE_TYPE_CD"]));
            return PartialView("GetMenuList");
        }
    }
}