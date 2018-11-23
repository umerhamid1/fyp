using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMonitoringSystem.Models
{
    public class parentStudentViewModel
    {
        public string IsApproved { get; set; }





// here is parents
        public int ID { get; set; }
        public string ParentsID { get; set; }
        public string PhoneNo { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CampusID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }



        public IList parentlist {get;set;}
}
}