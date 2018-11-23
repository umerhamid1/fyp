using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMonitoringSystem.Models
{
    public partial class Attendance
    {
        public List<StudentAttendance> StudentList { get; set; }
    }

    public class AttendanceViewModel
    {
        public int ClassID { get; set; }
        public DateTime Date { get; set; }
        public List<StudentAttendance> StudentList { get; set; }
    }


    public partial class StudentAttendence
    {
     //   public virtual Student Student2 { get; set; }
    }
}