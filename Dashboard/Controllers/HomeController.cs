using BAL.HomeBAL;
using BAL.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using DAL;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        
        ChartModel ReturnResult = new ChartModel();
        public ActionResult DashBoard()      
        {
            @ViewBag.ReportName = "";
            if (Session["LoginId"] != null)
            {
                HomeBAL objDashboardBAL = new HomeBAL();
                ViewBag.YearList= objDashboardBAL.getYearList();
                ViewBag.MonthList = objDashboardBAL.getMonthList();
                TempData["ReturnResult"] = objDashboardBAL.GetDashboardList();
                objDashboardBAL = null;
                if (TempData["ReturnResult"] != null)
                {
                    fillChartValue(2024);
                }
            }
            else
                return RedirectToAction("Login", "Login");
            return View(ReturnResult);
        }
        
        public void fillChartValue(int Year)
        {
            if (TempData["ReturnResult"] != null)
            {
                HomeBAL objDashboardBAL = new HomeBAL();
                ReturnResult = (ChartModel)TempData["ReturnResult"];
                ReturnResult.JsonLabel.Add(new JavaScriptSerializer().Serialize(objDashboardBAL.getDataJson(ReturnResult.objDashboardMonth, 1)));
                ReturnResult.JsonData.Add(new JavaScriptSerializer().Serialize(objDashboardBAL.getDataJson(ReturnResult.objDashboardMonth, 2)));

                ReturnResult.JsonLabel.Add(new JavaScriptSerializer().Serialize(objDashboardBAL.getDataJson(ReturnResult.objDashboardYear, 3)));
                ReturnResult.JsonData.Add(new JavaScriptSerializer().Serialize(objDashboardBAL.getDataJson(ReturnResult.objDashboardYear, 4)));

                objDashboardBAL = null;
            }
        }

        

    }

}