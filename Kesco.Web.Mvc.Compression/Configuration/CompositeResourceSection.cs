/*
* Do Not Remove
* Author: Douglas Branca http://www.linkedin.com/in/douglasbranca
* CodePlex: https://mvcscriptcompression.codeplex.com 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace Kesco.Web.Mvc.Compression.Configuration
{
    /// <summary>
    /// Configuration section for the composite resource handler
    /// </summary>
    public class CompositeResourceSection : ConfigurationSection
    {
        private static CompositeResourceSection _instance = (CompositeResourceSection)ConfigurationManager.GetSection("CompositeScriptResourceSection");

        /// <summary>
        /// Configuration Instance
        /// </summary>
        public static CompositeResourceSection Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets if the composite resource plugin is enabled
        /// </summary>
        [ConfigurationProperty("Enabled", IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["Enabled"];
            }
            set
            {
                this["Enabled"] = value;

            }
            
        }

        /// <summary>
        /// Gets the composite resource file path
        /// </summary>
        [ConfigurationProperty("DefinedCompositeResourcePath", IsRequired = false, DefaultValue="")]
        public string DefinedCompositeResourcePath
        {
            get
            {
                return (string)this["DefinedCompositeResourcePath"];
            }
            set
            {
                this["DefinedCompositeResourcePath"] = value;
            }
        }

		/// <summary>
		/// Gets the composite resource file path
		/// </summary>
		[ConfigurationProperty("ResourceCompositionAction", IsRequired = false, DefaultValue = "CompositeScriptResource/getresource/")]
		public string ResourceCompositionAction
		{
			get
			{
				return (string)this["ResourceCompositionAction"];
			}
			set
			{
				this["ResourceCompositionAction"] = value;
			}
		}

		/// <summary>
        /// Number of days to cache script client side
        /// 0 = unlimited
        /// </summary>
        [ConfigurationProperty("ClientCacheDays", IsRequired=false, DefaultValue=0)]
        public int ClientCacheDays
        {
            get
            {
                return (int)this["ClientCacheDays"];
            }
            set
            {
                this["ClientCacheDays"] = value;
            }
        }


    }
}
