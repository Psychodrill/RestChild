declare var clearTradeUnionChildForm: any;

function changeIsChecked(e) {
    var $e = $(e);
    var $json = $e.closest('tr').find('.hdn-json');
    var o = JSON.parse($json.val());
    o.IsChecked = $e.prop('checked');
    $json.val(JSON.stringify(o));
}

$(() => {
    $('select').select2({ dropdownAutoWidth: true });
    $('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('.date>input, input.date').inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    }).focusout((e) => {
        var val = moment($(e.target).val(), 'DD.MM.YYYY');
        if (!val.isValid()) {
            $(e.target).val('');
        }
    });

    $('#SelectedSchoolId').select2({
        initSelection: (element, callback) => {
            if (element.val() == '' || element.val() == 0)
                callback({ id: '', text: '-- Не выбрано --' });
            else
                callback({ id: element.val(), text: element.attr('titletext') });
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebEkisSchools',
            dataType: 'json',
            quietMillis: 250,
            data: (term, page) => {
                return {
                    query: term,
                };
            },
            results: (data, page) => {
                for (var i in data) {
                    data[i] = { id: data[i].id, text: data[i].name };
                }
                return {
                    results: data
                };
            },
            cache: true
        }
    });

    // лагеря
    $('.CampId').select2({
        initSelection: (element, callback) => {
            if (element.val() == '' || element.val() == 0)
                callback({ id: '', text: '-- Не выбрано --' });
            else
                callback({ id: element.val(), text: element.attr('titletext') });
        },
        allowClear: true,
        language: "ru",
        ajax: {
            url: rootPath + '/api/Orgs',
            dataType: 'json',
            quietMillis: 250,
            data: (term, page) => {
                return {
                    query: term,
                    typeId: 5
                };
            },
            results: (data, page) => {
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
    });

    $('#Child-DocumentTypeId').change((e) => {
        var birthCertId = '22';
        var passport = '50001';
        var attendantPassport = '60001';
        var selectorDialog = '#dialogPerson';
        var val = $(e.target).select2('val');
        if (val === passport || val === attendantPassport) {
            $(selectorDialog + ' .document-seria').inputmask('9999', { clearIncomplete: true });
            $(selectorDialog + ' .document-number').inputmask('999999', { clearIncomplete: true });
        } else if (val === birthCertId) {
            $(selectorDialog + ' .document-seria').inputmask('Regex', { regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]', clearIncomplete: true });
            $(selectorDialog + ' .document-number').inputmask('999999', { clearIncomplete: true });
        } else if (!val) {
            $(selectorDialog + ' .document-number').inputmask('remove');
            $(selectorDialog + ' .document-seria').inputmask('remove');
        } else {
            $(selectorDialog + ' .document-number').inputmask('remove');
            $(selectorDialog + ' .document-seria').inputmask('remove');
        }
    });

    $("#cbAddress").change(e => {
        if ($("#cbAddress").prop("checked")) {
            $(".div-address-control").removeClass("hidden");
            $(".div-address-text").addClass("hidden");
        } else {
            $(".div-address-control").addClass("hidden");
            $(".div-address-text").removeClass("hidden");
        }
    });

    var tradeUnionSetStateView = () => {
        var $e = $('#IsParentUnionist');
        var $x = $('#IsRelativeUnionist');
        if ($e.prop('checked') || $x.prop('checked')) {
            $('.tradeUnionOrgDiv').removeClass('hidden');
            if ($('#IsTradeUnionOrgNotPresent').prop('checked')) {
                $('#divOtherTradeUnionOrgName').removeClass('hidden');
            } else {
                $('#divOtherTradeUnionOrgName').addClass('hidden');
            }
        } else {
            $('.tradeUnionOrgDiv').addClass('hidden');
            $('#divOtherTradeUnionOrgName').addClass('hidden');
        }
    };

    $('#IsParentUnionist, #IsParentUnionist-False').change(e => {
        var $e = $('#IsParentUnionist');
        if ($e.prop('checked')) {
            $('#IsRelativeUnionist-False').prop('checked', true);
            $('#IsRelativeUnionist').trigger('change');
            $('.RelativeUnionistChecked').addClass('hidden');
        } else {
            $('.RelativeUnionistChecked').removeClass('hidden');
        }
        tradeUnionSetStateView();
    });

    $('#IsRelativeUnionist, #IsRelativeUnionist-False').change(e => {
        var $e = $('#IsRelativeUnionist');
        if ($e.prop('checked')) {
            $('.RelativeUnionist').removeClass('hidden');
        } else {
            $('.RelativeUnionist').addClass('hidden');
            $('.RelativeUnionist input').val('');
            $('.RelativeUnionist select').select2('val', -1);
        }
        tradeUnionSetStateView();
    });

    $('#IsScoolNotPresent').change(e => {
        if ($('#IsScoolNotPresent').prop('checked')) {
            $('#txtSchool').removeClass('hidden');
            $('#SelectedSchoolId').select2('data', { id: '', text: '-- Не выбрано --' });
            $('#SelectedSchoolId').prop("disabled", true);
        } else {
            $('#txtSchool').addClass('hidden');
            $('#School').val('');
            $('#SelectedSchoolId').prop("disabled", false);
        }
    });

    // профсоюзы
    $('#TradeUnionOrganizationId').select2({
        initSelection: (element, callback) => {
            if (element.val() == '' || element.val() == 0)
                callback({ id: '', text: '-- Не выбрано --' });
            else
                callback({ id: element.val(), text: element.attr('titletext') });
        },
        allowClear: true,
        language: "ru",
        ajax: {
            url: rootPath + '/api/Orgs',
            dataType: 'json',
            quietMillis: 250,
            data: (term, page) => {
                return {
                    query: term,
                    typeId: 4
                };
            },
            results: (data, page) => {
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
    });

    $('#IsTradeUnionOrgNotPresent').change(e => {
        if ($('#IsTradeUnionOrgNotPresent').prop('checked')) {
            $('#divOtherTradeUnionOrgName').removeClass('hidden');
            $('#TradeUnionOrganizationId').select2('data', { id: '', text: '-- Не выбрано --' });
            $('#TradeUnionOrganizationId').prop("disabled", true);
        } else {
            $('#divOtherTradeUnionOrgName').addClass('hidden');
            $('#TradeUnionOrganizationOther').val('');
            $('#TradeUnionOrganizationId').prop("disabled", false);
        }
    });

    $('#persons').on('click', '.remove-person-btn', (e) => {
        let $tr = $(e.target).closest('tr');
        if ($('#Data_Id').val().toString() === '0') {
            $tr.remove();
            if ($('#persons').find('tbody tr').length === 0) {
                $('#persons').addClass('hidden');
            }
        } else {
            let data = collectDataFromRow($tr)

            $.ajax({
                type: "post",
                url: rootPath + "/TradeUnion/DeleteChild?id=" + data.Id,
                contentType: 'application/json; charset=utf-8'
            }).done((data) => {
                if (!data){
                    $tr.remove();
                    if ($('#persons').find('tbody tr').length === 0) {
                        $('#persons').addClass('hidden');
                    }
                }
            });
        }
    });

    function clearTabsFormsValidation() {
        $('.dialog-validation-alert').addClass('hidden').html('Заполнены не все обязательные поля');
        $(".has-error").removeClass("has-error");
    }

    function validateSingleField(item, e) {
        var $element = $("#" + e);
        if (item[e] == null || (item[e] + "").trim() == "" || !(/[A-Za-zА-Яа-я0-9]/.test((item[e] + "")))) {
            $element.parent().addClass("has-error");
            return false;
        } else {
            $element.parent().removeClass("has-error");
            return true;
        }
    }

    // Проверка типа документа ребенка в зависимости от возраста (св-во или пасспорт 14 лет)
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
            } else if (birth.isBefore(ago14_1) && docId === '22') {
                $('#Child-DocumentTypeId, #Child-DateOfBirth').parent().addClass('has-error');
                return 'Указанный возраст ребенка предполагает наличие паспорта РФ';
            }
        }

        return null;
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
                    } else {
                        $element.parent().removeClass("has-error");
                    }
                }
            }
        }

        // для подраздела "Образовательное учреждение" логика сложнее
        if (item['IsScoolNotPresent'] === 'True' || item['IsScoolNotPresent'] === true) {
            requiredOk = requiredOk && validateSingleField(item, 'School');
        } else {
            requiredOk = requiredOk && validateSingleField(item, 'SelectedSchoolId');
        }

        // так же для "Родственник член профсоюза"
        if (item['IsRelativeUnionist'] === 'True' || item['IsRelativeUnionist'] === true) {
            requiredOk = validateSingleField(item, 'TradeUnionStatusByChildId') && requiredOk;
            requiredOk = validateSingleField(item, 'Unionist-LastName') && requiredOk;
            requiredOk = validateSingleField(item, 'Unionist-FirstName') && requiredOk;
            requiredOk = validateSingleField(item, 'RelativePlaceWork') && requiredOk;
        }

        // #22886
		/*if (item['IsParentUnionist'] === 'False' && item['IsRelativeUnionist'] === 'False') {
			errors.push('Не указано кто состоит в профсоюзе');
		}*/

        if (item['IsParentUnionist'] === 'True' || item['IsRelativeUnionist'] === 'True' || item['IsParentUnionist'] === true || item['IsRelativeUnionist'] === true) {
            // и для "Иного профсоюза"
            if ($('#IsTradeUnionOrgNotPresent').prop('checked')) {
                requiredOk = validateSingleField(item, 'TradeUnionOrganizationOther') && requiredOk;
            } else {
                requiredOk = validateSingleField(item, 'TradeUnionOrganizationId') && requiredOk;
            }
        }

        var mos = ($("#cbAddress").length && $("#cbAddress").prop("checked")) || $(".street-autocompleteAR").length;
        if ((mos && !addressControlValid()) || (!mos && !validateSingleField(item, "AddressChild"))) {
            errors.push('Не заполнен адрес регистрации');
        }

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
                    } else if ($element.is('input[type=checkbox]')) {
                        $element.prop('checked', item[e]);
                        $element.trigger('change');
                    } else if ($element.is('input[type=hidden]')) {
                        if (e == 'TradeUnionOrganizationId')
                            $element.select2('data', { id: item[e], text: item['TradeUnionOrganizationName'] });
                        else
                            $element.select2('data', { id: item[e], text: item['SelectedSchoolName'] });
                    } else if ($element.is('select')) {
                        if (item[e]) {
                            $element.select2('val', item[e]);
                            $element.trigger('change');
                        } else {
                            $element.select2('val', 22);
                            $element.trigger('change');
                        }
                    } else {
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

    function editPerson(item, callback) {
        $('#personSaveBtn').unbind();
        $('.retweet-by-snils').unbind();

        fillFormByData(item);

        $('#personSaveBtn').click(() => {
            for (var e in item) {
                if (item.hasOwnProperty(e)) {
                    var $element = $('#' + e);
                    if ($element.is('input[type=radio]')) {
                        item[e] = $element.prop('checked');
                    } else if ($element.is('input[type=checkbox]')) {
                        item[e] = $element.prop('checked');
                    } else if ($element.is('input[type=hidden]')) {
                        item[e] = $element.select2('val');
                    } else if ($element.is('select')) {
                        item[e] = $element.select2('val');
                    } else if ($element.length > 0) {
                        item[e] = $element.val();
                    }
                }
            }

            item['Child-DocumentTypeName'] = $('#Child-DocumentTypeId').select2('data').text;
            item['SelectedSchoolName'] = $('#SelectedSchoolId').select2('data').text;
            item['TradeUnionOrganizationName'] = $('#TradeUnionOrganizationId').select2('data').text;

            // Адрес
            var addr: any;
            if ($("#cbAddress").prop("checked") || $(".street-autocompleteAR").length) {
                addr = addressControlGetValue() || {};
                addr.id = addr.id || 0;
                item["AddressChild"] = "";
            } else {
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
                $.each(errors, (i, e) => { txt = txt + e + '<br/>'; });
                $('.dialog-validation-alert').html(txt);
            } else {
                $('.dialog-validation-alert').addClass('hidden');
            }

            // проверка сумм (предупреждение)
            var pf = function (str) { if (str == null) return 0; return parseFloat(str.replace(',', '.')); };
            var summa = pf(item['Summa']);
            var summaParent = pf(item['SummaParent']);
            var summaTradeUnion = pf(item['SummaTradeUnion']);
            var summaBudget = pf(item['SummaBudget']);
            var summaOrganization = pf(item['SummaOrganization']);

            item['Summa'] = pf(item['Summa']);
            item['SummaParent'] = pf(item['SummaParent']);
            item['SummaTradeUnion'] = pf(item['SummaTradeUnion']);
            item['SummaBudget'] = pf(item['SummaBudget']);
            item['SummaOrganization'] = pf(item['SummaOrganization']);
            var total = summaParent * 100 + summaTradeUnion * 100 + summaBudget * 100 + summaOrganization * 100;
            if (summa * 100 !== total) {
                BootstrapDialog.alert({
                    type: BootstrapDialog.TYPE_WARNING,
                    title: 'Внимание!',
                    message: 'Полная стоимость не равна сумме указанных средств на вкладке "Сведения о стоимости путевки".',
                    callback: function () {
                        if (isValid) {
                            callback(item);
                            $('#dialogPerson').modal('hide');
                        }
                    }
                });
            } else if (isValid) {
                callback(item);
                $('#dialogPerson').modal('hide');
            }
        });


        $('.retweet-by-snils').click(() => {

            var snils = $('.snils').val();

            $.ajax({
                type: "GET",
                url: rootPath + "api/WebTradeUnion/GetChildBySNILS?snils=" + snils,
                contentType: 'application/json; charset=utf-8'
            }).done((data) => {
                if (data) {
                    fillFormByData(data);
                }
            });
        });

        $('#dialogPerson').modal({ backdrop: false });
    }

    function collectDataFromRow($row) {
        let data = JSON.parse($row.find('.hdn-json').val());
        return data;
    }

    function bindDataToRow($row, item) {
        var name = item['Child-LastName'] + ' ' + item['Child-FirstName'] + ' ' + item['Child-MiddleName'];
        var sex = item['Child-Male'] ? "муж" : "жен";
        var dateBirth = item['Child-DateOfBirth'] ? moment(item['Child-DateOfBirth'], 'DD.MM.YYYY').format('DD.MM.YYYY') : '-';
        var document = item['Child-DocumentTypeName'] + ' ' + item['Child-DocumentSeria'] + ' ' + item['Child-DocumentNumber'];
        var tradeun = item['TradeUnionOrganizationId'] > 0 ? item['TradeUnionOrganizationName'] : item['TradeUnionOrganizationOther'];

        function mapDataToRow(object){
            $row.find('.fio').html(name);
            $row.find('.sex').html(sex);
            $row.find('.birthDate').html(dateBirth);
            $row.find('.document').html(document);
            $row.find('.tradeun').html(tradeun);
            $row.find('.hdn-json').val(JSON.stringify(object));
        }

        if ($('#Data_Id').val().toString() === '0'){
            mapDataToRow(item);
        } else {
            item.TradeUnionId = $('#Data_Id').val().toString();
            $.ajax({
                type: "post",
                url: rootPath + "/TradeUnion/SaveChild",
                data: 'child=' + encodeURI(JSON.stringify(item))
            }).done((data) => {
                mapDataToRow(JSON.parse(data));
            });
        }
    }

    var templateFn = doT.template($('#rowPerson').html());
    var key = 1;

    $('#addPerson').click(() => {
        var $row = $(templateFn(-key));
        editPerson(collectDataFromRow($row), (item) => {
            bindDataToRow($row, item);
            $('#persons').removeClass('hidden');
            $('#persons').children('tbody').append($row);
            key++;
        });
    });

    $('#persons').on('click', '.edit-person-btn', (e) => {
        var $row = $(e.target).closest('tr');
        editPerson(collectDataFromRow($row), (item) => {
            bindDataToRow($row, item);
        });
    });

    clearTradeUnionChildForm = (dataModificator) => {
        var $row = $(templateFn(-key));
        var data = collectDataFromRow($row);
        if (dataModificator)
            dataModificator(data);
        fillFormByData(data);
    };

    var linkMiddleNamePresent = function (selCb, selInp) {
        $(selCb).change(() => {
            if ($(selCb).prop("disabled"))
                return;
            if ($(selCb).prop('checked')) {
                $(selInp).val('');
                $(selInp).prop('disabled', true);
            } else {
                $(selInp).prop('disabled', false);
            }
        });
    };

    linkMiddleNamePresent('#Child-HaveMiddleName', '#Child-MiddleName');
    linkMiddleNamePresent('#Parent-HaveMiddleName', '#Parent-MiddleName');
    linkMiddleNamePresent('#Unionist-HaveMiddleName', '#Unionist-MiddleName');

    //$("select.bti-district-id").trigger("change");
});

// @ts-ignore
function confirmStateButtonTradeUnion(formSelector, actionSelector, actionCode, buttonName, description, commentSelector) {
    var error = '';
    if (!$('.GroupedTimeOfRestId').select2('val') || $('.GroupedTimeOfRestId').select2('val') <= 0) {
        error = error + '<li>Поле смена не заполнено</li>';
    }

    if (!$('.CampId').select2('val') || $('.CampId').select2('val') <= 0) {
        error = error + '<li>Поле лагерь не заполнено</li>';
    } else {
        $('.CampId').select2('val')
    }

    if (error) {
        ShowAlert('<ul>' + error + '</ul>', 'alert-danger', "", true);
        return;
    }

    return confirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector);
}
