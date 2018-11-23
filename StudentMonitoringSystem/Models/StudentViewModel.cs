using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMonitoringSystem.Models
{
    public class StudentViewModel
    {
        // here is student
        public int ID { get; set; }
        public Nullable<int> S_UserID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> ClassSectionID { get; set; }
        public string IsApproved { get; set; }
        public Nullable<int> ParentsID { get; set; }

       
        public virtual Parentss Parentss { get; set; }
        public virtual User S_User { get; set; }
        public virtual Class Class { get; set; }
      


        // here is parents




        public int P_ID { get; set; }
        public Nullable<int> P_UserID { get; set; }
        public string CNIC { get; set; }
        public string Occupation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Studentss> Studentsses { get; set; }
        public virtual User P_User { get; set; }




      
        public string P_FirstName { get; set; }
        public string P_LastName { get; set; }
        public string P_DOB { get; set; }
        public string P_Gender { get; set; }
        public string P_Address { get; set; }
        public string P_Nationality { get; set; }
        public string P_Phone { get; set; }
        public string P_LoginID { get; set; }
        public string P_Password { get; set; }
        
        public Nullable<int> P_RoleID { get; set; }
        public string P_Year { get; set; }

        public virtual Campu P_Campu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      
        public virtual Role P_Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Studentss> P_Studentsses { get; set; }


        // here is user details




        public int U_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string CampusID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string Year { get; set; }

        public virtual Campu Campu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parentss> U_Parentsses { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Studentss> U_Studentsses { get; set; }


        // here is class details
        public int C_ID { get; set; }
        public string C_Name { get; set; }

    }
}