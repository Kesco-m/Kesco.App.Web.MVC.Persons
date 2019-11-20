using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using BLToolkit.Mapping;
using BLToolkit.Reflection;
using BLToolkit.TypeBuilder;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Search.Filtering;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.Filtering;
using Kesco.Web.Mvc.UI;

namespace Kesco.Persons.Web.Models
{
	/// <summary>
	/// Модель представления формы поиска
	/// </summary>
	public class SearchViewModel : DialogViewModel
	{
		/// <summary>
		/// Класс, содержащий свойства, замапленные на клиентские настройки приложения для текущего пользователя
		/// </summary>
        public class ClientParameters : ClientParametersBase
		{
			[MapField("PersonLanguage"), Parameter(0)]
			public int Language { get; set; }

			[MapField("PersonHowSearch"), Parameter(1)]
			public int PersonHowSearch { get; set; }

			[MapField("PersonWhereSearch"), Parameter(1)]
			public int PersonWhereSearch { get; set; }

			[MapField("PersonType"), Parameter(7)]
			[MapValue(null, "")]
			public int? PersonType { get; set; }

			[MapField("PersonCheck"), Parameter(3)]
			[MapValue(null, "")]
			public int? PersonCheck { get; set; }

			[MapField("PersonOPForma"), Parameter(0)]
			[MapValue(null, "")]
			public int? PersonOPForma { get; set; }

			[MapField("PersonPageSize"), Parameter(50)]
			public int PersonPageSize { get; set; }

			[MapField("PersonSubThemes"), Parameter(0)]
			public int PersonSubThemes { get; set; }

			[MapField("PersonBProject")]
            [MapValue(null, "")]
			public string PersonBProject { get; set; }

			[MapField("PersonSubBProject"), Parameter(1)]
			public int PersonSubBProject { get; set; }

			[MapField("PersonAllBProject"), Parameter(0)]
			[MapValue(null, 0)]
			public int PersonAllBProject { get; set; }

			[MapField("PersonUsers")]
			[MapValue(null, "")]
			public string PersonUsers { get; set; }

			[MapField("PersonThemes")]
			[MapValue(null, "")]
			public string PersonThemes { get; set; }

			[MapField("PersonTUnion")]
			[MapValue(null, "")]
			public int? PersonTUnion { get; set; }

			[MapField("PersonLink")]
			[MapValue(null, "")]
			public int? PersonLink { get; set; }

			[MapField("PersonLinktype")]
			[MapValue(null, "")]
			public int? PersonLinktype { get; set; }

			[MapField("PersonForSend"), Parameter(0)]
			public int? PersonForSend { get; set; }

			[MapField("PersonAdvSearch")]
			[MapValue(null, "")]
			public int? PersonAdvSearch { get; set; }

            [MapValue(null, "")]
			public string PersonArea { get; set; }

            [MapField("PersonValidAt")]
            [MapValue(null, "")]
            public DateTime? PersonValidAt { get; set; }
            
			public DateTime? PersonLinkValidAt { get; set; }

			public int? PersonSignType { get; set; }

            [MapValue(null, "")]
			public int? PersonSelectTop { get; set; }
		}

		/// <summary>
		/// Настройки поиска для формы (объект данного класса используется в моделе на клиенте)
		/// </summary>
		public class FilterSettings
		{
			public ArrayQSFilterSetting PersonWhereSearch { get; protected set; }
			public PersonHowSearchFilterSetting PersonHowSearch { get; protected set; }
			public StringQSFilterSetting Search { get; protected set; }

            public StringQSFilterSetting PersonValidAt { get; protected set; }

			public ArrayQSFilterSetting PersonType { get; protected set; }
			public ArrayQSFilterSetting Veracity { get; protected set; }

			public BusinessProjectFilterSetting BusinessProject { get; protected set; }

			public PersonAreaFilterSetting PersonArea { get; protected set; }
			public UniqueIdQSFilterSetting PersonOPForma { get; protected set; }

			public PersonUsersFilterSetting PersonUsers { get; protected set; }
			public PersonThemesFilterSetting PersonThemes { get; protected set; }

            public StringQSFilterSetting PersonTUnion { get; protected set; }
            public StringQSFilterSetting PersonLink { get; protected set; }
            public string PersonLinkNickname { get; protected set; }
            public StringQSFilterSetting PersonLinkType { get; protected set; }

            public StringQSFilterSetting PersonForSend { get; protected set; }

            public StringQSFilterSetting PersonAdvSearch { get; protected set; }

