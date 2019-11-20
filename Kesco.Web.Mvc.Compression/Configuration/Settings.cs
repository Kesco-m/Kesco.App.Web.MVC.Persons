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
using System.Reflection;

namespace Kesco.Web.Mvc.Compression.Configuration
{
    public static class CompositeResourceSettings
    {

        /// <summary>
        /// Gets if the composite resource file has been configured
        /// </summary>
        public static bool CompositeResourceFileConfigured
        {
            get
            {
                return !string.IsNullOrEmpty(CompositeResourceSection.Instance.DefinedCompositeResourcePath);
            }

        }

        /// <summary>
        /// Checks if scripts should be compressed, if uncompress cookie is specified scripts are sent uncompressed
        /// </summary>
        /// <param name="context">httpcontext containing request context</param>
        /// <returns>true - to compress, false - to not compress</returns>
        public static bool CompressionEnabled(HttpContextBase context)
        {
            if (CompositeResourceSection.Instance.Enabled && context.Request.Cookies["UncompressScripts"] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Checks if scripts should be compressed, if uncompress cookie is specified scripts are sent uncompressed
        /// </summary>
        /// <param name="context">httpcontext containing request context</param>
        /// <returns>true - to compress, false - to not compress</returns>
        public static bool CompressionEnabled(HttpContext context)
        {
            if (CompositeResourceSection.Instance.Enabled && context.Request.Cookies["UncompressScripts"] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Version to use on urls if specified
        /// </summary>
        public static string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Gets if client cache expiration set
        /// </summary>
        public static bool SetClientCacheExpire
        {
            get
            {
                return (CompositeResourceSection.Instance.ClientCacheDays > 0);
            }
        }
    }
}
