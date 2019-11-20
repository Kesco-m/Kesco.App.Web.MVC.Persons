
function KescoInitDateRangeControl(controlID, fromDatePickerID, toDatePickerID) {
	var rangeSelectorName = controlID+'_RangeSelector';
	var dateFromName = ((fromDatePickerID)?fromDatePickerID:controlID+'_From');
	var dateToName = ((toDatePickerID)?toDatePickerID:controlID+'_To');
	var prevButtonName = controlID+'___PrevButton';
	var nextButtonName = controlID+'___NextButton';
	var fromLabelName = controlID+'___CellLabelFrom';
	var toLabelName = controlID+'___CellLabelTo';
	var dateToCellName = controlID+'___CellDatePickerTo';

	$(dateFromName).add(dateToName).datepicker("option", "onSelect", function(dateText, instance) {
		var range = $(rangeSelectorName).val();
		if (range != 'day')
			$(rangeSelectorName).selectmenu('value', 'any').change();
		instance.input.trigger("change");
		return true;
	});

	$(prevButtonName).add(nextButtonName).click(function () {
		var range = $(rangeSelectorName).val();
		var self = this;
		var dateFrom = $(dateFromName).datepicker('getDate');
		var dateTo = $(dateToName).datepicker('getDate');
		if (!dateFrom) dateFrom = new Date();
		if (!dateTo) dateTo = new Date();
		var sign = ($(this).attr('id') == (nextButtonName.substr(1,nextButtonName.length-1)))? -1 : 1;
		switch (range) {
		case 'day':
			dateFrom.setDate(dateFrom.getDate()- (sign * 1));
			break;
		case 'week':
			dateFrom.setDate(dateFrom.getDate() - (sign * 7));
			break;
		case 'month':
			dateFrom.setDate(1);
			dateFrom.setMonth(dateFrom.getMonth()-(sign * 1));
			break;
		case 'quarter':
			dateFrom.setDate(1);
			dateFrom.setMonth(parseInt((dateFrom.getMonth() / 3)) * 3 - (sign*3));
			break;
		case 'year':
			dateFrom.setDate(1);
			dateFrom.setMonth(0);
			dateFrom.setMonth(-(sign*12));
			break;
		default:
			dateFrom.setDate(dateFrom.getDate()- (sign * 1));
			dateTo.setDate(dateTo.getDate()- (sign * 1));
			$(dateToName).datepicker('setDate', dateTo).trigger("change");
			break;
		}
		$(dateFromName).datepicker('setDate', dateFrom).trigger("change");
		$(rangeSelectorName).change();
	});
		
	$(rangeSelectorName).selectmenu({
		style: 'dropdown'
		//, width: '150px'
	}).change(function() {
		var self = this;
		var range = $(this).val();
		var dateFrom = $(dateFromName).datepicker("getDate");
		var dateTo = $(dateToName).datepicker("getDate");
		if (!dateFrom) dateFrom = new Date();
		if (!dateTo) dateTo = new Date();

		$(dateFromName).datepicker('hide');
		$(dateToName).datepicker('hide');
		//$("#dateFrom").datepicker('option', 'maxDate', new Date(3000, 0, 1));
		//$("#dateTo").datepicker('option', 'minDate', new Date(0, 0, 1));
		switch (range) {
		case 'day':
			dateTo = new Date(dateFrom);
			$(fromLabelName).hide();
			$(toLabelName).hide();
			$(dateToName).datepicker('disable').hide();
			$(dateToCellName).hide();
			break;
		case 'week':
			var weekDay = dateFrom.getDay();
			dateFrom.setDate(dateFrom.getDate() - (weekDay ? (weekDay-1) : 6));
			dateTo  = new Date(dateFrom);
			dateTo.setDate(dateFrom.getDate() + 6);
			$(fromLabelName).show();
			$(toLabelName).show();
			$(dateToName).datepicker('enable').show();
			$(dateToCellName).show();
			break;
		case 'month':
			dateFrom.setDate(1);
			dateTo  = new Date(dateFrom);
			dateTo.setMonth(dateFrom.getMonth()+1);
			dateTo.setDate(0);
			$(fromLabelName).show();
			$(toLabelName).show();
			$(dateToName).datepicker('enable').show();
			$(dateToCellName).show();
			break;
		case 'quarter':
			dateFrom.setDate(1);
			dateFrom.setMonth(parseInt((dateFrom.getMonth() / 3)) * 3);
			dateTo  = new Date(dateFrom);
			dateTo.setMonth(dateFrom.getMonth()+3);
			dateTo.setDate(0);
			$(fromLabelName).show();
			$(toLabelName).show();
			$(dateToName).datepicker('enable').show();
			$(dateToCellName).show();
			break;
		case 'year':
			dateFrom.setDate(1);
			dateFrom.setMonth(0);
			dateTo  = new Date(dateFrom);
			dateTo.setYear(dateFrom.getFullYear()+1);
			dateTo.setDate(0);
			$(fromLabelName).show();
			$(toLabelName).show();
			$(dateToName).datepicker('enable').show();
			$(dateToCellName).show();
			break;
		default:
			$(fromLabelName).show();
			$(toLabelName).show();
			$(dateToName).datepicker('enable').show();
			$(dateToCellName).show();
			if (dateFrom.valueOf() > dateTo.valueOf()) dateTo = dateFrom;
			break;
		}
		$(dateFromName).datepicker("setDate", dateFrom).trigger("change");
		//$(dateFromName).datepicker('option', 'maxDate', dateTo);
		//$(dateToName).datepicker('option', 'minDate', dateFrom);
		$(dateToName).datepicker("setDate", dateTo).trigger("change");
		return true;
	}).change();
}
