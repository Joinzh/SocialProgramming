namespace Telerik.Web.Mvc.Examples
{
    using System.Linq;
    using System.Web.Mvc;
    using Models;

    public partial class GridController : Controller
    {
        public ActionResult HierarchyServerSide()
        {
            return View(new NorthwindDataContext().Employees);
        }
    }
}