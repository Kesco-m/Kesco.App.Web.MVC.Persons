using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    public class PersonTypesList : TrackableEntity<PersonType, int>
    {
        /// <summary>
        /// КодТипаЛицаВсписке
        /// </summary>
        public override int ID { get; set; }

        public override string GetInstanceFriendlyName()
        {
            return String.Format("#{0}", GetUniqueID());
        }

        [Kesco.Persons.Controls.ComponentModel.PersonThemeSelect]
        public int PersonThemeID { get; set; }

        public string PersonTypeIDs { get; set; }

        
    }
}
