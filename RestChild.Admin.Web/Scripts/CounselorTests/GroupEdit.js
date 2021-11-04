$(function () {
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('select').select2();
    var i = 0;
    function fixTableNums() {
        $('#tbl-add-conselor>tbody>tr').each(function (partyNum, party) {
            $(party).find('input[name^="Results["],textarea[name^="Results["]').each(function (inputNum, input) {
                var regexp = new RegExp('Results\\[.*?\\](.*)');
                var matched = $(input).attr('name').match(regexp);
                if (matched != null && matched.length > 1) {
                    var name = matched[1];
                    $(input).attr('name', 'Results[' + partyNum + ']' + name);
                }
            });
        });
        $('#tbl-add-test>tbody>tr').each(function (partyNum, party) {
            $(party).find('input[name^="Tests["],textarea[name^="Tests["]').each(function (inputNum, input) {
                var regexp = new RegExp('Tests\\[.*?\\](.*)');
                var matched = $(input).attr('name').match(regexp);
                if (matched != null && matched.length > 1) {
                    var name = matched[1];
                    $(input).attr('name', 'Tests[' + partyNum + ']' + name);
                }
            });
        });
    }
    function prepareRow(row) {
        row.find('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
        inputMaskDateAnytime($(row.find('.input-mask-date-anytime')));
        $(row.find('.price, .decimal')).inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
        $(row.find('.fio-control')).select2({
            initSelection: function (element, callback) {
                var e = $(element);
                if (e.val() === '') {
                    callback({ id: '', text: '-- Не выбрано --' });
                }
                else {
                    callback({ id: -1, text: e.val() });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + 'Api/CounselorTest/GetStudents',
                dataType: 'json',
                data: function (term, page) {
                    return {
                        search: term,
                        firstStage: $('#firstStage').prop('checked')
                    };
                },
                results: function (data, page) {
                    if (i > 50) {
                        i = 0;
                    }
                    var results = [{ id: '', text: '-- Не выбрано --' }];
                    results = results.concat($.map(data, function (item) {
                        i++;
                        return {
                            text: item.name,
                            administratorTourId: item.data.administratorTourId,
                            counselorsId: item.data.counselorsId,
                            id: i
                        };
                    }));
                    return {
                        results: results
                    };
                },
                cache: true
            }
        }).change(function (e) {
            var target = $(e.target);
            var data = target.select2('data');
            var admTour = $(target.parent().find(".administratorTourId"));
            admTour.val(data.administratorTourId);
            var couns = $(target.parent().find(".counselorsId"));
            couns.val(data.counselorsId);
        });
        $(row.find('.counselor-input')).select2({
            initSelection: function (element, callback) {
                var e = $(element);
                if (e.val() === '') {
                    callback({ id: '', text: '-- Не выбрано --' });
                }
                else {
                    callback({ id: e.val(), text: e.attr('testname') });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + 'Api/CounselorTest/GetTests',
                dataType: 'json',
                data: function (term, page) {
                    return {
                        search: term
                    };
                },
                results: function (data, page) {
                    if (i > 50) {
                        i = 0;
                    }
                    var results = [{ id: '', text: '-- Не выбрано --' }];
                    results = results.concat($.map(data, function (item) {
                        i++;
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
    }
    $('#add-conselor, #add-conselor-bottom').click(function (e) {
        var row = $($('#row-add-conselor').html());
        prepareRow(row);
        $('#tbl-add-conselor>tbody').append(row);
        fixTableNums();
    });
    $('#add-test, #add-test-bottom').click(function (e) {
        var row = $($('#row-add-test').html());
        prepareRow(row);
        $('#tbl-add-test>tbody').append(row);
        fixTableNums();
    });
    $('#tbl-add-conselor').on('click', '.remove-counselor', function (e) {
        $(e.target).parent().parent().remove();
        fixTableNums();
    });
    $('#tbl-add-test').on('click', '.remove-test', function (e) {
        $(e.target).parent().parent().remove();
        fixTableNums();
    });
    prepareRow($('#tbl-add-conselor'));
    prepareRow($('#tbl-add-test'));
    $('#tbl-add-test').on('change', '.count-attempt', function (e) {
        var cbx = $($(e.target).closest('input[type=\'checkbox\']'));
        var input = cbx.closest('td').find('input[type=text]');
        if (cbx.prop('checked')) {
            input.removeClass('hidden');
        }
        else {
            input.addClass('hidden');
            input.val('');
        }
    });
});
function showStudentTest(trainingCounselorsResultId) {
    $('#counselorTestingData').html('<p>Загрузка данных...</p>');
    $.ajax({
        dataType: 'html',
        url: rootPath + '/CounselorTest/ListTest?trainingCounselorsResultId=' + trainingCounselorsResultId,
        data: null,
        success: function (result) {
            $('#counselorTestingData').html(result);
        },
        error: function () {
            $('#counselorTestingData').empty();
            $('#counselorTestingData').append($('<option value="" data-district="" data-region="">Ошибка загрузки данных</option>'));
            $('#counselorTestingData').parent().find('.loading').addClass('invisible');
        }
    });
    $('#counselorTestingDialog').modal();
}
function showGroupTest(groupId) {
    $('#counselorTestingData').html('<p>Загрузка данных...</p>');
    $.ajax({
        dataType: 'html',
        url: rootPath + '/CounselorTest/ListPupil?groupId=' + groupId,
        data: null,
        success: function (result) {
            console.log(result);
            $('#counselorTestingData').html(result);
        },
        error: function () {
            $('#counselorTestingData').empty();
            $('#counselorTestingData').append($('<option value="" data-district="" data-region="">Ошибка загрузки данных</option>'));
            $('#counselorTestingData').parent().find('.loading').addClass('invisible');
        }
    });
    $('#counselorTestingDialog').modal();
}
//# sourceMappingURL=GroupEdit.js.map