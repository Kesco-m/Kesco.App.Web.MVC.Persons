/*
<appSettings>
	<add key="Culture.EnglishName" value="Corporate Culture Settings"/>
	<add key="Culture.NativeName" value="Корпоративные настройки культуры"/>
	<add key="Culture.Number.PositivePattern" value="n"/>
	<add key="Culture.Number.NegativePattern" value="-n"/>
	<add key="Culture.Number.DecimalSeparator" value=","/>
	<add key="Culture.Number.ThousandSeparator" value=" "/>
	<add key="Culture.Number.Percent.PositivePattern" value="n%"/>
	<add key="Culture.Number.Percent.NegativePattern" value="-n%"/>
	<add key="Culture.Number.Percent.DecimalSeparator" value=","/>
	<add key="Culture.Number.Percent.ThousandSeparator" value=" "/>
	<add key="Culture.Number.Currency.PositivePattern" value="n$"/>
	<add key="Culture.Number.Currency.NegativePattern" value="-n$"/>
	<add key="Culture.Number.Currency.DecimalSeparator" value=","/>
	<add key="Culture.Number.Currency.ThousandSeparator" value=" "/>
	<add key="Culture.Number.Currency.Symbol" value=""/>
</appSettings>
*/

(function( window, undefined ) {

var Globalize;

if ( typeof require !== "undefined"
	&& typeof exports !== "undefined"
	&& typeof module !== "undefined" ) {
	// Assume CommonJS
	Globalize = require( "globalize" );
} else {
	// Global variable
	Globalize = window.Globalize;
}

var cultureInfo = {
	name: "kesco",
	englishName: "Corporate Culture Settings",
	nativeName: "Корпоративные настройки культуры",
	language: "kesco",
	numberFormat: {
		",": " ",
		".": ",",
		negativeInfinity: "-бесконечность",
		positiveInfinity: "бесконечность",
		percent: {
			pattern: ["-n%","n%"],
			",": " ",
			".": ","
		},
		currency: {
			pattern: ["-n$","n$"],
			",": " ",
			".": ",",
			symbol: "р."
		}
	},
	calendars: {
		standard: {
			"/": ".",
			firstDay: 1,
			days: {
				names: ["воскресенье","понедельник","вторник","среда","четверг","пятница","суббота"],
				namesAbbr: ["Вс","Пн","Вт","Ср","Чт","Пт","Сб"],
				namesShort: ["Вс","Пн","Вт","Ср","Чт","Пт","Сб"]
			},
			months: {
				names: ["Январь","Февраль","Март","Апрель","Май","Июнь","Июль","Август","Сентябрь","Октябрь","Ноябрь","Декабрь",""],
				namesAbbr: ["янв","фев","мар","апр","май","июн","июл","авг","сен","окт","ноя","дек",""]
			},
			monthsGenitive: {
				names: ["января","февраля","марта","апреля","мая","июня","июля","августа","сентября","октября","ноября","декабря",""],
				namesAbbr: ["янв","фев","мар","апр","май","июн","июл","авг","сен","окт","ноя","дек",""]
			},
			AM: null,
			PM: null,
			patterns: {
				d: "dd.MM.yyyy",
				D: "d MMMM yyyy 'г.'",
				t: "H:mm",
				T: "H:mm:ss",
				f: "dd.MM.yyyy H:mm",
				F: "dd.MM.yyyy H:mm:ss",
				Y: "MMMM yyyy"
			}
		}
	}
};

Globalize.addCultureInfo("kesco", "default", cultureInfo);

}( this ));
