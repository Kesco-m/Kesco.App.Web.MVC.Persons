using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using BLToolkit.Mapping;
using BLToolkit.Reflection;
using BLToolkit.TypeBuilder;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.BusinessLogic.Dossier;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.Filtering;
using Kesco.Web.Mvc.UI;
using System.Web;
namespace Kesco.Persons.Web.Models.Dossier
{
	/// <summary>
	/// Класс, содержащий свойства, замапленные на клиентские настройки приложения для текущего пользователя
	/// </summary>
	public class ClientParameters : ClientParametersBase
	{
		/// <summary>
		/// Отображать столбец с изменил/изменено
		/// </summary>
		[Parameter(0)]
		public byte PersonChange { get; set; }

		/// <summary>
		/// Отображать старые реквизиты досье
		/// </summary>
		[Parameter(0)]
		public byte PersonDateOld { get; set; }

        
		[MapIgnore]
		internal virtual string PersonDSections { get { return String.Empty;  } }
	}

	public class ClientParametersJ : ClientParameters
	{
		[Parameter("")]
		[MapField("PersonDSectionsJ")]
		public string PersonDSectionsJ { get; set; }

		internal override string PersonDSections { get { return PersonDSectionsJ; } }

	}

	public class ClientParametersN : ClientParameters
	{
		[Parameter("")]
		[MapField("PersonDSectionsN")]
		public string PersonDSectionsN { get; set; }

		internal override string PersonDSections { get { return PersonDSectionsN; } }
	}

	#region Описание структур хранения данных в модели

	/// <summary>
	/// Структура хранения данных о лице (элементарное свойство)
	/// </summary>
	public class ContextItem
	{
		public int ID { get; set; }
		public string Caption { get; set; }
		public string Label { get; set; }
		public int LinkID { get; set; }
		public string LinkText { get; set; }
		public int Order { get; set; }

		public int? ChangedBy { get; set; }
		public string ChangedByFIO { get; set; }
		public string ChangedDate { get; set; }
        public string Icon { get; set; }
    }

	/// <summary>
	/// Сгруппированные по названию данные о лице
	/// </summary>
	public class ContextItemGroup
	{
		public string Caption { get; set; }
		public List<ContextItem> Items { get; set; }
	}

	/// <summary>
	/// Контакты лица, сгруппированные по типу
	/// </summary>
	public class PersonContactGroup
	{
		public string Caption { get; set; }
		public List<PersonLinkedContact> Items { get; set; }
	}

	/// <summary>
	/// Расширение структуры хранения данных о лице для сохранения контактов по этому свойству
	/// </summary>
	public class EmployeeContextItem : ContextItem
	{
		public List<PersonContactGroup> Contacts { get; set; }
		public EmployeeContextItem()
			: base()
		{
			Contacts = new List<PersonContactGroup>();
		}
	}

	/// <summary>
	/// Класс для хранения данных о контактах лица
	/// </summary>
	public class PersonContacts
	{
		/// <summary>
		/// сотрудник, проверивший актуальность
		/// </summary>
		public int? CheckedBy { get; set; }
		/// <summary>
		/// дата проверки актуальности контактов
		/// </summary>
		public string CheckedDate { get; set; }

		public List<ContextItemGroup> ContactTypes { get; set; }
	}


	#endregion

	/// <summary>
	/// Контекст для секции досье
	/// </summary>
	public class DossierSectionContext
	{
		public PersonAccessor.PersonDossierSection Section { get; set; }

		public ViewModel ViewModel { get; set; }

		public int AccessGranted
		{
			get { 
				if (Section == null || ViewModel == null) return 0;
				return (((int) ViewModel.AccessLevel) >= ((ViewModel.Model.Verified) ? Section.MinLevel_Checked : Section.MinLevel_NotChecked)) ? 1 : 0;
			}
		}

	}

	/// <summary>
	/// Модель данных для досье лица
	/// </summary>
	public class PersonDossierModel
	{

		/// <summary>
		/// Возвращает или устанавливает признак, указыващий имеет ли лицо логотипы.
		/// </summary>
		/// <value>
		/// 	<c>true</c> если лицо имеет логотипы; иначе, <c>false</c>.
		/// </value>
		public bool HasLogotypes { get; set; }