			public PersonSelectTopFilterSetting PersonSelectTop { get; protected set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="FilterSettings"/> class.
			/// </summary>
			/// <param name="clientParameters">The ClientParameters.</param>
			public FilterSettings(ClientParameters clientParameters)
			{
				PersonWhereSearch = ArrayQSFilterSetting.CreateInstance().InitFromQueryString("PersonWhereSearch", clientParameters.PersonWhereSearch).SetEnable(true);
				PersonHowSearch = PersonHowSearchFilterSetting.CreateInstance().InitFromQueryString("PersonHowSearch", clientParameters.PersonHowSearch).SetEnable(true);

				Search = StringQSFilterSetting.CreateInstance().InitFromQueryString("Search", "");
				PersonValidAt = StringQSFilterSetting.CreateInstance().InitFromQueryString("PersonValidAt", "");

				PersonType = ArrayQSFilterSetting.CreateInstance().InitFromQueryString("PersonType", clientParameters.PersonType);
				Veracity = ArrayQSFilterSetting.CreateInstance().InitFromQueryString("PersonCheck", clientParameters.PersonCheck);

                Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                string PersonAllBProject = HttpUtility.ParseQueryString(myUri.Query).Get("PersonAllBProject");
                clientParameters.PersonAllBProject = Convert.ToInt32(PersonAllBProject);

				BusinessProject = BusinessProjectFilterSetting.CreateInstance().InitFromQS(
					"PersonBProject", (clientParameters.PersonBProject == "" ? "0" : clientParameters.PersonBProject),
						"PersonSubBProject ", clientParameters.PersonSubBProject,
                        "PersonAllBProject ", clientParameters.PersonAllBProject);
                if (clientParameters.PersonAllBProject != 0)
                {
                    BusinessProject.SetEnable(true);
                }

				PersonArea = PersonAreaFilterSetting.CreateInstance().InitFromQueryString("PersonArea", clientParameters.PersonArea == "" ? "0" : clientParameters.PersonArea);
				PersonOPForma = UniqueIdQSFilterSetting.CreateInstance().InitFromQueryString("PersonOPForma", clientParameters.PersonOPForma);

				PersonUsers = PersonUsersFilterSetting.CreateInstance()
					.SetListAccessor(ids => Kesco.Employees.BusinessLogic.Repository.Employees.GetListByIds(ids))
					.InitFromQueryString("PersonUsers", clientParameters.PersonUsers);

				PersonThemes = PersonThemesFilterSetting.CreateInstance()
					.SetListAccessor(ids => Repository.PersonThemes.GetListByIds(ids))
					.InitFromQS("PersonThemes", clientParameters.PersonThemes,
						"PersonSubThemes", clientParameters.PersonSubThemes);

				PersonTUnion = StringQSFilterSetting.CreateInstance().InitFromQueryString("PersonTUnion", "");
				PersonLink = StringQSFilterSetting.CreateInstance().InitFromQueryString("PersonLink", "");

				int personID;

				if (int.TryParse(PersonLink.GetValue(), out personID))
				{
					var p = Repository.Persons.GetInstance(personID);
					PersonLinkNickname = p != null ? p.Nickname : "#PersonLink";
				}
				else
					PersonLinkNickname = "";

				PersonLinkType = StringQSFilterSetting.CreateInstance().InitFromQueryString("PersonLinkType", "");

				PersonForSend = StringQSFilterSetting.CreateInstance().InitFromQueryString("PersonForSend", "0");

				PersonAdvSearch = StringQSFilterSetting.CreateInstance().InitFromQueryString("PersonAdvSearch", "");

				//PersonSelectTop = PersonSelectTopFilterSetting.CreateInstance().InitFromQueryString("PersonSelectTop", 30);
			}

			/// <summary>
			/// Преобразование настроек фильтров к параметрам поиска
			/// </summary>
			/// <returns>Параметры для хранимой процедуры поиска</returns>
			public PersonAccessor.SearchParameters ToSearchParameters()
			{
				var searchParams = new PersonAccessor.SearchParameters();

				if (PersonWhereSearch.Enable) searchParams.PersonWhereSearch = PersonWhereSearch.GetValue() ?? 0;
				if (PersonHowSearch.Enable) searchParams.PersonHowSearch = PersonHowSearch.GetValue();

                searchParams.Search = Search.Enable ? Search.GetValue() : String.Empty;

				if (PersonType.Enable) searchParams.PersonType = PersonType.GetValue();
				if (Veracity.Enable) searchParams.PersonCheck = Veracity.GetValue();

				if (BusinessProject.Enable)
				{
					searchParams.PersonBProject = BusinessProject.GetValue();
					searchParams.PersonSubBProject = BusinessProject.SubBProject? 1 : 0;
                     
                    if (BusinessProject.AllBProject) searchParams.PersonAllBProject = 1;
				}

                if (PersonArea.Enable) searchParams.PersonArea = (PersonArea == null ? 0 : PersonArea.GetValue());
                if (PersonOPForma.Enable) searchParams.PersonOPForma = PersonOPForma.GetValue();

                searchParams.PersonUsers = PersonUsers.Enable ? PersonUsers.GetValue() : String.Empty;
				searchParams.PersonThemes = PersonThemes.Enable ? PersonThemes.GetValue() : String.Empty;

				if (PersonThemes.Enable) searchParams.PersonSubThemes = PersonThemes.Subthemes ? 1 : 0;

                if (PersonValidAt.GetValue().Length > 0)
			    {
                    searchParams.PersonValidAtTicks = String2Ticks(PersonValidAt.GetValue());
			    }

				return searchParams;
			}
		}

