using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Kesco.Persons.BusinessLogic.DataAccess;

namespace Kesco.Persons.Web.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class PersonTypesAsyncController : AsyncController
    {
        public ActionResult SavePersonTypes(string personID, string typeIDs, int sectionID)
        {
            PersonAccessor.Accessor.SavePersonTypes(Convert.ToInt32(personID), typeIDs);
            return JavaScript(String.Format(@"
                 if($('#loading-panel') != null) {{
                $('#loading-panel')[0].style.display = 'none'; 
                var  hrefs= $('#sect{0} a'); 
                for(i = 0; i < hrefs.length; i++) {{  hrefs[i].disabled = false; }}
                if( $('#sect{0} .sectionRefresh') != null){{
                $('#sect{0} .sectionRefresh')[0].style.display = ''; }}
                refreshSection({0}, false, true); }}
                
                if($('#loading-panel') == null || $('#sect{0} .sectionRefresh') == null){{
                    window.location.href = location.href;
                }}"
                , sectionID));
        }

    }
}
