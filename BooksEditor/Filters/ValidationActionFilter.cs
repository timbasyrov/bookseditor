using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BooksEditor.Services.Models;
using System.Net;

namespace BooksEditor.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                ActionResultModel result = new ActionResultModel();
                result.State = ActionResultState.Error;

                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                    {
                        result.Errors.Add(state.Errors.First().ErrorMessage);
                    }
                }

                context.Response = context.Request.CreateResponse((HttpStatusCode)422, result);
            }
        }
    }
}