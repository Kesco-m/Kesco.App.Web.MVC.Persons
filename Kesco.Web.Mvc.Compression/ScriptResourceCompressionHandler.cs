/*
* Do Not Remove
* Author: Douglas Branca http://www.linkedin.com/in/douglasbranca
* CodePlex: https://mvcscriptcompression.codeplex.com 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Web.Mvc.Compression.Cache;
using Kesco.Web.Mvc.Compression.Configuration;
using Kesco.Web.Mvc.Compression.Resource;

namespace Kesco.Web.Mvc.Compression
{
    /// <summary>
    /// Http Handler handles composite js, css resource requests. The handler will look in the cache for composite resources
    /// If no resource is found the handler will attempt to load and compress/cache the resource then return it
    /// If compression is disabled the handler will return the requested file uncompressed.
    /// </summary>
    public class ScriptResourceCompressionHandler : IHttpHandler
    {

        #region IHttpHandler Members

        /// <summary>
        /// Gets if the httphandler can be created once
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Handler returns JS script file compressed, non compressed or the cached composite/noncomposite file
        /// </summary>
        /// <param name="context">Current http context</param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {

                context.Response.ContentType = (context.Request.CurrentExecutionFilePathExtension == ".css") ? "text/css" : "text/javascript";

                //return the requested file
                if (!CompositeResourceSettings.CompressionEnabled(context))
                {
                    //return single file without looking in the cache
					context.Response.Write(GetNonCompressedResource(context.Request.PhysicalPath, context.Request.RawUrl, context.Response.ContentType, context)); ;
                }
                else
                {
                    context.Response.Clear();
                    context.Response.Cache.SetCacheability(HttpCacheability.Private);

                    if (CompositeResourceSettings.SetClientCacheExpire)
                    {
                        context.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(CompositeResourceSection.Instance.ClientCacheDays));
                    }
                    else
                    {
                        //don't expire cache
                        context.Response.Cache.SetExpires(DateTime.UtcNow.AddYears(1));
                    }

                    string filename = VirtualPathUtility.GetFileName(context.Request.FilePath);

					string body = GetAndCompressSingleResource(context.Request.PhysicalPath, context.Request.RawUrl, context.Response.ContentType, context);
                    

                    context.Response.Write(body);
                }
            }
            catch (Exception ex)
            {
                Kesco.Lib.Log.Logger.WriteEx(
						new Kesco.Lib.Log.DetailedException("Compression Module Request Processing Exception", ex, context.Request.RawUrl)
					);
				context.Response.ContentType = "text/plain";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.Write(ex.Message);
            }

        }

        #endregion

        /// <summary>
        /// Create a resource string and resolve CSS Urls
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contentType"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetNonCompressedResource(string path, string url, string contentType, HttpContext context)
        {
            ResourceType type = (contentType == "text/javascript") ? ResourceType.JS : ResourceType.CSS;

            ResourceBuilder builder = new ResourceBuilder();
			return builder.CreateResource(path, url, type, true, false);
        }

        /// <summary>
        /// Gets a compressed javascript file from the cache, if the file doesn't exist it get compressed and added to the cache
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="contentType">content mime type</param>
        /// <param name="context">current http conext</param>
        /// <returns>compressed script string</returns>
        private string GetAndCompressSingleResource(string path, string url, string contentType, HttpContext context)
        {
            string body = "";

            //check cache first
            if (context.Cache[path] != null)
            {
                body = context.Cache[path].ToString();
            }
            else
            {

                ResourceType type = (contentType == "text/javascript") ? ResourceType.JS : ResourceType.CSS;

                ResourceBuilder builder = new ResourceBuilder();
                body = builder.CreateResource(path, url, type);

                //insert compressed file into cached
                CacheDependency cd = new CacheDependency(path);
				context.Cache.Insert(path, body, cd, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
            }

            return body;
        }


    }
}