		/// <summary>
		/// Идентификатор лица
		/// </summary>
		public int PersonID { get; set; }

		/// <summary>
		/// Идентификатор сотрудника, с физ. лицом которого работаем
		/// </summary>
		public int EmployeeID { get; set; }

		/// <summary>
		/// Выводить меню на странице досье
		/// </summary>
		public bool PersonMainMenu { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PersonDossierModel" /> is confirmed.
		/// </summary>
		/// <value>
		///   <c>true</c> if confirmed; otherwise, <c>false</c>.
		/// </value>
		public bool Confirmed { get; set; }

		/// <summary>
		/// лицо проверено
		/// </summary>
		public bool Verified { get; set; }

        /// <summary>
        /// По бизнес-проекту
        /// </summary>
        public bool IsBProject { get; set; }

		/// <summary>
		/// Ответственные сотрудники
		/// </summary>
		public string ResponsibleEmpls { get; set; }

		/// <summary>
		/// Типы лиц
		/// </summary>
		/// 
		public string PersonTypes { get; set; }

		/// <summary>
		/// Темы лиц
		/// </summary>
		public string PersonThemes { get; set; }



	}

	/// <summary>
	/// Модель представления для досье лица
	/// </summary>
	public class ViewModel : ViewModel<PersonDossierModel>
	{
		protected static readonly object cacheLock = new object();

		/// <summary>
		/// Настройки пользователя для формы поиска, хранящиеся в БД
		/// </summary>
		public ClientParameters Params { get { return settings as ClientParameters; } }

		/// <summary>
		/// Уровень доступа к лицу
		/// </summary>
		public PersonAccessLevel AccessLevel { get; protected set; }

		/// <summary>
		/// Лицо
		/// </summary>
		public Person Person { get; set; }

		/// <summary>
		/// сотрудник, проверивший актуальность лица
		/// </summary>
		public int? CheckedBy { get; set; }

		/// <summary>
		/// дата проверки актуальности лица
		/// </summary>
		public string CheckedDate { get; set; }

        /// <summary>
        /// Заголовок документа
        /// </summary>
        public string DocumentTitle { get; set; }

	    /// <summary>
		/// Весь контекст для лица
		/// </summary>
		private List<PersonAccessor.PersonDossierContext> Contexts { get; set; }

		/// <summary>
		/// Разделы страницы досье
		/// </summary>
		internal List<PersonAccessor.PersonDossierSection> Sections { get; set; }
		/// <summary>
		/// Пункты меню страницы
		/// </summary>
		internal List<PersonMenuItem> MenuItems { get; set; }

		/// <summary>
		/// работники / места работы
		/// </summary>
		internal List<EmployeeContextItem> LinkedUsersUnder { get; set; }

		/// <summary>
		/// владельцы / собственность
		/// </summary>
		internal List<ContextItem> LinkedUsersMain { get; set; }

		/// <summary>
		/// Возвращает или устанавливает информацию об актуальности контактов.
		/// </summary>
		/// <value>
		/// Информацию об актуальности контактов.
		/// </value>
		internal ContactActual ContactActuality { get; set; }

		internal Employees.ObjectModel.EmployeePartial ContactActualityChangedBy { get; set; }

		private int[] sectionIDs = null;

		public ViewModel() : this(0, null, false) { }
       
