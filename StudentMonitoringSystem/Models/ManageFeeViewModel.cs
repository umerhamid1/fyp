using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMonitoringSystem.Models
{
    public class ManageFeeViewModel
    {

        /// <summary>
        /// here is payment information details
        /// </summary>
        public int paymentID { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
        public string Method { get; set; }
        public Nullable<int> InvoiceInformation { get; set; }

        public virtual InvoiceInformation InvoiceInformation1 { get; set; }

        //here is student invoice details

        public int InvoiceID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

        public virtual Class Class { get; set; }
        public virtual Student Student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentInformation> PaymentInformations { get; set; }

        public List<InvoiceInformation> StudentList { get; set; }



    }
}