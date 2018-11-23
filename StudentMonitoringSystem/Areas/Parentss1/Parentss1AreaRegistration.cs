using System.Web.Mvc;

namespace StudentMonitoringSystem.Areas.Parentss1
{
    public class Parentss1AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Parentss1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Parentss1_default",
                "Parentss1/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}