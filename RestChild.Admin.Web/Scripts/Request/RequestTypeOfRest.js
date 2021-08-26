$(function () {
    $('#YearOfRestId')
        .select2({
        initSelection: function (element, callback) {
            if ($('#YearOfRestId').val() === '') {
                callback(null);
            }
            else {
                callback({ id: $('#YearOfRestId').val(), text: $('#YearOfRestId').attr('YearName') });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebVocabulary/GetYearsForTypeOfRest',
            dataType: 'json',
            data: function (term, page) {
                return {
                    name: term,
                    typeOfRestId: $('.TypeOfRestId').val()
                };
            },
            results: function (data, page) {
                var results = [];
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
    });
    $('#representInterest').select2({
        initSelection: function (element, callback) {
            var $e = $('#representInterest');
            if ($e.val() === '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $e.val(), text: $e.attr('titleText') });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + 'Api/WebVocabulary/GetRepresentInterest',
            dataType: 'json',
            data: function () {
                var t = $('.TypeOfRestId').val();
                return {
                    id: t
                };
            },
            results: function (data) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                for (var j = 0; j < data.length; j++) {
                    results.push({
                        text: data[j].name,
                        id: data[j].id
                    });
                }
                return {
                    results: results
                };
            },
            cache: false
        }
    });
    $('.time-of-rest')
        .each(function (i, e) {
        $(e).select2({
            initSelection: function (element, callback) {
                if ($(e).val() === '') {
                    callback(null);
                }
                else {
                    callback({ id: $(e).val(), text: $(e).attr('TimeName') });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + 'Api/WebVocabulary/GetTimesOfRestWithoutFilter',
                dataType: 'json',
                data: function (term, page) {
                    var y = $('#YearOfRestId').val();
                    if (!y) {
                        y = '-1';
                    }
                    var t = $('.TypeOfRestId').val();
                    if (!t) {
                        t = '-1000';
                    }
                    return {
                        yearOfRestId: y,
                        typeOfRestId: t
                    };
                },
                results: function (data, page) {
                    var results = [];
                    var selected = [];
                    $('input.time-of-rest')
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
    $('#SaveButton')
        .click(function (e) {
        if ($('#manualTypeOfRest').val() === '1') {
            var allSelectedSelected = true;
            $('.select-must-selected').each(function (i, e) {
                var $e = $(e);
                var val = $e.select2('val');
                if ((!val || val === '0') && $e.closest('.hidden').length === 0) {
                    allSelectedSelected = false;
                }
            });
            if (!allSelectedSelected) {
                e.preventDefault();
                ShowAlert("Необходимо заполнить все обязательные поля", "alert-danger", "glyphicon-remove", true);
                return;
            }
            if (!$('.TypeOfRestId').val() || !$('#YearOfRestId').select2('val')) {
                e.preventDefault();
                ShowAlert("Для сохранения заявления необходимо указать цель обращения и год кампании", "alert-danger", "glyphicon-remove", true);
            }
        }
    });
});
//# sourceMappingURL=RequestTypeOfRest.js.map