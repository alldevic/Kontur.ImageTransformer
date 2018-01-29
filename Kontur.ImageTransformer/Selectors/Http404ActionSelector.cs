using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using Kontur.ImageTransformer.Controllers;

namespace Kontur.ImageTransformer.Selectors
{
    /// <summary>
    /// Overload for response 400 on bad filter name 
    /// </summary>
    public class Http404ActionSelector : ApiControllerActionSelector
    {
        public Http404ActionSelector()
        {
        }

        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                {
                    throw;
                }

                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Handle404";
                var httpController = new BadRequestController();
                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration,
                    "BadRequest", httpController.GetType());
                decriptor = base.SelectAction(controllerContext);
            }

            return decriptor;
        }
    }
}