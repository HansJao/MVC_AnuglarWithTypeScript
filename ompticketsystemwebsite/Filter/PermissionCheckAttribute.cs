using System.Web.Mvc;

namespace ompticketsystemwebsite.Filter
{
    public class PermissionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //未登入導回登入頁
            if (filterContext.HttpContext.Session["LoginData"] == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Index/");
                filterContext.Controller.TempData.Add("Error", "尚未登入");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}