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
            var applicationPath = "";

            if (context.Request.ApplicationPath != "/")
            {
                path = path.Remove(path.IndexOf(context.Request.ApplicationPath), context.Request.ApplicationPath.Length);
                applicationPath = context.Request.ApplicationPath;
            }

            context.Response.Redirect(applicationPath + string.Format("/Home/GetPhotoInCertainSize?width={0}&height={1}&path={2}", width, height, path));
        }
    }
}