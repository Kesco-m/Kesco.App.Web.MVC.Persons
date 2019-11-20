;(function($){
/**
 * jqGrid Italian Translation
 * Tony Tomov tony@trirand.com
 * http://trirand.com/blog/ 
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
**/
$.jgrid = $.jgrid || {};
$.extend($.jgrid,{
	defaults : {
		recordtext: "Mostra {0} - {1} di {2}",
		emptyrecords: "Non ci sono record da mostrare",
		loadtext: "Caricamento...",	
		pgtext : "Pagina {0} suc {1}"	
	},
	search : {
		caption: "Cerca...",
		Find: "Trova",
		Reset: "Reset",
		odata : ['equal', 'not equal', 'less', 'less or equal','greater','greater or equal', 'begins with','does not begin with','is in','is not in','ends with','does not end with','contains','does not contain'],
		groupOps: [	{ op: "AND", text: "all" },	{ op: "OR",  text: "any" }	],
		matchText: " match",
		rulesText: " rules"
	},
	edit : {
		addCaption: "Aggiungi Record",
		editCaption: "Modifica Record",
		bSubmit: "Invia",
		bCancel: "Annulla",
		bClose: "Chiudi",
		saveData: "I dati sono stati modificati! Salvare le modifiche?",
		bYes : "Si",
		bNo : "No",
		bExit : "Annulla",
		msg: {
			required:"Campo obbligatorio",
			number:"Per favore, inserisci un numero valido",
			minValue:"il valore deve essere maggiore o uguale a ",
			maxValue:"il valore deve essere minore o uguale a ",
			email: "non è una e-mail valida",
			integer: "Per favore, inserisci un intero valido",
			date: "Per favore, inserisci una data valida",
			url: "non è un URL valido. Prefissi richiesti ('http://' o 'https://')",
			nodefined : " non è definito!",
			novalue : " valore di ritorno richiesto!",
			customarray : "La funzione personalizzata deve restituire un array!",
			customfcheck : "La funzione personalizzata deve essere presente in caso di controlli personalizzati!"
			
		}
	},
	view : {
		caption: "Visualizza Record",
		bClose: "Chiudi"
	},
	del : {
		caption: "Cancella",
		msg: "Cancellare i record selezionati?",
		bSubmit: "Canella",
		bCancel: "Annulla"
	},
	nav : {
		edittext: "",
		edittitle: "Modifica riga selezionata",
		addtext:"",
		addtitle: "Aggiungi riga",
		deltext: "",
		deltitle: "Cancella riga",
		searchtext: "",
		searchtitle: "Trova record",
		refreshtext: "",
		refreshtitle: "Ricarica tabella",
		alertcap: "Attenzione",
		alerttext: "Per favore, seleziona un record",
		viewtext: "",
		viewtitle: "Visualizza riga selezionata"
	},
	col : {
		caption: "Seleziona colonne",
		bSubmit: "Ok",
		bCancel: "Annulla"
	},
	errors : {
		errcap : "Errore",
		nourl : "Nessun url impostato",
		norecords: "Non ci sono record da elaborare",
		model : "Lunghezza dei colNames <> colModel!"
	},
	formatter:{
		integer : {thousandsSeparator: ",", defaultValue: '0'},
		number : {decimalSeparator:".", thousandsSeparator: ",", decimalPlaces: 2, defaultValue: '0.00'},
		currency : {decimalSeparator:".", thousandsSeparator: ",", decimalPlaces: 2, prefix: "", suffix:"", defaultValue: '0.00'},
		date:{
			dayNames:["Dom","Lun","Mar","Mer","Gio","Ven","Sab","Domenica","Lunedì","Martedì","Mercoledì","Giovedì","Venerdì","Sabato"],
			monthNames:["Gen","Feb","Mar","Apr","Mag","Giu","Lug","Ago","Set","Ott","Nov","Dic","Gennaio","Febbraio","Marzo","Aprile","Maggio","Giugno","Luglio","Agosto","Settembre","Ottobre","Novembre","Dicembre"],
			AmPm:["am","pm","AM","PM"],
			S:function(b){return b<11||b>13?["st","nd","rd","th"][Math.min((b-1)%10,3)]:"th"},
			srcformat:"Y-m-d",
			newformat: 'n/j/Y',			
			masks:{
				ISO8601Long:"Y-m-d H:i:s",
				ISO8601Short:"Y-m-d", 			
				SortableDateTime:"Y-m-d\\TH:i:s",
				UniversalSortableDateTime:"Y-m-d H:i:sO",
				YearMonth:"F, Y"
			},
			reformatAfterEdit:false
		},
		baseLinkUrl:"",
		showAction:"",
		target:"",
		checkbox:{ disabled:true},
		idName:"id"
	},
	colmenu : {
		sortasc : "Sort Ascending",
		sortdesc : "Sort Descending",
		columns : "Columns",
		filter : "Filter",
		grouping : "Group By",
		ungrouping : "Ungroup",
		searchTitle : "Get items with value that:",
		freeze : "Freeze",
		unfreeze : "Unfreeze",
		reorder : "Move to reorder"
	}
});
})(jQuery);