	    public ViewModel(int personID, int[] sectionIDs, bool forceReload)
			: base()
		{
		    forceReload = true;
			this.sectionIDs = sectionIDs;

			Model.PersonID = UniqueIdQSFilterSetting.CreateInstance().InitFromQueryString("id", personID).GetValue();

			Model.PersonMainMenu = StringQSFilterSetting.CreateInstance().InitFromQueryString("PersonMainMenu", "true").GetValue() == "true";

			AccessLevel = LoadPersonAccessLevel();
			if (AccessLevel == PersonAccessLevel.None) return;

			Person = Repository.Persons.GetInstance(Model.PersonID);
            
			Kesco.Employees.ObjectModel.Employee employee = null;
			if (Model.PersonID != 0) {
				employee = Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeByPersonID(Model.PersonID);

				if (Person == null || (Person != null && Person.PersonType == Kesco.Persons.ObjectModel.PersonCardType.Natural))
					Model.EmployeeID = employee != null ? employee.ID : 0;
			}
            
			if (Person != null) {

				if (Person.PersonType == Kesco.Persons.ObjectModel.PersonCardType.Natural)
					settings = TypeAccessor<ClientParametersN>.CreateInstanceEx();
				else
					settings = TypeAccessor<ClientParametersJ>.CreateInstanceEx();

				Params.Width = Configuration.AppSettings.Width;
				Params.Height = Configuration.AppSettings.Height;
				LoadSettings(Params);

				Model.HasLogotypes = Kesco.Persons.BusinessLogic.Repository.Logotypes.GetLogotypeCountByPersonID(Person.ID) > 0;
                Model.IsBProject = Person.BusinessProjectID.HasValue;

				if (Person.Verified) {
					Model.Verified = Person.Verified;
                    
					CheckedBy = Person.ChangedBy;
					CheckedDate = Person.ChangedDate.HasValue ? (Person.ChangedDate.Value.FromUtcToClient().ToString("F")) : "---";
				} else {
					Model.Verified = false;
				}

				Init(forceReload);
			} else
			{
			    DocumentTitle = employee.EmployerID + " " + employee.FullName;
				settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
				Params.Width = Configuration.AppSettings.Width;
				Params.Height = Configuration.AppSettings.Height;
			}
		}

		/// <summary>
		/// Загрузка типизированных параметров (клиентских настроек), используемых на странице
		/// </summary>
		protected override void CreateSettings()
		{
			settings = null; // TypeAccessor<ClientParameters>.CreateInstanceEx();
		}


		/// <summary>
		/// Loads the data using cache.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cacheKey">The cache key.</param>
		/// <param name="recycler">The recycler.</param>
		/// <returns></returns>
		private T LoadDataUsingCache<T>(string cacheKey, Func<T> recycler, bool forceReload)
		{
			ObjectCache cache = MemoryCache.Default;
			T data = default(T);

			if (!cache.Contains(cacheKey) || forceReload) {

				CacheItemPolicy policy = new CacheItemPolicy();

				policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(100.0);

				data = recycler();

				cache.Set(cacheKey, data, policy);
			}

			data = (T) cache[cacheKey];

			return data;

		}

		/// <summary>
		/// Загрузка данных досье используя кеш.
		/// </summary>
		protected void LoadDossierData(bool forceReload)
		{
			string cacheKey = String.Format("{0}-PersonDossier-{1}-{2}", 
					HttpContext.Current.Session.SessionID,
					Model.PersonID,
					Params.PersonDateOld
				);

			var data = LoadDataUsingCache<List<PersonAccessor.PersonDossierContext>>(cacheKey,
					() => {
						var paramsContext = new PersonAccessor.PersonsDossierContextParameters { 
								КодЛица = Model.PersonID,
                                Версия = 1,
                                DateOld = Params.PersonDateOld
						};
						return Repository.Persons.PersonDossierData(paramsContext).OrderBy(c => c.Order).ToList();
					}, forceReload);

			if (sectionIDs != null && sectionIDs.Length > 0)
				Contexts = data.Where(line => sectionIDs.Contains(line.TabID)).ToList();
			else Contexts = data; 

		}

		/// <summary>
		/// Loads the menu items.
		/// </summary>
		protected void LoadMenuItems() {
			string cacheKey = String.Format("{0}-PersonDossier-{1}-{2}-Menu", 
					HttpContext.Current.Session.SessionID,
					Model.PersonID,
					Params.PersonDateOld
				);

			var data = LoadDataUsingCache<List<PersonMenuItem>>(cacheKey,
					() => {
						var paramsMenu = new PersonAccessor.PersonsDossierMenuParameters { КодЛица = Model.PersonID, ТипЛица = (int)Person.PersonType };
						return Repository.Persons.PersonsDossierMenus(paramsMenu);
					}, false);

			MenuItems = data; 

		}

