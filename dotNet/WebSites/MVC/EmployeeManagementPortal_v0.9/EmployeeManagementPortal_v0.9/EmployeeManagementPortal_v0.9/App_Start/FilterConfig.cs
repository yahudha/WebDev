using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementPortal_v0._8
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
