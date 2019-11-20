using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Kesco.Web.Mvc
{
	public static class CultureInfoExtensions
	{
		public static CultureInfo ToCorporateCulture(this CultureInfo ci, CultureSettings settings)
		{
			CultureInfo corporate = (CultureInfo) ci.Clone();

			corporate.NumberFormat.NumberNegativePattern = settings.Number.NegativePattern.ToNumberNegativePattern();
			corporate.NumberFormat.NumberDecimalSeparator = settings.Number.DecimalSeparator;
			corporate.NumberFormat.NumberGroupSeparator = settings.Number.GroupSeparator;
			corporate.NumberFormat.NumberGroupSizes = settings.Number.GroupSizes.ToArray<int>(new char[] { ',' }, s => Int32.Parse(s));
			corporate.NumberFormat.NumberDecimalDigits = settings.Number.Decimals;

			corporate.NumberFormat.PercentNegativePattern = settings.Percent.NegativePattern.ToPercentNegativePattern();
			corporate.NumberFormat.PercentPositivePattern = settings.Percent.PositivePattern.ToPercentPositivePattern();
			corporate.NumberFormat.PercentDecimalSeparator = settings.Percent.DecimalSeparator;
			corporate.NumberFormat.PercentGroupSeparator = settings.Percent.GroupSeparator;
			corporate.NumberFormat.PercentGroupSizes = settings.Percent.GroupSizes.ToArray<int>(new char[] { ',' }, s => Int32.Parse(s));
			corporate.NumberFormat.PercentDecimalDigits = settings.Percent.Decimals;
			corporate.NumberFormat.PercentSymbol = settings.Percent.Symbol;

			corporate.NumberFormat.CurrencyNegativePattern = settings.Currency.NegativePattern.ToCurrencyNegativePattern();
			corporate.NumberFormat.CurrencyPositivePattern = settings.Currency.PositivePattern.ToCurrencyPositivePattern();
			corporate.NumberFormat.CurrencyDecimalSeparator = settings.Currency.DecimalSeparator;
			corporate.NumberFormat.CurrencyGroupSeparator = settings.Currency.GroupSeparator;
			corporate.NumberFormat.CurrencyGroupSizes = settings.Currency.GroupSizes.ToArray<int>(new char[] { ',' }, s => Int32.Parse(s));
			corporate.NumberFormat.CurrencyDecimalDigits = settings.Currency.Decimals;
			corporate.NumberFormat.CurrencySymbol = settings.Currency.Symbol;

			corporate.DateTimeFormat.DateSeparator = settings.DateTime.DatePartsSeparator;
			corporate.DateTimeFormat.TimeSeparator = settings.DateTime.TimePartsSeparator;
			corporate.DateTimeFormat.AMDesignator = settings.DateTime.AM;
			corporate.DateTimeFormat.PMDesignator = settings.DateTime.PM;

			corporate.DateTimeFormat.ShortDatePattern = settings.DateTime.ShortDatePattern;
			corporate.DateTimeFormat.LongDatePattern = settings.DateTime.LongDatePattern;
			corporate.DateTimeFormat.ShortTimePattern = settings.DateTime.ShortTimePattern;
			corporate.DateTimeFormat.LongTimePattern = settings.DateTime.LongTimePattern;
			corporate.DateTimeFormat.FullDateTimePattern = settings.DateTime.FullDateTimePattern;

			return corporate;
		}
	}
}
