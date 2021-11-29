var typeOfCampOptions = [];
var typeOfTransportOptions = [];
var inited = false;
var attendantFn = doT.template($('#attendantTemplate').html());
var CompensationEnum = -2;
var CompensationYouthRestEnum = 19;
function containsInArray(a, obj) {
    for (var i = 0; i < a.length; i++) {
        if (a[i] === obj) {
            return true;
        }
    }
    return false;
}
function refreshLinkOnChilds(name) {
    $('#' + name + 'Link').find('a').each(function (index) {
        var el = $(this);
        el.attr('href', '#' + name + index);
        el.attr('id', name + 'Ref' + index);
    });
    $('.remove-btn').click(function (e) {
        var target = $(e.target);
        var parent = target.parent().parent();
        parent.find('.remove-btn').addClass("hidden");
        parent.find('.fileinput-button').removeClass('hidden');
        parent.find('.file-span').removeClass('hidden');
        parent.find('.DocumentFileTitle').val('');
        parent.find('.DocumentFileUrl').val('');
        var href = parent.find('.href-file');
        href.attr('href', '');
        href.html('');
        href.addClass('hidden');
    });
    changeIndexInNames($('#' + name + 's'), name);
    $('[data-spy="scroll"]').each(function () {
        $(this).scrollspy('refresh');
    });
}
function getFio(guid) {
    var panel = $('input.guid[value="' + guid + '"]').closest('.request-block');
    var firstName = panel.find('[name$=".FirstName"]').val();
    var lastName = panel.find('[name$=".LastName"]').val();
    var middleName = panel.find('[name$=".MiddleName"]').val();
    return lastName + ' ' + firstName + ' ' + middleName;
}
function getVoucherInfo(guid) {
    var panel = $('#InformationVoucherDataRows input.id[value="' + guid + '"]').closest('.request-block');
    var organizationName = panel.find('.organizationName').val();
    var dateFrom = panel.find('.informationVouchers-dateFrom').val();
    return organizationName + ' - ' + dateFrom;
}
function bindAutoFill(self, name) {
    $(self).find('.name-input').keyup(function () {
        var id = $(this).closest('.request-block').find('.id').val();
        var guid = $(this).closest('.request-block').find('.guid').val();
        var regExp = new RegExp("([0-9]+)", "g");
        var string = '';
        $(self).find('.name-input').each(function () {
            string += $(this).val() + ' ';
        });
        if (name !== 'Applicant') {
            $('#' + name + 'Ref' + this.name.match(regExp)[0]).text(string);
            $('#' + name + 'Title' + this.name.match(regExp)[0]).text(string);
            $('input.child-attendant-fio[value="' + guid + '"]').select2("data", { id: guid, text: getFio(guid) });
        }
        if (name === 'Applicant' || name === 'Attendant') {
            $('.service-link-applicant-id[value="' + id + '"]').each(function (i, val) {
                $(val).closest('tr').find('.addon-service-camper-name').html(string);
            });
        }
        else if (name === 'Child') {
            $('.service-link-applicant-id[value="' + id + '"]').each(function (i, val) {
                $(val).closest('tr').find('.addon-service-camper-name').html(string);
            });
        }
    });
}
function addElement(name, templateFn) {
    var model = $(templateFn({}));
    var $item = $('#' + name + 's');
    $item.append(model);
    var link = $('<li><a href="#' + name + '" id="' + name + 'Ref">-</a></li>');
    $('#' + name + 'Link').append(link);
    bindAutoFill(model, name);
    refreshLinkOnChilds(name);
    $(document).trigger("AddNewChild", model);
    changeGuids($item, name);
    try {
        $('.datepicker-my').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date() });
    }
    catch (e) {
    }
    try {
        $('.datepicker-future').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', minDate: new Date() });
    }
    catch (e) {
    }
    try {
        $('.datepicker-general').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    }
    catch (e) {
    }
    inputMaskConfig($item);
    return model;
}
function removeElement(self, name) {
    try {
        var elementToRemove = $(self).parent().parent().parent();
        var regExp = new RegExp("([0-9]+)", "g");
        $('#' + name + 'Ref' + elementToRemove.find('.name-input').attr('name').match(regExp)[0]).remove();
        elementToRemove.remove();
    }
    catch (e) {
    }
    refreshLinkOnChilds(name);
}
function removeElementWithConfirmation(self, name, onOk, onCancel) {
    BootstrapDialog.show({
        title: 'Удаление',
        message: 'Вы действительно хотите удалить запись?',
        buttons: [
            {
                label: 'Удалить',
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    removeElement(self, name);
                    if (onOk) {
                        onOk();
                    }
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: function (dialogItself) {
                    if (onCancel) {
                        onCancel();
                    }
                    dialogItself.close();
                }
            }
        ]
    });
}
var newAttendantId = 2;
function addAttendant() {
    var model = addElement('Attendant', attendantFn);
    var attendantId = -(newAttendantId++);
    model.find('.id').val(String(attendantId));
}
function changeCountAttendant(countSetted, count) {
    if (countSetted > count) {
        for (var i = 0; i < (countSetted - count); i++) {
            addAttendant();
        }
        $('.remove-attendant-button').addClass('hidden');
    }
    else if (count > countSetted) {
        for (var j = 0; j < (count - countSetted); j++) {
            removeElement($('#Attendants').children('fieldset:last').find('.remove-attendant-button'), 'Attendant');
        }
    }
}
function lockAttendantAddButton() {
    var typeOfRest = $('select.type-of-rest').select2('val');
    if (!typeOfRest || typeOfRest === CompensationEnum.toString()) {
        if (typeOfRest === CompensationEnum.toString()) {
            $('#AddAttendant').removeClass('hidden');
        }
        return;
    }
    var pb = $('#agentProxyBlock');
    if ($('#is-agent-accomp').select2('val') === 'True'
        && $('.TypeOfRestId').val() != CompensationEnum.toString()
        && $('.TypeOfRestId').val() != CompensationYouthRestEnum.toString()) {
        pb.removeClass('hidden');
    }
    else {
        pb.addClass('hidden');
        pb.find('input').val('');
    }
    $('.remove-attendant-button').addClass('hidden');
    $('#AddAttendant').addClass('hidden');
    var count = $('input#Data_CountAttendants').length !== 0
        ? $('input#Data_CountAttendants').val()
        : $('select#Data_CountAttendants').select2('val');
    if (count) {
        var attendantsCount = parseInt(count);
        var attendants = $('.attendant-panel').length + ($('.is-accomp').select2('val') === 'True' ? 1 : 0) + ($('#is-agent-accomp').select2('val') === 'True' ? 1 : 0);
        changeCountAttendant(attendantsCount, attendants);
    }
}
function removeChildElement(self) {
    removeElementWithConfirmation(self, 'Child');
}
function removeInformationVoucherElement(self) {
    var elementToRemove = $(self).parent().parent().parent();
    BootstrapDialog.show({
        title: 'Удаление',
        message: 'Вы действительно хотите удалить путевку?',
        buttons: [
            {
                label: 'Удалить',
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    var id = $(self).closest('.request-block').find('input.id').val();
                    elementToRemove.remove();
                    changeIndexInNamesVoucher($('#InformationVoucherDataRows'), 'InformationVouchers');
                    $('input.child-voucher-name').each(function (i, e) {
                        if ($(e).val() == id) {
                            $(e).select2('data', { id: '', text: '-- Не выбрано --' });
                        }
                    });
                    if ($('#InformationVoucherDataRows').children().length === 0) {
                        $('#InformationVoucherEmptyRow').removeClass('hidden');
                    }
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }
        ]
    });
}
function removeInformationVoucherAttendantElement(self) {
    var elementToRemove = $(self).parent().parent();
    var $tbody = elementToRemove.parent();
    var $p = $tbody.parent().parent();
    BootstrapDialog.show({
        title: 'Удаление',
        message: $('select.type-of-rest').select2('val') == CompensationYouthRestEnum.toString() ?
            'Вы действительно хотите удалить отдыхающего из путевки?' : 'Вы действительно хотите удалить сопровождающего из путевки?',
        buttons: [
            {
                label: 'Удалить',
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    elementToRemove.remove();
                    changeIndexInNamesVoucher($p.find('table tbody'), 'AttendantsPrice');
                    if ($tbody.children().length === 0) {
                        $tbody.append($('<tr class="emptyRow"><td colspan="5"><i>' + ($('select.type-of-rest').select2('val') == CompensationYouthRestEnum.toString() ? 'Нет отдыхающих' : 'Нет сопровождающих') + '</i></td></tr>'));
                    }
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }
        ]
    });
}
function removeAttendantElement(self) {
    removeElementWithConfirmation(self, 'Attendant', function () {
        var id = $(self).closest('.request-block').find('input.id').val();
        var guid = $(self).closest('.request-block').find('input.guid').val();
        $('input.child-attendant-fio[value="' + guid + '"]').select2('data', { id: '', text: '-- Не выбрано --' });
        lockAttendantAddButton();
        $('#AddonServicesTable tbody').find('tr:has(.service-link-applicant-id[value="' + id + '"])').remove();
        if ($('#AddonServicesTable tbody tr').length === 0) {
            $('#AddonServicesTable').addClass('hidden');
        }
    });
}
function configDropdowns() {
    $('.select2').not('.select2-container, .inited').addClass('inited').select2();
    $('.child-attendant-fio').not('.select2-container, .inited').each(function (i, e) {
        var $e = $(e);
        $e.addClass('inited');
        $e.select2({
            initSelection: function (element, callback) {
                if (element.val() == '')
                    callback({ id: '', text: '-- Не выбрано --' });
                else
                    callback({ id: element.val(), text: getFio(element.val()) });
            },
            query: function (query) {
                var result = [{ id: '', text: '-- Не выбрано --' }];
                var guids = $('.guid');
                guids.each(function (i) {
                    var guid = $(guids[i]);
                    if (guid.hasClass('applicant-guid') && $('#NeedPlacmentApplicant').find('.is-accomp').select2('val') !== 'True') {
                        return;
                    }
                    if (guid.hasClass('agent-guid') && !($('#is-agent-accomp').select2('val') === 'True')) {
                        return;
                    }
                    result.push({
                        id: guid.val(),
                        text: getFio(guid.val())
                    });
                });
                query.callback({
                    results: result,
                    more: false
                });
            }
        }).change(function () {
            $e.attr('value', $e.select2('val'));
        });
    });
    $('.child-voucher-name').not('.select2-container, .inited').addClass('inited').select2({
        initSelection: function (element, callback) {
            if (element.val() == '')
                callback({ id: '', text: '-- Не выбрано --' });
            else
                callback({ id: element.val(), text: getVoucherInfo(element.val()) });
        },
        query: function (query) {
            var result = [{ id: '', text: '-- Не выбрано --' }];
            var guids = $('#InformationVoucherDataRows .id');
            guids.each(function (i) {
                var guid = $(guids[i]);
                result.push({
                    id: guid.val(),
                    text: getVoucherInfo(guid.val())
                });
            });
            query.callback({
                results: result,
                more: false
            });
        }
    });
    $('.school-select2').not('.select2-container, .inited').addClass('inited').select2({
        initSelection: function (element, callback) {
            callback({ id: element.attr('data-default-id'), text: element.attr('data-default-text') });
        },
        minimumInputLength: 1,
        ajax: {
            url: '../../api/WebSchools',
            dataType: 'json',
            quietMillis: 250,
            data: function (term) {
                return {
                    query: term
                };
            },
            results: function (data) {
                var result = [{ id: '', text: '-- Не выбрано --' }];
                if (data) {
                    for (var i = 0; i < data.length; i++) {
                        result.push({ id: data[i].id, text: data[i].name });
                    }
                }
                return {
                    results: result
                };
            },
            cache: true
        }
    });
}
function getPlacement(id) {
    if (id == 0) {
        return {
            Id: 0,
            Name: '-- Не выбрано --'
        };
    }
    for (var i = 0; i < placeOfRest.length; i++) {
        if (placeOfRest[i].Id == id) {
            return placeOfRest[i];
        }
    }
    return undefined;
}
function getTypeOfRest(id) {
    if (id == 0) {
        return {
            Id: 0,
            Name: '-- Не выбрано --'
        };
    }
    for (var i = 0; i < typeOfRest.length; i++) {
        if (typeOfRest[i].Id == id) {
            return typeOfRest[i];
        }
    }
    return undefined;
}
function getBenefitType(id) {
    if (id == 0) {
        return {
            Id: 0,
            Name: '-- Не выбрано --'
        };
    }
    for (var i = 0; i < benefitType.length; i++) {
        if (benefitType[i].Id == id) {
            return benefitType[i];
        }
    }
    return undefined;
}
window.onload = function () {
    $("#statusApplicant").select2().trigger('change'); // необходимо для того чтобы отобразился раздел "Сведения о представителе заявителя"
    $.each($(".benefit-type-id"), function (i, el) {
        var hiddenValue = $(el).val();
        if (hiddenValue.length > 0) {
            $(el).siblings("select.benefit-type-dropdown").select2().val(hiddenValue);
            $(el).siblings("select.benefit-type-dropdown").select2().trigger("change");
        }
        else {
            $(el).val("0");
        }
    });
    $("select.typeOfCamp").select2().find("option").each(function (i, option) {
        var campId = $(option).val();
        var campName = $(option).text();
        typeOfCampOptions.push({ id: campId, text: campName });
    });
    $("select.priorityTypeOfTransport").select2().find("option").each(function (i, option) {
        var typeOfTransportId = $(option).val();
        var typeOfTransportName = $(option).text();
        typeOfTransportOptions.push({ id: typeOfTransportId, text: typeOfTransportName });
    });
    ToggleTypeOfTransportBlock();
    ToggleTypeOfCampBlock();
    // небходимо для скрытия лишних блоков при копировании заявления
    var buf = $('#mainPlaces').select2('val');
    $('#mainPlaces').val('15');
    $('#mainPlaces').trigger("change");
    $('#mainPlaces').val(buf);
    $('#mainPlaces').trigger("change");
};
function attendantChangeProxy($e) {
    var pb = $e.closest('fieldset.attendant-panel').find('.proxy-block');
    if ($e.prop('checked') && $('.TypeOfRestId').val() != CompensationEnum.toString()) {
        pb.removeClass('hidden');
    }
    else {
        pb.addClass('hidden');
        pb.find('input').val('');
    }
}
// Отображать или скрывать блок с приоритетныи и дополнительным видом транспорта в зависимости от цели обращения, направления отдыха и способа проезда
function ToggleTypeOfTransportBlock() {
    var typeOfRestId = parseInt($('select.type-of-rest').select2('val'));
    var placeOfRestId = parseInt($('.placeOfRestId').select2('val'));
    var transferFromVal = parseInt($('select.transferFrom').select2('val'));
    var transferToVal = parseInt($('select.transferTo').select2('val'));
    if (typesOfRestRequiringTransportSelection.indexOf(typeOfRestId) != -1
        && placesOfRestRequiringTransportSelection.indexOf(placeOfRestId) != -1
        && (transferFromVal == typeOfTransferRequiringTransportSelection || transferToVal == typeOfTransferRequiringTransportSelection)) {
        $("#TypeOfTransport").parent().removeClass('hidden');
        $("#TypeOfTransportLink").removeClass('hidden');
        $('#TypeOfTransportLink').html('<a href="#TypeOfTransport">Тип транспорта</a>');
        /*if ($('.priorityTypeOfTransport').se==)*/
        $('.priorityTypeOfTransport').trigger('change');
    }
    else {
        $("#TypeOfTransport").parent().addClass('hidden');
        $('#TypeOfTransportLink').html('');
        $('.priorityTypeOfTransport').select2('val', null);
        $('.additionalTypeOfTransport').select2('val', null);
    }
}
// Отображать или скрывать блок с приоритетныи и дополнительным типом лагеря в зависимости от цели обращения
function ToggleTypeOfCampBlock() {
    var typeOfRestId = parseInt($('select.type-of-rest').select2('val'));
    var typeOfRest = getTypeOfRest(typeOfRestId);
    if (typeOfRestRequiringCampTypeSelection == (typeOfRest === null || typeOfRest === void 0 ? void 0 : typeOfRest.ParentId)) {
        $("#TypeOfCamp").parent().removeClass('hidden');
        $("#TypeOfCampLink").removeClass('hidden');
        $('#TypeOfCampLink').html('<a href="#TypeOfCamp">Тип лагеря</a>');
    }
    else {
        $("#TypeOfCamp").parent().addClass('hidden');
        $('#TypeOfCampLink').html('');
        $('.typeOfCamp').select2('val', '-1');
        $('.typeOfCampAddon').select2('val', '-1');
    }
}
$(function () {
    var childFn = doT.template($('#childTemplate').html());
    var voucherFn = doT.template($('#voucherTemplate').html());
    var voucherAttendantFn = doT.template($('#voucherTemplateAttendant').html());
    var fileRowFn = doT.template($('#fileRowTemplate').html());
    $('.datepicker-general').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('.datepicker-my').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date() });
    $('.datepicker-future').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', minDate: new Date() });
    $('.datetimepicker').datetimepicker({ showTodayButton: true, maxDate: new Date() });
    $('#Attendants').on('change', 'input.is-proxy', function (e) {
        attendantChangeProxy($(e.target));
    });
    $('#Attendants').on('change', 'select.applicantType', function (e) {
        var $e = $(e.target);
        var $cbx = $e.closest('.form-group').find('input.is-proxy');
        $cbx.prop('checked', $e.select2('val') === '3');
        attendantChangeProxy($cbx);
    });
    $('#addPlace').click(function (e) {
        var target = $(e.target);
        var urlToGo = target.attr('urlToGo');
        var types = $('#typeOfRoomsSelect select');
        var typesString = '';
        types.each(function (i, em) {
            var v = $(em).select2('val');
            if (v) {
                typesString = typesString + (typesString ? ',' : '') + v;
            }
        });
        if (!typesString) {
            $('#AddonPlaceModalError').removeClass('hidden');
            return;
        }
        $('#AddonPlaceModalError').addClass('hidden');
        window.location.replace(urlToGo + '&rateTypeString=' + typesString);
    });
    function changeAgent() {
        if ($('#AgentApplicant').prop('checked')) {
            $('#Agent').parent('fieldset').removeClass('hidden');
            $('#AgentLink').html('<a href="#Agent">Сведения о представителе заявителя</a>');
            $('#AgentLink').show();
            $('[data-spy="scroll"]').each(function () {
                $(this).scrollspy('refresh');
            });
            if (inited) {
                if ($("#is-agent-accomp option[value='']").length !== 0) {
                    $('#is-agent-accomp').select2('val', null);
                }
                else {
                    $('#is-agent-accomp').select2('val', $("#is-agent-accomp").select2().val());
                }
            }
        }
        else {
            $('#Agent').parent('fieldset').addClass('hidden');
            $('#AgentLink').html('');
            $('[data-spy="scroll"]').each(function () {
                $(this).scrollspy('refresh');
            });
            $('#is-agent-accomp').select2('val', 'False');
            lockAttendantAddButton();
        }
    }
    function removeFile(e) {
        var $item = $(e.target);
        var $tbody = $item.parent().parent().parent();
        var $row = $item.parent().parent();
        $row.remove();
        var childs = $tbody.find('tr');
        var regExp = new RegExp("\[[0-9]+\].D", "g");
        var _loop_1 = function (i) {
            $(childs[i]).find('input').each(function () {
                if ($(this).attr('name')) {
                    $(this).attr('name', $(this).attr('name').replace(regExp, '[' + i.toString() + '].D'));
                }
            });
        };
        for (var i = 0; i < childs.length; i++) {
            _loop_1(i);
        }
    }
    $('.request-file-remove').click(removeFile);
    $('.fileinput-button').each(function () {
        var realName = '';
        var fu = $(this);
        fu.fileupload({
            url: rootPath + '/Upload.ashx',
            dataType: 'json',
            pasteZone: null,
            dropZone: null,
            maxChunkSize: 1000000,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("X-FileName", realName);
            },
            submit: function (e) {
                var target = $(e.target);
                var parent = $(target.parent().parent().parent()[0]);
                parent.find('.file-upload-div').addClass('hidden');
                parent.find('.file-uploading-div').removeClass('hidden');
            },
            always: function (e) {
                var target = $(e.target);
                var parent = $(target.parent().parent().parent()[0]);
                parent.find('.file-upload-div').removeClass('hidden');
                parent.find('.file-uploading-div').addClass('hidden');
            },
            done: function (e, data) {
                realName = '';
                $.each(data.result, function (index, file) {
                    var target = $(e.target);
                    var parent = $(target.parent().parent().parent()[0]);
                    var tbody = parent.find('table tbody');
                    var entity = {
                        fileTypesIndex: parent.find(".index-hidden").val(),
                        fileTitle: file.name,
                        fileName: file.realname,
                        fileIndex: parent.find('table tbody tr').length
                    };
                    var row = $(fileRowFn(entity));
                    row.find('.request-file-remove').click(removeFile);
                    tbody.append(row);
                });
            }
        });
        fu.on('fileuploadchunkdone', function (e, data) {
            $.each(data.result, function (index, file) {
                realName = file.realname;
            });
        });
    });
    var htmlBenefitTypeDropdown = '';
    var newChildId = 1;
    function initTypeOfSubrestrtiction($e) {
        var $restriction = $e.find('.restriction-select');
        var $subres = $e.find('.subrestriction-select');
        var $div = $e.find('.type-of-subrestriction');
        $subres.select2({
            initSelection: function (element, callback) {
                if ($subres.val() === '') {
                    callback({ id: '', text: '-- Не выбрано --' });
                }
                else {
                    callback({ id: $subres.val(), text: $subres.attr('titleText') });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + 'Api/WebVocabulary/GetTypeOfSubRestriction',
                dataType: 'json',
                data: function () {
                    var t = $restriction.select2('val');
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
                cache: true
            }
        });
        $restriction.on('change', function () {
            var val = $restriction.select2('val');
            $subres.select2('data', { id: '', text: '-- Не выбрано --' });
            if (containsInArray(typeOfRestrictionSubs, parseInt(val))) {
                $div.removeClass('hidden');
            }
            else {
                $div.addClass('hidden');
            }
        });
    }
    $('.child-info-block').each(function (i, e) {
        initTypeOfSubrestrtiction($(e));
    });
    function addChild() {
        var model = addElement('Child', childFn);
        model.find('.id').val(String(-newChildId));
        newChildId++;
        var typeOfRest = getTypeOfRest($('#typeOfRest-select2').val());
        var priorityPlacement = getPlacement($('#priority-placement').val());
        var additionalPlacement = getPlacement($('#additional-placement').val());
        if (typeOfRest.NeedAttendant) {
            if ($('#isFirstCompany').val() === 'False') {
                $('.attendant-row').removeClass('hidden');
            }
        }
        if (typeOfRest.FirstRequestCompanySelect && inited) {
            $('.firstRequestCompanyHide').addClass('hidden');
        }
        if (priorityPlacement && (priorityPlacement.IsForegin || additionalPlacement.IsForegin)) {
            $('.foreign-document').removeClass('hidden');
        }
        else {
            $('.foreign-document').addClass('hidden');
        }
        if (typeOfRest.Id == CompensationEnum.toString()) {
            $('.RequestInformationVoucherChild').removeClass('hidden');
        }
        else {
            $('.RequestInformationVoucherChild').addClass('hidden');
        }
        if ($('#typeOfRest-select2').val() != 0) {
            $('.child-age-restriction').removeClass('hidden');
            $('.child-age-restriction-val').text('Допустимый возраст: ' + typeOfRest.MinAge + '-' + typeOfRest.MaxAge + ' лет');
        }
        else {
            $('.child-age-restriction').addClass('hidden');
        }
        if (htmlBenefitTypeDropdown) {
            $('select.benefit-type-dropdown').html(htmlBenefitTypeDropdown);
            $('select.benefit-type-dropdown').select2('val', 0);
            $('select.benefit-type-dropdown').trigger('change');
        }
        initTypeOfSubrestrtiction(model);
    }
    $('#AddChild').click(function () {
        addChild();
    });
    $('#AddAttendant').click(function () {
        addAttendant();
        lockAttendantAddButton();
    });
    var newVoucherId = 1;
    var addVoucherAttendantClick = function (e) {
        var $p = $(e.target).parent();
        var model = $(voucherAttendantFn({}));
        inputMaskConfig(model);
        $p.find('table tbody').append(model);
        $p.find('table tbody .emptyRow').remove();
        changeIndexInNamesVoucher($('#InformationVoucherDataRows'), 'InformationVouchers');
        changeIndexInNamesVoucher($p.find('table tbody'), 'AttendantsPrice');
        configDropdowns();
    };
    $('#AddVoucher').click(function () {
        var model = $(voucherFn({}));
        newVoucherId = -(newVoucherId++);
        model.find('.id').val(String(newVoucherId));
        model.find('select').select2();
        inputMaskConfig(model);
        try {
            model.find('.datepicker-my').datetimepicker({
                showTodayButton: true,
                format: 'DD.MM.YYYY',
                maxDate: new Date()
            });
        }
        catch (e) {
        }
        $('#InformationVoucherDataRows').append(model);
        changeIndexInNamesVoucher($('#InformationVoucherDataRows'), 'InformationVouchers');
        $('#InformationVoucherEmptyRow').addClass('hidden');
        model.find('.AddVoucherAttendant').click(addVoucherAttendantClick);
    });
    $('.AddVoucherAttendant').click(addVoucherAttendantClick);
    $('#InformationVoucherDataRows select').select2();
    $('#sidebar').affix({
        offset: {
            top: 70
        }
    });
    refreshLinkOnChilds('Child');
    $(document).on('AddNewChild', function () {
        configDropdowns();
    });
    $('#Childs').children().each(function () {
        var self = this;
        bindAutoFill(self, 'Child');
    });
    refreshLinkOnChilds('Attendant');
    $('#Attendants').children().each(function () {
        var self = this;
        bindAutoFill(self, 'Attendant');
    });
    bindAutoFill($('#ApplicantFieldset'), 'Applicant');
    configDropdowns();
    function changeCountChildren() {
        var typeOfRest = $('select.type-of-rest').select2('val');
        if (!typeOfRest || typeOfRest === CompensationEnum.toString() || typeOfRest === CompensationYouthRestEnum.toString()) {
            return;
        }
        var countSetted = parseInt($('#mainPlaces').select2('val'));
        var childrens = $('#Childs').children('fieldset');
        var countChildren = childrens.length;
        if (countSetted > countChildren) {
            for (var i = 0; i < (countSetted - countChildren); i++) {
                addChild();
            }
        }
        else if (countChildren > countSetted) {
            for (var j = 0; j < (countChildren - countSetted); j++) {
                removeElement($('#Childs').children('fieldset:last').find('.remove-child-button'), 'Child');
            }
        }
    }
    var timeOfRestFn = doT.template($('#timeOfRestTemplate').html());
    var statuses = $('select#statusApplicant').html();
    // при выборе приоритетным наземный транспорт дополнительный воздушный
    function changeAdditionalTransport(target) {
        var additionalTransport = $(target).closest('fieldset').find('.additionalTypeOfTransport');
        var $typeOfadditional = $("select.additionalTypeOfTransport");
        if (target.selectedIndex == 2) {
            additionalTransport.find("option[value=2]").remove();
            $('.additionalTypeOfTransport').select2('val', 1);
        }
        else {
            var optionToAdd = typeOfTransportOptions.filter(function (obj) { return obj.id == 2; })[0];
            if (optionToAdd && additionalTransport[1].childElementCount < 3) {
                $typeOfadditional.append(new Option(optionToAdd.text, optionToAdd.id, false, false));
            }
        }
    }
    function changeTypeOfRest(target) {
        var savedInited = inited;
        var val = getTypeOfRest($(target).select2('val'));
        if (!val) {
            return;
        }
        if (savedInited) {
            $('input.placeOfRestId').each(function (i, e) {
                $(e).select2('data', null);
            });
        }
        $('.time-of-rest')
            .each(function (i, e) {
            $(e).select2('data', null);
        });
        $.ajax(rootPath + 'Api/WebVocabulary/GetBenefitTypeInternal', {
            data: {
                typeOfRestId: val.Id
            },
            success: function (data) {
                var res = {
                    data: [{ id: 0, name: '-- Не выбрано --' }].concat(data)
                };
                htmlBenefitTypeDropdown = timeOfRestFn(res);
                $('select.benefit-type-dropdown')
                    .each(function (i, item) {
                    var $item = $(item);
                    var selected = $item.select2('data');
                    if (savedInited || !selected || selected.id === '0') {
                        $('select.benefit-type-dropdown').html(htmlBenefitTypeDropdown);
                        $('select.benefit-type-dropdown').select2('val', 0);
                    }
                });
            }
        });
        $.ajax({
            url: rootPath + '/api/WebVocabulary/GetYearsForTypeOfRest?typeOfRestId=' + (val.Id.toString()),
            dataType: 'json',
            success: function (data) {
                if (data.length === 1 && savedInited) {
                    $('#YearOfRestId')
                        .select2('data', {
                        text: data[0].name,
                        id: data[0].id
                    });
                }
            }
        });
        if (inited) {
            $('.TypeOfRestId').val(val.Id);
        }
        if (val.FirstRequestCompanySelect && inited) {
            $('.firstRequestCompanyHide').addClass('hidden');
        }
        if (val.Id == '13' || val.Id == '14' || val.Id == '21') {
            $('#ApplicantFieldset .address-control').removeClass('hidden');
        }
        else {
            $('#ApplicantFieldset .address-control').addClass('hidden');
        }
        var $placeRestLink = $('#PlaceRestLink');
        var $transfers = $('.transfers');
        if (val.NeedPlace) {
            $('#PlaceRest').parent().removeClass('hidden');
            $placeRestLink.html('<a href="#PlaceRest">Место отдыха</a>');
            $placeRestLink.show();
            $transfers.removeClass('hidden');
        }
        else {
            $('#PlaceRest').parent().addClass('hidden');
            $placeRestLink.html('');
            $placeRestLink.hide();
            $transfers.addClass('hidden');
            $transfers.find('select').each(function (i, e) {
                $(e).select2('val', '');
            });
        }
        if (val.NotChildren) {
            $('#mainPlaces').select2('val', 0);
            $('select.is-accomp').select2('val', 'True');
            $('#is-agent-accomp').select2('val', 'False');
            var $selector = $('select#statusApplicant');
            var savedVal = $selector.select2('val');
            $selector.empty();
            $selector.append(statuses);
            $selector.find('*[value=1]').remove();
            $selector.find('*[value=2]').remove();
            $selector.find('*[value=3]').remove();
            if (inited) {
                $selector.select2('val', 5);
            }
            else {
                $selector.select2('val', savedVal);
            }
            changeAgent();
            var childrens = $('#Childs').children('fieldset');
            var countChildren = childrens.length;
            for (var j = 0; j < countChildren; j++) {
                removeElement($('#Childs').children('fieldset:last').find('.remove-child-button'), 'Child');
            }
            $('#ChildsReference').addClass('hidden');
            $('#ChildLinksHidden').append($('#ChildLinks').children());
            $('#Placements').addClass('hidden');
            $('#PlacesLink').html('');
        }
        else {
            var $selector = $('select#statusApplicant');
            var savedVal = $selector.select2('val');
            $selector.empty();
            $selector.append(statuses);
            $selector.find('*[value=5]').remove();
            if (val.Id == 12 || val.Id == 23 || val.Id == 6) {
                if (savedVal != 1) {
                    $selector.find('*[value=1]').remove();
                }
                if (savedVal != 2) {
                    $selector.find('*[value=2]').remove();
                }
                if (inited) {
                    $selector.select2('val', 3);
                }
                else {
                    $selector.select2('val', savedVal);
                }
            }
            else {
                if (inited) {
                    $selector.select2('val', 1);
                }
                else {
                    $selector.select2('val', savedVal);
                }
            }
            if (!$('#mainPlaces').select2('val') || $('#mainPlaces').select2('val') == '0') {
                $('#mainPlaces').select2('val', 1);
            }
            $('#ChildsReference').removeClass('hidden');
            $('#ChildLinks').append($('#ChildLinksHidden').children());
            if (val.NeedPlacment || ($('#hasBooking').val() === 'False'
                && val.Id !== CompensationEnum
                && val.Id !== CompensationYouthRestEnum
                && val.Id !== 20 && !(containsInArray(certificateTypeOFRests, val.Id)))) {
                $('#Placements').removeClass('hidden');
                $('#PlacesLink').html('<a href="#Places">Размещение</a>');
                $('#PlacesLink').show();
                if (val.NeedPlacment) {
                    $('#PlacesAttendants').removeClass('hidden');
                    if (savedInited) {
                        $('#Data_CountAttendants').select2('val', "1");
                    }
                }
                else {
                    $('#PlacesAttendants').addClass('hidden');
                    $('#Data_CountAttendants').select2('val', null);
                    if (inited) {
                        if ($("select.is-accomp option[value='']").length !== 0) {
                            $('select.is-accomp').select2('val', null);
                        }
                        else {
                            $('select.is-accomp').select2('val', 'False');
                        }
                        if ($("#is-agent-accomp option[value='']").length !== 0) {
                            $('#is-agent-accomp').select2('val', null);
                        }
                        else {
                            $('#is-agent-accomp').select2('val', 'False');
                        }
                    }
                    $('#Placements').addClass('hidden');
                    $('#PlacesLink').html('');
                }
            }
            else {
                if (containsInArray(certificateTypeOFRests, val.Id)) {
                    $('#Placements').removeClass('hidden');
                    if (containsInArray(moneyAttendants, val.Id)) {
                        $("#PlacesAttendants").removeClass("hidden");
                    }
                    else {
                        $("#PlacesAttendants").addClass("hidden");
                    }
                }
                else {
                    $('#Placements').addClass('hidden');
                    $('#PlacesLink').html('');
                    $('#mainPlaces').select2('val', "1");
                }
                if (containsInArray(moneyAttendants, val.Id)) {
                    $('#NeedPlacmentApplicant').removeClass('hidden');
                }
                else {
                    $('#NeedPlacmentApplicant').addClass('hidden');
                }
                if (inited) {
                    if (containsInArray(moneyAttendants, val.Id)) {
                        $('#Data_CountAttendants').select2('val', "1");
                    }
                    else {
                        $('#Data_CountAttendants').select2('val', null);
                    }
                    if ($("select.is-accomp option[value='']").length !== 0) {
                        $('select.is-accomp').select2('val', null);
                    }
                    else {
                        $('select.is-accomp').select2('val', 'False');
                    }
                    if ($("#is-agent-accomp option[value='']").length !== 0) {
                        $('#is-agent-accomp').select2('val', null);
                    }
                    else {
                        $('#is-agent-accomp').select2('val', 'False');
                    }
                }
            }
            changeCountChildren();
            lockAttendantAddButton();
        }
        if (val.NeedPlacment) {
            if ($('#isFirstCompany').val() === 'True') {
                $('#NeedPlacmentApplicant').removeClass('hidden');
                $('#NeedPlacmentAgent').removeClass('hidden');
            }
        }
        else {
            if (containsInArray(moneyAttendants, val.Id)) {
                $('#NeedPlacmentApplicant').removeClass('hidden');
                $('#NeedPlacmentAgent').removeClass('hidden');
            }
            else {
                $('#NeedPlacmentApplicant').addClass('hidden');
                $('#NeedPlacmentAgent').addClass('hidden');
            }
            if (inited) {
                if ($("select.is-accomp option[value='']").length !== 0) {
                    $('select.is-accomp').select2('val', null);
                }
                else {
                    $('select.is-accomp').select2('val', 'False');
                }
                if ($("#is-agent-accomp option[value='']").length !== 0) {
                    $('#is-agent-accomp').select2('val', null);
                }
                else {
                    $('#is-agent-accomp').select2('val', 'False');
                }
            }
        }
        if (val.NeedAttendant && val.Id !== CompensationYouthRestEnum) {
            if ($('#isFirstCompany').val() === 'False') {
                $('.attendant-row').removeClass('hidden');
            }
            $('#AttendantsReference').removeClass('hidden');
            $('#Attendants').removeClass('hidden');
            $('#AttendantLinks').append($('#hiddenNav > li#AttendantLinks').children());
        }
        else {
            $('.attendant-row').addClass('hidden');
            $('#AttendantsReference').addClass('hidden');
            $('#Attendants').addClass('hidden');
            $('#hiddenNav > li#AttendantLinks').append($('#AttendantLinks').children());
        }
        $('[data-spy="scroll"]').each(function () {
            $(this).scrollspy('refresh');
        });
        if ($('#typeOfRest-select2').val() != 0) {
            $('.child-age-restriction').removeClass('hidden');
            $('.child-age-restriction-val').text('Допустимый возраст: ' + val.MinAge + '-' + val.MaxAge + ' лет');
        }
        else {
            $('.child-age-restriction').addClass('hidden');
        }
        var needBank = $('#Data_BankName').val() !== '';
        if (val.Id === CompensationYouthRestEnum || val.Id === CompensationEnum) {
            $('#TypeAndTime').html('Цель обращения');
            $('#TypeAndTimeLinkA').html('Цель обращения');
            $('#InformationVoucherLink').html('<a href="#InformationVoucher">Путевки</a>');
            $('#InformationVoucher').removeClass('hidden');
            $('#BankLink').html('<a href="#Bank">Банковские реквизиты</a>');
            $('#Bank').removeClass('hidden');
            $('.time-div').addClass('hidden');
            $('.RequestInformationVoucherChild').removeClass('hidden');
            if (val.Id === CompensationEnum) {
                $('#NeedPlacmentApplicant').removeClass('hidden');
                $('#AddChild').removeClass('hidden');
                $('.remove-child-button').removeClass('hidden');
            }
            else {
                $('#AddChild').addClass('hidden');
                $('.remove-child-button').addClass('hidden');
                $('#NeedPlacmentApplicant').addClass('hidden');
            }
        }
        else if (containsInArray(certificateTypeOFRests, val.Id)) {
            $('#TypeAndTime').html('Цель обращения');
            $('#TypeAndTimeLinkA').html('Цель обращения');
            $('#InformationVoucherLink').html('');
            if (needBank) {
                $('#BankLink').html('<a href="#Bank">Банковские реквизиты</a>');
                $('#Bank').removeClass('hidden');
            }
            else {
                $('#BankLink').html('');
                $('#Bank').addClass('hidden');
            }
            $('.RequestInformationVoucherChild').addClass('hidden');
            $('#InformationVoucher').addClass('hidden');
            $('.time-div').addClass('hidden');
            $('#AddChild').addClass('hidden');
            $('.remove-child-button').addClass('hidden');
            if (containsInArray(moneyAttendants, val.Id)) {
                $('#NeedPlacmentApplicant').removeClass('hidden');
                $('#NeedPlacmentAgent').removeClass('hidden');
            }
            else {
                $('#NeedPlacmentApplicant').addClass('hidden');
                $('#NeedPlacmentAgent').addClass('hidden');
            }
        }
        else {
            $('#TypeAndTime').html('Цель обращения и время отдыха');
            $('#TypeAndTimeLinkA').html('Цель обращения и время отдыха');
            $('#InformationVoucherLink').html('');
            if ($('#requestOnMoney').val() === 'True' && $('#isFirstCompany').val() === 'True' && needBank) {
                $('#BankLink').html('<a href="#Bank">Банковские реквизиты</a>');
                $('#Bank').removeClass('hidden');
            }
            else {
                $('#BankLink').html('');
                $('#Bank').addClass('hidden');
            }
            $('.time-div').removeClass('hidden');
            $('#InformationVoucher').addClass('hidden');
            $('.RequestInformationVoucherChild').addClass('hidden');
            if (val.NeedPlacment) {
                $('#NeedPlacmentApplicant').removeClass('hidden');
            }
            else {
                $('#NeedPlacmentApplicant').addClass('hidden');
            }
            $('#AddChild').addClass('hidden');
            $('.remove-child-button').addClass('hidden');
        }
    }
    $('.type-of-rest').change(function (event) {
        var target = event.target;
        changeTypeOfRest(target);
    });
    $('.priorityTypeOfTransport').change(function (event) {
        var target = event.target;
        changeAdditionalTransport(target);
    });
    $('#mainPlaces')
        .change(function () {
        changeCountChildren();
    });
    $('#Data_CountAttendants')
        .change(function () {
        lockAttendantAddButton();
    });
    $('#is-agent-accomp')
        .change(function () {
        lockAttendantAddButton();
    });
    $('.is-accomp').change(function () {
        var id = $('#ApplicantFieldset').find('.id').val();
        if ($('.is-accomp').select2('val') === 'True' && ((getPlacement($('.PlaceOfRestId').val())) || {}).IsForegin) {
            $('.applicant-foreign-document').removeClass('hidden');
        }
        else {
            $('.applicant-foreign-document').addClass('hidden');
        }
        if ($('.is-accomp').select2('val') === 'True') {
            var name_1 = '';
            $('#ApplicantFieldset').find('.name-input').each(function () {
                name_1 += $(this).val() + ' ';
            });
            $('.applicant-offer-in-request').removeClass('hidden');
        }
        else {
            $('.applicant-offer-in-request').addClass('hidden');
            $('#AddonServicesTable tbody').find('tr:has(.service-link-applicant-id[value="' + id + '"])').remove();
        }
        lockAttendantAddButton();
    });
    $('select.beneficiariesId').change(function (e) {
        var val = $(e.target).val();
        if (val === '3') {
            $('#AttendantInvalid').removeClass('hidden');
        }
        else {
            $('#AttendantInvalid').addClass('hidden');
        }
    });
    $(document).on('change', 'select.benefit-type-dropdown', function (event) {
        var val = getBenefitType($(event.target).val());
        if (inited) {
            $('.benefit-type-id').val(val.Id);
        }
        var parentIvalid = false;
        $('select.benefit-type-dropdown').each(function (i, e) {
            var s = $(e).select2('val');
            if (s == 9 || s == 31) {
                parentIvalid = true;
            }
        });
        if (parentIvalid) {
            $('#parentIvalidLi').removeClass('hidden');
            $('#parentIvalid').removeClass('hidden');
            $('[data-spy="scroll"]').each(function () {
                $(this).scrollspy('refresh');
            });
        }
        else {
            $('#parentIvalidLi').addClass('hidden');
            $('#parentIvalid').addClass('hidden');
            $('[data-spy="scroll"]').each(function () {
                $(this).scrollspy('refresh');
            });
            $('select.beneficiariesId').select2('val', -1);
            $('#AttendantInvalid').empty();
        }
        if (val.NeedApproveDocument) {
            $(event.target).closest('.request-block').find('.benefit').removeClass('hidden');
        }
        else {
            $(event.target).closest('.request-block').find('.benefit').addClass('hidden');
        }
        if (val.NeedTypeOfRestriction) {
            $(event.target).closest('.request-block').find('.child-is-invalid-hidden').val('true');
            $(event.target).closest('.request-block').find('.child-is-invalid').attr('disabled', 'disabled');
            $(event.target).closest('.request-block').find('.child-is-invalid').prop('checked', true);
            $(event.target).closest('.request-block').find('.child-is-invalid').trigger('change');
        }
        else {
            $(event.target).closest('.request-block').find('.child-is-invalid').removeAttr('disabled');
        }
        var fieldset = $(event.target).closest('fieldset');
        if (val.Id != 0 && val.Id != 1 && val.Id != 12) {
            fieldset.find('.registered-in-Moscow').prop('checked', true);
            fieldset.find('.registered-in-Moscow').attr('disabled', 'disabled');
            fieldset.find('.registered-in-Moscow-hidden').val('true');
            fieldset.find('.registered-in-Moscow').trigger('change');
        }
        else {
            fieldset.find('.registered-in-Moscow').removeAttr('disabled');
            fieldset.find('.registered-in-Moscow').trigger('change');
        }
    });
    $(document).on('change', '.child-is-invalid', function (event) {
        var $target = $(event.target);
        var $e = $(event.target).closest('.request-block');
        var $restriction = $e.find('.restriction-select');
        var $subres = $e.find('.subrestriction-select');
        var $div = $e.find('.type-of-subrestriction');
        if ($target.is(':checked')) {
            $e.find('.type-of-restriction, .benefit-group-invalid').removeClass('hidden');
        }
        else {
            $e.find('.type-of-restriction, .benefit-group-invalid').addClass('hidden');
            $div.addClass('hidden');
        }
    });
    $('#statusApplicant').change(function (e) {
        var val = $('#statusApplicant').select2('val');
        $('#AgentApplicant').prop('checked', val === '4');
        changeAgent();
        lockAttendantAddButton();
    });
    $(document).on('change', '#AgentApplicant', function (event) {
        changeAgent();
    });
    $(document).on('change', '.benefit-never-end', function (event) {
        if ($(event.target).is(':checked')) {
            $('.benefit-end-date').closest('.row').addClass('hidden');
        }
        else {
            $('.benefit-end-date').closest('.row').removeClass('hidden');
        }
    });
    $(document).on('change', '.school-not-present', function (event) {
        var prefix = $(event.target).attr('name').match(/^(.*)SchoolNotPresent/)[1];
        if ($(event.target).is(':checked')) {
            $('[name="' + prefix + 'SchoolId"').closest('.row').addClass('hidden');
        }
        else {
            $('[name="' + prefix + 'SchoolId"').closest('.row').removeClass('hidden');
        }
    });
    $(document).on('change', '.registered-in-Moscow', function (event) {
        if ($(event.target).is(':checked')) {
            $(event.target).closest('.request-block').find('.address-control').removeClass('hidden');
        }
        else {
            $(event.target).closest('.request-block').find('.address-control').addClass('hidden');
        }
    });
    configDropdowns();
    $('select.priority-placement').find('option').each(function (i, el) {
        var val = getPlacement($(el).val());
        if (val.Id == 0)
            $(el).attr('data-sea-zone', '0');
        else
            $(el).attr('data-sea-zone', val.ZoneOfSea);
        $(el).attr('data-id', val.Id);
    });
    var historyAjax;
    $('.applicant-firstname, .applicant-lastname, .applicant-middlename,.agent-firstname, .agent-lastname, .agent-middlename, .attendant-firstname, .attendant-lastname, .attendant-middlename')
        .on('input', function (e) {
        var guid = $(e.target).closest('.request-block').find('.guid').val();
        $('input.child-attendant-fio[value="' + guid + '"]').select2("data", { id: guid, text: getFio(guid) });
    });
    $('.comment-button').click(function () {
        $('#CommentModal').modal();
    });
    $('.edit-save-button').click(function () {
        $('#Data_InternalCommentary').val($('#CommentModalValue').val());
        $('#CommentModal').modal('toggle');
    });
    $('.copy-request').click(function () {
        $('#copyRequestModal').modal('toggle');
    });
    $('.history-button').click(function () {
        $('#HistoryModalError').addClass('hidden');
        $('#HistoryModalTable').addClass('hidden');
        $('#HistoryModalLoading').removeClass('hidden');
        $('#HistoryModal').modal();
        if (historyAjax) {
            historyAjax.abort();
        }
        historyAjax = $.ajax({
            type: 'GET',
            url: '/api/WebFirstRequestCompany/LoadRequestHistory/' + $('#RequestId').val(),
            success: function (result) {
                $('#HistoryModalLoading').addClass('hidden');
                $('#HistoryModalTable').removeClass('hidden');
                var template = doT.template($('#historyTableTemplate').html());
                $('#HistoryModalTable').find('tbody').html(template(result));
            },
            error: function () {
                $('#HistoryModalLoading').addClass('hidden');
                $('#HistoryModalError').addClass('hidden');
            }
        });
    });
    function documentInputType($target) {
        var self = $target;
        var $item = self.find("option:selected");
        var text = $item.text();
        var $fieldset = self.closest('fieldset');
        if (text === 'Паспорт гражданина РФ' || text === 'Паспорт РФ') {
            $fieldset.find('.input-mask-passport-series').inputmask('9999', {
                placeholder: "сссс",
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-passport-number').inputmask('999999', {
                placeholder: "нннннн",
                clearIncomplete: true
            });
        }
        else if (text === 'Свидетельство о рождении') {
            $fieldset.find('.input-mask-passport-series').inputmask('Regex', {
                regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]',
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-passport-number').inputmask('999999', { clearIncomplete: true });
        }
        else {
            $fieldset.find('.input-mask-passport-series').inputmask('Regex', { regex: '.*' });
            $fieldset.find('.input-mask-passport-number').inputmask('Regex', { regex: '.*' });
        }
        if ($item.val() !== '22' && $item.val() !== '23' && $('#isFirstCompany').val() === 'False') {
            $fieldset.find('.cert-birth-block').removeClass('hidden');
        }
        else {
            $fieldset.find('.cert-birth-block').addClass('hidden');
            $fieldset.find('.cert-birth-block>div>select').select2('val', 0);
            $fieldset.find('.cert-birth-block>div>input').val('');
        }
    }
    function documentCertInputType($target) {
        var self = $target;
        var $item = self.find("option:selected");
        var text = $item.text();
        var $fieldset = self.closest('fieldset');
        if (text === 'Паспорт гражданина РФ' || text === 'Паспорт РФ') {
            $fieldset.find('.input-mask-cert-series').inputmask('9999', { placeholder: "сссс", clearIncomplete: true });
            $fieldset.find('.input-mask-cert-number').inputmask('999999', {
                placeholder: "нннннн",
                clearIncomplete: true
            });
        }
        else if (text === 'Свидетельство о рождении') {
            $fieldset.find('.input-mask-cert-series').inputmask('Regex', {
                regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]',
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-cert-number').inputmask('999999', { clearIncomplete: true });
        }
        else {
            $fieldset.find('.input-mask-cert-series').inputmask('Regex', { regex: '.*' });
            $fieldset.find('.input-mask-cert-number').inputmask('Regex', { regex: '.*' });
        }
    }
    $(document).on('change', 'select.document-dropdown', function (e) {
        documentInputType($(e.target));
    });
    $(document).on('change', 'select.document-cert-dropdown', function (e) {
        documentCertInputType($(e.target));
    });
    $('select.document-dropdown').each(function (num, val) {
        documentInputType($(val));
    });
    $('select.type-of-rest')
        .add('.placeOfRestId')
        .add('select.transferFrom')
        .add('select.transferTo')
        .on('change', ToggleTypeOfTransportBlock);
    $('select.type-of-rest').on('change', ToggleTypeOfCampBlock);
    var $typeOfCamp = $("select.typeOfCamp");
    var $typeOfCampAddon = $("select.typeOfCampAddon");
    $typeOfCamp.on("change", function () {
        if ($(this).select2('val') == isCamping) {
            $typeOfCampAddon.select2().find("option[value=" + isCamping + "]").remove();
            $typeOfCampAddon.select2("val", "-1");
        }
        else {
            if ($typeOfCampAddon.find("option[value=" + isCamping + "]").length == 0) {
                var optionToAdd = typeOfCampOptions.filter(function (obj) { return obj.id == isCamping; })[0];
                if (optionToAdd) {
                    $typeOfCampAddon.append(new Option(optionToAdd.text, optionToAdd.id, false, false));
                }
                $typeOfCampAddon.select2("val", "-1");
            }
        }
    });
    $('body').on('change', '.middlename-havenot', function (e) {
        var isChecked = $(e.target).is(':checked');
        var middlename = $(e.target).closest('fieldset').find('.middlename');
        if (isChecked) {
            middlename.val(null);
            middlename.attr('disabled', 'disabled');
        }
        else {
            middlename.removeAttr('disabled');
        }
    });
    var isExcludeChildConfirmed = false;
    $('#mainForm').submit(function () {
        if ($('input.exclude-attendant:not(:checked)').length === 0
            && $('input.exclude-attendant').length > 0
            && $('#Applicant_Data_IsAccomp').val().toLowerCase() == 'false') {
            ShowAlert('Нельзя исключать всех сопровождающих', "alert-danger", "glyphicon-ok", true);
            return false;
        }
        if ($('input.exclude-child:not(:checked)').length === 0 && $('input.exclude-child').length > 0) {
            ShowAlert('Нельзя исключать всех детей', "alert-danger", "glyphicon-ok", true);
            return false;
        }
        var excludedChilds = $('.exclude-from-request, .exclude-from-request-a').filter(function (i, val) {
            var hidden = $(val).closest('.checkbox').find('.exclude-from-request-hidden, .exclude-from-request-hidden-a');
            if (hidden.val() !== 'True' && $(val).is(':checked')) {
                return true;
            }
            return false;
        }).map(function (i, val) {
            var block = $(val).closest('.request-block');
            var lastname = block.find('.info-lastname').val();
            var firstname = block.find('.info-firstname').val();
            var middlename = block.find('.info-middlename').val();
            var havenotMiddlename = block.find('.middlename-havenot').is(':checked');
            return '<li>' + lastname + ' ' + firstname + (havenotMiddlename ? '' : ' ' + middlename) + '</li>';
        });
        if (isExcludeChildConfirmed || excludedChilds.length === 0) {
            isExcludeChildConfirmed = false;
            return true;
        }
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Перечисленные отдыхающие будут исключены из заявления: <ol>' + excludedChilds.get().join('') + '</ol>',
            buttons: [
                {
                    label: 'Исключить',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        isExcludeChildConfirmed = true;
                        $('#mainForm').submit();
                        dialogItself.close();
                    }
                }, {
                    label: 'Отмена',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
        return false;
    });
    $('.exclude-from-request').change(function (e) {
        var block = $(e.target).closest('.request-block');
        var disabled = $(e.target).is(':checked');
        block.find('input, select').not('.exclude-from-request, .id, .disabled, input[type=hidden]').prop('disabled', disabled);
    });
    changeTypeOfRest($('select.type-of-rest'));
    inited = true;
    var $certificateCreateLink = $('#certificateCreateLink');
    $certificateCreateLink.on('click', function (e) {
        e.preventDefault();
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите создать сертификаты на выплату?',
            buttons: [
                {
                    label: "Создать сертификаты",
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        window.location.replace($certificateCreateLink.attr('href'));
                        dialogItself.close();
                    }
                }, {
                    label: 'Отмена',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
    });
    var $requestCreateLink = $('#requestCreateLink');
    $requestCreateLink.on('click', function (e) {
        e.preventDefault();
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите создать заявление на следующий год?',
            buttons: [
                {
                    label: "Создать заявление",
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        window.location.replace($requestCreateLink.attr('href'));
                        dialogItself.close();
                    }
                }, {
                    label: 'Отмена',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
    });
    $(".ApplicantAttendantChange").click(function () {
        var $item = $('#AttendantModal .ReplacingAccompanyBody');
        $(".ReplacingAccompanyErr").html('');
        AddAttendantPopup($item);
        $('#AttendantModal').modal();
    });
    $(".AttendantChange").click(function (event) {
        var $item = $('#AttendantModal .ReplacingAccompanyBody');
        var aVal = $(event.target).closest("fieldset").find(".id").val();
        $(".ReplacingAccompanyErr").html('');
        AddAttendantPopup($item);
        $('#ReplacingAccompanyId').val(aVal);
        $('#AttendantModal').modal();
    });
    $(".attendantmodalsave").click(function () {
        $(".attendantmodalsave").prop("disabled", true);
        var $item = $('#AttendantModal #AttendantModalForm');
        var data = getFormData($item);
        data['HasNotMiddlename'] = $($item.find("input[name='HasNotMiddlename']")).is(':checked');
        //$("input[name='HasNotMiddlename']").is(':checked')
        $.ajax({
            method: "POST",
            url: attendantModalSaveUrl + '?RequestId=' + $("#Data_Id").val() + '&ReplacingAccompanyId=' + $("#ReplacingAccompanyId").val(),
            data: data
        })
            .done(function (msg) {
            if (msg.IsError) {
                $(".ReplacingAccompanyErr").html(msg.ErrorText);
                $(".attendantmodalsave").prop("disabled", false);
            }
            else {
                location.reload();
            }
        }).fail(function (msg) {
            $(".ReplacingAccompanyErr").html("Ошибка при выполнении запроса");
            $(".attendantmodalsave").prop("disabled", false);
        });
    });
});
function getFormData($form) {
    var unindexedArray = $form.serializeArray();
    var indexed_array = {};
    $.map(unindexedArray, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });
    return indexed_array;
}
function AddAttendantPopup($item) {
    $($item).html('');
    var model = $(attendantFn({}));
    $($item).append(model);
    $($item.find(".remove-attendant-button")).closest(".row").remove();
    $($item.find(".exclude-attendant")).closest(".row").remove();
    $($item.find("fieldset")).removeClass("bs-callout-info bs-callout");
    $($item.find(".type-violation")).closest(".row").remove();
    $($item.find(".AttendantChange")).closest(".row").remove();
    //applicantType
    $('#ReplacingAccompanyId').val('');
    var regExp = new RegExp("(.*\[[0-9]+\].)", "g");
    $($item.find("*")).each(function () {
        if ($(this).attr('name')) {
            $(this).attr('name', $(this).attr('name').replace(regExp, ''));
        }
    });
    try {
        $('.datepicker-my').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date() });
    }
    catch (e) {
    }
    try {
        $('.datepicker-future').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', minDate: new Date() });
    }
    catch (e) {
    }
    try {
        $('.datepicker-general').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    }
    catch (e) {
    }
    inputMaskConfig($item);
    $($item.find(".select2")).select2();
    $item.on('change', 'select.applicantType', function (e) {
        var $e = $(e.target);
        var $cbx = $e.closest('.form-group').find('input.is-proxy');
        $cbx.prop('checked', $e.select2('val') === '3');
        attendantChangeProxy($cbx);
    });
}
function changeIndexInNames(parent, name) {
    var childs = parent.children();
    var regExp = new RegExp("(.*\[[0-9]+\])", "g");
    var _loop_2 = function (i) {
        $(childs[i]).find('*').each(function () {
            if ($(this).attr('name')) {
                $(this).attr('name', $(this).attr('name').replace(regExp, name + '[' + i + ']'));
            }
        });
        $(childs[i]).find('.name-ref').attr("id", name + i);
        $(childs[i]).find('.name-title').attr("id", name + 'Title' + i);
    };
    for (var i = 0; i < childs.length; i++) {
        _loop_2(i);
    }
}
function changeIndexInNamesVoucher(parent, name) {
    var childs = parent.children();
    var regExp = new RegExp(name + "\[[0-9]+\].", "g");
    var _loop_3 = function (i) {
        $(childs[i]).find('*').each(function () {
            if ($(this).attr('name')) {
                $(this).attr('name', $(this).attr('name').replace(regExp, name + '[' + (i).toString() + '].'));
            }
        });
        $(childs[i]).find('.name-ref').attr("id", name + i);
        $(childs[i]).find('.name-title').attr("id", name + 'Title' + i);
    };
    for (var i = 0; i < childs.length; i++) {
        _loop_3(i);
    }
}
function changeGuids(parent, name) {
    var childs = parent.children();
    var _loop_4 = function (i) {
        var guidInputs = $(childs[i]).find('[name$=".Guid"]');
        guidInputs.each(function (j) {
            var guid = newGuid();
            $(guidInputs[j]).val(guid);
        });
    };
    for (var i = 0; i < childs.length; i++) {
        _loop_4(i);
    }
}
function confirmButton(buttonName, actionCode, statusId) {
    if (statusId === void 0) { statusId = undefined; }
    if (statusId == '1050') {
        var res_1 = true;
        $('.approve-checkboxs').find('input[type=checkbox]').each(function (i, e) {
            res_1 = res_1 && $(e).prop('checked');
        });
        if (!res_1) {
            ShowAlert('Для регистрации заявления необходимо подтвердить согласие', "alert-danger", "glyphicon-ok", true);
            return;
        }
    }
    BootstrapDialog.show({
        title: 'Подтвердить действие',
        message: 'Вы действительно хотите ' + buttonName.toLowerCase() + '?',
        buttons: [
            {
                label: buttonName,
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    $('#action').val(actionCode);
                    $('#mainForm').submit();
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }
        ]
    });
}
function formatDecline(state) {
    return state.text;
}
function confirmButtonWithDecline(buttonName, actionCode, statusId) {
    var $content;
    BootstrapDialog.show({
        title: 'Подтвердить действие',
        message: function () {
            var fn = doT.template($("#declineReasons" + statusId).html());
            $content = $(fn({ name: "Вы действительно хотите " + buttonName.toLowerCase() + '?' }));
            $content.find('.declineReasonsSelect').select2({
                width: 'copy',
                formatResult: formatDecline,
                formatSelection: formatDecline
            });
            return $content;
        },
        buttons: [
            {
                label: buttonName,
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    $('#action').val(actionCode);
                    var declineReason = dialogItself.$modalContent.find('select.declineReasonsSelect').val();
                    if (declineReason != '0') {
                        $('#Data_DeclineReasonId').val(declineReason);
                        dialogItself.$modalContent.find('.declineReasonsSelectMessage').hide();
                    }
                    else {
                        dialogItself.$modalContent.find('.declineReasonsSelectMessage').show();
                        return;
                    }
                    $('#mainForm').submit();
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }
        ]
    });
}
var arr;
$('.copy-request').on('click', function (e) {
    arr = [];
    var $copyOptions = $("#copyOptions");
    $copyOptions.empty();
    //$('#copyRequestModal').modal('show');
    var nodes = $("#scrollspy").html();
    $.each($(nodes).find("li").not(".hidden").find("a").not("li ul li a"), function (i, elem) {
        var ob = {};
        ob["parent"] = elem.getAttribute("href");
        ob["children"] = [];
        if (ob["parent"] == "#ChildsReference") {
            $.each($(".childToCopy"), function (i, val) {
                ob["children"].push(val);
            });
        }
        if (ob["parent"] == "#AttendantsReference") {
            $.each($(".attendantToCopy"), function (i, val) {
                ob["children"].push(val);
            });
        }
        arr.push(ob);
    });
    $copyOptions.append("<ul id='mainList'></ul>");
    $.each(arr, function (i, val) {
        var key = val["parent"];
        var children;
        children = val["children"].length > 0 ? val["children"] : "";
        var cond = false;
        var childrenHtml = "";
        if (children.length > 0) {
            cond = true;
            childrenHtml += "<ul>";
            $.each(children, function (i, val) {
                childrenHtml +=
                    "<li><div class='checkbox'><label>" +
                        "<input id=" + (val.getAttribute("Id")) + " class='listItem' onclick='CheckHandler(event, 0)' type='checkbox' value>" +
                        "<input id=" + (val.getAttribute("Id")) + " class='listItem' type='hidden' value='false'>" +
                        $(val).val() + "</label></div></li>";
            });
            childrenHtml += "</ul>";
        }
        var html;
        if (mapperDict[key].blocked != null) {
            html =
                "<li><div class='checkbox'><label name=" + key + ">" +
                    "<input disabled name=" + mapperDict[key].prop + " type='checkbox' value>" +
                    "<input disabled name=" + mapperDict[key].prop + " type='hidden' value='false'>" +
                    mapperDict[key].text +
                    (cond ? childrenHtml : "") +
                    "</label></div></li>";
        }
        else {
            html =
                "<li><div class='checkbox'><label name=" + key + ">" +
                    "<input name=" + mapperDict[key].prop + " onclick='CheckHandler(event, 1)' type='checkbox' value>" +
                    "<input name=" + mapperDict[key].prop + " type='hidden' value='false'>" +
                    mapperDict[key].text +
                    (cond ? childrenHtml : "") +
                    "</label></div></li>";
        }
        $("#mainList").append(html);
    });
});
$("#submitRequestCopy").on('click', function (e) {
    var data = {};
    data["RequestId"] = $(".requestToCopy").attr("Id");
    // alert("Sending...");
    $.each(arr, function (i, val) {
        var dictVal = mapperDict[val.parent];
        if (dictVal != undefined) {
            var prop = dictVal.prop;
            if (prop != undefined)
                data[prop] = $("#mainList").find("input[name=" + dictVal.prop + "]").prop('checked');
            if (dictVal.list != undefined) {
                var listName = dictVal.list;
                if (listName == "AttendantsIds") {
                    data[listName] = [];
                    $.each($("#mainList").find("label[name='#AttendantsReference']").find(".listItem:checked"), function (i, val) {
                        data[listName].push($(val).attr("Id"));
                    });
                }
                if (listName == "ChildrenIds") {
                    data[listName] = [];
                    $.each($("#mainList").find("label[name='#ChildsReference']").find(".listItem:checked"), function (i, val) {
                        data[listName].push($(val).attr("Id"));
                    });
                }
            }
        }
    });
    $.ajax({
        url: rootPath + 'FirstRequestCompany/CopyRequest',
        type: 'POST',
        data: JSON.stringify(data),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (res) {
            window.location.href = res.newUrl;
        }
    });
});
function CheckHandler(event, isParentNode) {
    var src = event.target;
    var cond = $(src).prop("checked");
    if (isParentNode) {
        if (cond) {
            $.each($(src).siblings("ul").find("[type='checkbox']"), function (i, val) {
                $(val).prop("checked", true);
            });
        }
        else {
            $.each($(src).siblings("ul").find("[type='checkbox']"), function (i, val) {
                $(val).prop("checked", false);
            });
        }
    }
    else {
        var total = $(src).parents("ul").first().find("[type='checkbox']").length;
        var checked = $(src).parents("ul").first().find("[type='checkbox']:checked").length;
        if (checked == total) {
            $(src).parents("ul").first().siblings("input[type='checkbox']").prop("checked", true);
        }
        else {
            $(src).parents("ul").first().siblings("input[type='checkbox']").prop("checked", false);
        }
    }
}
function SelectAll() {
    var cond = $("[name='selectAll']").prop("checked");
    if (cond) {
        $.each($("#copyRequestModal").find("[type='checkbox']").not(":disabled"), function (i, val) {
            $(val).prop("checked", true);
        });
    }
    else {
        $.each($("#copyRequestModal").find("[type='checkbox']").not(":disabled"), function (i, val) {
            $(val).prop("checked", false);
        });
    }
}
var mapperDict = {
    "#GeneralInfo": { text: "Общие сведения", prop: "TransferGeneralData" },
    "#TypeAndTime": { text: "Цель обращения и время отдыха", prop: "TransferTargetAndTimeOfRestData" },
    "#PlaceRest": { text: "Место отдыха", blocked: "true" },
    "#Places": { text: "Размещение", blocked: "true" },
    "#Applicant": { text: "Сведения о заявителе", prop: "TransferApplicantData" },
    "#Agent": { text: "Сведения о представителе заявителя", prop: "TransferAgentData" },
    "#AttendantsReference": { text: "Сведения о сопровождающих", prop: "TransferAttendantData", list: "AttendantsIds" },
    "#InformationVoucher": { text: "Путевки", blocked: "true" },
    "#ChildsReference": { text: "Сведения о детях", prop: "TransferChildData", list: "ChildrenIds" },
    "#parentIvalid": { text: "Сведения о родителе-инвалиде", blocked: "true" },
    "#Bank": { text: "Банковские реквизиты", prop: "TransferBankData" },
    "#ChildrenRequests": { text: "Сведения о созданных заявлениях", blocked: "true" },
    "#ParentRequest": { text: "Сведения о заявлении на основе которого выдано текущее", blocked: "true" },
    "#RequestCertificates": { text: "Сведения о погашенном сертификате", blocked: "true" },
    "#FileReference": { text: "Документы", prop: "TransferFilesData" },
    "#TypeOfTransport": { text: "Тип транспорта", prop: "TransferTypeOfTransportData", blocked: "true" },
    "#TypeOfCamp": { text: "Тип лагеря", prop: "TransferTypeOfCampData" },
};
//# sourceMappingURL=RequestEdit.js.map