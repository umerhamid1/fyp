using System.Web.Mvc;

namespace StudentMonitoringSystem.Areas.Studentss1
{
    public class Studentss1AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Studentss1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Studentss1_default",
                "Studentss1/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}