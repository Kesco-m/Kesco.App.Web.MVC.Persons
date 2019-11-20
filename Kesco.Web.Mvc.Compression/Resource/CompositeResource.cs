/*
* Do Not Remove
* Author: Douglas Branca http://www.linkedin.com/in/douglasbranca
* CodePlex: https://mvcscriptcompression.codeplex.com 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.Compression.Resource
{
    /// <summary>
    /// Composite Resource Model, holds data from the Composite Resource schema
    /// </summary>
    public class CompositeResource
    {
        /// <summary>
        /// Get or sets the Composite Resource Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Composite Resource type
        /// </summary>
        public ResourceType type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list of virtual script paths for the composite resource
        /// </summary>
        public List<string> Scripts
        {
            get;
            set;
        }
    }
}
