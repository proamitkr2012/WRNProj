using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdmissionUI.Helpers
{
    public class ResultFilterZone : Attribute, IActionFilter, IResultFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {

           
            //filterContext.Controller.ViewBag.OnActionExecuted = "IActionFilter.OnActionExecuted filter called";
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.Controller.ViewBag.OnActionExecuting = "IActionFilter.OnActionExecuting filter called";
        }

        void IResultFilter.OnResultExecuted(ResultExecutedContext filterContext)
        {
            //filterContext.Controller.ViewBag.OnResultExecuted = "IResultFilter.OnResultExecuted filter called";
        }

        void IResultFilter.OnResultExecuting(ResultExecutingContext filterContext)
        {
            //filterContext.Controller.ViewBag.OnResultExecuting = "IResultFilter.OnResultExecuting filter called";
        }        
    }
}