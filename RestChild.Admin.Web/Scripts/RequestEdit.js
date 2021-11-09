var inited = false;
var attendantFn = doT.template($('#attendantTemplate').html());
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
    if (!typeOfRest || typeOfRest === '-2') {
        if (typeOfRest === '-2') {
            $('#AddAttendant').removeClass('hidden');
        }
        return;
    }
    var pb = $('#agentProxyBlock');
    if ($('#is-agent-accomp').select2('val') === 'True') {
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
        message: 'Вы действительно хотите удалить сопровождающего из путевки?',
        buttons: [
            {
                label: 'Удалить',
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    elementToRemove.remove();
                    changeIndexInNamesVoucher($p.find('table tbody'), 'AttendantsPrice');
                    if ($tbody.children().length === 0) {
                        $tbody.append($('<tr class="emptyRow"><td colspan="5"><i>Нет сопровождающих</i></td></tr>'));
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
    ;
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
    function attendantChangeProxy($e) {
        var pb = $e.closest('fieldset.attendant-panel').find('.proxy-block');
        if ($e.prop('checked')) {
            pb.removeClass('hidden');
        }
        else {
            pb.addClass('hidden');
            pb.find('input').val('');
        }
    }
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
                    $('#is-agent-accomp').select2('val', 'False');
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
        function contains(a, obj) {
            for (var i = 0; i < a.length; i++) {
                if (a[i] == obj) {
                    return true;
                }
            }
            return false;
        }
        $restriction.on('change', function () {
            var val = $restriction.select2('val');
            $subres.select2('data', { id: '', text: '-- Не выбрано --' });
            if (contains(typeOfRestrictionSubs, val)) {
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
        if (typeOfRest.Id == -2) {
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
        if (!typeOfRest || typeOfRest === '-2') {
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
    function changeTypeOfRest(target) {
        var savedInited = inited;
        var val = getTypeOfRest($(target).select2('val'));
        if (!val) {
            return;
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
            $selector.empty();
            $selector.append(statuses);
            $selector.find('*[value=5]').remove();
            if (!$('#mainPlaces').select2('val') || $('#mainPlaces').select2('val') == '0') {
                $('#mainPlaces').select2('val', 1);
            }
            $('#ChildsReference').removeClass('hidden');
            $('#ChildLinks').append($('#ChildLinksHidden').children());
            if (val.NeedPlacment || ($('#hasBooking').val() === 'False' && val.Id !== -2 && val.Id !== 17 && val.Id !== 18)) {
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
                $('#Placements').addClass('hidden');
                $('#PlacesLink').html('');
                $('#mainPlaces').select2('val', "1");
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
            $('#NeedPlacmentApplicant').addClass('hidden');
            $('#NeedPlacmentAgent').addClass('hidden');
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
        if (val.NeedAttendant) {
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
        if (val.Id === -2) {
            $('#TypeAndTime').html('Цель обращения');
            $('#TypeAndTimeLinkA').html('Цель обращения');
            $('#InformationVoucherLink').html('<a href="#InformationVoucher">Путевки</a>');
            $('#InformationVoucher').removeClass('hidden');
            $('#BankLink').html('<a href="#Bank">Банковские реквизиты</a>');
            $('#Bank').removeClass('hidden');
            $('.time-div').addClass('hidden');
            $('.RequestInformationVoucherChild').removeClass('hidden');
            $('#NeedPlacmentApplicant').removeClass('hidden');
            $('#AddChild').removeClass('hidden');
            $('.remove-child-button').removeClass('hidden');
        }
        else if (val.Id === 17 || val.Id === 18 || val.Id === 21) {
            $('#TypeAndTime').html('Цель обращения');
            $('#TypeAndTimeLinkA').html('Цель обращения');
            $('#InformationVoucherLink').html('');
            $('#BankLink').html('<a href="#Bank">Банковские реквизиты</a>');
            $('#Bank').removeClass('hidden');
            $('#InformationVoucher').addClass('hidden');
            $('.time-div').addClass('hidden');
            $('#AddChild').addClass('hidden');
            $('.remove-child-button').addClass('hidden');
            $('#NeedPlacmentApplicant').addClass('hidden');
            $('.RequestInformationVoucherChild').addClass('hidden');
        }
        else {
            $('#TypeAndTime').html('Цель обращения и время отдыха');
            $('#TypeAndTimeLinkA').html('Цель обращения и время отдыха');
            $('#InformationVoucherLink').html('');
            if ($('#requestOnMoney').val() === 'True' && $('#isFirstCompany').val() === 'True') {
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
        }
    });
    $(document).on('change', '.child-is-invalid', function (event) {
        var $target = $(event.target);
        var $e = $(event.target).closest('.request-block');
        var $restriction = $e.find('.restriction-select');
        var $subres = $e.find('.subrestriction-select');
        var $div = $e.find('.type-of-subrestriction');
        if (inited) {
            $restriction.select2('data', { id: '', text: '-- Не выбрано--' });
            $subres.select2('data', { id: '', text: '-- Не выбрано--' });
        }
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
            $fieldset.find('.input-mask-passport-series').inputmask('9999', { placeholder: "сссс", clearIncomplete: true });
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
            $fieldset.find('.input-mask-cert-number').inputmask('999999', { placeholder: "нннннн", clearIncomplete: true });
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
        var excludedChilds = $('.exclude-from-request, .exclude-from-request-a').filter(function (i, val) {
            var hidden = $(val).closest('.checkbox').find('.exclude-from-request-hidden, .exclude-from-request-hidden-a');
            if (hidden.val() !== 'True' && $(val).is(':checked')) {
                return true;
            }
            return false;
        }).map(function (i, val) {
            var block = $(val).closest('.request-block');
            var lastname = block.find('.child-info-lastname').val();
            var firstname = block.find('.child-info-firstname').val();
            var middlename = block.find('.child-info-middlename').val();
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
        block.find('input, select').not('.exclude-from-request, .id, .disabled').prop('disabled', disabled);
    });
    changeTypeOfRest($('select.type-of-rest'));
    inited = true;
});
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
//# sourceMappingURL=RequestEdit.js.map