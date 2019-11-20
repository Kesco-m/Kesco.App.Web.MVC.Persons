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
using Kesco.Web.Mvc.Compression.Minifiers;
using Kesco.Web.Mvc.Compression.Compression;
using System.Text.RegularExpressions;

namespace Kesco.Web.Mvc.Compression.Resource
{
    /// <summary>
    /// Builds composite resources, addes composite resources to teh cache
    /// </summary>
    public class ResourceBuilder
    {
        private HttpContextBase _context = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public ResourceBuilder()
        {

        }

        /// <summary>
        /// Constructor that must be used in order to cache composite resources
        /// </summary>
        /// <param name="context">http context to use</param>
        public ResourceBuilder(HttpContextBase context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a composite resource from a list of resources
        /// </summary>
        /// <param name="urls">List of virtual script urls</param>
        /// <param name="resoucetype">Resource type, js or css</param>
        /// <returns>compressed combined resource string</returns>
        public string CreateCompositeResource(List<string> urls, ResourceType resoucetype)
        {
            StringBuilder resource = new StringBuilder();

            urls.ForEach(x =>
            {
                string path = _context.Server.MapPath(x);

                resource.Append(CreateResource(path, x, resoucetype));
               
            });

            return resource.ToString();
        }

        public string CreateResource(string filePath, string url, ResourceType type)
        {
			return CreateResource(filePath, url, type, true, true);
        }

        /// <summary>
        /// Creates a compressed resource
        /// </summary>
        /// <param name="filePath">server file path to resource</param>
        /// <param name="type">Type of resource, js or css</param>
        /// <returns>compressed resource body</returns>
        public string CreateResource(string filePath, string url, ResourceType type, bool resolveCssURLs, bool compress)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("Requested file not found {0}", filePath));
            }

            string resource = "";
     

            using (var sr = new StreamReader(filePath))
            {
                resource = sr.ReadToEnd();
            }

			if (resolveCssURLs && type == ResourceType.CSS)
            {
				resource = ResolveCSSURLs(resource, url);
            }

            if (compress)
            {
                switch (type)
                {
                    case ResourceType.JS:
                        resource = CompressJS(resource);
                        break;

                    case ResourceType.CSS:
                        resource = CompressCss(resource);
                        break;

                    default:
                        throw new ArgumentException("Unknown Resource Type");
                }
            }


            return resource;
        }

		/// <summary>
		/// Resolves all CSSURLs specifed by ~/ to full URL path for the site
		/// </summary>
		/// <param name="uncompressedCSS">The uncompressed CSS.</param>
		/// <param name="resourceUrl">The resource URL.</param>
		/// <returns></returns>
		public string ResolveCSSURLs(string uncompressedCSS, string resourceUrl)
        {
			
			// Заменим пути для ссылок вида
			// url("images/ui-bg_glass_55_fbf9ee_1x400.png")
			// url(images/ui-bg_glass_55_fbf9ee_1x400.png)
			var myRegex = new Regex(@"url\(([""]?)([^~^""].+)\)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
			var replacement = String.Format(@"url($1{0}$2)", VirtualPathUtility.GetDirectory(resourceUrl));
			uncompressedCSS = myRegex.Replace(
					uncompressedCSS,
					replacement
				); 

			// Заменим пути для ссылок вида
			// url(~/images/ui-bg_glass_55_fbf9ee_1x400.png)
			// url("~/images/ui-bg_glass_55_fbf9ee_1x400.png")
            string path = VirtualPathUtility.ToAbsolute("~/");
			uncompressedCSS = uncompressedCSS.Replace("~/", path);

            return uncompressedCSS;
        }

        /// <summary>
        /// Minifies JS file
        /// </summary>
        /// <param name="js">string of javascript to minify</param>
        /// <returns>minified string of js</returns>
        public string CompressJS(string js)
        {
            ICompress min = new JSMinify();
			return min.Compress(js);
        }

        /// <summary>
        /// Minifies CSS file
        /// </summary>
        /// <param name="css">css string to minify</param>
        /// <returns>minified css string</returns>
        public string CompressCss(string css)
        {
            ICompress min = new CSSMinify();
            return min.Compress(css);
        }
    }
}
