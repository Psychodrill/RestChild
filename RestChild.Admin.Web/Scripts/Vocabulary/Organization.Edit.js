$(function () {
    function processTable() {
        $("form")
            .removeData("validator")
            .removeData("unobtrusiveValidation");
        if ($('#bankTable>tbody').children('tr').length > 0) {
            $('#bankTable').removeClass('hidden');
            $('#bankTable>tbody>tr').each(function (num, val) {
                $(val).find('input[name^="Banks["],select[name^="Banks["]').each(function (inputNum, input) {
                    var regexp = new RegExp('Banks\\[.*?\\](.*)');
                    var matched = $(input).attr('name').match(regexp);
                    if (matched != null && matched.length > 1) {
                        var name = matched[1];
                        $(input).attr('name', 'Banks[' + num + ']' + name);
                        $(input).parent().find('span.field-validation-valid').attr('data-valmsg-for', 'Banks[' + num + ']' + name);
                    }
                });
            });
        }
        else {
            $('#bankTable').addClass('hidden');
        }
        $.validator
            .unobtrusive
            .parse("form");
    }
    function processOkvedTable() {
        $("form")
            .removeData("validator")
            .removeData("unobtrusiveValidation");
        if ($('#okvedTable>tbody').children('tr').length > 0) {
            $('#okvedDiv').removeClass('hidden');
            $('#okvedTable>tbody>tr').each(function (num, val) {
                $(val).find('input[name^="Okveds["],select[name^="Okveds["]').each(function (inputNum, input) {
                    var regexp = new RegExp('Okveds\\[.*?\\](.*)');
                    var matched = $(input).attr('name').match(regexp);
                    if (matched != null && matched.length > 1) {
                        var name = matched[1];
                        $(input).attr('name', 'Okveds[' + num + ']' + name);
                        $(input).parent().find('span.field-validation-valid').attr('data-valmsg-for', 'Okveds[' + num + ']' + name);
                    }
                });
            });
        }
        else {
            $('#okvedDiv').addClass('hidden');
        }
        $.validator
            .unobtrusive
            .parse("form");
    }
    function removeOkved(e) {
        var $row = $(e.target).closest('tr');
        $row.remove();
        processOkvedTable();
    }
    function removeBank(e) {
        var $row = $(e.target).closest('tr');
        $row.remove();
        processTable();
    }
    function addOkved(e) {
        var $tbody = $('#okvedTable>tbody');
        var $row = $($('#okvedTemplate').html());
        var data = $('#okvedId').select2('data');
        if (data.id <= 0) {
            ShowAlert('?????????????? ?????????? ?????? ????????????????????', "alert-danger", "glyphicon-remove", true);
            return;
        }
        $row.find('input').val(data.id);
        $row.find('span').html(data.text);
        $row.find('.remove-okved-btn').click(removeOkved);
        $tbody.append($row);
        processOkvedTable();
        $('#okvedId').select2('data', { id: -1, text: '-- ???? ?????????????? --' });
    }
    function addBank(e) {
        var $tbody = $('#bankTable>tbody');
        var $row = $($('#bankTemplate').html());
        $row.find('.remove-bank-btn').click(removeBank);
        $tbody.append($row);
        processTable();
    }
    $('.remove-bank-btn').click(removeBank);
    $('.addbank').click(addBank);
    $('.remove-okved-btn').click(removeOkved);
    $('#okvedAdd').click(addOkved);
    $('#okvedId').select2({
        initSelection: function (element, callback) {
            callback({ id: -1, text: '-- ???? ?????????????? --' });
        },
        minimumInputLength: 1,
        ajax: {
            url: rootPath + 'api/okveds',
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                return {
                    query: term,
                };
            },
            results: function (data, page) {
                var results = [];
                results.push({ id: 0, text: '-- ???? ?????????????? --' });
                for (var i in data) {
                    results.push({ id: data[i].id, text: data[i].code + ' - ' + data[i].name });
                }
                return {
                    results: results
                };
            },
            cache: true
        }
    });
    $('.curator-main-id').select2({
        initSelection: function (element, callback) {
            callback({ id: element.val(), text: element.attr('accountname') });
        },
        minimumInputLength: 1,
        ajax: {
            url: rootPath + '/api/WebAccount/SearchAccount/',
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                return {
                    q: term,
                };
            },
            results: function (data, page) {
                var results = [];
                results.push({ id: 0, text: '-- ???? ?????????????? --' });
                for (var i in data) {
                    results.push({ id: data[i].Id, text: data[i].FullName });
                }
                return {
                    results: results
                };
            },
            cache: true
        }
    });
});
//# sourceMappingURL=Organization.Edit.js.map