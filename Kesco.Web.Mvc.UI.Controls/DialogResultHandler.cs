using System;
using System.Web;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesco.Web.Mvc.UI.Controls
{
    class DialogResultHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var version = context.Request.Form["version"] ?? context.Request.QueryString["version"];

            if (!string.IsNullOrEmpty(version))
                ProcessRequestVersion(context);
        }

        private void ProcessRequestVersion(HttpContext context)
        {
            var rs = context.Response;

            rs.StatusCode = 200;
            rs.ContentType = "application/json";

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(Assembly.GetExecutingAssembly().GetName().Version);
            rs.Write(json);

            rs.Flush();
            rs.SuppressContent = true;
            context.ApplicationInstance.CompleteRequest();
        }

        /// <summary>
        ///     Может ли другой запрос использовать экземпляр класса
        /// </summary>
        public bool IsReusable => false;


    }
}
