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
    
    public partial class NoticeBoardUserType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeBoardUserType()
        {
            this.NoticsBoards = new HashSet<NoticsBoard>();
        }
    
        public int ID { get; set; }
        public string userType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticsBoard> NoticsBoards { get; set; }
    }
}
