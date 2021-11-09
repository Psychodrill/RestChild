$(function () {
    $('select').select2({ dropdownAutoWidth: true });
    $('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('.date>input, input.date').inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    }).focusout(function (e) {
        var val = moment($(e.target).val(), 'DD.MM.YYYY');
        if (!val.isValid()) {
            $(e.target).val('');
        }
    });
    var templateFn = doT.template($('#rowPerson').html());
    var key = 1;
    $('#addPerson').click(function () {
        var $row = $(templateFn(-key));
        editPerson(collectDataFromRow($row), function (item) {
            bindDataToRow($row, item);
            $('#persons').removeClass('hidden');
            $('#persons').children('tbody').append($row);
            key++;
        });
    });
    $('#persons').on('click', '.edit-person-btn', function (e) {
        var $row = $(e.target).closest('tr');
        editPerson(collectDataFromRow($row), function (item) {
            bindDataToRow($row, item);
        });
    });
    var linkMiddleNamePresent = function (selCb, selInp) {
        $(selCb).change(function () {
            if ($(selCb).prop("disabled"))
                return;
            if ($(selCb).prop('checked')) {
                $(selInp).val('');
                $(selInp).prop('disabled', true);
            }
            else {
                $(selInp).prop('disabled', false);
            }
        });
    };
    linkMiddleNamePresent('#Child-HaveMiddleName', '#Child-MiddleName');
    linkMiddleNamePresent('#Parent-HaveMiddleName', '#Parent-MiddleName');
    linkMiddleNamePresent('#Unionist-HaveMiddleName', '#Unionist-MiddleName');
    $('#persons').on('click', '.remove-person-btn', function (e) {
        var $tr = $(e.target).closest('tr');
        if ($('#Data_Id').val().toString() === '0') {
            $tr.remove();
            if ($('#persons').find('tbody tr').length === 0) {
                $('#persons').addClass('hidden');
            }
        }
        else {
            var data = collectDataFromRow($tr);
            $.ajax({
                type: "post",
                url: rootPath + "/TradeUnionCashback/DeleteChild?id=" + data.Id,
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                if (!data) {
                    $tr.remove();
                    if ($('#persons').find('tbody tr').length === 0) {
                        $('#persons').addClass('hidden');
                    }
                }
            });
        }
    });
    $(".cashbackRequested input").on("click", function (e) {
        var $tr = $(e.target).closest('tr');
        var data = collectDataFromRow($tr);
        var cashbackRequested = $(e.target).prop("checked") ? true : false;
        var camperId = data.Id;
        $.ajax({
            type: "post",
            url: rootPath + "/TradeUnionCashback/SaveCashbackRequested?id=" + camperId + "&cashbackRequested=" + cashbackRequested,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
        });
    });
});
// @ts-ignore
function confirmStateButtonTradeUnion(formSelector, actionSelector, actionCode, buttonName, description, commentSelector) {
    var error = '';
    if (!$('.GroupedTimeOfRestId').select2('val') || $('.GroupedTimeOfRestId').select2('val') <= 0) {
        error = error + '<li>Поле смена не заполнено</li>';
    }
    if (error) {
        ShowAlert('<ul>' + error + '</ul>', 'alert-danger', "", true);
        return;
    }
    return confirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector);
}
function validateTabsForms(item) {
    var requiredOk = true;
    var errors = new Array();
    clearTabsFormsValidation();
    for (var e in item) {
        if (item.hasOwnProperty(e)) {
            var $element = $("#" + e);
            if ($element.hasClass("required") && !$element.prop("disabled")) {
                if (item[e] == null || (item[e] + "").trim() == "" || !(/[A-Za-zА-Яа-я0-9]/.test((item[e] + "")))) {
                    requiredOk = false;
                    $element.parent().addClass("has-error");
                }
                else {
                    $element.parent().removeClass("has-error");
                }
            }
        }
    }
    /*    var mos = ($("#cbAddress").length && $("#cbAddress").prop("checked")) || $(".street-autocompleteAR").length;
        if ((mos && !addressControlValid()) || (!mos && !validateSingleField(item, "AddressChild"))) {
            errors.push('Не заполнен адрес регистрации');
        }*/
    if (item["Child-DateOfBirth"] && item["Child-DocumentDateOfIssue"] && moment(item["Child-DateOfBirth"], "DD.MM.YYYY").isAfter(moment(item["Child-DocumentDateOfIssue"], "DD.MM.YYYY"))) {
        errors.push("Дата выдачи документа не должна быть раньше даты рождения");
        $("#Child-DateOfBirth").parent().addClass("has-error");
        $("#Child-DocumentDateOfIssue").parent().addClass("has-error");
    }
    if (!requiredOk) {
        errors.push('Заполнены не все обязательные поля');
    }
    var ageErr = validateDocByAge(item);
    if (ageErr) {
        errors.push(ageErr);
    }
    return errors;
}
function fillFormByData(item) {
    for (var e in item) {
        if (item.hasOwnProperty(e)) {
            var $element = $('#' + e);
            if ($element.length) {
                if ($element.is('input[type=radio]')) {
                    $element.prop('checked', item[e]);
                    $('#' + e + '-False').prop('checked', !item[e]);
                    $element.trigger('change');
                }
                else if ($element.is('input[type=checkbox]')) {
                    $element.prop('checked', item[e]);
                    $element.trigger('change');
                }
                else if ($element.is('input[type=hidden]')) {
                    if (e == 'TradeUnionOrganizationId')
                        $element.select2('data', { id: item[e], text: item['TradeUnionOrganizationName'] });
                    else
                        $element.select2('data', { id: item[e], text: item['SelectedSchoolName'] });
                }
                else if ($element.is('select')) {
                    if (item[e]) {
                        $element.select2('val', item[e]);
                        $element.trigger('change');
                    }
                    else {
                        $element.select2('val', 22);
                        $element.trigger('change');
                    }
                }
                else {
                    $element.val(item[e]);
                    $element.trigger('change');
                }
            }
        }
    }
    // Для контролов, которые не завязаны на свойства модели, логика заполнения в зависимости от значения других свойств модели.
    // Например чекбокс "Профсоюза нет в списке" зависит от заполненности либо поля Профсоюз либо поля Иной профсоюз. В приоритете "Профсоюз".
    $('#IsTradeUnionOrgNotPresent').prop('checked', !(item['TradeUnionOrganizationId'] > 0 || !$('#TradeUnionOrganizationOther').val()));
    $('#IsTradeUnionOrgNotPresent').change();
    // Адрес
    var addrValue = {
        id: item["Child-AddressId"],
        addressId: item["Child-Address-Id"],
        btiAddressId: item["Child-Address-BtiAddressId"],
        btiAddressBtiStreetId: item["Child-Address-BtiAddress-BtiStreet-Id"],
        btiAddressBtiStreetName: item["Child-Address-BtiAddress-BtiStreet-Name"],
        street: item["Child-Address-Street"],
        house: item["Child-Address-House"],
        corpus: item["Child-Address-Corpus"],
        stroenie: item["Child-Address-Stroenie"],
        vladenie: item["Child-Address-Vladenie"],
        appartment: item["Child-Address-Appartment"],
        btiRegionId: item["Child-Address-BtiRegionId"],
        btiDistrictId: item["Child-Address-BtiDistrictId"],
        fiasId: item["Child-Address-FiasId"],
        region: item["Child-Address-Region"],
        district: item["Child-Address-District"]
    };
    addressControlSetValue(addrValue, false);
    // Адрес
    $("#cbAddress").prop("checked", item["Child-AddressId"] || !item["AddressChild"]);
    $("#cbAddress").change();
    if ($("#cbAddress").prop("checked")) {
        $("#AddressChild").val("");
    }
    clearTabsFormsValidation();
}
function clearTabsFormsValidation() {
    $('.dialog-validation-alert').addClass('hidden').html('Заполнены не все обязательные поля');
    $(".has-error").removeClass("has-error");
}
function validateSingleField(item, e) {
    var $element = $("#" + e);
    if (item[e] == null || (item[e] + "").trim() == "" || !(/[A-Za-zА-Яа-я0-9]/.test((item[e] + "")))) {
        $element.parent().addClass("has-error");
        return false;
    }
    else {
        $element.parent().removeClass("has-error");
        return true;
    }
}
function validateDocByAge(item) {
    var birthText = $('#Child-DateOfBirth').val();
    if (birthText) {
        var birth = moment(birthText, "DD.MM.YYYY");
        var ago14 = moment(new Date()).subtract(14, 'years');
        var ago14_1 = moment(new Date()).subtract(14, 'years').subtract(1, 'month');
        var docId = $('#Child-DocumentTypeId').select2('val');
        // 50001	Паспорт гражданина РФ
        // 50005	Паспорт иностранного образца
        // 22			Свидетельство о рождении
        // 23			Свидетельство о рождении иностранного образца
        if (birth.isAfter(ago14) && docId === '50001') {
            $('#Child-DocumentTypeId, #Child-DateOfBirth').parent().addClass('has-error');
            return 'Указанный возраст ребенка не предполагает наличие паспорта РФ';
        }
        else if (birth.isBefore(ago14_1) && docId === '22') {
            $('#Child-DocumentTypeId, #Child-DateOfBirth').parent().addClass('has-error');
            return 'Указанный возраст ребенка предполагает наличие паспорта РФ';
        }
    }
    return null;
}
function editPerson(item, callback) {
    $('#personSaveBtn').unbind();
    $('.retweet-by-snils').unbind();
    fillFormByData(item);
    $('#personSaveBtn').click(function () {
        for (var e in item) {
            if (item.hasOwnProperty(e)) {
                var $element = $('#' + e);
                if ($element.is('input[type=radio]')) {
                    item[e] = $element.prop('checked');
                }
                else if ($element.is('input[type=checkbox]')) {
                    item[e] = $element.prop('checked');
                }
                else if ($element.is('input[type=hidden]')) {
                    item[e] = $element.select2('val');
                }
                else if ($element.is('select')) {
                    item[e] = $element.select2('val');
                }
                else if ($element.length > 0) {
                    item[e] = $element.val();
                }
            }
        }
        item['Child-DocumentTypeName'] = $('#Child-DocumentTypeId').select2('data').text;
        item['SelectedSchoolName'] = $('#SelectedSchoolId').select2('data').text;
        item['TradeUnionOrganizationName'] = $('#TradeUnionOrganizationId').select2('data').text;
        // Адрес
        var addr;
        if ($("#cbAddress").prop("checked") || $(".street-autocompleteAR").length) {
            addr = addressControlGetValue() || {};
            addr.id = addr.id || 0;
            item["AddressChild"] = "";
        }
        else {
            addr = {};
        }
        item["Child-AddressId"] = addr.id;
        item["Child-Address-Id"] = addr.id;
        item["Child-Address-BtiAddressId"] = addr.btiAddressId;
        item["Child-Address-BtiAddress-BtiStreet-Id"] = addr.btiAddressBtiStreetId;
        item["Child-Address-BtiAddress-BtiStreet-Name"] = addr.btiAddressBtiStreetName;
        item["Child-Address-Street"] = addr.street;
        item["Child-Address-Name"] = addr.street;
        item["Child-Address-House"] = addr.house;
        item["Child-Address-Corpus"] = addr.corpus;
        item["Child-Address-Stroenie"] = addr.stroenie;
        item["Child-Address-Vladenie"] = addr.vladenie;
        item["Child-Address-Appartment"] = addr.appartment;
        item["Child-Address-BtiRegionId"] = addr.btiRegionId;
        item["Child-Address-BtiDistrictId"] = addr.btiDistrictId;
        item["Child-Address-FiasId"] = addr.fiasId;
        item["Child-Address-Region"] = addr.region;
        item["Child-Address-District"] = addr.district;
        var errors = validateTabsForms(item);
        var isValid = !(errors && errors.length > 0);
        if (!isValid) {
            $('.dialog-validation-alert').removeClass('hidden');
            var txt = '';
            $.each(errors, function (i, e) { txt = txt + e + '<br/>'; });
            $('.dialog-validation-alert').html(txt);
        }
        else {
            $('.dialog-validation-alert').addClass('hidden');
        }
        // проверка сумм (предупреждение)
        var pf = function (str) { if (!str)
            return 0; return parseFloat(str.toString().replace(',', '.')); };
        var summa = pf(item['Summa']);
        item['Summa'] = pf(item['Summa']);
        item['SummaParent'] = pf(item['SummaParent']);
        item['SummaTradeUnion'] = pf(item['SummaTradeUnion']);
        item['SummaBudget'] = pf(item['SummaBudget']);
        item['SummaOrganization'] = pf(item['SummaOrganization']);
        if (isValid) {
            callback(item);
            $('#dialogPerson').modal('hide');
        }
    });
    $('#dialogPerson').modal({ backdrop: false });
}
function bindDataToRow($row, item) {
    var name = item['Child-LastName'] + ' ' + item['Child-FirstName'] + ' ' + item['Child-MiddleName'];
    var sex = item['Child-Male'] ? "муж" : "жен";
    var dateBirth = item['Child-DateOfBirth'] ? moment(item['Child-DateOfBirth'], 'DD.MM.YYYY').format('DD.MM.YYYY') : '-';
    var document = item['Child-DocumentTypeName'] + ' ' + item['Child-DocumentSeria'] + ' ' + item['Child-DocumentNumber'];
    function mapDataToRow(object) {
        $row.find('.fio').html(name);
        $row.find('.sex').html(sex);
        $row.find('.birthDate').html(dateBirth);
        $row.find('.document').html(document);
        $row.find('.hdn-json').val(JSON.stringify(object));
    }
    if ($('#Data_Id').val().toString() === '0') {
        mapDataToRow(item);
    }
    else {
        item.TradeUnionId = $('#Data_Id').val().toString();
        $.ajax({
            type: "post",
            url: rootPath + "/TradeUnionCashback/SaveChild",
            data: 'child=' + encodeURI(JSON.stringify(item))
        }).done(function (data) {
            mapDataToRow(JSON.parse(data));
        });
    }
}
function collectDataFromRow($row) {
    var data = JSON.parse($row.find('.hdn-json').val());
    return data;
}
//# sourceMappingURL=TradeUnionCashbackEdit.js.map