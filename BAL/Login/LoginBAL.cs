using DAL;
using DAL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BAL.Login
{
   public class LoginBAL
    {
        private static DBManager GetConnection() => new DBManager("connstr");
        public int GetLoginDetails(Models.Login obj)
        {
            int roleID = 0;
            try
            {
                string strPassword = string.Empty;
                using (General objGeneral = new General())
                {
                    strPassword = objGeneral.ConvertUserNamePasswordToSHA512(obj.LoginID.Trim(), obj.Password.Trim()).ToString();
                }
                //strPassword = obj.Password.Trim();
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = _objDBManager.CreateParameter("@LOGIN_ID", obj.LoginID, DbType.String);
                sqlParameters[1] = _objDBManager.CreateParameter("@LOGIN_PASSWORD", strPassword, DbType.String);
                DataSet ds = _objDBManager.GetDataSet("UDP_GET_LOGIN_DETAIL_DASHBOARD", CommandType.StoredProcedure, sqlParameters);
                if (ds.Tables[0] != null)
                {
                    HttpContext.Current.Session["USER_SRNO"] = ds.Tables[0].Rows[0]["USER_SRNO"].ToString();
                    HttpContext.Current.Session["LoginId"] = ds.Tables[0].Rows[0]["LOGIN_ID"].ToString();
                    HttpContext.Current.Session["USER_TYPE"] = ds.Tables[0].Rows[0]["USER_TYPE"].ToString();
                    HttpContext.Current.Session["PERSON_FULL_NAME"] = ds.Tables[0].Rows[0]["PERSON_FULL_NAME"].ToString();
                    roleID = 1;

                }
            }
            catch (Exception ex)
            {
            }
            return roleID;
        }
    }
}
