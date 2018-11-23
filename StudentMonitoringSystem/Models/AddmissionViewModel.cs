using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMonitoringSystem.Models
{
  public  class AddmissionViewModel
    {



        //public int GenderID { get; set; }
        //public bool GenderName { get; set; }




        // here is student



       



        [DisplayName("ID")]
        public string StudentID { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$" , ErrorMessage = "invalid format'")]
      
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string LastName { get; set; }
        [Required]
         [DisplayName("Nationality")]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string Nationality { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string  DOB { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }


        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }

        //[Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Mobile")]
        [MaxLength(11)]
       
        [RegularExpression("^[0-9]*$", ErrorMessage = "Number should be this for '03212220317'")]
        public string PhoneNo { get; set; }


        

       







        [DisplayName("Class")]
        public Nullable<int> ClassID { get; set; }

      

        [DisplayName("Section")]
        public Nullable<int> SectionID { get; set; }

        //[Required]
        [DisplayName("Acadmic Year")]
        public string AdmissionYear { get; set; }

        //[Required]
        [DisplayName("IsApproved")]
        public string IsApproved { get; set; }

        //[Required]
        [DisplayName("Campus Name")]
        public string CampusID { get; set; }



        //// here is campus deatils

        //public virtual Campu Campu { get; set; }
        //public virtual Class Class { get; set; }
        //public virtual Parent Parent { get; set; }

        // here is parents table



        public int P_ID { get; set; }

        [DisplayName("ID")]
        //[Required]
        public string P_ParentsID { get; set; }

        [DisplayName("Mobile")]
        [Required]
        public string P_PhoneNo { get; set; }

        [DisplayName("Occupation")]
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string P_Occupation { get; set; }


        [DisplayName("Email")]
        
        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address")]
        public string P_Email { get; set; }

        //[Required]

        [DisplayName("Password")]
        public string P_Password { get; set; }

        [DisplayName("Campus Name")]
        //[Required]
        public string P_CampusID { get; set; }

        [DisplayName("Frist Name")]
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string P_FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string P_LastName { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students { get; set; }


        // start validation


        //[MetadataType(typeof(AddmissionViewModel))]
        //public partial class Student
        //{
        //}



        //[MetadataType(typeof(AddmissionViewModel))]
        //public partial class Parents
        //{
        //}
    }
}
