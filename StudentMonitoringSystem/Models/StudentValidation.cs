using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMonitoringSystem.Models
{
    class StudentValidation
    {


        [DisplayName("ID")]
        public string StudentID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]



        [DisplayName("Nationality")]
        public string Nationality { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }

      
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Mobile")]
        public string PhoneNo { get; set; }


      

        [DisplayName("Class")]
        public Nullable<int> ClassID { get; set; }

        

        [DisplayName("Section")]
        public Nullable<int> SectionID { get; set; }

        
        [DisplayName("Acadmic Year")]
        public string AdmissionYear { get; set; }

        
        [DisplayName("IsApproved")]
        public string IsApproved { get; set; }

        
        [DisplayName("Campus Name")]
        public string CampusID { get; set; }





    }


    [MetadataType(typeof(StudentValidation))]
    public partial class Student
    {
    }
}
