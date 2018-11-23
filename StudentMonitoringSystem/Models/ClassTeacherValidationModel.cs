using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentMonitoringSystem.Models
{
    public class ClassTeacherValidationModel
    {
        [Required]
        public Nullable<int> SectionID { get; set; }
        [Required]
        public Nullable<int> TeacherID { get; set; }
        [Required]

        public Nullable<int> ClassID { get; set; }
        [Required]

        public string CampusID { get; set; }
    }



    [MetadataType(typeof(ClassTeacherValidationModel))]
    public partial class ClassAndSectionDetail
    {
    }
}