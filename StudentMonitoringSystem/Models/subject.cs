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
    
    public partial class subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public subject()
        {
            this.Assignments = new HashSet<Assignment>();
            this.ExamsTimeTables = new HashSet<ExamsTimeTable>();
            this.SubjectTimeTables = new HashSet<SubjectTimeTable>();
            this.SubjectTeachers = new HashSet<SubjectTeacher>();
            this.SubjectTimeTable1 = new HashSet<SubjectTimeTable1>();
        }
    
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string CampusID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual Campu Campu { get; set; }
        public virtual Class Class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamsTimeTable> ExamsTimeTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTimeTable> SubjectTimeTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTimeTable1> SubjectTimeTable1 { get; set; }
    }
}
