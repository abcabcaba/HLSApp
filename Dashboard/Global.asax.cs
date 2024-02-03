using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DAL;

namespace Dashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static DBManager GetConnection() => new DBManager("connstr");
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Session_Start(object sender, EventArgs e)
        {
            SetAllMasterList();
        }
        public void SetAllMasterList()
        {
            DataSet ReturnResult = new DataSet();
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[0];
                ReturnResult = _objDBManager.GetDataSet("UDP_GET_MASTER", CommandType.StoredProcedure, sqlParameters);
                HttpContext.Current.Session["MenuTable"] = ReturnResult;
            }
            catch (Exception e)
            {

            }
        }
    }
}
