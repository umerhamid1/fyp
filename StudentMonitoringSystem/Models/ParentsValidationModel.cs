using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentMonitoringSystem.Models
{
    public class ParentsValidationModel
    {
        public int P_ID { get; set; }

        [DisplayName("ID")]
        [Required]
        public string P_ParentsID { get; set; }

        [DisplayName("Mobile")]
        [Required]
        public string P_PhoneNo { get; set; }

        [DisplayName("Occupation")]
        [Required]
        public string P_Occupation { get; set; }


        [DisplayName("Email")]
        [Required]
        public string P_Email { get; set; }

        [Required]

        [DisplayName("Password")]
        public string P_Password { get; set; }

        [DisplayName("Campus Name")]
        [Required]
        public string P_CampusID { get; set; }

        [DisplayName("Frist Name")]
        public string P_FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string P_LastName { get; set; }



        [MetadataType(typeof(StudentValidation))]
        public partial class Parents
        {
        }

    }
}