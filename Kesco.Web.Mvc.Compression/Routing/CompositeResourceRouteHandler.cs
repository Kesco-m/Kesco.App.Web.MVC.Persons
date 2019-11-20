/*
* Do Not Remove
* Author: Douglas Branca http://www.linkedin.com/in/douglasbranca
* CodePlex: https://mvcscriptcompression.codeplex.com 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Kesco.Web.Mvc.Compression.Routing
{
    public class CompositeResourceRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string name = (string)requestContext.RouteData.Values["name"];
            string type = (string)requestContext.RouteData.Values["type"];

            return new CompositeResourceHandler(name, type);
        }

        #endregion
    }
}
