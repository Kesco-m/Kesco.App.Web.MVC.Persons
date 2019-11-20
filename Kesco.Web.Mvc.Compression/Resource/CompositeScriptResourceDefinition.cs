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
using System.Xml.Linq;
using Kesco.Web.Mvc.Compression.Configuration;

namespace Kesco.Web.Mvc.Compression.Resource
{
    /// <summary>
    /// Maintains a list of composite resources loaded from a composite resource config
    /// </summary>
    public class CompositeScriptResourceDefinition : List<CompositeResource>
    {

        private static CompositeScriptResourceDefinition _instance = null;

        /// <summary>
        /// Gets the Current instance of the script resource definition
        /// </summary>
        /// <param name="context">HttpContext used to create the instance</param>
        /// <returns>CompositeScriptResourceDefinition instance</returns>
        public static CompositeScriptResourceDefinition GetInstance(HttpContextBase context)
        {
            if(_instance == null || _instance.Count <= 0){
            
                _instance = new CompositeScriptResourceDefinition();
                _instance.LoadCompositeResourceDefinitions(context);
            }

            return _instance;

        }

        /// <summary>
        /// Gets a composite resource by key, ie "Framework"
        /// </summary>
        /// <param name="key">Key for composite resource</param>
        /// <returns>Composite resource</returns>
        public CompositeResource this[string key]
        {
            get
            {
                CompositeResource resource = null;

                resource = this.Where(x => x.Name == key).FirstOrDefault();

                return resource;
            }
        }

        /// <summary>
        /// Loads the composite resources
        /// </summary>
        /// <param name="context">httpcontext used for reading file</param>
        public void LoadCompositeResourceDefinitions(HttpContextBase context)
        {
            string vpath = CompositeResourceSection.Instance.DefinedCompositeResourcePath;

            string filepath = context.Server.MapPath(vpath);

            XDocument doc = XDocument.Load(filepath);

            //select a list of objects from the xml doc
            List<CompositeResource> tempresources = (from el in doc.Element("CompositeScriptResources").Elements("CompositeScriptResource")
                                                 select new CompositeResource()
                                                 {
                                                     Name = el.Attribute("Name").Value,
                                                     type = (ResourceType)Enum.Parse(typeof(ResourceType), el.Attribute("type").Value, true),
                                                     Scripts = (from path in el.Elements("script")
                                                                select path.Attribute("path").Value).ToList()
                                                 }).ToList();
            //cache the resources
            this.Clear();
            this.AddRange(tempresources);

        }
    }
}
