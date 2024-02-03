
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAL.CommonClass
{
    public class CommonDataFliter
    {
        private static DBManager GetConnection() => new DBManager("connstr");
        public List<SelectListItem> GetDistrictList(int STATE_CD)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            DataSet ReturnResult = new DataSet();
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = _objDBManager.CreateParameter("@STATE_CD", STATE_CD, DbType.Int32);
                ReturnResult = _objDBManager.GetDataSet("UDP_GET_DISTRICT", CommandType.StoredProcedure, sqlParameters);
                if (ReturnResult.Tables.Count > 0)
                {
                    for (int i = 0; ReturnResult.Tables[0].Rows.Count > 0; i++)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = ReturnResult.Tables[0].Rows[i]["DISTRICT"].ToString(),
                            Value = ReturnResult.Tables[0].Rows[i]["DISTRICT_CD"].ToString()
                        });
                    }
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
        public List<SelectListItem> GetGenderList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["GENDER"].ToString(),
                        Value = dt.Rows[i]["GENDER_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
        public List<SelectListItem> GetCountryList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["NATIONALITY"].ToString(),
                        Value = dt.Rows[i]["NATIONALITY_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
        public List<SelectListItem> GetStateList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["STATE"].ToString(),
                        Value = dt.Rows[i]["STATE_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
        public List<SelectListItem> GetBloodGroupList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["BLOOD_GRP"].ToString(),
                        Value = dt.Rows[i]["BLOOD_GROUP_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
        public List<SelectListItem> GetMaritalStatusList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["MARITAL_STATUS"].ToString(),
                        Value = dt.Rows[i]["MARITAL_STATUS_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
        public List<SelectListItem> GetOfficerRankList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["OFFICER_RANK"].ToString(),
                        Value = dt.Rows[i]["OFFICER_RANK_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
        public List<SelectListItem> GetEducationalQualificationList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["EDU_QUAL"].ToString(),
                        Value = dt.Rows[i]["EDU_QUAL_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }

        public List<SelectListItem> GetShoeDetailsList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["SHOE_SIZE"].ToString(),
                        Value = dt.Rows[i]["SHOE_SIZE_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }

        public List<SelectListItem> GetTShirtDetailsList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["T_SHIRT"].ToString(),
                        Value = dt.Rows[i]["T_SHIRT_SIZE_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }

        public List<SelectListItem> GetTrousersDetailsList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["TROUSERS_SIZE"].ToString(),
                        Value = dt.Rows[i]["TROUSERS_SIZE_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }

        public List<SelectListItem> GetSportDetailsList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["SPORT"].ToString(),
                        Value = dt.Rows[i]["SPORT_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }

        public List<SelectListItem> GetSportLevelDetailsList(DataTable dt)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                for (int i = 0; dt.Rows.Count > 0; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["SPORT_LEVEL"].ToString(),
                        Value = dt.Rows[i]["SPORT_LEVEL_CD"].ToString()
                    });
                }
            }
            catch (Exception e)
            {

            }
            return items;
        }
    }
}