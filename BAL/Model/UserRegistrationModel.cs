using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;

namespace BAL.Model
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime dateOfBirth)
            {
                if (dateOfBirth > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Date cannot be a future date.");
                }
            }

            return ValidationResult.Success;
        }
    }
    public class UserRegistrationModel : IValidatableObject
    {
        public Int64 PERSON_CODE { get; set; }
        [Required(ErrorMessage = "Please enter employee code.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Employee code can only contain letters and numbers.")]
        public string EMPLOYEE_CODE { get; set; }
        [Required(ErrorMessage = "Please enter full name.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please enter a valid name.")]
        public string FULL_NAME { get; set; }
        [Required(ErrorMessage = "Please enter father name.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please enter a valid name.")]
        public string FATHER_NAME { get; set; }
        [Required(ErrorMessage = "Please select gender")]
        public string GENDER_CD { get; set; }
        [Required(ErrorMessage = "Please enter aadhaar number.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Please enter a valid 12-digit Aadhar number.")]
        public string ADHAAR_NUMBER { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string EMAIL { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 12 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^])[A-Za-z\d!@#$%^]+$", 
            ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one digit and one special character.")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Please enter confirm password.")]
        [Compare("PASSWORD", ErrorMessage = "The password and confirmation password do not match.")]
        public string CONFIRMPASSWORD { get; set; }
        [Required(ErrorMessage = "Please enter date of birth.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [NotFutureDate(ErrorMessage = "Date cannot be a future date.")]
        public string DOB { get; set; }
        [Required(ErrorMessage = "Please enter the age.")]
        [Range(2, 150, ErrorMessage = "Age must be between 2 and 150.")]
        [RegularExpression(@"^\d{1,3}$", ErrorMessage = "Age must be a numeric value.")]
        public string AGE { get; set; }
        [Required(ErrorMessage = "Please enter date of joining.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [NotFutureDate(ErrorMessage = "Date cannot be a future date.")]
        public string DOJ { get; set; }
        [Required(ErrorMessage = "Please enter the height.")]
        [Range(1, 300, ErrorMessage = "Height must be between 1 and 300.")]
        [RegularExpression(@"^\d{1,3}$", ErrorMessage = "Height must be a numeric value.")]
        public string HEIGHT { get; set; }
        [Required(ErrorMessage = "Please enter the weight.")]
        [Range(1, 1000, ErrorMessage = "Weight must be between 1 and 1000.")]
        [RegularExpression(@"^\d{1,4}$", ErrorMessage = "Weight must be a numeric value.")]
        public string WEIGHT { get; set; }

        [Required(ErrorMessage = "Please select marital mtatus")]
        public string MARITAL_STATUS_CD { get; set; }
        [Required(ErrorMessage = "Please select blood group")]
        public string BLOOD_GROUP_CD { get; set; }
        [Required(ErrorMessage = "Please enter batch number.")]
        public string BATCH_NUMBER { get; set; }
        [Required(ErrorMessage = "Please select rank")]
        public string RANK_CD { get; set; }
        [Required(ErrorMessage = "Please select sport level")]
        public string SPORT_LEVEL_CD { get; set; }
        [Required(ErrorMessage = "Please select sport details")]
        public string SPORT_CD { get; set; }
        public string EXTRA_SPORT { get; set; }
        [Required(ErrorMessage = "Please select T-Shirt size")]
        public string T_SHIRT_SIZE { get; set; }
        [Required(ErrorMessage = "Please select trousers size")]
        public string TROUSERS_SIZE { get; set; }
        [Required(ErrorMessage = "Please select shoe size")]
        public string SHOE_SIZE { get; set; }
        [Required(ErrorMessage = "Please select education")]
        public string EDUCATION_CD { get; set; }


        public string HOUSE { get; set; }
        public string STREET { get; set; }
        public string LANDMARK { get; set; }
        public string VILLAGE { get; set; }
        public string POST_OFFICE { get; set; }
        public string SUB_DISTRICT { get; set; }
        [Required(ErrorMessage = "Please select district")]
        public string DISTRICT_CD { get; set; }
        [Required(ErrorMessage = "Please select state")]
        public string STATE_CD { get; set; }
        public string COUNTRY_CD { get; set; }
        [Required(ErrorMessage = "Please enter pin code.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Enter a valid PIN code.")]
        public string PINCODE { get; set; }
        public string FILENAMES { get; set; }
        [Required(ErrorMessage = "Please select a file.")]
        public HttpPostedFileBase FILEPIC { get; set; }
        public Byte[] bytes { get; set; }
        public string ImageString { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FILEPIC == null || FILEPIC.ContentLength == 0)
            {
                yield return new ValidationResult("Please select a file.", new[] { nameof(FILEPIC) });
            }
        }
        public string MOBILE_NUMBER { get; set; }
    }
    public class UserDetailsViewModel
    {
        public Int64 PERSON_CODE { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string FULL_NAME { get; set; }
        public string FATHER_NAME { get; set; }
        public string GENDER_CD { get; set; }
        public string ADHAAR_NUMBER { get; set; }
        public string EMAIL { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string DOB { get; set; }
        public Int32 AGE { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string DOJ { get; set; }
        public Int32 HEIGHT { get; set; }
        public Int32 WEIGHT { get; set; }
        public string MARITAL_STATUS_CD { get; set; }
        public string BLOOD_GROUP_CD { get; set; }
        public string BATCH_NUMBER { get; set; }
        public string RANK_CD { get; set; }
        public string SPORT_LEVEL_CD { get; set; }
        public string SPORT_CD { get; set; }
        public string EXTRA_SPORT { get; set; }
        public string T_SHIRT_SIZE { get; set; }
        public string TROUSERS_SIZE { get; set; }
        public string SHOE_SIZE { get; set; }
        public string EDUCATION_CD { get; set; }
        public string HOUSE { get; set; }
        public string STREET { get; set; }
        public string LANDMARK { get; set; }
        public string VILLAGE { get; set; }
        public string POST_OFFICE { get; set; }
        public string SUB_DISTRICT { get; set; }
        public string DISTRICT_CD { get; set; }
        public string STATE_CD { get; set; }
        public string COUNTRY_CD { get; set; }
        public string PINCODE { get; set; }

        public string FILENAMES { get; set; }
        public HttpPostedFileBase FILEPIC { get; set; }
        public Byte[] bytes { get; set; }
        public string ImageString { get; set; }

        public string MOBILE_NUMBER { get; set; }
    }
    public class SignatureUpload
    {
        public string EMPLOYEE_CODE { get; set; }
        public string FILENAMES { get; set; }
        public HttpPostedFileBase FILEPIC { get; set; }
        public Byte[] bytes { get; set; }
        public string ImageString { get; set; }
    }
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter mobile number.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
        public string MobileNumber { get; set; }

    }
    public class UserVerificationModel
    {
        [Required(ErrorMessage = "Please enter OTP.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be 6 characters.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Enter a valid OTP.")]
        public string OTP { get; set; }

        public string errorMassage { get; set; }
    }
}
