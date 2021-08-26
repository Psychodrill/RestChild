$(function () {
    $('.placeOfRestId')
        .each(function (i, e) {
        $(e).select2({
            initSelection: function (element, callback) {
                if ($(e).val() === '') {
                    callback(null);
                }
                else {
                    callback({ id: $(e).val(), text: $(e).attr('placeName') });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + 'Api/WebRestPlace/GetPlaceOfRests',
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                params: {
                    contentType: 'application/json'
                },
                data: function (term, page) {
                    var selected = [];
                    $('input.placeOfRestId')
                        .each(function (i, s) {
                        var v = $(s).select2('val');
                        if (v && !$(e).is($(s))) {
                            selected.push(parseInt(v));
                        }
                    });
                    return JSON.stringify(selected);
                },
                results: function (data, page) {
                    var results = [];
                    var selected = [];
                    $('input.placeOfRestId')
                        .each(function (i, s) {
                        var v = $(s).select2('val');
                        if (v && !$(e).is($(s))) {
                            selected.push(parseInt(v));
                        }
                    });
                    for (var j = 0; j < data.length; j++) {
                        if ($.inArray(data[j].id, selected) < 0) {
                            results.push({
                                text: data[j].name,
                                id: data[j].id
                            });
                        }
                    }
                    return {
                        results: results
                    };
                },
                cache: true
            }
        });
    });
});
//# sourceMappingURL=RequestPlaceOfRest.js.map