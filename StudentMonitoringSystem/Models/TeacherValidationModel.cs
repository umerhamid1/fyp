using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMonitoringSystem.Models
{
    class TeacherValidationModel
    {


        [Required]
        [DisplayName("Full Name")]
        [MaxLength(50)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        public string Gender { get; set; }

        [Required]
        
        [MaxLength(150)]
        
        public string Address { get; set; }

        [Required]
        [DisplayName("Mobile")]
        [MaxLength(11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Number should be this for '03212220317'")]
        public string Phone { get; set; }


        
        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }


        [Required]
        [MaxLength(20)]

        public string Password { get; set; }

        
        

    }


    [MetadataType(typeof(TeacherValidationModel))]
    public partial class Teacher
    {
    }
}
