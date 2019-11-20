using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web.Services;
using System.Data;
using System.Text.RegularExpressions;
using Kesco.Lib.Log;
using System.Xml;

namespace Kesco.Persons.Web
{
    /// <summary>
    /// Сводное описание для srv
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class srv : WebService
    {
        public srv()
        {
            //CODEGEN: This call is required by the ASP.NET Web Services Designer
            InitializeComponent();
        }

        #region Component Designer generated code

        //Required by the Web Services Designer
        private IContainer components = null;


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        [WebMethod(Description = @"<br><pre>
Осуществляет поиск лиц в поле <b>КличкаRL</b> с применением функций преобразования <i>fn_ReplaceKeySymbols</i>, <i>fn_SplitWords</i> и <i>fn_ReplaceRusLat</i>.
Возвращаемое значение - <i>количество найденных лиц</i>.
Параметры:
  out int    id           - код найденного лица (если количество найденнных лиц <b>=1</b>.)
      string searchText   - строка поиска.
      string searchParams - параметры поиска в фомате(<i>param1=val1&amp;param2=val2...</i>, на данный момент реализована обработка параметров
			<b>persontype</b>-тип лица,
			<b>personcheck</b>-проверено бухгалтерией,
			<b>personallbproject</b>-лица по бизнес-проектам,
			<b>personexcept</b>-все, за исключением указанного,
			<b>personarea</b>-страна регистрации,
			<b>persontunion</b>-таможенный союз,
			<b>personlink</b>-лица, связанные с указанным лицом,
			<b>personlinktype</b>-тип связи лиц(работает только, при наличии предыдущего параметра)
			<b>personvalidat</b>-лица с действующими карточками
			<b>header</b>-заголовок pop-up окна,
			<b>footer</b>-'расширенный поиск'
			<b>idlimit</b>-жесткое ограничение лиц(footer не учитывается)
			<b>idrecommended</b>-рекомендованное ограничение лиц)
</pre>
")]
        public string Search(string searchText, string searchParams)
        {
            string header = "";
            string footer = "";

            string sql = "";
            string sqlWhere = "";
            string sqlWhereOrig = "";

            string personLink = "";
            string personLinkType = "";

            string idlimit = "";
            string idrecommended = "";

            SqlDataAdapter da = null;
            var ds = new DataSet();
            var dt = new DataTable();
            DataTable dt1 = null;

            try
            {
                searchText = searchText.Trim();

                if (Regex.IsMatch(searchText, "^[0-9]{1,}$", RegexOptions.IgnoreCase))
                {
                    sqlWhere += " (Лица.Инн LIKE '" + searchText + "%'";
                    if (searchText.Length <= 9) sqlWhere += " OR Лица.КодЛица=" + searchText;
                    sqlWhere += ") ";
                }
                else
                {
                    sql = @"
SET @searchText = Инвентаризация.dbo.fn_ReplaceKeySymbols(Инвентаризация.dbo.fn_SplitWords(Инвентаризация.dbo.fn_ReplaceRusLat(@searchText)))
SET @searchText = RTRIM(LTRIM(@searchText))
WHILE CHARINDEX('  ',@searchText) > 0 SET @searchText = REPLACE(@searchText,'  ',' ')
SET @searchText=REPLACE(' ' + @searchText,' ','% ') + '%'
";
                    sqlWhere += " (' ' + КличкаRL) LIKE @searchText";

                }

                if (searchParams != "")
                {
                    string[] p = searchParams.Split('&');
                    string[] v;
                    for (int i = 0; i < p.Length; i++)
                    {
                        v = p[i].Split('=');
                        switch (v[0].ToLower())
                        {
                            case "persontype":
                                if (v[1] != "")
                                {
                                    if (v[1] != "2") v[1] = "1";
                                    sqlWhere += " AND Лица.ТипЛица=" + v[1];
                                }
                                break;
                            case "personcheck":
                                if (v[1] != "")
                                {
                                    if (v[1] != "1") v[1] = "0";
                                    sqlWhere += " AND Лица.Проверено=" + v[1];
                                }
                                break;
                            case "personallbproject":
                                if (v[1] != "")
                                {
                                    if (v[1] == "1")
                                        sqlWhere += " AND Лица.КодБизнесПроекта IS NOT NULL";
                                }
                                break;
                            case "personexcept":
                                if (v[1] != "") sqlWhere += " AND Лица.КодЛица <>" + v[1];
                                break;

                            case "personarea":
                                if (v[1] != "") sqlWhere += " AND ISNULL(Лица.КодТерритории,0)=" + v[1];
                                break;
                            case "persontunion":
                                if (v[1] != "0") sqlWhere += " AND EXISTS(SELECT * FROM Справочники.dbo.ТаможенныйСоюз WHERE КодТерритории=Лица.КодТерритории)";
                                break;
                            case "personvalidat":
                                if (v[1] != "0") sqlWhere += " AND (EXISTS(SELECT * FROM Справочники.dbo.vwКарточкиЮрЛиц КЮ WHERE КЮ.КодЛица=Лица.КодЛица AND От <='" + v[1] + "' AND До>'" + v[1] + "') OR EXISTS(SELECT * FROM Справочники.dbo.vwКарточкиФизЛиц КФ WHERE КФ.КодЛица=Лица.КодЛица AND От <='" + v[1] + "' AND До>'" + v[1] + "'))";
                                break;

                            case "personlink":
                                if (v[1] != "") personLink = v[1];
                                break;
                            case "personlinktype":
                                if (v[1] != "") personLinkType = v[1];
                                break;

                            case "idlimit":
                                if (v[1] != "") idlimit = v[1];
                                break;
                            case "idrecommended":
                                if (v[1] != "") idrecommended = v[1];
                                break;
                            case "header":
                                if (v[1] != "") header = v[1];
                                break;
                            case "footer":
                                if (v[1] != "") footer = v[1];
                                break;

                        }
                    }
                }

                if (personLink != "") sqlWhere += " AND + Лица.КодЛица IN(" + GetSQL_PersonLink(personLink, personLinkType) + ")";

                idlimit = (!Regex.IsMatch(idlimit, "^[0-9,]{1,}$", RegexOptions.IgnoreCase)) ? "" : idlimit;
                idrecommended = (!Regex.IsMatch(idrecommended, "^[0-9,]{1,}$", RegexOptions.IgnoreCase)) ? "" : idrecommended;

                sqlWhereOrig = sqlWhere;

                if (idlimit != "")
                {
                    sqlWhere += " AND + Лица.КодЛица IN(" + idlimit + ")";
                    footer = "";
                }
                else if (idrecommended != "") sqlWhere += " AND + Лица.КодЛица IN(" + idrecommended + ")";

                sql += @"
SELECT Лица.КодЛица, Лица.Кличка, Лица.Инн  FROM Справочники.dbo.vwЛица Лица
WHERE " + sqlWhere + @"
ORDER BY Лица.Кличка
";

                if (idrecommended != "")
                    sql += @"
SELECT Лица.КодЛица, Лица.Кличка, Лица.Инн  FROM Справочники.dbo.vwЛица Лица
WHERE " + sqlWhereOrig + @"
ORDER BY Лица.Кличка
";

                da = new SqlDataAdapter(sql, MvcApplication.DS_person);
                var param = new SqlParameter("@searchText", searchText); param.Size = 50;
                da.SelectCommand.Parameters.Add(param);
                da.Fill(ds);

                dt = ds.Tables[0];

                if ((idrecommended != "") && (ds.Tables.Count == 2))
                    dt1 = ds.Tables[1];
            }

            catch (Exception ex)
            {
                Logger.WriteEx(new DetailedException("Ошибка поиска лиц", ex, da.SelectCommand));

            }

            XmlElement el;
            var doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("SearchResult"));
            doc.DocumentElement.SetAttribute("showColumnNames", "true");

            if (footer != "") doc.DocumentElement.SetAttribute("advancedSearchRowCaption", footer);
            if (header != "") doc.DocumentElement.SetAttribute("caption", header);

            doc.DocumentElement.SetAttribute("showAdvancedSearchRow",
                                              (
                                                (dt.Rows.Count == 1) ||
                                                    (idlimit != "" && dt.Rows.Count > 0) ||
                                                    (idlimit == "" && idrecommended == "" && dt.Rows.Count <= 12) ||
                                                    (idrecommended != "" && dt.Rows.Count == dt1.Rows.Count)) ? "false" : "true");

            doc.DocumentElement.AppendChild(el = doc.CreateElement("Column"));
            el.SetAttribute("name", "Name");
            el.SetAttribute("caption", "Название");

            doc.DocumentElement.AppendChild(el = doc.CreateElement("Column"));
            el.SetAttribute("name", "INN");
            el.SetAttribute("caption", "ИНН");
            el.SetAttribute("width", "50%");

            if (dt1 != null && dt1.Rows.Count > 0 && dt.Rows.Count == 0) dt = dt1;

            if ((dt.Rows.Count <= 12) || (idlimit != ""))
            {
                foreach (DataRow row in dt.Rows)
                {
                    doc.DocumentElement.AppendChild(el = doc.CreateElement("Row"));
                    el.SetAttribute("ID", row[0].ToString());
                    el.SetAttribute("Name", row[1].ToString());
                    el.SetAttribute("INN", row[2].ToString());
                }
            }

            return doc.OuterXml;
        }


        public string GetSQL_PersonLink(string link, string type)
        {
            var ret = @"
SELECT КодЛицаПотомка FROM Справочники.dbo.vwСвязиЛиц WHERE КодЛицаРодителя=" + link + @"
UNION
SELECT КодЛицаРодителя FROM Справочники.dbo.vwСвязиЛиц WHERE КодЛицаПотомка=" + link;

            if (type == "") return ret;

            var _type = int.Parse(type) / 10;
            _type = (_type == 0) ? 1 : _type;

            if ((int.Parse(type) & 1) == 1)
                ret = "SELECT КодЛицаПотомка FROM Справочники.dbo.vwСвязиЛиц WHERE КодЛицаРодителя=" + link + " AND КодТипаСвязиЛиц =" + _type;
            else
                ret = "SELECT КодЛицаРодителя FROM Справочники.dbo.vwСвязиЛиц WHERE КодЛицаПотомка=" + link + " AND КодТипаСвязиЛиц =" + _type;

            return ret;
        }


        [WebMethod(Description = @"<br><pre>
Осуществляет поиск лиц в поле <b>БИК</b> с применением функций преобразования <i>fn_ReplaceKeySymbols</i>, <i>fn_SplitWords</i>.
Возвращаемое значение - <i>количество найденных лиц</i>.
Параметры:
  out int    id           - код найденного лица (если количество найденнных лиц <b>=1</b>.)
      string searchText   - строка поиска.
      string searchParams - параметры поиска в фомате(<i>param1=val1&amp;param2=val2...</i>, на данный момент реализована обработка параметров <b>PersonType</b>,<b>PersonСheck</b>,
                            <b>personallbproject</b>,<b>personexcept</b>)

</pre>
")]
        public int SearchBIK(out int id, string searchText, string searchParams)
        {
            int rCount;
            var sqlWhere = "";
            string sql;
            id = 0;
            SqlDataAdapter da = null;
            var dt = new DataTable("result");

            try
            {
                if (searchParams != "")
                {
                    string[] p = searchParams.Split('&');
                    string[] v;
                    for (int i = 0; i < p.Length; i++)
                    {
                        v = p[i].Split('=');
                        switch (v[0].ToLower())
                        {
                            case "persontype":
                                if (v[1] != "")
                                {
                                    if (v[1] != "2") v[1] = "1";
                                    sqlWhere += " AND ТипЛица=" + v[1];
                                }
                                break;
                            case "personcheck":
                                if (v[1] != "")
                                {
                                    if (v[1] != "1") v[1] = "0";
                                    sqlWhere += " AND Проверено=" + v[1];
                                }
                                break;
                            case "personallbproject":
                                if (v[1] != "")
                                    if (v[1] == "1")
                                        sqlWhere += " AND КодБизнесПроекта IS NOT NULL";
                                break;
                            case "personexcept":
                                if (v[1] != "") sqlWhere += " AND КодЛица <>" + v[1];
                                break;
                        }
                    }
                }

                sql = @"
SET @searchText = RTRIM(LTRIM(Инвентаризация.dbo.fn_ReplaceKeySymbols(Инвентаризация.dbo.fn_SplitWords(@searchText))))
SET @searchText = @searchText + '%'
SELECT КодЛица FROM Справочники.dbo.vwЛица WHERE БИК LIKE @searchText " + sqlWhere;

                da = new SqlDataAdapter(sql, MvcApplication.DS_person);
                da.SelectCommand.Parameters.AddWithValue("@searchText", searchText);
                da.Fill(dt);
                rCount = dt.Rows.Count;
                if (rCount == 1)
                    id = (int)dt.Rows[0]["КодЛица"];
            }
            catch (Exception ex)
            {
                throw new DetailedException("Ошибка поиска лиц по БИК", ex, da.SelectCommand);
            }

            return rCount;
        }


        [WebMethod(Description = @"<br><pre>
Осуществляет поиск лиц в поле <b>БИК</b> с применением функций преобразования <i>fn_ReplaceKeySymbols</i>, <i>fn_SplitWords</i>.
Возвращаемое значение - <i>количество найденных лиц</i>.
Параметры:
  out int    id           - код найденного лица (если количество найденнных лиц <b>=1</b>.)
      string searchText   - строка поиска.
      string searchParams - параметры поиска в фомате(<i>param1=val1&amp;param2=val2...</i>, на данный момент реализована обработка параметров <b>PersonType</b>,<b>PersonСheck</b>,
                            <b>personallbproject</b>,<b>personexcept</b>)
</pre>
")]
        public int SearchSWIFT(out int id, string searchText, string searchParams)
        {
            int rCount;
            string sqlWhere = "";
            string sql;
            id = 0;
            SqlDataAdapter da = null;
            var dt = new DataTable("result");

            try
            {
                if (searchParams != "")
                {
                    string[] p = searchParams.Split('&');
                    string[] v;
                    for (int i = 0; i < p.Length; i++)
                    {
                        v = p[i].Split('=');
                        switch (v[0].ToLower())
                        {
                            case "persontype":
                                if (v[1] != "")
                                {
                                    if (v[1] != "2") v[1] = "1";
                                    sqlWhere += " AND ТипЛица=" + v[1];
                                }
                                break;
                            case "personcheck":
                                if (v[1] != "")
                                {
                                    if (v[1] != "1") v[1] = "0";
                                    sqlWhere += " AND Проверено=" + v[1];
                                }
                                break;
                            case "personallbproject":
                                if (v[1] != "")
                                    if (v[1] == "1")
                                        sqlWhere += " AND КодБизнесПроекта IS NOT NULL";
                                break;
                            case "personexcept":
                                if (v[1] != "") sqlWhere += " AND КодЛица <>" + v[1];
                                break;
                        }
                    }
                }

                sql = @"
SET @searchText = RTRIM(LTRIM(Инвентаризация.dbo.fn_ReplaceKeySymbols(Инвентаризация.dbo.fn_SplitWords(@searchText))))
SET @searchText = @searchText + '%'
SELECT КодЛица FROM Справочники.dbo.vwЛица WHERE SWIFT LIKE @searchText " + sqlWhere;

                da = new SqlDataAdapter(sql, MvcApplication.DS_person);
                da.SelectCommand.Parameters.AddWithValue("@searchText", searchText);
                da.Fill(dt);
                rCount = dt.Rows.Count;
                if (rCount == 1)
                    id = (int)dt.Rows[0]["КодЛица"];
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка поиска лиц по SWIFT", ex, da.SelectCommand);

            }
            return rCount;
        }


        [WebMethod(Description = @"<br><pre>
Получить <b>псевдоним</b> лица по коду лица.
Параметры:
  int id - код лица.
		</pre>
")]
        public string GetCaption(int id)
        {
            string retVal;
            SqlCommand cm = null;
            try
            {
                cm = new SqlCommand("SET @Alias=ISNULL((SELECT Кличка FROM Справочники.dbo.vwЛица WHERE КодЛица=" + id + "),'')");
                cm.Parameters.Add("@Alias", SqlDbType.VarChar, 50);
                cm.Parameters["@Alias"].Direction = ParameterDirection.Output;
                cm.Connection = new SqlConnection(MvcApplication.DS_person);
                cm.Connection.Open();
                cm.ExecuteNonQuery();
                retVal = cm.Parameters["@Alias"].Value.ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка получения названия лица", ex, cm);

            }
            finally
            {
                if (cm != null) cm.Connection.Close();
            }

            if (retVal.Equals("")) retVal = "#" + id.ToString();
            return retVal;
        }


        [WebMethod(Description = @"<br><pre>
			Получить <b>код</b> текущего лица.
		</pre>")]
        public string GetPersonID()
        {
            string retVal = "";
            SqlDataAdapter da = null;
            var dt = new DataTable();
            try
            {
                da = new SqlDataAdapter("SELECT КодЛица FROM vwНастройки", MvcApplication.DS_doc);
                da.Fill(dt);

                if (dt.Rows.Count == 1) retVal = dt.Rows[0]["КодЛица"].ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка получения текущего лица", ex, da.SelectCommand);
            }
            return retVal;
        }

        [WebMethod(Description = @"<br><pre>
			Получить <b>полное название</b> о лице на указанную дату по коду лица.
			Параметры:
				  int    id          - код лица.
				  string date        - дата действия реквизитов, если =<b>null</b>, то реквизиты берутся на текущую дату.
			</pre>")]
        public string GetFullName(int id, string date)
        {
            string retVal;
            var sql = @"
SELECT ISNULL(ISNULL(NULLIF(JP.КраткоеНазваниеРус,''),JP.КраткоеНазваниеЛат), Persons.Кличка) FullName
FROM Справочники.dbo.vwЛица Persons
	LEFT OUTER JOIN	Справочники.dbo.vwКарточкиЮрЛиц JP ON Persons.КодЛица = JP.КодЛица
	LEFT OUTER JOIN	Справочники.dbo.vwКарточкиФизЛиц NP ON Persons.КодЛица = NP.КодЛица
WHERE Persons.КодЛица=@Id AND ((JP.От<=@Date AND JP.До > @Date) OR (NP.От<=@Date AND NP.До > @Date))";

            SqlDataAdapter da = null;
            var dt = new DataTable("info");
            try
            {
                if ((date == null) || (date.Trim().Equals(""))) date = DateTime.Now.ToShortDateString();
                da = new SqlDataAdapter(sql, MvcApplication.DS_person);
                da.SelectCommand.Parameters.AddWithValue("@ID", id);
                da.SelectCommand.Parameters.AddWithValue("@Date", date);

                da.Fill(dt);
                if (dt.Rows.Count == 0) retVal = ""; //throw new Exception("Лицо №" + id + " не найдено или на дату '" + date +"' отсутствуют данные.");
                else if (dt.Rows.Count > 1) retVal = ""; // throw new Exception("Данные лица №" + id + " не корректны.");
                else
                    retVal = dt.Rows[0]["FullName"].ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка получения полного наименования лица", ex, da.SelectCommand);

            }

            if (retVal.Equals("")) retVal = "#" + id.ToString();
            return retVal;
        }


        [WebMethod(Description = @"<br><pre>
Получить <b>тип</b> лица по коду лица.
Параметры:
  int id - код лица.
		</pre>
")]
        public int GetType(int id)
        {
            int retVal;
            SqlCommand cm = null;
            try
            {
                cm = new SqlCommand("SET @Type=ISNULL((SELECT CONVERT(int,ТипЛица) ТипЛица FROM Справочники.dbo.vwЛица WHERE КодЛица=" + id + "),-1)");
                cm.Parameters.Add("@Type", SqlDbType.Int, 4);
                cm.Parameters["@Type"].Direction = ParameterDirection.Output;
                cm.Connection = new SqlConnection(MvcApplication.DS_person);
                cm.Connection.Open();
                cm.ExecuteNonQuery();
                retVal = int.Parse(cm.Parameters["@Type"].Value.ToString());
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка получения типа лица", ex, cm);

            }
            finally
            {
                cm.Connection.Close();
            }

            return retVal;
        }


        [WebMethod(Description = @"<br><pre>
Получить <b>инн</b> лица по коду лица.
Параметры:
  int id - код лица.
		</pre>
")]
        public string GetInn(int id)
        {
            string retVal;
            SqlCommand cm = null;
            try
            {
                cm = new SqlCommand("SET @Inn=ISNULL((SELECT Инн FROM Справочники.dbo.vwЛица WHERE КодЛица=" + id + "),'-1')");
                cm.Parameters.Add("@Inn", SqlDbType.VarChar, 50);
                cm.Parameters["@Inn"].Direction = ParameterDirection.Output;
                cm.Connection = new SqlConnection(MvcApplication.DS_person);
                cm.Connection.Open();
                cm.ExecuteNonQuery();
                retVal = cm.Parameters["@Inn"].Value.ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при получении ИНН лица", ex, cm);
            }
            finally
            {
                cm.Connection.Close();
            }

            if (retVal.Equals("-1")) retVal = "#" + id.ToString();
            return retVal;
        }


        [WebMethod(Description = @"<br><pre>
Получить информацию о лице на указанную дату по коду лица.
Параметры:
      int    id          - код лица.
      string date        - дата действия реквизитов, если =<b>null</b>, то реквизиты берутся на текущую дату.
  out string fullName    - возвращает <b>краткое название(рус.)</b> для юридического и <b>кличку</b> для физического лица.
  out string inn         - возвращает инн лица.
  out string corAccount  - возвращает корр. счет лица.
  out string bik         - возвращает бик лица.
  out string bProject    - возвращает код бизнес проекта.
  out string kpp         - возвращает кпп лица.
  out string jAddress    - возвращает юридический адрес лица.
		</pre>
")]
        public void GetInfo(int id, string date, out string fullName, out string inn, out string corAccount, out string bik, out string bProject, out string kpp, out string jAddress)
        {
            var sql = @"
SELECT
	ISNULL(ISNULL(NULLIF(JP.КраткоеНазваниеРус,''),JP.КраткоеНазваниеЛат), Persons.Кличка) FullName,
	Persons.ИНН INN,
	Persons.КорСчет CorAccount,
	Persons.БИК AS BIK,
	ISNULL(Persons.КодБизнесПроекта,'') BPROJECT,
	ISNULL(CASE WHEN Persons.ТипЛица=1 THEN JP.КПП ELSE NP.КПП END,'') KPP,
	ISNULL(CASE WHEN Persons.ТипЛица=1 THEN ISNULL(NULLIF(JP.АдресЮридический,''),JP.АдресЮридическийЛат) ELSE ISNULL(NULLIF(NP.АдресЮридический,''),NP.АдресЮридическийЛат) END,'') JADDRESS
FROM Справочники.dbo.vwЛица Persons
	LEFT OUTER JOIN (SELECT * FROM Справочники.dbo.vwКарточкиЮрЛиц J  WHERE (J.От<=@Date AND J.До > @Date)) JP ON Persons.КодЛица = JP.КодЛица
	LEFT OUTER JOIN	(SELECT * FROM Справочники.dbo.vwКарточкиФизЛиц N WHERE (N.От<=@Date AND N.До > @Date)) NP ON Persons.КодЛица = NP.КодЛица
WHERE Persons.КодЛица=@Id";

            SqlDataAdapter da = null;
            var dt = new DataTable("info");

            try
            {
                fullName = "#" + id.ToString();
                inn = corAccount = bik = bProject = kpp = jAddress = "";

                if ((date == null) || (date.Trim().Equals(""))) date = DateTime.Now.ToShortDateString();
                da = new SqlDataAdapter(sql, MvcApplication.DS_person);
                da.SelectCommand.Parameters.AddWithValue("@ID", id);
                da.SelectCommand.Parameters.AddWithValue("@Date", date);

                da.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    fullName = dt.Rows[0]["FullName"].ToString();
                    inn = dt.Rows[0]["INN"].ToString();
                    corAccount = dt.Rows[0]["CorAccount"].ToString();
                    bik = dt.Rows[0]["BIK"].ToString();
                    bProject = dt.Rows[0]["BPROJECT"].ToString();
                    kpp = dt.Rows[0]["KPP"].ToString();
                    jAddress = dt.Rows[0]["JADDRESS"].ToString();
                }
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при получении информации по лицу", ex, da.SelectCommand);

            }
        }


        [WebMethod(Description = @"<br><pre>
Получить по контактам лица по коду лица.
Параметры:
      int    id          - код лица.
  out string workPhones    - возвращает рабочие телефоны лица через запятую.
  out string postAddress   - возвращает почтовый адрес лица.
		</pre>
")]
        public void GetContactInfo(int id, out string workPhones, out string postAddress)
        {
            var sql = @"SELECT КодТипаКонтакта, Контакт FROM vwКонтакты WHERE КодЛица=@Id";
            SqlDataAdapter da = null;
            var dt = new DataTable("info");

            try
            {
                workPhones = postAddress = "";
                da = new SqlDataAdapter(sql, MvcApplication.DS_person);
                da.SelectCommand.Parameters.AddWithValue("@Id", id);

                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch ((int)dt.Rows[i]["КодТипаКонтакта"])
                    {
                        case 20:
                            workPhones += ", " + dt.Rows[i]["Контакт"].ToString();
                            break;
                        case 13:
                            if (postAddress != "") continue;
                            else postAddress = dt.Rows[i]["Контакт"].ToString();
                            break;
                    }
                }
                if (workPhones != "") workPhones = workPhones.Substring(1).Trim();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при получении информации по контактам лица", ex, da.SelectCommand);

            }
        }


        [WebMethod(Description = @"<br><pre>
Получить <b>типы контактов</b> лица.
		</pre>
")]
        public string GetContactTypes()
        {
            string retVal = "";
            SqlDataAdapter da = null;
            var dt = new DataTable();
            try
            {
                da = new SqlDataAdapter("SELECT КодТипаКонтакта,ТипКонтакта FROM Справочники.dbo.ТипыКонтактов", MvcApplication.DS_person);
                da.Fill(dt);
                if (dt.Rows.Count == 0) return retVal;

                for (int i = 0; i < dt.Rows.Count; i++)
                    retVal += ((retVal.Length > 0) ? ";" : "") + dt.Rows[i][0].ToString() + ";" + dt.Rows[i][1].ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при получении типов контактов", ex, da.SelectCommand);
            }

            return retVal;
        }


        //+++++++++++++++//Сервисы для ЖД Объектов


        [WebMethod(Description = @"<br><pre>
Получить <b>название</b> по коду мпс.
Параметры:
  int id - код мпс.
		</pre>
")]
        public string RW_GetCaption(int id)
        {
            string retVal;
            SqlCommand cm = null;
            try
            {
                cm = new SqlCommand(@"
SET @Alias=ISNULL((SELECT СтанцияЖД  Псевдоним FROM Справочники.dbo.[СтанцииЖД] WHERE КодМПС="
                    + id + "),'')");

                cm.Parameters.Add("@Alias", SqlDbType.VarChar, 60);
                cm.Parameters["@Alias"].Direction = ParameterDirection.Output;
                cm.Connection = new SqlConnection(MvcApplication.DS_person);
                cm.Connection.Open();
                cm.ExecuteNonQuery();
                retVal = cm.Parameters["@Alias"].Value.ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("шибка при получении названия транспортного узла", ex, cm);

            }
            finally
            {
                cm.Connection.Close();
            }

            if (retVal.Equals("")) retVal = "#" + id.ToString();
            return retVal;
        }


        [WebMethod(Description = @"<br><pre>
Получить <b>название</b> по коду мпс в формате <i>название(кодМПС), где кодМПС имеет формат 000000</i>.
Параметры:
  int id - код мпс.
		</pre>
")]
        public string RW_GetFormatCaption(int id)
        {
            string retVal;
            SqlCommand cm = null;
            try
            {
                cm = new SqlCommand(@"
SET @Alias=ISNULL((SELECT СтанцияЖД + '(' + REPLACE(STR(КодМПС,6),' ','0') + ')' Псевдоним FROM Справочники.dbo.[СтанцииЖД] WHERE КодМПС="
                    + id + "),'')");

                cm.Parameters.Add("@Alias", SqlDbType.VarChar, 60);
                cm.Parameters["@Alias"].Direction = ParameterDirection.Output;
                cm.Connection = new SqlConnection(MvcApplication.DS_person);
                cm.Connection.Open();
                cm.ExecuteNonQuery();
                retVal = cm.Parameters["@Alias"].Value.ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при получении форматированного названия транспортного узла", ex, cm);
            }
            finally
            {
                cm.Connection.Close();
            }

            if (retVal.Equals("")) retVal = "#" + id.ToString();
            return retVal;
        }


        [WebMethod(Description = @"<br><pre>
Осуществляет поиск лиц в поле <b>КличкаRL</b> с применением функций преобразования <i>fn_ReplaceKeySymbols</i>, <i>fn_SplitWords</i> и <i>fn_ReplaceRusLat</i>.
Возвращаемое значение - <i>количество найденных лиц</i>.
Параметры:
  out int    id           - код найденного лица (если количество найденнных лиц <b>=1</b>.)
      string searchText   - строка поиска.
      string searchParams - параметры поиска в фомате(<i>param1=val1&amp;param2=val2...</i>, на данный момент реализована обработка параметров <b>PersonType</b>,<b>PersonСheck</b>,
                            <b>personallbproject</b>,<b>personexcept</b>)
</pre>
")]
        public string RW_Search(string searchText, string searchParams)
        {
            var header = "";
            var footer = "";

            var sql = "";
            var sqlWhere = "";

            var idlimit = "";
            var idrecommended = "";
            var RailWay = -1;

            SqlDataAdapter da = null;
            var ds = new DataSet();
            var dt = new DataTable();
            DataTable dt1 = null;

            if (searchParams != "")
            {
                string[] p = searchParams.Split('&');
                string[] v;
                for (var i = 0; i < p.Length; i++)
                {
                    v = p[i].Split('=');
                    switch (v[0].ToLower())
                    {
                        case "rwstationroad":
                            if (v[1] != "") RailWay = int.Parse(v[1]);
                            break;
                        case "idlimit":
                            if (v[1] != "") idlimit = v[1];
                            break;
                        case "idrecommended":
                            if (v[1] != "") idrecommended = v[1];
                            break;
                        case "header":
                            if (v[1] != "") header = v[1];
                            break;
                        case "footer":
                            if (v[1] != "") footer = v[1];
                            break;
                    }
                }
            }

            idlimit = (!Regex.IsMatch(idlimit, "^[0-9,]{1,}$", RegexOptions.IgnoreCase)) ? "" : idlimit;
            idrecommended = (!Regex.IsMatch(idrecommended, "^[0-9,]{1,}$", RegexOptions.IgnoreCase)) ? "" : idrecommended;

            if (idlimit != "")
            {
                sqlWhere += " AND + КодМПС IN(" + idlimit + ")";
                footer = "";
            }
            else if (idrecommended != "") sqlWhere += " AND + КодМПС IN(" + idrecommended + ")";

            searchText = searchText.Trim();
            try
            {
                da = new SqlDataAdapter(sql, MvcApplication.DS_person);
                if (Regex.IsMatch(searchText, "[0-9]", RegexOptions.IgnoreCase))
                    sql = @"
SELECT КодМПС, СтанцияЖД, ЖелезнаяДорога FROM
		dbo.СтанцииЖД ST
		INNER JOIN dbo.ЖелезныеДороги RW ON ST.КодЖелезнойДороги=RW.КодЖелезнойДороги
	WHERE КодМПС=@Search
ORDER BY СтанцияЖД,ЖелезнаяДорога
";
                else
                {
                    sql = @"
SET @Search = Инвентаризация.dbo.fn_ReplaceKeySymbols(Инвентаризация.dbo.fn_SplitWords(Инвентаризация.dbo.fn_ReplaceRusLat(@Search)))
SET @Search = RTRIM(LTRIM(@Search))
WHILE CHARINDEX('  ',@Search) > 0 SET @Search = REPLACE(@Search,'  ',' ')

	SET @Search=REPLACE(' ' + @Search,' ','% ') + '%'

	SELECT TOP 50 КодМПС, СтанцияЖД, ЖелезнаяДорога FROM
		dbo.СтанцииЖД ST
		INNER JOIN dbo.ЖелезныеДороги RW ON ST.КодЖелезнойДороги=RW.КодЖелезнойДороги
	WHERE (' ' + СтанцияЖДRL) LIKE @Search
			AND (ST.КодЖелезнойДороги=@RailWay OR @RailWay=-1)
" + sqlWhere + " ORDER BY СтанцияЖД,ЖелезнаяДорога";
                    da.SelectCommand.Parameters.AddWithValue("@RailWay", RailWay);
                }
                if (idrecommended != "")
                    sql += @"
SELECT КодМПС, СтанцияЖД, ЖелезнаяДорога
FROM dbo.СтанцииЖД ST
		INNER JOIN dbo.ЖелезныеДороги RW ON ST.КодЖелезнойДороги=RW.КодЖелезнойДороги
WHERE КодМПС IN(" + idrecommended + @")
ORDER BY СтанцияЖД,,ЖелезнаяДорога
";
                da.SelectCommand.CommandText = sql;
                da.SelectCommand.Parameters.AddWithValue("@Search", searchText);
                da.Fill(ds);

                dt = ds.Tables[0];

                if ((idrecommended != "") && (ds.Tables.Count == 2))
                    dt1 = ds.Tables[1];
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при поиске траспортного узла", ex, da.SelectCommand);

            }

            XmlElement el;
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("SearchResult"));
            doc.DocumentElement.SetAttribute("showColumnNames", "true");

            if (footer != "") doc.DocumentElement.SetAttribute("advancedSearchRowCaption", footer);
            if (header != "") doc.DocumentElement.SetAttribute("caption", header);

            doc.DocumentElement.SetAttribute("showAdvancedSearchRow",
                                              (
                                                (dt.Rows.Count == 1) ||
                                                    (idlimit != "" && dt.Rows.Count > 0) ||
                                                    (idlimit == "" && idrecommended == "") ||
                                                    (idrecommended != "" && dt.Rows.Count == dt1.Rows.Count)) ? "false" : "true");

            doc.DocumentElement.AppendChild(el = doc.CreateElement("Column"));
            el.SetAttribute("name", "KodMps");
            el.SetAttribute("caption", "КодМПС");
            el.SetAttribute("width", "15px");

            doc.DocumentElement.AppendChild(el = doc.CreateElement("Column"));
            el.SetAttribute("name", "StantsiyaZhD");
            el.SetAttribute("caption", "СтанцияЖД");
            el.SetAttribute("width", "235px");

            doc.DocumentElement.AppendChild(el = doc.CreateElement("Column"));
            el.SetAttribute("name", "ZheleznayaDoroga");
            el.SetAttribute("caption", "ЖелезнаяДорога");
            el.SetAttribute("width", "150px");

            if (dt1 != null && dt1.Rows.Count > 0 && dt.Rows.Count == 0) dt = dt1;

            foreach (DataRow row in dt.Rows)
            {
                doc.DocumentElement.AppendChild(el = doc.CreateElement("Row"));
                el.SetAttribute("ID", row[0].ToString());
                el.SetAttribute("KodMps", row[0].ToString());
                el.SetAttribute("StantsiyaZhD", row[1].ToString());
                el.SetAttribute("ZheleznayaDoroga", row[2].ToString());

            }

            return doc.OuterXml;

        }


        [WebMethod(Description = @"<br><pre>
Осуществляет проверку на вхождение страны в таможенный союз.
Возвращаемое значение - <i>true/false</i>.
Параметры:
  out bool                - true если входит в таможенный союз.
      int idCountry       - код страны.
</pre>
")]

        public int IsCountryInCustomUnion(int idCountry)
        {
            string q = "SELECT COUNT(*) FROM ТаможенныйСоюз WHERE КодТерритории = " + idCountry;
            var conn = new SqlConnection(MvcApplication.DS_person);
            var cm = new SqlCommand(q, conn);
            int rezult;
            try
            {
                conn.Open();
                rezult = ((int)cm.ExecuteScalar());
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при проверке вхождения лица в таможенный союз", ex, cm);

            }
            finally
            {
                conn.Close();
            }
            return rezult;
        }


        [WebMethod(Description = @"<br><pre>
Осуществляет проверку на вхождение страны в таможенный союз.
Возвращаемое значение - <i>true/false</i>.
Параметры:
  out bool                - true если входит в таможенный союз.
      int idCountry       - код страны.
</pre>
")]
        public string Contact(string АдресИндекс, string АдресОбласть, string АдресГород, string Адрес, int КодСтраны)
        {
            string q = "SELECT dbo.fn_Контакт(@АдресИндекс, @АдресОбласть, @АдресГород, '', @Адрес, @КодСтраны, 0)";
            var conn = new SqlConnection(MvcApplication.DS_person);
            var cm = new SqlCommand(q, conn);
            cm.Parameters.AddWithValue("@АдресИндекс", АдресИндекс);
            cm.Parameters.AddWithValue("@АдресОбласть", АдресОбласть);
            cm.Parameters.AddWithValue("@АдресГород", АдресГород);
            cm.Parameters.AddWithValue("@Адрес", Адрес);
            cm.Parameters.AddWithValue("@КодСтраны", КодСтраны);
            string rezult;
            try
            {
                conn.Open();
                rezult = cm.ExecuteScalar().ToString();
            }

            catch (Exception ex)
            {
                throw new DetailedException("Ошибка при формировании полного контакта", ex, cm);

            }
            finally
            {
                conn.Close();
            }
            return rezult;
        }
    }
}