		/// <summary>
		/// Loads the menu items.
		/// </summary>
		protected void LoadDossierSections()
		{
			string cacheKey = String.Format("{0}-PersonDossier-{1}-{2}-Sections",
					HttpContext.Current.Session.SessionID,
					Model.PersonID,
					Params.PersonDateOld
				);

			var data = LoadDataUsingCache<List<PersonAccessor.PersonDossierSection>>(cacheKey,
					() => {
						return Repository.Persons.PersonDossierSections((byte)Person.PersonType);
					}, false);

			Sections = data.OrderBy(section => {
				if (section.ID == 5 || section.ID == 6) return section.ID + 4;
				if (section.ID > 6 && section.ID < 11) return section.ID - 2;
				return section.ID;
			}).ToList();

		}

		/// <summary>
		/// Loads the menu items.
		/// </summary>
		protected List<int> LoadRoles()
		{
			string cacheKey = String.Format("{0}-PersonDossier-{1}-{2}-Roles",
					HttpContext.Current.Session.SessionID,
					Model.PersonID,
					Params.PersonDateOld
				);

			return LoadDataUsingCache<List<int>>(cacheKey,
					() => {
						return Kesco.Employees.BusinessLogic.Repository.Employees.CurrentRoles(Model.PersonID);
					}, false);

		}

		/// <summary>
		/// </summary>
		/// <param name="ids">The ids.</param>
		/// <returns></returns>
		protected List<PersonLinkedContact> LoadLinkedContacts(IEnumerable<string> ids)
		{
			string idList = String.Join(",", ids);
			string cacheKey = String.Format("{0}-PersonDossier-{1}-{2}-LinkedContacts-{3}",
					HttpContext.Current.Session.SessionID,
					Model.PersonID,
					Params.PersonDateOld,
					idList
				);

			return LoadDataUsingCache<List<PersonLinkedContact>>(cacheKey,
					() => {
						return Repository.Contacts.GetContactsForPersonLinks(idList, Model.PersonID);
					}, true);

		}

		/// <summary>
		/// </summary>
		/// <param name="ids">The ids.</param>
		/// <returns></returns>
		protected PersonAccessLevel LoadPersonAccessLevel()
		{
			string cacheKey = String.Format("{0}-PersonDossier-{1}-AccessLevel",
					HttpContext.Current.Session.SessionID,
					Model.PersonID
				);

            var accessLevel = LoadDataUsingCache<PersonAccessLevel>(cacheKey,
                () =>
                {
                    return Repository.Persons.GetPersonAccessLevel(Model.PersonID);
                }, true);

            return accessLevel;

        }

		/// <summary>
		/// Инициализация свойств лица на основании общего контекста досье
		/// </summary>
		private void Init(bool forceReload)
		{
			#region Загрузка исходных данных из БД

			LoadDossierData(forceReload);
			LoadMenuItems();
			LoadDossierSections();

			foreach (PersonAccessor.PersonDossierSection item in Sections)
				if (!String.IsNullOrEmpty(item.CaptionKey))
					item.Caption = Resources.Resources.ResourceManager.GetString(item.CaptionKey);

			List<int> roles = LoadRoles();

			foreach (PersonMenuItem item in MenuItems) {
				if (!String.IsNullOrEmpty(item.CaptionKey))
					item.Caption = Resources.Resources.ResourceManager.GetString(item.CaptionKey);

				#region Удаление пунктов, которые требуют отсутствующей роли
				if (!String.IsNullOrEmpty(item.RolesAccess)) {
					bool hasRole = false;
					string[] col = item.RolesAccess.Split(',');

					foreach (string id in col)
						if (roles.Contains(Int32.Parse(id))) {
							hasRole = true;
							break;
						}

					if (!hasRole)
						MenuItems.Remove(item);
				}

				#endregion
			}

			#endregion

			#region Обработка данных, формирование модели для представления

			// проверка актуальности контактов лица
			ContactActuality = Repository.ContactActuals.GetInstance(Model.PersonID);
			if (ContactActuality != null && ContactActuality.ChangedBy.HasValue)
				ContactActualityChangedBy = Employees.BusinessLogic.Repository.EmployeePartials.GetInstance(ContactActuality.ChangedBy.Value);
			
			List<string> ids = new List<string>();
			if (sectionIDs == null || sectionIDs.Contains(11) || sectionIDs.Contains(12))
				LinkedUsersUnder = GetContextItemsEx<EmployeeContextItem>(c => c.TabID == 11 || c.TabID == 12, k => {
					if (!ids.Contains(k.LinkID.ToString()))
							ids.Add(k.LinkID.ToString());
				}).OrderBy(c => c.LinkText + c.Label).ToList();

			// загрузка контактов связанных лиц
			//if (LinkedUsersUnder != null) {
			//	List<PersonLinkedContact> linkedContacts = LoadLinkedContacts(ids);
			//	LinkedUsersUnder
			//		.GroupBy(u => u.LinkText)
			//		.SelectMany(gr => {
			//			var l = new List<EmployeeContextItem>();
			//			l.Add(gr.LastOrDefault());
			//			return l;
			//		}).ToList()
			//		.ForEach(ec => {
			//			ec.Contacts = linkedContacts.Where(c => c.PersonID == ec.LinkID || c.LinkedPersonParentID == ec.LinkID)
			//						.GroupBy(c => c.ContactTypeDesc)
			//						.OrderBy(gr => gr.First().ContactTypeID)
			//						.Select(g => new PersonContactGroup {
			//							Caption = g.Key,
			//							Items = g.Select(c => c).ToList()
			//						}).ToList();
			//		});
			//}

			#endregion
		}


