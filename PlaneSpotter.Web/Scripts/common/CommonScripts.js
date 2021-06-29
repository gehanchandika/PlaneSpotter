function SetDatePicker(element, dateText) {
    debugger;
    $.datepicker.setDefaults($.datepicker.regional['en-GB']);
    $(element).datepicker({
        format: window.$DateFormat,
        autoclose: true,
        todayHighlight: true,
        orientation: 'bottom',
        forceParse: true
    });

    if (dateText != undefined) {
        $(element).datepicker("setDate", BuildValidDateFromText(dateText));
    }
}