$(function () {
    $('select').select2();
    $('#TimeOfRestId').select2({
        initSelection: function (element, callback) {
            if ($('#_TimeOfRestId').val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $('#_TimeOfRestId').val(), text: $('#_TimeOfRestName').val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebRestTime/GetByTypeAndYear',
            dataType: 'json',
            data: function (term, page) {
                return {
                    name: term,
                    typeOfRestId: $('#TypeOfRestId').select2('val'),
                    yearOfRestId: $('#YearOfRestId').select2('val')
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                results = results.concat($.map(data, function (item) {
                    return {
                        text: item.name,
                        id: item.id
                    };
                }));
                return {
                    results: results
                };
            },
            cache: true
        }
    }).select2('val', []);
    $('#HotelId').select2({
        initSelection: function (element, callback) {
            if ($('#_HotelsId').val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $('#_HotelsId').val(), text: $('#_HotelsName').val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebHotels',
            dataType: 'json',
            data: function (term, page) {
                return {
                    name: term,
                    typeOfRest: $('#TypeOfRestId').val(),
                    onlyApproved: 'True'
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                results = results.concat($.map(data, function (item) {
                    return {
                        text: item.name,
                        id: item.id
                    };
                }));
                return {
                    results: results
                };
            },
            cache: true
        }
    }).select2('val', []);
    var excludeDisable = function () {
        if ($('#TypeOfServiceId').val() == "0") {
            $("input.include, input.exclude").attr("disabled", "disabled");
        }
        else {
            $("input.include, input.exclude").removeAttr("disabled");
        }
    };
    $('#TypeOfServiceId').change(excludeDisable);
    excludeDisable();
});
//# sourceMappingURL=ToursList.js.map