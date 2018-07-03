using DayBook.JobsScheduler;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DayBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Scheduler.Start();
        }
    }
}