        public static long String2Ticks(String s)
        {
            try
            {
                const string rex = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
                const string rex2 = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))(?:[0]?[1-9]|[1][012])(?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
                const string rex3 = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])";
                const string rex4 = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])";

                DateTime d;

                if (Regex.Match(s, rex).Success)
                { // 1999-12-31
                    d = new DateTime(int.Parse(s.Substring(0, 4)), int.Parse(s.Substring(5, 2)), int.Parse(s.Substring(8, 2)));

                }
                else if (Regex.Match(s, rex2).Success)
                {// 19991231
                    d = new DateTime(int.Parse(s.Substring(0, 4)), int.Parse(s.Substring(4, 2)), int.Parse(s.Substring(6, 2)));
                }
                else if (Regex.Match(s, rex3).Success)
                {// 31.12.1999
                    d = new DateTime(int.Parse(s.Substring(6, 4)), int.Parse(s.Substring(3, 2)), int.Parse(s.Substring(0, 2)));
                }
                else if (Regex.Match(s, rex4).Success)
                { // 31/12/1999
                    d = new DateTime(int.Parse(s.Substring(6, 4)), int.Parse(s.Substring(3, 2)), int.Parse(s.Substring(0, 2)));
                }
                else
                    return -1;

                var d0 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                return d.Ticks - d0.Ticks;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static DateTime String2Date(String s)
        {
            const string rex = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
            const string rex2 = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))(?:[0]?[1-9]|[1][012])(?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
            const string rex3 = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])";
            const string rex4 = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])";

            DateTime d;

            if (Regex.Match(s, rex).Success)
            { // 1999-12-31
                d = new DateTime(int.Parse(s.Substring(0, 4)), int.Parse(s.Substring(5, 2)), int.Parse(s.Substring(8, 2)));

            }
            else if (Regex.Match(s, rex2).Success)
            {// 19991231
                d = new DateTime(int.Parse(s.Substring(0, 4)), int.Parse(s.Substring(4, 2)), int.Parse(s.Substring(6, 2)));
            }
            else if (Regex.Match(s, rex3).Success)
            {// 31.12.1999
                d = new DateTime(int.Parse(s.Substring(6, 4)), int.Parse(s.Substring(3, 2)), int.Parse(s.Substring(0, 2)));
            }
            else if (Regex.Match(s, rex4).Success)
            { // 31/12/1999
                d = new DateTime(int.Parse(s.Substring(6, 4)), int.Parse(s.Substring(3, 2)), int.Parse(s.Substring(0, 2)));
            }
            else
            {
                return new DateTime();
            }

            return d;
        }

		public List<Person> Persons { get; internal set; }

		public List<IncorporationForm> IncorporationForms { get; internal set; }

        public List<int> HasRolesForBProject { get; internal set; }

		/// <summary>
		/// Настройки пользователя для формы поиска, хранящиеся в БД
		/// </summary>
		public ClientParameters Params { get { return settings as ClientParameters; } }

		public FilterSettings filterSettings { get; internal set; }

		public SearchViewModel(): this(0, String.Empty, false) {}

		public SearchViewModel(int clid, string returnUri, bool isDialog) : base(clid, returnUri, isDialog)
		{
			//Params = TypeAccessor<ClientParameters>.CreateInstanceEx();
			Params.Width = Configuration.AppSettings.Width;
			Params.Height = Configuration.AppSettings.Height;
			LoadSettings(Params);
		    Params.PersonPageSize = Params.PersonPageSize == 0 ? 40 : Params.PersonPageSize;

            filterSettings = new FilterSettings(Params);
			IncorporationForms = Repository.IncorporationForms.GetAll();

            HasRolesForBProject = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForBProject();

		    HelpTopic = "search";
		}

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

	}
}