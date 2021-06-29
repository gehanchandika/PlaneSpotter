(function () {
    ko.bindingHandlers.datePicker = {
        //    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        //        ko.utils.registerEventHandler(element, "change", function () {
        //            debugger;
        //            var value = valueAccessor();
        //            value(new Date(element.value));
        //        });
        //    },
        //    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        //        var value = valueAccessor();
        //        element.value = value().toISOString();
        //    }
        //};

        //ko.bindingHandlers.timepicker = {
        //    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        //        $(element).datetimepicker({
        //            format: 'HH:mm',
        //            allowInputToggle: true
        //        });
        //    }
        //};

        ////Invalid input is validated and set to current date
        //ko.bindingHandlers.datepicker = {
        //    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        //        var dt = allBindings.get('dateText');
        //        SetDatePicker(element, dt)
        //    }
        //};

        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            //initialize datepicker with some optional options
            var options = {
                format: 'DD/MM/YYYY HH:mm',
                defaultDate: valueAccessor()()
            };

            if (allBindingsAccessor() !== undefined) {
                if (allBindingsAccessor().datepickerOptions !== undefined) {
                    options.format = allBindingsAccessor().datepickerOptions.format !== undefined ? allBindingsAccessor().datepickerOptions.format : options.format;
                }
            }

            $(element).datetimepicker(options);
            var picker = $(element).data('datetimepicker');

            //when a user changes the date, update the view model
            ko.utils.registerEventHandler(element, "dp.change", function (event) {
                var value = valueAccessor();
                if (ko.isObservable(value)) {
                    value(event.date);
                }
            });

            var defaultVal = $(element).val();
            var value = valueAccessor();
            value(moment(defaultVal, options.format));
        },
        update: function (element, valueAccessor) {
            var widget = $(element).data("datepicker");
            //when the view model is updated, update the widget
            if (widget) {
                widget.date = ko.utils.unwrapObservable(valueAccessor());
                if (widget.date) {
                    widget.setValue();
                }
            }
        }
    };
})();