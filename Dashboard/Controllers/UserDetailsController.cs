using BAL.CommonClass;
using BAL.Model;
using BAL.UserProfile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class UserDetailsController : Controller
    {
        
        public ActionResult ListOfUser()
        {
            UserProfileBAL objUserProfileBAL = new UserProfileBAL();
            List<UserDetailsViewModel> userList=new List<UserDetailsViewModel>();
            if (Session["USER_TYPE"] == null)
            {
                Response.Redirect("~/Login/Logout");

            }
            else
                userList = objUserProfileBAL.GetUserDetails(Session["LoginId"].ToString(), Convert.ToInt32(Session["USER_TYPE"]));
            return View(userList);
        }
        public ActionResult UserDetails(Int64 id)
        {
            UserProfileBAL objUserProfileBAL = new UserProfileBAL();
            UserDetailsViewModel userList = new UserDetailsViewModel();
            if (Session["USER_TYPE"] == null)
            {
                Response.Redirect("~/Login/Logout");

            }
            else
            {
                userList = objUserProfileBAL.GetUserDetailsOf(id, Session["LoginId"].ToString(), Convert.ToInt32(Session["USER_TYPE"]));
                FillDropDownList();
                userList.COUNTRY_CD = "80";
            }
            return View(userList);
        }
        private void FillDropDownList()
        {
            DataSet objTables = new DataSet();
            objTables = (DataSet)Session["MenuTable"];
            if (objTables.Tables[0].Rows.Count > 0)
                ViewBag.GENDER_CD = new CommonDataFliter().GetGenderList(objTables.Tables[0]);
            if (objTables.Tables[1].Rows.Count > 0)
                ViewBag.COUNTRY_CD = new CommonDataFliter().GetCountryList(objTables.Tables[1]);
            if (objTables.Tables[2].Rows.Count > 0)
                ViewBag.STATE_CD = new CommonDataFliter().GetStateList(objTables.Tables[2]);
            if (objTables.Tables[3].Rows.Count > 0)
                ViewBag.BLOOD_GROUP_CD = new CommonDataFliter().GetBloodGroupList(objTables.Tables[3]);
            if (objTables.Tables[4].Rows.Count > 0)
                ViewBag.MARITAL_STATUS_CD = new CommonDataFliter().GetMaritalStatusList(objTables.Tables[4]);
            if (objTables.Tables[5].Rows.Count > 0)
                ViewBag.RANK_CD = new CommonDataFliter().GetOfficerRankList(objTables.Tables[5]);
            if (objTables.Tables[6].Rows.Count > 0)
                ViewBag.EDUCATION_CD = new CommonDataFliter().GetEducationalQualificationList(objTables.Tables[6]);
            if (objTables.Tables[7].Rows.Count > 0)
                ViewBag.SHOE_SIZE = new CommonDataFliter().GetShoeDetailsList(objTables.Tables[7]);
            if (objTables.Tables[8].Rows.Count > 0)
                ViewBag.T_SHIRT_SIZE = new CommonDataFliter().GetTShirtDetailsList(objTables.Tables[8]);
            if (objTables.Tables[9].Rows.Count > 0)
                ViewBag.TROUSERS_SIZE = new CommonDataFliter().GetTrousersDetailsList(objTables.Tables[9]);
            if (objTables.Tables[10].Rows.Count > 0)
                ViewBag.SPORT_CD = new CommonDataFliter().GetSportDetailsList(objTables.Tables[10]);
            if (objTables.Tables[11].Rows.Count > 0)
                ViewBag.SPORT_LEVEL_CD = new CommonDataFliter().GetSportLevelDetailsList(objTables.Tables[11]);
        }

        private string ViewImage(byte[] arrayImage)
        {
            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
            return "data:image/png;base64," + base64String;
        }

        private SignatureUpload getUploadedFiles()
        {
            UserProfileBAL objSignatureFileUploadBAL = new UserProfileBAL();
            return objSignatureFileUploadBAL.GetEmployeeSignature(Session["LoginId"].ToString());
        }
    }
}
