using System.Collections.Generic;
using BLToolkit.Aspects;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Reflection;
using Kesco.DataAccess;

namespace Kesco.Web.Mvc.SharedViews.Models
{
	public static class TapiClient
	{
		public static List<AvailablePhone> GetClientPhoneNumbers(string networkName)
		{
			return TypeAccessor<Accessor>.CreateInstanceEx().GetClientPhoneNumbers(networkName);
		}

        public static string GetPhoneNumberDialDigits(string phoneNumber, int phoneStationCode)
		{
			return TypeAccessor<Accessor>.CreateInstanceEx().GetPhoneNumberDialDigits(phoneNumber, phoneStationCode);
		}

		public static string GetInternationalNumber(string phoneNumber, string ext)
		{
			return TypeAccessor<Accessor>.CreateInstanceEx().GetInternationalNumber(phoneNumber, ext);
		}

		
	}

	/// <summary>
	/// Класс описывает доступный телефон сотрудника
	/// </summary>
	public class AvailablePhone
	{
		[MapField("Исходящий")]
		[MapValue(0, false)]
		[MapValue(1, true)]
		public bool Outcoming { get; set; }

		/// <summary>
		/// Gets or sets the equipment ID.
		/// </summary>
		/// <value>
		/// The equipment ID.
		/// </value>
		[MapField("КодОборудования")]
		public int EquipmentID { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		/// <value>
		/// The phone number.
		/// </value>
		[MapField("ТелефонныйНомер")]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the CTI.
		/// </summary>
		/// <value>
		/// The CTI.
		/// </value>
		[MapField("CTI")]
		public string CTI { get; set; }

		/// <summary>
		/// Gets or sets the phone station code.
		/// </summary>
		/// <value>
		/// The phone station code.
		/// </value>
		[MapField("КодТелефоннойСтанции")]
		public int PhoneStationCode { get; set; }

		/// <summary>
		/// Gets or sets the equipment ID.
		/// </summary>
		/// <value>
		/// The equipment ID.
		/// </value>
		[MapField("КодТипаОборудования")]
		public int EquipmentTypeID { get; set; }

		/// <summary>
		/// Gets or sets the type of the equipment.
		/// </summary>
		/// <value>
		/// The type of the equipment.
		/// </value>
		[MapField("Тип")]
		public string EquipmentType { get; set; }

		/// <summary>
		/// Gets or sets the equipment.
		/// </summary>
		/// <value>
		/// The equipment.
		/// </value>
		[MapField("Оборудование")]
		public string Equipment { get; set; }

		/// <summary>
		/// Gets or sets the name of the net.
		/// </summary>
		/// <value>
		/// The name of the net.
		/// </value>
		[MapField("СетевоеИмя")]
		public string NetName { get; set; }

		/// <summary>
		/// Gets or sets the slave.
		/// </summary>
		/// <value>
		/// The slave.
		/// </value>
		[MapField("Ведомый")]
		public string Slave { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		[MapField("Расположение")]
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[MapField("ОписаниеАТС")]
		public string Description { get; set; }
	}

	public abstract class Accessor : DataAccessor
	{
		public class DB : Database { public DB() : base("DS_User") { } }

		[NoInterception]
		protected override DbManager CreateDbManager() { return new DB(); }

		[SprocName("sp_ДоступныеТелефоны")]
		public abstract List<AvailablePhone> GetClientPhoneNumbers(string @СетевоеИмя);

        [SqlQuery("select dbo.fn_НомерМеждународный2НомерНабора(@МеждународныйНомер, @КодТелефоннойСтанции, NULL) НомерНабора ")]
		public abstract string GetPhoneNumberDialDigits(string @МеждународныйНомер, int @КодТелефоннойСтанции);

		[SqlQuery(@"SELECT dbo.fn_Лица_ФормированиеКонтакта(20, '', '', '', '', '', '', '', '', 
					@ТелефонНомер, @ТелефонДоп, '', 1
		)")]
		public abstract string GetInternationalNumber(string @ТелефонНомер, string ТелефонДоп = "");

	}

}