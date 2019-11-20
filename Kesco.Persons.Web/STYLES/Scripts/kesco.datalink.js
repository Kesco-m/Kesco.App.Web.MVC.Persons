
function datalink_ConvertDateFromDatePicker(value, source, target) {
	// source - html input control
	// target - linked object
	var $source = $(source);
	var prop = $source.attr('name') || $source.attr('id');
	target[prop] = $source.datepicker("getDate");
}

function datalink_ConvertDateToDatePicker(value, source, target) {
	$(target).datepicker("setDate", value);
}


$.convertFn.getDateFromDatePicker = function (value, source, target) {
	// source - html input control
	// target - linked object
	var $source = $(source);
	var prop = $source.attr('name') || $source.attr('id');
	target[prop] = $source.datepicker("getDate");
}

$.convertFn.setDateToDatePicker = function (value, source, target) {
	$(target).datepicker("setDate", value);
}

$.convertFn.getTimeFromTimeEntry = function (value, source, target) {
	// source - html input control
	// target - linked object
	var $source = $(source);
	var prop = $source.attr('name') || $source.attr('id');
	target[prop] = $source.timeEntry("getTime");
}

$.convertFn.setTimeToTimeEntry = function (value, source, target) {
	$(target).timeEntry("setTime", value);
}

$.convertFn.getNumeric = function (value, source, target) {
	// source - html input control
	// target - linked object
	var $source = $(source);
	var prop = $source.attr('name') || $source.attr('id');
	target[prop] = $source.numeric("value");
}

$.convertFn.setNumeric = function (value, source, target) {
	$(target).numeric("value", value);
}
