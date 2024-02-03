using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Routing;
using System.Web.UI;
using BAL.Model;
using DAL;

namespace BAL.UserProfile
{
    public class UserProfileBAL
    {
        private static DBManager GetConnection() => new DBManager("connstr");
        public string SaveUserProfile(UserRegistrationModel obj)
        {
            string status = "0";
            string statusData = "0";
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[36];
                sqlParameters[0] = _objDBManager.CreateParameter("@EMPLOYEE_CODE", obj.EMPLOYEE_CODE, DbType.String);
                sqlParameters[1] = _objDBManager.CreateParameter("@FULL_NAME", obj.FULL_NAME, DbType.String);
                sqlParameters[2] = _objDBManager.CreateParameter("@FATHER_NAME", obj.FATHER_NAME, DbType.String);
                sqlParameters[3] = _objDBManager.CreateParameter("@GENDER_CD", obj.GENDER_CD, DbType.String);
                sqlParameters[4] = _objDBManager.CreateParameter("@ADHAAR_NUMBER", obj.ADHAAR_NUMBER, DbType.String);
                sqlParameters[5] = _objDBManager.CreateParameter("@EMAIL", obj.EMAIL, DbType.String);
                sqlParameters[6] = _objDBManager.CreateParameter("@PASSWORD", obj.PASSWORD, DbType.String);
                sqlParameters[7] = _objDBManager.CreateParameter("@DOB", DateTime.ParseExact(obj.DOB, "dd-MM-yyyy", CultureInfo.InvariantCulture), DbType.Date);
                sqlParameters[8] = _objDBManager.CreateParameter("@AGE", obj.AGE, DbType.String);
                sqlParameters[9] = _objDBManager.CreateParameter("@DOJ", DateTime.ParseExact(obj.DOJ, "dd-MM-yyyy", CultureInfo.InvariantCulture), DbType.Date);
                sqlParameters[10] = _objDBManager.CreateParameter("@HEIGHT", obj.HEIGHT, DbType.String);
                sqlParameters[11] = _objDBManager.CreateParameter("@WEIGHT", obj.WEIGHT, DbType.String);
                sqlParameters[12] = _objDBManager.CreateParameter("@MARITAL_STATUS_CD", obj.MARITAL_STATUS_CD, DbType.String);
                sqlParameters[13] = _objDBManager.CreateParameter("@BLOOD_GROUP_CD", obj.BLOOD_GROUP_CD, DbType.String);
                sqlParameters[14] = _objDBManager.CreateParameter("@BATCH_NUMBER", obj.BATCH_NUMBER, DbType.String);
                sqlParameters[15] = _objDBManager.CreateParameter("@RANK_CD", obj.RANK_CD, DbType.String);
                sqlParameters[16] = _objDBManager.CreateParameter("@SPORT_LEVEL_CD", obj.SPORT_LEVEL_CD, DbType.String);
                sqlParameters[17] = _objDBManager.CreateParameter("@SPORT_CD", obj.SPORT_CD, DbType.String);
                sqlParameters[18] = _objDBManager.CreateParameter("@EXTRA_SPORT", string.IsNullOrEmpty(obj.EXTRA_SPORT) ? "" : obj.EXTRA_SPORT, DbType.String);
                sqlParameters[19] = _objDBManager.CreateParameter("@T_SHIRT_SIZE", obj.T_SHIRT_SIZE, DbType.String);
                sqlParameters[20] = _objDBManager.CreateParameter("@TROUSERS_SIZE", obj.TROUSERS_SIZE, DbType.String);
                sqlParameters[21] = _objDBManager.CreateParameter("@SHOE_SIZE", obj.SHOE_SIZE, DbType.String);
                sqlParameters[22] = _objDBManager.CreateParameter("@EDUCATION_CD", obj.EDUCATION_CD, DbType.String);
                sqlParameters[23] = _objDBManager.CreateParameter("@HOUSE", string.IsNullOrEmpty(obj.HOUSE) ? "" : obj.HOUSE, DbType.String);
                sqlParameters[24] = _objDBManager.CreateParameter("@STREET", string.IsNullOrEmpty(obj.STREET) ? "" : obj.STREET, DbType.String);
                sqlParameters[25] = _objDBManager.CreateParameter("@LANDMARK", string.IsNullOrEmpty(obj.LANDMARK) ? "" : obj.LANDMARK, DbType.String);
                sqlParameters[26] = _objDBManager.CreateParameter("@VILLAGE", string.IsNullOrEmpty(obj.VILLAGE) ? "" : obj.VILLAGE, DbType.String);
                sqlParameters[27] = _objDBManager.CreateParameter("@POST_OFFICE", string.IsNullOrEmpty(obj.POST_OFFICE) ? "" : obj.POST_OFFICE, DbType.String);
                sqlParameters[28] = _objDBManager.CreateParameter("@SUB_DISTRICT", string.IsNullOrEmpty(obj.SUB_DISTRICT) ? "" : obj.SUB_DISTRICT, DbType.String);
                sqlParameters[29] = _objDBManager.CreateParameter("@DISTRICT_CD", obj.DISTRICT_CD, DbType.String);
                sqlParameters[30] = _objDBManager.CreateParameter("@STATE_CD", obj.STATE_CD, DbType.String);
                sqlParameters[31] = _objDBManager.CreateParameter("@COUNTRY_CD", obj.COUNTRY_CD, DbType.String);
                sqlParameters[32] = _objDBManager.CreateParameter("@PINCODE", obj.PINCODE, DbType.String);
                sqlParameters[33] = _objDBManager.CreateParameter("@FILENAMES", obj.FILEPIC.FileName, DbType.String);
                sqlParameters[34] = _objDBManager.CreateParameter("@FILEPIC", obj.bytes, DbType.Binary);
                sqlParameters[35] = _objDBManager.CreateParameter("@MOBILENUMBER", obj.MOBILE_NUMBER, DbType.String);
                status = _objDBManager.InsertData("UDP_INSERT_DEATILS_OF_EMPLOYEE", CommandType.StoredProcedure, sqlParameters, out statusData).ToString();
            }
            catch (Exception)
            {
                status = "0";
            }
            return status;
        }
        public string CheckUserAvailability(string UserName)
        {
            string returnResult = "1";
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = _objDBManager.CreateParameter("@UserId", UserName, DbType.String);
                returnResult = _objDBManager.InsertData("UDP_GET_USER_AVAILABILITY", CommandType.StoredProcedure, sqlParameters, out returnResult).ToString();
            }
            catch (Exception)
            {
                returnResult = "1";
            }
            if (returnResult.Equals("0"))
                returnResult = "false";
            else
                returnResult = "true";
            return returnResult;
        }

        public List<UserDetailsViewModel> GetUserDetails(string LOGIN_ID, Int32 USER_TYPE)
        {
            List<UserDetailsViewModel> _objUserDetailsViewModel = new List<UserDetailsViewModel>();
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = _objDBManager.CreateParameter("@PERSON_CODE", 0, DbType.Int64);
                sqlParameters[1] = _objDBManager.CreateParameter("@LOGIN_ID", LOGIN_ID, DbType.String);
                sqlParameters[2] = _objDBManager.CreateParameter("@USER_TYPE", USER_TYPE, DbType.Int32);
                DataTable dt = _objDBManager.GetDataTable("UDP_GET_ALL_EMPLOYEE_DETAILS", CommandType.StoredProcedure, sqlParameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UserDetailsViewModel user = new UserDetailsViewModel
                        {
                            PERSON_CODE = Convert.ToInt64(row["PERSON_CODE"]),
                            EMPLOYEE_CODE = row["EMPLOYEE_CODE"].ToString(),
                            FULL_NAME = row["FULL_NAME"].ToString(),
                            FATHER_NAME = row["FATHER_NAME"].ToString(),
                            GENDER_CD = row["GENDER_CD"].ToString(),
                            ADHAAR_NUMBER = row["ADHAAR_NUMBER"].ToString(),
                            EMAIL = row["EMAIL"].ToString(),
                            DOB = row["DOB"].ToString(),
                            AGE = Convert.ToInt32(row["AGE"]),
                            DOJ = row["DOJ"].ToString(),
                            HEIGHT = Convert.ToInt32(row["HEIGHT"]),
                            WEIGHT = Convert.ToInt32(row["WEIGHT"]),
                            MARITAL_STATUS_CD = row["MARITAL_STATUS_CD"].ToString(),
                            BLOOD_GROUP_CD = row["BLOOD_GROUP_CD"].ToString(),
                            BATCH_NUMBER = row["BATCH_NUMBER"].ToString(),
                            RANK_CD = row["RANK_CD"].ToString(),
                            SPORT_LEVEL_CD = row["SPORT_LEVEL_CD"].ToString(),
                            SPORT_CD = row["SPORT_CD"].ToString(),
                            EXTRA_SPORT = row["EXTRA_SPORT"].ToString(),
                            T_SHIRT_SIZE = row["T_SHIRT_SIZE"].ToString(),
                            TROUSERS_SIZE = row["TROUSERS_SIZE"].ToString(),
                            SHOE_SIZE = row["SHOE_SIZE"].ToString(),
                            EDUCATION_CD = row["EDUCATION_CD"].ToString(),
                            HOUSE = row["HOUSE"].ToString(),
                            STREET = row["STREET"].ToString(),
                            LANDMARK = row["LANDMARK"].ToString(),
                            VILLAGE = row["VILLAGE"].ToString(),
                            POST_OFFICE = row["POST_OFFICE"].ToString(),
                            SUB_DISTRICT = row["SUB_DISTRICT"].ToString(),
                            DISTRICT_CD = row["DISTRICT_CD"].ToString(),
                            STATE_CD = row["STATE_CD"].ToString(),
                            COUNTRY_CD = row["COUNTRY_CD"].ToString(),
                            PINCODE = row["PINCODE"].ToString(),
                        };
                        _objUserDetailsViewModel.Add(user);
                    }
                }
            }
            catch (Exception)
            {

            }
            return _objUserDetailsViewModel;
        }


        public UserDetailsViewModel GetUserDetailsOf(Int64 PERSON_CODE, string LOGIN_ID, Int32 USER_TYPE)
        {
            UserDetailsViewModel _objUserDetailsViewModel = new UserDetailsViewModel();
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = _objDBManager.CreateParameter("@PERSON_CODE", PERSON_CODE, DbType.Int64);
                sqlParameters[1] = _objDBManager.CreateParameter("@LOGIN_ID", LOGIN_ID, DbType.String);
                sqlParameters[2] = _objDBManager.CreateParameter("@USER_TYPE", USER_TYPE, DbType.Int32);
                DataSet ds = _objDBManager.GetDataSet("UDP_GET_EMPLOYEE_DETAILS", CommandType.StoredProcedure, sqlParameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {

                            _objUserDetailsViewModel.PERSON_CODE = Convert.ToInt64(row["PERSON_CODE"]);
                            _objUserDetailsViewModel.EMPLOYEE_CODE = row["EMPLOYEE_CODE"].ToString();
                            _objUserDetailsViewModel.FULL_NAME = row["FULL_NAME"].ToString();
                            _objUserDetailsViewModel.FATHER_NAME = row["FATHER_NAME"].ToString();
                            _objUserDetailsViewModel.GENDER_CD = row["GENDER_CD"].ToString();
                            _objUserDetailsViewModel.ADHAAR_NUMBER = row["ADHAAR_NUMBER"].ToString();
                            _objUserDetailsViewModel.EMAIL = row["EMAIL"].ToString();
                            _objUserDetailsViewModel.DOB = row["DOB"].ToString();
                            _objUserDetailsViewModel.AGE = Convert.ToInt32(row["AGE"]);
                            _objUserDetailsViewModel.DOJ = row["DOJ"].ToString();
                            _objUserDetailsViewModel.HEIGHT = Convert.ToInt32(row["HEIGHT"]);
                            _objUserDetailsViewModel.WEIGHT = Convert.ToInt32(row["WEIGHT"]);
                            _objUserDetailsViewModel.MARITAL_STATUS_CD = row["MARITAL_STATUS_CD"].ToString();
                            _objUserDetailsViewModel.BLOOD_GROUP_CD = row["BLOOD_GROUP_CD"].ToString();
                            _objUserDetailsViewModel.BATCH_NUMBER = row["BATCH_NUMBER"].ToString();
                            _objUserDetailsViewModel.RANK_CD = row["RANK_CD"].ToString();
                            _objUserDetailsViewModel.SPORT_LEVEL_CD = row["SPORT_LEVEL_CD"].ToString();
                            _objUserDetailsViewModel.SPORT_CD = row["SPORT_CD"].ToString();
                            _objUserDetailsViewModel.EXTRA_SPORT = row["EXTRA_SPORT"].ToString();
                            _objUserDetailsViewModel.T_SHIRT_SIZE = row["T_SHIRT_SIZE"].ToString();
                            _objUserDetailsViewModel.TROUSERS_SIZE = row["TROUSERS_SIZE"].ToString();
                            _objUserDetailsViewModel.SHOE_SIZE = row["SHOE_SIZE"].ToString();
                            _objUserDetailsViewModel.EDUCATION_CD = row["EDUCATION_CD"].ToString();
                            _objUserDetailsViewModel.HOUSE = row["HOUSE"].ToString();
                            _objUserDetailsViewModel.STREET = row["STREET"].ToString();
                            _objUserDetailsViewModel.LANDMARK = row["LANDMARK"].ToString();
                            _objUserDetailsViewModel.VILLAGE = row["VILLAGE"].ToString();
                            _objUserDetailsViewModel.POST_OFFICE = row["POST_OFFICE"].ToString();
                            _objUserDetailsViewModel.SUB_DISTRICT = row["SUB_DISTRICT"].ToString();
                            _objUserDetailsViewModel.DISTRICT_CD = row["DISTRICT_CD"].ToString();
                            _objUserDetailsViewModel.STATE_CD = row["STATE_CD"].ToString();
                            _objUserDetailsViewModel.COUNTRY_CD = row["COUNTRY_CD"].ToString();
                            _objUserDetailsViewModel.PINCODE = row["PINCODE"].ToString();
                        }
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    _objUserDetailsViewModel.bytes = (byte[])ds.Tables[1].Rows[0]["FILEPIC"];
                    _objUserDetailsViewModel.ImageString = "data:image/png;base64," + Convert.ToBase64String(_objUserDetailsViewModel.bytes, 0, _objUserDetailsViewModel.bytes.Length);
                }
            }
            catch (Exception)
            {

            }
            return _objUserDetailsViewModel;
        }





        public SignatureUpload GetEmployeeSignature(string EMPLOYEE_CODE)
        {
            SignatureUpload _SignatureUpload = new SignatureUpload();
            try
            {
                var _objDBManager = GetConnection();
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = _objDBManager.CreateParameter("@EMPLOYEE_CODE", EMPLOYEE_CODE, DbType.String);
                DataSet ds = _objDBManager.GetDataSet("UDP_GET_EMPLOYEE_IMAGE", CommandType.StoredProcedure, sqlParameters);
                
            }
            catch (Exception e)
            {

            }
            return _SignatureUpload;
        }

    }
}
