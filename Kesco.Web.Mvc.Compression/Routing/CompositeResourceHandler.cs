/*
* Do Not Remove
* Author: Douglas Branca http://www.linkedin.com/in/douglasbranca
* CodePlex: https://mvcscriptcompression.codeplex.com 
*/

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using Kesco.Web.Mvc.Compression.Cache;
using Kesco.Web.Mvc.Compression.Configuration;
using Kesco.Web.Mvc.Compression.Extensions;
using Kesco.Web.Mvc.Compression.Resource;

namespace Kesco.Web.Mvc.Compression.Routing
{
    /// <summary>
    /// MVC CompositeResource HttpHandler, returns the requested resource.
    /// Can only be used with the CompositeResourceRouteHandler, can not be used standalone
    /// </summary>
    public class CompositeResourceHandler : IHttpHandler
    {
        private string _name;
        private ResourceType _type;

        /// <summary>
        /// Constructor takes composites resource key and type to return from route data
        /// </summary>
        /// <param name="name">Composite Resource Name</param>
        /// <param name="type">Composite resource type, css or js</param>
        public CompositeResourceHandler(string name, string type)
        {
            this._name = name;
            this._type = (ResourceType)Enum.Parse(typeof(ResourceType), type, true);
        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Returns the cached resource
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
			var response = context.Response;
			var request = context.Request;
			var cachePolicy = response.Cache;
            try
            {
				response.ContentType = (this._type == ResourceType.CSS) ? "text/css" : "text/javascript";

                response.Clear();
				cachePolicy.SetCacheability(HttpCacheability.ServerAndPrivate);
                if (CompositeResourceSettings.SetClientCacheExpire)
                {
					cachePolicy.SetExpires(DateTime.UtcNow.AddDays(CompositeResourceSection.Instance.ClientCacheDays));
                }
                else
                {
                    //don't expire cache
					cachePolicy.SetExpires(DateTime.UtcNow.AddYears(1));
                }

				if (CompositeResourceSettings.SetClientCacheExpire) {
					string eTag = ResourceCache.GetCachedResourceETag(this._name, context);
					if (!String.IsNullOrEmpty(request.Headers["If-None-Match"])
						&& request.Headers["If-None-Match"] == eTag) {
						response.StatusCode = 304;
						response.StatusDescription = "Not Modified";
						//response.End();
						return;
					}
					response.Cache.SetETag(eTag);
				}
				string body = ResourceCache.GetCachedResource(this._name, context);
				response.Write(body);
				string acceptEncoding = (request.Headers["Accept-Encoding"] ?? String.Empty).ToUpperInvariant();
				if (acceptEncoding.Contains("GZIP")) {
					response.AppendHeader("Content-encoding", "gzip");
					response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
				} else if (acceptEncoding.Contains("DEFLATE")) {
					response.AppendHeader("Content-encoding", "deflate");
					response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
				}
            }
            catch (Exception ex)
            {
				Kesco.Lib.Log.Logger.WriteEx(
						new Kesco.Lib.Log.DetailedException("Compression Module Request Processing Exception", ex, context.Request.RawUrl)
					);
                response.ContentType = "text/plain";
				cachePolicy.SetCacheability(HttpCacheability.NoCache);
                response.Write(ex.Message);
            }
        }

        #endregion
    }
}
