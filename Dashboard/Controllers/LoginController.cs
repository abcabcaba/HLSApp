using BAL.Login;
using BAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Dashboard.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.Msg = string.Empty;
            return View(new BAL.Models.Login());
        }

        [HttpPost]
        public ActionResult Login(BAL.Models.Login obj)
        {
            int result = 0;
            ViewBag.Msg = string.Empty;
            result =new LoginBAL().GetLoginDetails(obj);
            if (result == 1)
            {
                if (Session["USER_TYPE"] != null)
                {
                    if (Session["USER_TYPE"].Equals("1"))
                        return RedirectToAction("DashBoard", "Home");
                    else
                        return RedirectToAction("ListOfUser", "UserDetails");
                }
            }
            else
                ViewBag.Msg = "Invalid User Id or Password, Please enter correct credentials.";
            return View(obj);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}