		/// <summary>
		/// Возвращает список, сгруппированный по названию группы
		/// </summary>
		/// <param name="predicate">выражение для условия выборки</param>
		/// <returns>список элементов досье одной группы</returns>
		public List<T> GetContextItemsEx<T>(
			Func<PersonAccessor.PersonDossierContext, bool> predicate,
			Action<T> action
		)
			where T : ContextItem, new()
		{
			return Contexts.Where(predicate)
							.OrderBy(c => c.Order)
							.Select(k => {
								var item = new T {
									ID = k.ID,
									Caption = k.Caption,
									Label = k.Field,
									LinkID = k.Link,
									LinkText = k.LinkText,
									Order = k.Order,
                                    Icon = k.Icon,
                                    ChangedBy = k.ChangedBy,
									ChangedByFIO = k.ChangedByFIO,
									ChangedDate = (k.Changed.HasValue ? (k.Changed.Value.FromUtcToClient().ToString("F")) : "---")
								};
								action(item);
								return item;
							}).ToList();
		}

		/// <summary>
		/// Возвращает список, сгруппированный по названию группы
		/// </summary>
		/// <param name="predicate">выражение для условия выборки</param>
		/// <returns>список элементов досье одной группы</returns>
		public List<ContextItem> GetContextItems(
			Func<PersonAccessor.PersonDossierContext, bool> predicate
		)
		{
			return Contexts.Where(predicate)
							.OrderBy(c => c.Order)
							.Select(k => new ContextItem {
								ID = k.ID,
								Caption = k.Caption,
								Label = k.Field,
								LinkID = k.Link,
								LinkText = k.LinkText,
								Order = k.Order,
                                Icon = k.Icon,
                                ChangedBy = k.ChangedBy,
								ChangedByFIO = k.ChangedByFIO,
								ChangedDate = (k.Changed.HasValue ? (k.Changed.Value.FromUtcToClient().ToString("F")) : "---")
							}).ToList();
		}

		/// <summary>
		/// Возвращает список, сгруппированный по названию группы
		/// </summary>
		/// <param name="predicate">выражение для условия выборки</param>
		/// <param name="groupBy">выражение для группировки</param>
		/// <returns>структура из сгруппированных списков</returns>
		public List<ContextItemGroup> GetContextItemsGroup(
			Func<PersonAccessor.PersonDossierContext, bool> predicate,
			Func<PersonAccessor.PersonDossierContext, string> groupBy
		)
		{
			return Contexts.Where(predicate)
							.OrderByDescending(c => c.Sprt).OrderBy(c => c.Order)
							.GroupBy(groupBy)
							.Select(g => new ContextItemGroup {
								Caption = g.Key,
								Items = g.Select(k => new ContextItem {
									ID = k.ID,
									Caption = k.Caption,
									Label = k.Field,
									LinkID = k.Link,
									LinkText = k.LinkText,
									Order = k.Order,
                                    Icon = k.Icon,
									ChangedBy = k.ChangedBy,
									ChangedByFIO = k.ChangedByFIO,
									ChangedDate = (k.Changed.HasValue ? (k.Changed.Value.FromUtcToClient().ToString("F")) : "---")
								}).ToList()
							}).ToList();
		}

	}

