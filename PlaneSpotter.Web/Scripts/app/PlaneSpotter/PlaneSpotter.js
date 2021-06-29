
var APIURL = 'https://localhost:44363/v1/spotter/';

var PlaneSpotterVM = {

    PageTitle: ko.observable(),
    IsPageSummeryVisible: ko.observable(),

    SpotId: ko.observable(),
    Location: ko.observable(),
    SpottedDateTime: ko.observable(),
    AircraftRegistration: ko.observable(),
    AircraftMake: ko.observable(),
    AircraftModel: ko.observable(),
    SpottedImageFile: ko.observable(),

    AircraftMakeFilter: ko.observable(),
    AircraftModelFilter: ko.observable(),
    AircraftRegFilter: ko.observable(),

    SpottedPlanes: ko.observableArray([]),
    FilteredSpottedPlanes: ko.observableArray([]),



    Init: function () {

        PlaneSpotterVM.PageTitle("Plane Spotter");
        PlaneSpotterVM.IsPageSummeryVisible(true);

        PlaneSpotterVM.GetAllSpottedAircraftDetails();

        PlaneSpotterVM.AircraftMakeFilter('');
        PlaneSpotterVM.AircraftModelFilter('');
        PlaneSpotterVM.AircraftRegFilter('');


        ko.applyBindings(PlaneSpotterVM, $("#PlaneSpotterMain")[0]);
    },

    GetAllSpottedAircraftDetails: function () {

        var url = APIURL + 'GetAllSpottedPlanes';
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
                alert("Found and error!")
            }

        });
    },


    FilterAircraftList: function () {

        if (PlaneSpotterVM.SpottedPlanes().length > 0) {

            //var searchKey = PlaneSpotterVM.AircraftMakeFilter().toLowerCase();

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

    AddNewSpottedAircraftDetails: function () {

        $.ajax({
            url: APIURL + 'getallspottedplanes',
            type: 'GET',
            async: true,
            contentType: 'application/json',
            success: function (d) {

            },
            error: function (err, type, httpStatus) {

            }

        });
    },

    UpdateSpottedAircraftDetails: function () {

        $.ajax({
            url: APIURL + 'getallspottedplanes',
            type: 'GET',
            async: true,
            contentType: 'application/json',
            success: function (d) {

            },
            error: function (err, type, httpStatus) {

            }

        });
    },

    DeleteSpottedAircraftDetails: function (data, e) {

        if (confirm('Do you want to delete this record?')) {
            var url = APIURL + 'DeleteSelectedSpottedPlanes?id=' + data.SpotId;

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

                }

            });

        }
    },

    EditSpottingInfo: function (data, e) {
        PlaneSpotterVM.IsPageSummeryVisible(false);

        PlaneSpotterVM.SpotId(data.SpotId);
        PlaneSpotterVM.AircraftMake(data.AircraftMake);
        PlaneSpotterVM.AircraftModel(data.AircraftModel);
        PlaneSpotterVM.AircraftRegistration(data.AircraftRegistration);
        PlaneSpotterVM.Location(data.Location);
        //PlaneSpotterVM.SpottedDateTime(new Date(data.SpottedDateTime));
        PlaneSpotterVM.SpottedDateTime(data.SpottedDateTime);
        PlaneSpotterVM.SpottedImageFile(data.SpottedImageFile);
    },

    SaveSpottingInfo: function () {

        if (confirm('Do you want to save this record?')) {

            var url = APIURL + 'SaveSpottedPlanes';

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

                }

            });

        }


    },

    AddNewSpottingInfo: function () {
        PlaneSpotterVM.IsPageSummeryVisible(false);
        PlaneSpotterVM.ResetForm();
    },

    ResetForm: function () {
        SpotId: ko.observable('');
        Location: ko.observable('');
        SpottedDateTime: ko.observable('');
        AircraftRegistration: ko.observable('');
        AircraftMake: ko.observable('');
        AircraftModel: ko.observable('');
        SpottedImageFile: ko.observable('');
    }
}

$(document).ready(PlaneSpotterVM.Init);