using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using System;
using System.Reflection;
using System.ComponentModel;

namespace Kesco.Persons.Web.Models.PersonLink
{
	public class ViewModel : ViewModel<PersonLink>
	{
		/// <summary>
		/// Тип связи. true - от родителя (он фиксирован), false - к потомку (он фиксирован)
		/// </summary>
		public bool ParentLinkType { get; internal set; }

		/// <summary>
		/// Код типа лица родителя - для доп. ограничений в контролах
		/// </summary>
		public int? ParentTypeID { get; internal set; }

		/// <summary>
		/// Код типа лица потомка - для доп. ограничений в контролах
		/// </summary>
		public int? ChildTypeID { get; internal set; }

		public ViewModel()
		{
		}

		public void InitFromLink(int linkId)
		{
			var link = Repository.Links.GetInstance(linkId);
			if (link==null)
				throw new ApplicationException(String.Format(Resources.Resources.Persons_Link_NotFound, linkId));

			Model.ID = link.ID;
			Model.PersonLinkTypeID = link.PersonLinkTypeID;

			Model.From = (link.From == PersonCard.MinFromDate) ? null : (DateTime?) link.From;
			Model.To = (link.To == PersonCard.MaxToDate) ? null : (DateTime?) link.To.AddDays(-1);

			Model.ParentPersonID = link.ParentPersonID;
			Model.ChildPersonID = link.ChildPersonID;
			Model.Description = link.Description;
			Model.Parameter = link.Parameter;
			Model.ChangedBy = link.ChangedBy;
			Model.ChangedDate = link.ChangedDate;

			PersonLinkType linkType = Kesco.Persons.BusinessLogic.Repository.PersonLinkTypes.GetInstance(Model.PersonLinkTypeID);
			if(linkType!=null)
			{
				ParentTypeID = linkType.ParentPersonType;
				ChildTypeID = linkType.ChildPersonType;
			}
		}

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

	}
}