using BAL.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BAL.HomeBAL
{
    public class HomeBAL
    {
        private static DBManager GetConnection() => new DBManager("connstr");
        public ChartModel GetDashboardList()
        {
            ChartModel ReturnResult = new ChartModel();
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[1];
                DataSet ds = _objDBManager.GetDataSet("Dashboard_REG_COUNT", CommandType.StoredProcedure, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HomeModel result = new HomeModel();
                    result.Name = ds.Tables[0].Rows[i]["SRNo"].ToString();
                    result.Count = Convert.ToInt64(ds.Tables[0].Rows[i]["TotalCount"]);
                    ReturnResult.objDashboardYear.Add(result);                    
                }
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    HomeModel result = new HomeModel();
                    result.Name = ds.Tables[1].Rows[i]["FieldName"].ToString();
                    result.Count = Convert.ToInt64(ds.Tables[1].Rows[i]["TotalCount"]);
                    ReturnResult.objDashboardMonth.Add(result);
                }
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    ReturnResult.MaxValue = Convert.ToInt64(ds.Tables[2].Rows[i]["maxvalue"]) + 2;
                }
            }
            catch (Exception e)
            {

            }
            return ReturnResult;
        }


        public List<string> getDataJson(List<HomeModel> objList, int type)
        {
            List<string> ReturnResult = new List<string>();
            try
            {
                foreach (HomeModel obj in objList)
                {
                    if (type == 1)
                        ReturnResult.Add(obj.Name.ToString());
                    if (type == 2)
                        ReturnResult.Add(obj.Count.ToString());
                    if (type == 3)
                        ReturnResult.Add(obj.Name.ToString());
                    if (type == 4)
                        ReturnResult.Add(obj.Count.ToString());
                }
            }
            catch (Exception e)
            {

            }
            return ReturnResult;
        }
        public List<string> getPieDataJsonLevel(List<HomeModel> objList, int type)
        {
            List<string> ReturnResult = new List<string>();
            try
            {
                foreach (HomeModel obj in objList)
                {
                    if (type == 1)
                        ReturnResult.Add(obj.Name.ToString());
                    if (type == 2)
                        ReturnResult.Add(obj.Count.ToString());
                }
            }
            catch (Exception e)
            {

            }
            return ReturnResult;
        }
        public List<SelectListItem> getYearList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            int CurrentYear = DateTime.Now.Year;
            for (int i = 5; i >= 0; i--)
            {
                items.Add(new SelectListItem
                {
                    Text = (CurrentYear-i).ToString(),
                    Value = (CurrentYear - i).ToString()
                });
            }
            return items;
        }
        public List<SelectListItem> getMonthList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = Convert.ToInt32(DateTime.Now.ToString("MM"))-1; i >=0 ; i--)
            {
                items.Add(new SelectListItem
                {
                    Text = DateTime.Today.AddMonths(-i).ToString("MMMM"),
                    Value = DateTime.Today.AddMonths(-i).ToString("MMMM")
                });
            }
            return items;
        }

    }
}
