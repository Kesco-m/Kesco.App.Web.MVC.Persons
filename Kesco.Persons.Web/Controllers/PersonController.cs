using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.BusinessLogic.Persons;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Web.Controllers
{
	public partial class PersonController : ControllerEx
	{

		public PersonController()
			: base()
		{
			UseCompressHtml = true;
		}

		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}


		public virtual ActionResult Index(int? id)
		{
			var viewName = "Index";

			if (IsAjaxRequest) 
                viewName += "Ajax";
             
			return View(viewName, new PersonViewModel(id ?? 0));
		}

		public virtual ActionResult CreateIndividual()
        {
            return View(new IndividualViewModel());
        }

        public class NewIndividualForSaveModel
        {
			public PersonCardNatural Card { get; set; }
			public List<Kesco.Employees.ObjectModel.Employee> ResponsibleEmployees { get; set; }
            public List<int> PersonTypeIDs { get; set; }
            public bool Confirmed { get; set; }
        }


        public class IndividualPersonTypeIDModel
        {
            public List<int> PersonTypeIDs { get; set; }
        }


        public class IndividualPersonThemesModel
        {
            public List<PersonTheme> PersonThemes { get; set; }
        }

        public virtual ActionResult CreateNewIndividual(NewIndividualForSaveModel model)
        {
			try {
                model.Card.Person.INN = model.Card.Person.INN == null || model.Card.Person.INN == "null" ? string.Empty : model.Card.Person.INN;
                model.Card.Person.OGRN = model.Card.Person.OGRN == null || model.Card.Person.OGRN == "null" ? string.Empty : model.Card.Person.OGRN;
                model.Card.Person.OKPO = model.Card.Person.OKPO == null || model.Card.Person.OKPO == "null" ? string.Empty : model.Card.Person.OKPO;
                model.Card.OKONH = model.Card.OKONH == null || model.Card.OKONH == "null" ? string.Empty : model.Card.OKONH;
                model.Card.OKVED = model.Card.OKVED == null || model.Card.OKVED == "null" ? string.Empty : model.Card.OKVED;
                model.Card.KPP = model.Card.KPP == null || model.Card.KPP == "null" ? string.Empty : model.Card.KPP;
                model.Card.RwID = model.Card.RwID == null || model.Card.RwID == "null" ? string.Empty : model.Card.RwID;
                model.Card.AddressLegal = model.Card.AddressLegal == null || model.Card.AddressLegal == "null" ? string.Empty : model.Card.AddressLegal;
                model.Card.AddressLegalLat = model.Card.AddressLegalLat == null || model.Card.AddressLegalLat == "null" ? string.Empty : model.Card.AddressLegalLat;
                model.Card.Person.Comment = model.Card.Person.Comment == null || model.Card.Person.Comment == "null" ? string.Empty : model.Card.Person.Comment;

                model.Card.MiddleNameLat = model.Card.MiddleNameLat == null || model.Card.MiddleNameLat == "null" ? string.Empty : model.Card.MiddleNameLat;
                model.Card.MiddleNameRus = model.Card.MiddleNameRus == null || model.Card.MiddleNameRus == "null" ? string.Empty : model.Card.MiddleNameRus;

                model.Card.LastNameLat = model.Card.LastNameLat == null || model.Card.LastNameLat == "null" ? string.Empty : model.Card.LastNameLat;
                model.Card.LastNameRus = model.Card.LastNameRus == null || model.Card.LastNameRus == "null" ? string.Empty : model.Card.LastNameRus;

                model.Card.FirstNameLat = model.Card.FirstNameLat == null || model.Card.FirstNameLat == "null" ? string.Empty : model.Card.FirstNameLat;
                model.Card.FirstNameRus = model.Card.FirstNameRus == null || model.Card.FirstNameRus == "null" ? string.Empty : model.Card.FirstNameRus;

                model.Card.FIORus = model.Card.FIORus == null || model.Card.FIORus == "null" ? null : model.Card.FIORus;
                model.Card.IOFRus = model.Card.IOFRus == null || model.Card.IOFRus == "null" ? null : model.Card.IOFRus;

                model.Card.IncorporationFormID = model.Card.IncorporationFormID == 0
                                                     ? null
                                                     : model.Card.IncorporationFormID;

                model.Card.Person.BusinessProjectID = model.Card.Person.BusinessProjectID == 0
                                                          ? null
                                                          : model.Card.Person.BusinessProjectID;

                model.Card.Person.TerritoryID = model.Card.Person.TerritoryID == 0
                                                          ? null
                                                          : model.Card.Person.TerritoryID;

                // сохранение лица
                var personID = Repository.Persons.SaveCard(model.Card, model.Confirmed);

                // сохранение типов лиц
                if (model.PersonTypeIDs != null)
                {
                    foreach (var personTypeID in model.PersonTypeIDs)
                    {
                        Repository.Persons.AssignPersonTypeToPerson(new PersonAccessor.PersonTypeForSave
                                                                                     {
                                                                                         WhatDo = 1,
                                                                                         КодЛица = personID,
                                                                                         КодТипаЛица = personTypeID
                                                                                     });
                    }
                }

			    return JsonModel(new { PersonID = personID, Name = model.Card.Person.Nickname }, JsonRequestBehavior.AllowGet);
            } catch (DuplicateNicknameException duplicateNicknameException)
            {
                return JsonError(duplicateNicknameException.Message, duplicateNicknameException.Ids, JsonRequestBehavior.AllowGet);

            } catch (SavePersonException siex) {
				return JsonError(siex.Message, siex.Issues, JsonRequestBehavior.AllowGet);

			} catch (Exception ex) {
				Kesco.Lib.Log.Logger.WriteEx(ex);

				return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
			}
        }


        public class ThemeCatalog
        {
            public string Theme { get; set; }
            public string Catalog { get; set; }
        }


        /// <summary>
        /// Поставляет во вьюмодель массив наименований каталогов в соответствии с выбранными типами лиц
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual ActionResult SetPersonTypeIDs(IndividualPersonTypeIDModel model)
        {
            var sc = new StringCollection();

            var themeCatalogList = new List<ThemeCatalog>();

            for (var index = 0; index < model.PersonTypeIDs.Count; index++)
            {
                var pt = Repository.PersonTypes.GetInstance(model.PersonTypeIDs.ToArray()[index]);

                if (pt == null) continue;
                var c = Repository.Catalogs.GetInstance(pt.CatalogID);

                if (c == null) continue;
                if (!sc.Contains(c.CatalogName))
                    sc.Add(c.CatalogName);

                var themeCatalog = new ThemeCatalog
                                       {
                                           Catalog = c.CatalogName,
                                           Theme =
                                               Repository.PersonThemes.GetInstance(pt.PersonThemeID).Name
                                       };

                themeCatalogList.Add(themeCatalog);
            }

            return JsonModel(new { Catalogs = sc, ThemeCatalogs = themeCatalogList }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Поставляет во вьюмодель список ид-шников типов, параметр надо ли уточнять этот список и список наименований каталогов в соответствии с выбранными темами
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual ActionResult PostPersonThemes(IndividualPersonThemesModel model)
        {
            /*
                    (сервер) по темам набираем типы
                    (сервер) если хотя бы для одной темы есть два типа (имеется двусмысленность), то параметр "надо ли уточнять" = 1, иначе 0.
                    (сервер) возвращаем модель с параметром надо ли уточнять, списком ид-шников типов и список имен каталогов.
             */
            var personTypes = new List<PersonType>();

            var needToClarify = false;

            foreach (var personTypesByThemeID in model.PersonThemes.Select(t => Repository.PersonTypes.GetListByThemeId(t.ID)).Where(personTypesByThemeID => personTypesByThemeID != null))
            {
                if (personTypesByThemeID.Count > 1)
                    needToClarify = true;

                personTypes.AddRange(personTypesByThemeID);
            }

            var sc = new StringCollection();

            var themeCatalogList = new List<ThemeCatalog>();

            foreach (var personTheme in model.PersonThemes)
            {
                var personTypeList = Repository.PersonTypes.GetListByThemeId(personTheme.ID);
                var tc = new ThemeCatalog();
                tc.Theme = personTheme.Name;

                if (personTypeList.Count > 0)
                    tc.Catalog = Repository.Catalogs.GetInstance(personTypeList[0].CatalogID).CatalogName;

                themeCatalogList.Add(tc);
            }

            foreach (
                var c in
                    personTypes.Select(t => Repository.Catalogs.GetInstance(t.CatalogID)).Where(
                        c => c != null).Where(c => !sc.Contains(c.CatalogName)))
                sc.Add(c.CatalogName);

            return JsonModel(new { NeedToClarify = needToClarify, PersonTypes = personTypes.ToArray(), Catalogs = sc, ThemeCatalogs = themeCatalogList }, JsonRequestBehavior.AllowGet);
        }


	    public virtual ActionResult CreateJuridical()
        {
            return View(new Kesco.Persons.Web.Models.Juridicals.ViewModel());
        }

		public virtual ActionResult CreateRequisites()
        {
            return View(new RequisitesViewModel());
        }

		public virtual ActionResult Territory(int? id)
		{
			try {
				var model = Kesco.Territories.BusinessLogic.Repository.Territories.GetInstance(id ?? 0);
				return JsonModel(model, JsonRequestBehavior.AllowGet);
			} catch (Exception ex) {
				Kesco.Lib.Log.Logger.WriteEx(ex);
				return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
			}
		}

	}
}
