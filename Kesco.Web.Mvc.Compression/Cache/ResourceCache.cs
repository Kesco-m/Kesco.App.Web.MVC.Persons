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
using System.Web.Caching;

namespace Kesco.Web.Mvc.Compression.Cache
{
    /// <summary>
    /// ResourceCache manages the web cache of composite resources
    /// </summary>
    public static class ResourceCache
    {
		
		/*static void cacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration) {
			expensiveObject = null;
			dependency = null;
			absoluteExpiration = System.Web.Caching.Cache.NoAbsoluteExpiration;
			slidingExpiration = System.Web.Caching.Cache.NoSlidingExpiration;
			Kesco.Logger.WriteEx(new Kesco.Log.LogicalException(string.Format("Delete item from cache: key = {0}, reason = {1}", key, reason),"", System.Reflection.Assembly.GetExecutingAssembly().GetName()));
		}*/

        /// <summary>
        /// Caches are resource
        /// </summary>
        /// <param name="key">key to cache resource with</param>
        /// <param name="resource">resource string</param>
        /// <param name="context">http context base to use for caching</param>
        public static void CacheResource(string key, string resource, HttpContextBase context)
        {
            if (context.Cache[key] != null)
            {
                context.Cache[key] = resource;
            }
            else
            {
                //add to cache no expiration or dependency
				context.Cache.Insert(
						key, resource, null,
						System.Web.Caching.Cache.NoAbsoluteExpiration,
						System.Web.Caching.Cache.NoSlidingExpiration//, cacheItemUpdateCallback
					);
            }

			var key2 = key + "$eTag";
			var eTag = String.Format("{0:X8}", resource.GetHashCode());
			if (context.Cache[key2] != null) {
				context.Cache[key2] = eTag;
			} else {
				//add to cache no expiration or dependency
				context.Cache.Insert(
						key2, eTag, null,
						System.Web.Caching.Cache.NoAbsoluteExpiration,
						System.Web.Caching.Cache.NoSlidingExpiration//, cacheItemUpdateCallback
					);
			}
		}

        /// <summary>
        /// Gets a cached resource
        /// </summary>
        /// <param name="key">key to get resource from cache</param>
        /// <param name="context">Http context to use to get cache</param>
        /// <returns>resource string</returns>
        public static string GetCachedResource(string key, HttpContext context)
        {
            if (context.Cache[key] != null)
            {
                return context.Cache[key].ToString();
            }
            else
            {

				throw new ArgumentException(string.Format("Cached resource for key {0} not found: EffectivePrivateBytesLimit = {1}, EffectivePercentagePhysicalMemoryLimit = {2}", key, context.Cache.EffectivePrivateBytesLimit, context.Cache.EffectivePercentagePhysicalMemoryLimit));
            }
        }

		public static string GetCachedResourceETag(string key, HttpContext context)
		{
			var key2 = key + "$eTag";
			if (context.Cache[key2] != null) {
				return context.Cache[key2].ToString();
			} else {

				throw new ArgumentException(string.Format("Cached resource ETag for key {0} not found", key));
			}
		}
		
		/// <summary>
        /// Checks if the resource key exists in the cache
        /// </summary>
        /// <param name="key">key to check if exists</param>
        /// <param name="context">HttpContextBase to use to check</param>
        /// <returns>true if exists</returns>
        public static bool ExistsInCache(string key, HttpContextBase context)
        {
            return context.Cache[key] != null;
        }
    }
}
