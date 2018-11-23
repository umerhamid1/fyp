//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentMonitoringSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attendance()
        {
            this.StudentAttendances = new HashSet<StudentAttendance>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public string Date { get; set; }
        public Nullable<int> TeacherID { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Section Section { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
    }
}
