using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BooksEditor.Services.Models;

namespace BooksEditor.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                ActionResultModel actionResult = new ActionResultModel();
                actionResult.IsSuccess = false;

                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                    {
                        actionResult.Errors.Add(state.Errors.First().ErrorMessage);
                    }
                }

                context.Response = context.Request.CreateResponse(actionResult);
            }
        }
    }
}