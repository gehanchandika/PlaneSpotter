
var PlaneSpotterVM = {

    PageTitle: ko.observable(),
    IsPageSummeryVisible: ko.observable(),
    IsPageInEditMode: ko.observable(),

    PlaneSpotterAPIServiceURL: ko.observable(),

    SpotId: ko.observable(),
    Location: ko.observable(),
    SpottedDateTime: ko.observable(),
    AircraftRegistration: ko.observable(),
    AircraftMake: ko.observable(),
    AircraftModel: ko.observable(),

    SpotAircraftImage: ko.observable(),

    AircraftMakeFilter: ko.observable(),
    AircraftModelFilter: ko.observable(),
    AircraftRegFilter: ko.observable(),

    ValidationMessage: ko.observable(),
    ValidationSuccess: ko.observable(),

    SpottedPlanes: ko.observableArray([]),
    FilteredSpottedPlanes: ko.observableArray([]),

    Init: function () {

        var obj = JSON.parse(vwModel);

        PlaneSpotterVM.PlaneSpotterAPIServiceURL(obj.PlaneSpotterAPIServiceURL);
        PlaneSpotterVM.IsPageSummeryVisible(true);

        PlaneSpotterVM.GetAllSpottedAircraftDetails();

        PlaneSpotterVM.AircraftMakeFilter('');
        PlaneSpotterVM.AircraftModelFilter('');
        PlaneSpotterVM.AircraftRegFilter('');

        ko.applyBindings(PlaneSpotterVM, $("#PlaneSpotterMain")[0]);
    },

    GetAllSpottedAircraftDetails: function () {

        var url = PlaneSpotterVM.PlaneSpotterAPIServiceURL() + 'GetAllSpottedPlanes';
        $.ajax({
            url: url,
            type: 'GET',
            async: true,
            crossDomain: true,
            contentType: 'application/json',
            success: function (d) {
                PlaneSpotterVM.SpottedPlanes(d);
                PlaneSpotterVM.FilteredSpottedPlanes(d);
            },
            error: function (err, type, httpStatus) {
                //alert("Found and error!");
            }
        });
    },

    GetSelectedSpottedImage: function () {

        var url = PlaneSpotterVM.PlaneSpotterAPIServiceURL() + 'GetSelectedSpottedPlaneImage?id=' + PlaneSpotterVM.SpotId();
        $.ajax({
            url: url,
            type: 'GET',
            async: false,
            crossDomain: true,
            contentType: 'application/json',
            success: function (d) {
                PlaneSpotterVM.SpotAircraftImage(d.SpotAircraftImage);
            },
            error: function (err, type, httpStatus) {
                //alert("Found and error!");
            }
        });
    },

    FilterAircraftList: function () {

        if (PlaneSpotterVM.SpottedPlanes().length > 0) {

            var searchKey = PlaneSpotterVM.AircraftMakeFilter().toLowerCase();

            var RegSearchKey = $("#RegSearchKey").val().toLowerCase();;
            var MakeSearchKey = $("#MakeSearchKey").val().toLowerCase();;
            var ModelSearchKey = $("#ModelSearchKey").val().toLowerCase();;

            var searchResult = ko.computed(function () {
                if (RegSearchKey == "") {
                    return PlaneSpotterVM.SpottedPlanes();
                } else {
                    return ko.utils.arrayFilter(PlaneSpotterVM.SpottedPlanes(), function (i) {
                        return i.AircraftRegistration.toLowerCase().indexOf(RegSearchKey) >= 0;
                    });
                }
            });

            var tempSearchResult = searchResult();

            searchResult = ko.computed(function () {
                if (MakeSearchKey == "") {
                    return tempSearchResult;
                } else {
                    return ko.utils.arrayFilter(tempSearchResult, function (i) {
                        return i.AircraftMake.toLowerCase().indexOf(MakeSearchKey) >= 0;
                    });
                }
            });

            tempSearchResult = searchResult();

            searchResult = ko.computed(function () {
                if (ModelSearchKey == "") {
                    return tempSearchResult;
                } else {
                    return ko.utils.arrayFilter(tempSearchResult, function (i) {
                        return i.AircraftModel.toLowerCase().indexOf(ModelSearchKey) >= 0;
                    });
                }
            });

            PlaneSpotterVM.FilteredSpottedPlanes(searchResult());
        }
    },

    ValidateSpottingInfo: function () {
        if (PlaneSpotterVM.AircraftMake() == "") {
            PlaneSpotterVM.ValidationMessage("Please enter valid Aircraft Make!");
            PlaneSpotterVM.ValidationSuccess(false);
        }
        else if (PlaneSpotterVM.AircraftModel() == "") {
            PlaneSpotterVM.ValidationMessage("Please enter valid Aircraft Model!");
            PlaneSpotterVM.ValidationSuccess(false);
        }
        else if (PlaneSpotterVM.AircraftRegistration() == "") {
            PlaneSpotterVM.ValidationMessage("Please enter valid Aircraft Registration Number!");
            PlaneSpotterVM.ValidationSuccess(false);
        }
        else if (PlaneSpotterVM.Location() == "") {
            PlaneSpotterVM.ValidationMessage("Please enter valid Location!");
            PlaneSpotterVM.ValidationSuccess(false);
        }
        else if (PlaneSpotterVM.SpottedDateTime() == "") {
            PlaneSpotterVM.ValidationMessage( "Please select valid Date and Time!");
            PlaneSpotterVM.ValidationSuccess(false);
        }
        else {
            PlaneSpotterVM.ValidationSuccess(true);
        }

    },

    UploadSpottedImage: function (data, e) {
        PlaneSpotterVM.SpotAircraftImage("");
        if (data != undefined) {
            var file = e.target.files[0];
            var reader = new FileReader();

            reader.onloadend = function (onloadend_e) {
                var result = reader.result; 
                PlaneSpotterVM.SpotAircraftImage(result);
            };

            if (file) {
                reader.readAsDataURL(file);
            }
        }
    },

    ClickSaveSpottedAircraftDetails: function () {
        PlaneSpotterVM.ValidateSpottingInfo();

        if (PlaneSpotterVM.ValidationSuccess()) {
            if (!PlaneSpotterVM.IsPageInEditMode()) {
                if (confirm('Do you want to save this record?')) {
                    PlaneSpotterVM.SaveNewSpottingInfo();
                }
            }
            else {
                if (confirm('Do you want to update this record?')) {
                    PlaneSpotterVM.UpdateSpottedAircraftDetails();
                }
            }
        }
    },

    SaveNewSpottingInfo: function () {
        var url = PlaneSpotterVM.PlaneSpotterAPIServiceURL() + 'SaveSpottedPlanes';
        var data = ko.toJSON(PlaneSpotterVM);

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            async: true,
            contentType: 'application/json',
            success: function (d) {
                PlaneSpotterVM.GetAllSpottedAircraftDetails();
                PlaneSpotterVM.IsPageSummeryVisible(true);
                PlaneSpotterVM.ResetForm();
            },
            error: function (err, type, httpStatus) {
                //alert("Found and error!");
            }
        });
    },

    UpdateSpottedAircraftDetails: function () {
        var url = PlaneSpotterVM.PlaneSpotterAPIServiceURL() + 'UpdateSpottedPlanes';
        var data = ko.toJSON(PlaneSpotterVM);

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            async: true,
            contentType: 'application/json',
            success: function (d) {
                PlaneSpotterVM.GetAllSpottedAircraftDetails();
                PlaneSpotterVM.IsPageSummeryVisible(true);
                PlaneSpotterVM.ResetForm();
            },
            error: function (err, type, httpStatus) {
                //alert("Found and error!");
            }
        });
    },

    DeleteSpottedAircraftDetails: function (data, e) {

        if (confirm('Do you want to delete this record?')) {
            var url = PlaneSpotterVM.PlaneSpotterAPIServiceURL() + 'DeleteSelectedSpottedPlanes?id=' + data.SpotId;

            $.ajax({
                url: url,
                type: 'DELETE',
                data: data,
                async: true,
                contentType: 'application/json',
                success: function (d) {
                    PlaneSpotterVM.GetAllSpottedAircraftDetails();
                },
                error: function (err, type, httpStatus) {
                    //alert("Found and error!");
                }

            });

        }
    },

    EditSpottingInfo: function (data, e) {
        PlaneSpotterVM.IsPageSummeryVisible(false);
        PlaneSpotterVM.ValidationSuccess(true);
        PlaneSpotterVM.IsPageInEditMode(true);
        PlaneSpotterVM.SpotId(data.SpotId);
        PlaneSpotterVM.AircraftMake(data.AircraftMake);
        PlaneSpotterVM.AircraftModel(data.AircraftModel);
        PlaneSpotterVM.AircraftRegistration(data.AircraftRegistration);
        PlaneSpotterVM.Location(data.Location);
        PlaneSpotterVM.SpottedDateTime(data.SpottedDateTime);

        PlaneSpotterVM.GetSelectedSpottedImage();
    },

    AddNewSpottingInfo: function () {
        PlaneSpotterVM.IsPageSummeryVisible(false);
        PlaneSpotterVM.ValidationSuccess(true);
        PlaneSpotterVM.ResetForm();
    },

    ResetForm: function () {
        PlaneSpotterVM.SpotId('');
        PlaneSpotterVM.Location('');
        PlaneSpotterVM.SpottedDateTime('');
        PlaneSpotterVM.AircraftRegistration('');
        PlaneSpotterVM.AircraftMake('');
        PlaneSpotterVM.AircraftModel('');
        PlaneSpotterVM.SpotAircraftImage('');
        PlaneSpotterVM.ValidationMessage('');
        PlaneSpotterVM.IsPageInEditMode(false);
        PlaneSpotterVM.ValidationSuccess(true);
    },

    NavigateToHome: function () {
        PlaneSpotterVM.ResetForm();
        PlaneSpotterVM.IsPageSummeryVisible(true);
    }
}

$(document).ready(PlaneSpotterVM.Init);