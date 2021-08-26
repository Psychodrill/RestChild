$(function () {
    //$('#Data_SignDate, #Data_StartDate, #Data_EndDate').inputmask("d.m.y", {
    //	placeholder: "дд.мм.гггг",
    //	clearIncomplete: true
    //});
    //$('#Data_SignDate, #Data_StartDate, #Data_EndDate').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    function processTable() {
        if ($('#agreementsTable>tbody').children('tr').length > 0) {
            $('#agreementsTable').removeClass('hidden');
            $('#agreementsTable>tbody>tr').each(function (num, val) {
                $(val).find('input[name^="Agreements["],select[name^="Agreements["]').each(function (inputNum, input) {
                    var regexp = new RegExp('Agreements\\[.*?\\](.*)');
                    var matched = $(input).attr('name').match(regexp);
                    if (matched != null && matched.length > 1) {
                        var name = matched[1];
                        $(input).attr('name', 'Agreements[' + num + ']' + name);
                        $(input).parent().find('span.field-validation-valid').attr('data-valmsg-for', 'Agreements[' + num + ']' + name);
                    }
                });
            });
        }
        else {
            $('#agreementsTable').addClass('hidden');
        }
    }
    function removeContract(e) {
        var $row = $(e.target).closest('tr');
        $row.remove();
        processTable();
    }
    function addContract(e) {
        var $tbody = $('#agreementsTable>tbody');
        var $row = $($('#contractTemplate').html());
        $row.find('.remove-agreement-btn').click(removeContract);
        $row.find('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
        $row.find('.date>input').inputmask("d.m.y", {
            placeholder: "дд.мм.гггг",
            clearIncomplete: true
        });
        $tbody.append($row);
        processTable();
    }
    $('.remove-agreement-btn').click(removeContract);
    $('.addagreement').click(addContract);
    $('#Data_Summa, #Data_PlanCount').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
    $('select').select2();
    $('#Data_SupplierId').select2({
        initSelection: function (element, callback) {
            if (element.val() == '')
                callback({ id: '', text: '-- Не выбрано --' });
            else
                callback({ id: $('#_SupplierId').val(), text: $('#_SupplierName').val() });
        },
        minimumInputLength: 1,
        allowClear: true,
        language: "ru",
        ajax: {
            url: rootPath + '/api/Orgs',
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                return {
                    query: term,
                };
            },
            results: function (data, page) {
                var res = [];
                for (var i = 0; i < data.length; i++) {
                    if (data[i].id) {
                        res.push({ id: data[i].id, text: data[i].name });
                    }
                }
                return {
                    results: res
                };
            },
            cache: true
        }
    }).change(function () {
        $('.organization-bank-id').select2('data', { id: '', text: '-- Не выбрано --' });
    });
    $('.organization-bank-id').select2({
        initSelection: function (element, callback) {
            callback({ id: element.val(), text: element.attr('accountname') });
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/orgBanks/',
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                var supplier = $('#Data_SupplierId').select2('val');
                if (!supplier || supplier === '0') {
                    supplier = '0';
                }
                return {
                    orgId: supplier
                };
            },
            results: function (data, page) {
                var results = [];
                results.push({ id: 0, text: '-- Не выбрано --' });
                for (var i in data) {
                    results.push({ id: data[i].id, text: data[i].name });
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
                results.push({ id: 0, text: '-- Не выбрано --' });
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
//# sourceMappingURL=ContractEdit.js.map