	public static class DossierHtmlHelpers
	{

		/// <summary>
		/// Возвращает локализованную строку для данных об лице.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="propLabel">The prop label.</param>
		public static string GetInfoLocalizedString(this HtmlHelper htmlHelper, string propLabel)
		{
			return GetInfoLocalizedString(htmlHelper, propLabel, null);
		}

		/// <summary>
		/// Возвращает локализованную строку для данных об лице.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="propLabel">The prop label.</param>
		/// <param name="personId">код лица</param>
		public static string GetInfoLocalizedString(this HtmlHelper htmlHelper, string propLabel, int? personId)
		{
			string label = (propLabel ?? String.Empty).ToUpper();
			switch (label) {
				case "СТРАНА РЕГИСТРАЦИИ": return Localization.Resources.Models_Dossier_Info_RegistrationCountry;
				case "ИНН": return Localization.Resources.Models_Dossier_Info_INN;
				case "ОГРН":
					if(personId.HasValue)
					{
						Person p = Repository.Persons.GetInstance(personId.Value);
						if (p.TerritoryID != Territories.ObjectModel.Territory.Russia)
							return Localization.Resources.Models_Dossier_Info_GRN;
					}
					return Localization.Resources.Models_Dossier_Info_OGRN;
				case "ОКПО": return Localization.Resources.Models_Dossier_Info_OKPO;
				case "БИК": return Localization.Resources.Models_Dossier_Info_BIK;
				case "КорСчет": return Localization.Resources.Models_Dossier_Info_CorrAccount;
				case "БИКРКЦ": return Localization.Resources.Models_Dossier_Info_RCCBIK;
				case "БИЗНЕС ПРОЕКТ": return Localization.Resources.Models_Dossier_Info_BusinessProject;
				case "ПРИМЕЧАНИЕ": return Localization.Resources.Models_Dossier_Info_Comment;
				case "ДАТА НАЧАЛА": return Localization.Resources.Models_Dossier_Info_DateStart;
				case "ДАТА КОНЦА": return Localization.Resources.Models_Dossier_Info_DateEnd;

				case "<I>ГОСУДАРСТВЕННАЯ ОРГАНИЗАЦИЯ</I>": return Localization.Resources.Models_Dossier_Info_StateOrganization;

				default:
					return propLabel;
			}

		}

		/// <summary>
		/// Возвращает локализованную строку для свойства лица.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="propLabel">The prop label.</param>
		/// <returns></returns>
		public static string GetPropsLocalizedString(this HtmlHelper htmlHelper, string propLabel)
		{
			string label = (propLabel ?? String.Empty).ToUpper();
			switch (label) {
				case "ОПФ": return Localization.Resources.Models_JuridicalPersonCard_IncorporationForm_ShortName;
				case "КРАТКОЕ НАЗВАНИЕ": return Localization.Resources.Models_JuridicalPersonCard_ShortNameRus_ShortName;
				case "НАЗВАНИЕ В РОДИТЕЛЬНОМ ПАДЕЖЕ": return Localization.Resources.Models_JuridicalPersonCard_ShortNameRusGenitive_ShortName;
				case "SHORT NAME": return Localization.Resources.Models_JuridicalPersonCard_ShortNameLat_ShortName;
				case "ПОЛНОЕ НАЗВАНИЕ": return Localization.Resources.Models_JuridicalPersonCard_FullName_ShortName;
				case "ОКОНХ": return Localization.Resources.Models_JuridicalPersonCard_OKONH_ShortName;
				case "ОКВЭД": return Localization.Resources.Models_JuridicalPersonCard_OKVED_ShortName;

				case "КПП": return Localization.Resources.Models_JuridicalPersonCard_KPP_ShortName;
				case "Ж/Д КОД": return Localization.Resources.Models_JuridicalPersonCard_RwID_ShortName;
				case "МЕСТО НАХОЖДЕНИЯ (ЮРИДИЧЕСКИЙ АДРЕС)": return Localization.Resources.Models_JuridicalPersonCard_AddressLegal_ShortName;
				case "LEGAL ADDRESS": return Localization.Resources.Models_JuridicalPersonCard_AddressLegalLat_ShortName;

				case "LAST NAME": return Localization.Resources.Models_NaturalPersonCard_LastNameAZ;
				case "FIRST NAME": return Localization.Resources.Models_NaturalPersonCard_FirstNameAZ;
				case "MIDDLE NAME": return Localization.Resources.Models_NaturalPersonCard_MiddleNameAZ;

				default:
					return propLabel;
			}

		}

