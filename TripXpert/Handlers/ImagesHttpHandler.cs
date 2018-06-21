using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace TripXpert.Handlers
{
    public class ImagesHttpHandler : IHttpHandler, IReadOnlySessionState
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            var width = context.Request.QueryString["width"];
            var height = context.Request.QueryString["height"];
            var path = context.Request.Path;

            context.Response.Redirect(string.Format("/Home/GetPhotoInCertainSize?width={0}&height={1}&path={2}", width, height, path));
        }
    }
}