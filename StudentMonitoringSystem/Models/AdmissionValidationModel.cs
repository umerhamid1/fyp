    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentMonitoringSystem.Models
{
    public class AdmissionValidationModel
    {

        // students details


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<int> ParentsID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public string AdmissionYear { get; set; }
        public string IsApproved { get; set; }
        public string CampusID { get; set; }


        [MetadataType(typeof(AdmissionValidationModel))]
        public partial class Student
        {
        }
    }
}