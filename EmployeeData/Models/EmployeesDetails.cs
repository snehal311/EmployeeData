using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeData.Models
{
    public class EmployeesDetails
    {
        public int EmpId { get; set; }

        [Required(ErrorMessage ="Please enter Employee name")]
        public string EmpName { get; set; }

        [Required(ErrorMessage ="Please select designation")]
        public List<Designations> Designation { get; set; }

        [Required(ErrorMessage ="Please enter phone no")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Phone number must be a 10-digit number.")]
        public int PhoneNo { get; set; }

        [Required(ErrorMessage ="Please enter email id")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please select region")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Please select branch")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Please select zone")]
        public int ZoneId { get; set; }

        [Required(ErrorMessage = "Please select date of birth")]
        [DataType(DataType.Date,ErrorMessage ="Please select valid date of birth")]
        //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [DateOfBirth(ErrorMessage ="enter correct date")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select gender")]
        public string Gender { get; set; }

        
        [MaxLength(50,ErrorMessage ="Please enter 50 character in address")]
        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select Technologies")]
        public List<Technologies> Technology { get; set; }


        
        [DataType(DataType.Upload,ErrorMessage ="Please upload valid documents" )]
        [Required(ErrorMessage = "Please upload document")]
        public List<HttpPostedFileBase> Documents { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public enum Designations
    {
        BOM,
        ZOM,
        RLM,
        Admin,
        ZLM,
        ZLO

    }

    public enum Technologies
    {
        DotNet,
        CSharp,
        Java,
        PHP,
        Python,
        Javascript,
        SQL,
        Oracle

    }
}