		/// <summary>
		/// Получение текста даты для карточки лица
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="dates">строчка с диапазоном дат</param>
		/// <returns>
		/// строковая интерпретация данных
		/// </returns>
		public static string GetDateString_Card(this HtmlHelper htmlHelper, string dates)
		{
			DateTime fromD, toD;
			fromD = Convert.ToDateTime(dates.Substring(0, dates.IndexOf('#'))).FromUtcToClient();
			toD = Convert.ToDateTime(dates.Substring(dates.IndexOf('#') + 1)).FromUtcToClient();

			string ret = "";
			if ((fromD.Year <= 1980) && (toD.Year >= 2050))
				ret = Resources.Resources.Persons_Dossier_CardValid;
			if ((fromD.Year <= 1980) && (toD.Year < 2050))
				ret = Resources.Resources.Persons_Dossier_CardTo + toD.AddDays(-1).ToString("dd.MM.yyyy");
			if ((fromD.Year > 1980) && (toD.Year >= 2050))
				ret = Resources.Resources.Persons_Dossier_CardFrom + fromD.ToString("dd.MM.yyyy");
			if ((fromD.Year > 1980) && (toD.Year < 2050))
				ret = Resources.Resources.Persons_Dossier_CardFrom + fromD.ToString("dd.MM.yyyy") + " " + Resources.Resources.Persons_Dossier_CardTo + toD.AddDays(-1).ToString("dd.MM.yyyy");
			if ((fromD.Year <= 1980) && (toD.Year <= 1980))
				ret = Resources.Resources.Persons_Dossier_CardNotValid;

			return ret;
		}

		/// <summary>
		/// Регистрирует клиентский скрипт для скрытия секции.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="sectionID">The section ID.</param>
		public static void DossierClientSideScript_HideSection(this HtmlHelper htmlHelper, int sectionID)
		{
			htmlHelper.RegisterCommonScriptCode(
					"dossier_HideSection_" + sectionID.ToString(), 
					String.Format(@"
						$('#sect{0}, #trsect{0}').remove();
						", sectionID
					)
			);
		}

		/// <summary>
		/// Регистрирует клиентский скрипт для открытия страницы редактирования секции.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="sectionName">Name of the section.</param>
		/// <param name="url">The URL.</param>
        public static void DossierClientSideScript_OpenStoreSection(this HtmlHelper htmlHelper, string sectionName, string url)
        {
            htmlHelper.RegisterCommonScriptCode("dossierOpenStore_" + sectionName, String.Format(@"
					function dossierOpenStore_{0}(id, sectionId, hideOldVer) {{
						var url = '{1}&id='+id+'&sectionId='+{0}+'&mvc=1';
                        url = url + '&callbackUrl='+ encodeURIComponent(getDefaultFullPathAction());
						openPopupWindow(url, null,  function (value) {{ window.location.href = window.location.href; window.focus(); }},
                        'wndEditStore_{0}_'+id, 850, 600);
					}}
				", sectionName, url
                 ));
        }

		/// <summary>
		/// Регистрирует клиентский скрипт для редактирования связи лиц.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="sectionName">Name of the section.</param>
		/// <param name="url">The URL.</param>
		public static void DossierClientSideScript_EditLink(this HtmlHelper htmlHelper, string sectionName, string url)
		{
			htmlHelper.RegisterCommonScriptCode("dossierEditLink_" + sectionName, String.Format(@"
					function dossierEditLink_{0}(id, sectionId, hideOldVer) {{
						var url = '{1}&id='+id+'&sectionId='+sectionId;
						openPopupWindow(url, null, 
							function (result) {{
								if ($.isArray(result)) 
									window.location.href = window.location.href;
							}}, 'wndEditLink_'+id,  520, 300);
					}}
				", sectionName, url
			));
		}

	}
}
