using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMonitoringSystem.Models
{
    class ParentValidationModel
    {

        [Required]
        [DisplayName("Mobile")]
        [MaxLength(11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Number should be this for '03212220317'")]
        public string PhoneNo { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }


        [Required] [MaxLength(50)] public string Password { get; set; }


        [Required]
        [DisplayName("First Name")]
        [MaxLength(50)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string FirstName { get; set; }


        [Required]
        [DisplayName("Last Name")]
        [MaxLength(50)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "invalid format'")]
        public string LastName { get; set; }
    }

    [MetadataType(typeof(ParentValidationModel))]
    public partial class Parent
    {
    }
}
