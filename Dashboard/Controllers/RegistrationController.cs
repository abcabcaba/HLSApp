using BAL.CommonClass;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.UserProfile;
using DocumentFormat.OpenXml.Office2013.Excel;
using System.IO;
using BAL.Model;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.EMMA;
using DAL.Common;

namespace Dashboard.Controllers
{

    public class RegistrationController : Controller
    {
        string filesize = ConfigurationManager.AppSettings["fileSize"];
        public ActionResult GetDistrictList(int State_CD)
        {
            ViewBag.DDLDISTRICT = new CommonDataFliter().GetDistrictList(State_CD);
            return PartialView("DisplayDistrict");
        }
        public ActionResult Registration()
        {
            BAL.Model.UserRegistrationModel obj = new BAL.Model.UserRegistrationModel();
            if (Session["MobileNo"] == null)
            {
                Response.Redirect(string.Format("~/Registration/MobileVerification"));
            }
            FillDropDownList();
            obj.COUNTRY_CD = "80";
            return View(obj);
        }
        public JsonResult CheckUserAvailability(string UserName)
        {
            string retval = "";
            retval = new UserProfileBAL().CheckUserAvailability(UserName);
            return Json(retval, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Registration(BAL.Model.UserRegistrationModel modelData)
        {
            if (Session["MobileNo"] != null)
            {
                modelData.MOBILE_NUMBER= Session["MobileNo"].ToString();
                modelData.GENDER_CD = Request.Form["GENDER_CD"].ToString();
                modelData.MARITAL_STATUS_CD = Request.Form["MARITAL_STATUS_CD"].ToString();
                modelData.BLOOD_GROUP_CD = Request.Form["BLOOD_GROUP_CD"].ToString();
                modelData.RANK_CD = Request.Form["RANK_CD"].ToString();
                modelData.SPORT_LEVEL_CD = Request.Form["SPORT_LEVEL_CD"].ToString();
                modelData.COUNTRY_CD = "80";
                modelData.STATE_CD = Request.Form["STATE_CD"].ToString();
                modelData.DISTRICT_CD = Request.Form["DISTRICT_CD"].ToString();
                modelData.SPORT_CD = Request.Form["SPORT_CD"].ToString();
                modelData.T_SHIRT_SIZE = Request.Form["T_SHIRT_SIZE"].ToString();
                modelData.TROUSERS_SIZE = Request.Form["TROUSERS_SIZE"].ToString();
                modelData.SHOE_SIZE = Request.Form["SHOE_SIZE"].ToString();
                modelData.EDUCATION_CD = Request.Form["EDUCATION_CD"].ToString();
                if (ModelState.IsValid)
                {
                    UserProfileBAL objUserProfileBAL = new UserProfileBAL();
                    try
                    {
                        Byte[] bytes = null;
                        if (modelData != null && modelData.FILEPIC != null && modelData.FILEPIC.FileName != null)
                        {
                            Stream fs = modelData.FILEPIC.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            bytes = br.ReadBytes((Int32)fs.Length);
                            modelData.bytes = bytes;
                            modelData.ImageString = ViewImage(bytes);
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    using (General objGeneral = new General())
                    {
                        modelData.PASSWORD = objGeneral.ConvertUserNamePasswordToSHA512(modelData.MOBILE_NUMBER.Trim(), modelData.PASSWORD.Trim()).ToString();
                    }
                    string status = objUserProfileBAL.SaveUserProfile(modelData);
                    if (status.Equals("0"))
                        ViewBag.Msg = "Some Error Occured. Please try again.";
                    else if (status.Equals("9223372036854775807"))
                        ViewBag.Msg = "User already exists. Please try another user.";
                    else
                    {
                        Response.Redirect(string.Format("~/Registration/SuccessMessage?q={0}", status));
                    }
                }
                else
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            else
                Response.Redirect(string.Format("~/Registration/MobileVerification"));
            FillDropDownList();
            return View(modelData);
        }
        public ActionResult SuccessMessage()
        {
            return View();
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

        public ActionResult MobileVerification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MobileVerification(BAL.Model.UserModel modelData)
        {
            if (ModelState.IsValid)
            {
                Session["MobileNo"] = modelData.MobileNumber;
                Response.Redirect(string.Format("~/Registration/VerifyOTP"));
            }
            return View(modelData);
        }
        public ActionResult VerifyOTP()
        {
            BAL.Model.UserVerificationModel modelData = new BAL.Model.UserVerificationModel();
            if (Session["MobileNo"] != null)
            {
                //Session["VerifyCode"] = GenerateVerificationCode();
                Session["VerifyCode"] = "123456";
                SendVerificationCode(Session["MobileNo"].ToString(), Session["VerifyCode"].ToString());
            }
            else
                Response.Redirect(string.Format("~/Registration/MobileVerification"));
            modelData.errorMassage=string.Empty;
            return View(modelData);
        }
        [HttpPost]
        public ActionResult VerifyOTP(BAL.Model.UserVerificationModel modelData)
        {
            if (ModelState.IsValid)
            {
                modelData.errorMassage = string.Empty;
                if (Session["MobileNo"] != null && Session["VerifyCode"] != null)
                {
                    if (Session["VerifyCode"].ToString().Equals(modelData.OTP))
                        Response.Redirect(string.Format("~/Registration/Registration"));
                    else
                        modelData.errorMassage = "Enter a valid OTP.";
                }
                else if (Session["MobileNo"] == null)
                    Response.Redirect(string.Format("~/Registration/MobileVerification"));
            }
            return View(modelData);
        }
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }
        private void SendVerificationCode(string mobileNumber, string verificationCode)
        {
        }

    }
}