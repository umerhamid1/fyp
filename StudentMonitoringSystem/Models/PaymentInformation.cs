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
    
    public partial class PaymentInformation
    {
        public int ID { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
        public string Method { get; set; }
        public Nullable<int> InvoiceInformation { get; set; }
    
        public virtual InvoiceInformation InvoiceInformation1 { get; set; }
    }
}
