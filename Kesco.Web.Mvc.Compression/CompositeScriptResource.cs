/*
* Do Not Remove
* Author: Douglas Branca http://www.linkedin.com/in/douglasbranca
* CodePlex: https://mvcscriptcompression.codeplex.com 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kesco.Web.Mvc.Compression.Cache;
using Kesco.Web.Mvc.Compression.Configuration;
using Kesco.Web.Mvc.Compression.Resource;

namespace Kesco.Web.Mvc.Compression
{
    /// <summary>
    /// Composite resource extension adds provides script compression, combining and caching
    /// </summary>
    public static class CompositeScriptResource
    {
        /// <summary>
        /// Creates a composite resource from a list of URLs
        /// </summary>
        /// <param name="helper">the view helper for rendering tags, passed by default</param>
        /// <param name="scripts">list of scripts to compresss and combine</param>
        /// <param name="Name">Name of resource will only cache the first resource request with the specified name</param>
        /// <param name="type">type of resource css, javascript</param>
        /// <returns></returns>
        public static string CompositeScriptResource_CompositeScript(
            this HtmlHelper helper,
            List<string> scripts,
            string Name,
            ResourceType type,
			string clientID = "")
        {
            HttpContextBase context = helper.ViewContext.RequestContext.HttpContext;

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            if (CompositeResourceSettings.CompressionEnabled(context))
            {

                BuildCompositeResource(scripts, Name, type, context);

				var url = urlHelper.Content("~/") + CompositeResourceSection.Instance.ResourceCompositionAction + Name + "/" + type.ToString() +
                    (!string.IsNullOrEmpty(CompositeResourceSettings.Version) ? "?v=" + CompositeResourceSettings.Version : string.Empty);

                //return compressed script tag
                return CreateTag(url, type, clientID);
            }
            else
            {
                //return non compressed script tags
				return BuildNonCompressedSript(scripts, urlHelper, type, clientID);
            }
        
        }



		/// <summary>
		/// Creates a script tag with link to pre-defined composite script resource
		/// </summary>
		/// <param name="helper">html helper</param>
		/// <param name="Name">Name of the composite script</param>
		/// <param name="clientID">ID of client script tag.</param>
		/// <returns>
		/// script tag for composite resource
		/// </returns>
        public static string CompositeScriptResource_DefinedCompositeScriptResource(
            this HtmlHelper helper,
            string Name, string clientID = "")
        {
            HttpContextBase context = helper.ViewContext.RequestContext.HttpContext;

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            CompositeScriptResourceDefinition csrdInstance = CompositeScriptResourceDefinition.GetInstance(context);
            Resource.CompositeResource resource = csrdInstance[Name];


            if (CompositeResourceSettings.CompressionEnabled(context))
            {

                BuildCompositeResource(resource.Scripts, resource.Name, resource.type, context);

                var url = urlHelper.Content("~/") + "CompositeScriptResource/getresource/" + Name + "/" + resource.type.ToString() +
                    (!string.IsNullOrEmpty(CompositeResourceSettings.Version) ? "?v=" + CompositeResourceSettings.Version : string.Empty);

                //return compressed script tag
				return CreateTag(url, resource.type, clientID);
            }
            else
            {
                //return non compressed script tags
                return BuildNonCompressedSript(resource.Scripts, urlHelper, resource.type, clientID);
            }
        }

        /// <summary>
        /// Renders a standard script tag for the file, appends compression configuration version string if sepecified
        /// to force recache of new versions
        /// </summary>
        /// <param name="helper">page url helper</param>
        /// <param name="script">virtual path to script</param>
        /// <returns>script tags</returns>
        public static string CompositeScriptResource_RenderScriptTag(this HtmlHelper helper,
            string script)
        {
            HttpContextBase context = helper.ViewContext.RequestContext.HttpContext;

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            ResourceType type = ResourceType.JS;

            if (script.ToLower().Contains(".css"))
            {
                type = ResourceType.CSS;
            }

            var url = urlHelper.Content(script) + (!string.IsNullOrEmpty(CompositeResourceSettings.Version) ? "?v=" + CompositeResourceSettings.Version : string.Empty);

            return CreateTag(url, type);
        }

		/// <summary>
		/// Renders a standard script tag for the file, appends compression configuration version string if sepecified
		/// to force recache of new versions
		/// </summary>
		/// <param name="helper">page url helper</param>
		/// <param name="script">virtual path to script</param>
		/// <returns>script tags</returns>
		public static string CompositeScriptResource_RenderScriptTagEx(this HtmlHelper helper,
			string script, string clientID)
		{
			HttpContextBase context = helper.ViewContext.RequestContext.HttpContext;

			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			ResourceType type = ResourceType.JS;

			if (script.ToLower().Contains(".css")) {
				type = ResourceType.CSS;
			}

			var url = urlHelper.Content(script) + (!string.IsNullOrEmpty(CompositeResourceSettings.Version) ? "?v=" + CompositeResourceSettings.Version : string.Empty);

			return CreateTag(url, type, clientID);
		}



        /// <summary>
        /// Builds a composite resource and caches it
        /// </summary>
        /// <param name="scripts">list of script to compress together</param>
        /// <param name="Name">Name, key in cache</param>
        /// <param name="type">type of scripts css, js</param>
        /// <param name="context">current http context</param>
        private static void BuildCompositeResource(List<string> scripts, string Name, ResourceType type, HttpContextBase context)
        {
            //only cache the resource if it doesn't exist in the cache
            if (!ResourceCache.ExistsInCache(Name, context))
            {
                ResourceBuilder resourcebuilder = new ResourceBuilder(context);

                string resouce = resourcebuilder.CreateCompositeResource(scripts, type);

                ResourceCache.CacheResource(Name, resouce, context);
            }
        }

        /// <summary>
        /// Returns a list of script tags for all resource passed in, used when compression is disabled
        /// </summary>
        /// <param name="scripts">list of scripts to build</param>
        /// <param name="helper">url helper</param>
        /// <param name="type">type of scripts to build, determines tag</param>
        /// <returns>script tags</returns>
        private static string BuildNonCompressedSript(List<string> scripts, UrlHelper helper, ResourceType type, string clientID = "")
        {
            StringBuilder tags = new StringBuilder();

            scripts.ForEach(scr => {

				tags.Append(CreateTag(helper.Content(scr), type, clientID));
            });

            return tags.ToString();
        }

		/// <summary>
		/// Creates a tag for the passed url
		/// </summary>
		/// <param name="url">Url to create tag for</param>
		/// <param name="type">type js or css</param>
		/// <param name="clientID">The client ID of script.</param>
		/// <returns>
		/// script tag
		/// </returns>
		/// <exception cref="System.ArgumentException">Unknown Resource Type</exception>
        private static string CreateTag(string url, ResourceType type, string clientID = "")
        {
            switch(type)
            {
                case ResourceType.CSS:
                    TagBuilder csstag = new TagBuilder("link");
					if (!String.IsNullOrEmpty(clientID))
						csstag.MergeAttribute("id", clientID);
                    csstag.MergeAttribute("type", "text/css");
                    csstag.MergeAttribute("href", url);
                    csstag.MergeAttribute("rel", "stylesheet");

                    return csstag.ToString(TagRenderMode.Normal);
        

                case ResourceType.JS:
                     TagBuilder scripttag = new TagBuilder("script");
					if (!String.IsNullOrEmpty(clientID))
						scripttag.MergeAttribute("id", clientID);
                    scripttag.MergeAttribute("type", "text/javascript");
                    scripttag.MergeAttribute("src", url);

                    return scripttag.ToString(TagRenderMode.Normal);
                    

                default:
                    throw new ArgumentException("Unknown Resource Type");
            }
        }
    }


    
}
