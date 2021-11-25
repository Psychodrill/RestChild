declare var placeOfRest;
declare var typeOfRest;
declare var benefitType;
declare var typeOfRestrictionSubs;
declare var BootstrapDialog: IBootstrapDialog;

declare var attendantModalSaveUrl;
declare var moneyAttendants;
declare var certificateTypeOFRests;

declare var typeOfTransferRequiringTransportSelection;
declare var typesOfRestRequiringTransportSelection;
declare var placesOfRestRequiringTransportSelection;

declare var typeOfRestRequiringCampTypeSelection;
declare var isCamping;
var typeOfCampOptions=[];
var typeOfTransportOptions = [];
let inited = false;
let attendantFn = doT.template($('#attendantTemplate').html());

const CompensationEnum = -2;
const CompensationYouthRestEnum = 19;

function containsInArray(a, obj) {
    for (let i = 0; i < a.length; i++) {
        if (a[i] === obj) {
            return true;
        }
    }
    return false;
}

function refreshLinkOnChilds(name) {
    $('#' + name + 'Link').find('a').each(function (index) {
        let el = $(this);
        el.attr('href', '#' + name + index);
        el.attr('id', name + 'Ref' + index);
    });

    $('.remove-btn').click((e) => {
        let target = $(e.target);
        let parent = target.parent().parent();
        parent.find('.remove-btn').addClass("hidden");
        parent.find('.fileinput-button').removeClass('hidden');
        parent.find('.file-span').removeClass('hidden');
        parent.find('.DocumentFileTitle').val('');
        parent.find('.DocumentFileUrl').val('');
        let href = parent.find('.href-file');
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
    let panel = $('input.guid[value="' + guid + '"]').closest('.request-block');
    let firstName = panel.find('[name$=".FirstName"]').val();
    let lastName = panel.find('[name$=".LastName"]').val();
    let middleName = panel.find('[name$=".MiddleName"]').val();
    return lastName + ' ' + firstName + ' ' + middleName;
}

function getVoucherInfo(guid) {
    let panel = $('#InformationVoucherDataRows input.id[value="' + guid + '"]').closest('.request-block');
    let organizationName = panel.find('.organizationName').val();
    let dateFrom = panel.find('.informationVouchers-dateFrom').val();
    return organizationName + ' - ' + dateFrom;
}

function bindAutoFill(self, name) {
    $(self).find('.name-input').keyup(function () {
        let id = $(this).closest('.request-block').find('.id').val();
        let guid = $(this).closest('.request-block').find('.guid').val();
        let regExp = new RegExp("([0-9]+)", "g");
        let string = '';
        $(self).find('.name-input').each(function () {
            string += $(this).val() + ' ';
        });


        if (name !== 'Applicant') {
            $('#' + name + 'Ref' + this.name.match(regExp)[0]).text(string);
            $('#' + name + 'Title' + this.name.match(regExp)[0]).text(string);
            $('input.child-attendant-fio[value="' + guid + '"]').select2("data", {id: guid, text: getFio(guid)});
        }

        if (name === 'Applicant' || name === 'Attendant') {
            $('.service-link-applicant-id[value="' + id + '"]').each((i, val) => {
                $(val).closest('tr').find('.addon-service-camper-name').html(string);
            });
        } else if (name === 'Child') {
            $('.service-link-applicant-id[value="' + id + '"]').each((i, val) => {
                $(val).closest('tr').find('.addon-service-camper-name').html(string);
            });
        }
    });
}

function addElement(name, templateFn) {
    let model = $(templateFn({}));
    let $item = $('#' + name + 's');
    $item.append(model);
    let link = $('<li><a href="#' + name + '" id="' + name + 'Ref">-</a></li>');
    $('#' + name + 'Link').append(link);
    bindAutoFill(model, name);
    refreshLinkOnChilds(name);
    $(document).trigger("AddNewChild", model);
    changeGuids($item, name);
    try {
        $('.datepicker-my').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date()});

    } catch (e) {
    }

    try {
        $('.datepicker-future').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY', minDate: new Date()});
    } catch (e) {
    }

    try {
        $('.datepicker-general').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY'});
    } catch (e) {
    }

    inputMaskConfig($item);
    return model;
}

function removeElement(self, name) {
    try {
        let elementToRemove = $(self).parent().parent().parent();
        let regExp = new RegExp("([0-9]+)", "g");
        $('#' + name + 'Ref' + elementToRemove.find('.name-input').attr('name').match(regExp)[0]).remove();
        elementToRemove.remove();
    } catch (e) {
    }
    refreshLinkOnChilds(name);
}

function removeElementWithConfirmation(self, name, onOk?, onCancel?) {
    BootstrapDialog.show({
        title: 'Удаление',
        message: 'Вы действительно хотите удалить запись?',
        buttons: [
            {
                label: 'Удалить',
                cssClass: 'btn-danger',
                action: (dialogItself) => {
                    removeElement(self, name);
                    if (onOk) {
                        onOk();
                    }
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: (dialogItself) => {
                    if (onCancel) {
                        onCancel();
                    }
                    dialogItself.close();
                }
            }
        ]
    });
}

let newAttendantId = 2;

function addAttendant() {
    let model = addElement('Attendant', attendantFn);
    let attendantId = -(newAttendantId++);
    model.find('.id').val(String(attendantId));
}

function changeCountAttendant(countSetted, count) {

    if (countSetted > count) {
        for (let i = 0; i < (countSetted - count); i++) {
            addAttendant();
        }
        $('.remove-attendant-button').addClass('hidden');

    } else if (count > countSetted) {
        for (let j = 0; j < (count - countSetted); j++) {
            removeElement($('#Attendants').children('fieldset:last').find('.remove-attendant-button'), 'Attendant');
        }
    }

}

function lockAttendantAddButton() {
    let typeOfRest = $('select.type-of-rest').select2('val');
    if (!typeOfRest || typeOfRest === CompensationEnum.toString()) {
        if (typeOfRest === CompensationEnum.toString()) {
            $('#AddAttendant').removeClass('hidden');
        }
        return;
    }

    let pb = $('#agentProxyBlock');
    if ($('#is-agent-accomp').select2('val') === 'True'
        && $('.TypeOfRestId').val() != CompensationEnum.toString()
        && $('.TypeOfRestId').val() != CompensationYouthRestEnum.toString()) {
        pb.removeClass('hidden');
    } else {
        pb.addClass('hidden');
        pb.find('input').val('');
    }

    $('.remove-attendant-button').addClass('hidden');
    $('#AddAttendant').addClass('hidden');

    let count = $('input#Data_CountAttendants').length !== 0
        ? $('input#Data_CountAttendants').val()
        : $('select#Data_CountAttendants').select2('val');

    if (count) {
        let attendantsCount = parseInt(count);
        let attendants = $('.attendant-panel').length + ($('.is-accomp').select2('val') === 'True' ? 1 : 0) + ($('#is-agent-accomp').select2('val') === 'True' ? 1 : 0);

        changeCountAttendant(attendantsCount, attendants);
    }

}

function removeChildElement(self) {
    removeElementWithConfirmation(self, 'Child');
}

function removeInformationVoucherElement(self) {
    let elementToRemove = $(self).parent().parent().parent();
    BootstrapDialog.show({
        title: 'Удаление',
        message: 'Вы действительно хотите удалить путевку?',
        buttons: [
            {
                label: 'Удалить',
                cssClass: 'btn-danger',
                action: dialogItself => {
                    let id = $(self).closest('.request-block').find('input.id').val();
                    elementToRemove.remove();
                    changeIndexInNamesVoucher($('#InformationVoucherDataRows'), 'InformationVouchers');
                    $('input.child-voucher-name').each((i, e) => {
                        if ($(e).val() == id) {
                            $(e).select2('data', {id: '', text: '-- Не выбрано --'});
                        }
                    });

                    if ($('#InformationVoucherDataRows').children().length === 0) {
                        $('#InformationVoucherEmptyRow').removeClass('hidden');
                    }
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: dialogItself => {
                    dialogItself.close();
                }
            }
        ]
    });
}

function removeInformationVoucherAttendantElement(self) {
    let elementToRemove = $(self).parent().parent();
    let $tbody = elementToRemove.parent();
    let $p = $tbody.parent().parent();

    BootstrapDialog.show({
        title: 'Удаление',
        message: $('select.type-of-rest').select2('val') == CompensationYouthRestEnum.toString() ?
            'Вы действительно хотите удалить отдыхающего из путевки?' : 'Вы действительно хотите удалить сопровождающего из путевки?',
        buttons: [
            {
                label: 'Удалить',
                cssClass: 'btn-danger',
                action: dialogItself => {
                    elementToRemove.remove();
                    changeIndexInNamesVoucher($p.find('table tbody'), 'AttendantsPrice');

                    if ($tbody.children().length === 0) {
                        $tbody.append($('<tr class="emptyRow"><td colspan="5"><i>' + ($('select.type-of-rest').select2('val') == CompensationYouthRestEnum.toString() ? 'Нет отдыхающих' : 'Нет сопровождающих') + '</i></td></tr>'));
                    }
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: dialogItself => {
                    dialogItself.close();
                }
            }
        ]
    });
}

function removeAttendantElement(self) {
    removeElementWithConfirmation(self, 'Attendant', () => {
        let id = $(self).closest('.request-block').find('input.id').val();
        let guid = $(self).closest('.request-block').find('input.guid').val();
        $('input.child-attendant-fio[value="' + guid + '"]').select2('data', {id: '', text: '-- Не выбрано --'});
        lockAttendantAddButton();
        $('#AddonServicesTable tbody').find('tr:has(.service-link-applicant-id[value="' + id + '"])').remove();
        if ($('#AddonServicesTable tbody tr').length === 0) {
            $('#AddonServicesTable').addClass('hidden');
        }
    });
}


function configDropdowns() {
    $('.select2').not('.select2-container, .inited').addClass('inited').select2();
    $('.child-attendant-fio').not('.select2-container, .inited').each((i, e) => {
        let $e = $(e);
        $e.addClass('inited');
        $e.select2({
            initSelection: (element, callback) => {
                if (element.val() == '')
                    callback({id: '', text: '-- Не выбрано --'});
                else
                    callback({id: element.val(), text: getFio(element.val())});
            },
            query: (query) => {
                let result = [{id: '', text: '-- Не выбрано --'}];
                let guids = $('.guid');
                guids.each((i) => {

                    let guid = $(guids[i]);
                    if (guid.hasClass('applicant-guid') && $('#NeedPlacmentApplicant').find('.is-accomp').select2('val') !== 'True') {
                        return;
                    }
                    if (guid.hasClass('agent-guid') && !($('#is-agent-accomp').select2('val') === 'True')) {
                        return;
                    }

                    result.push(
                        {
                            id: guid.val(),
                            text: getFio(guid.val())
                        });
                });
                query.callback({
                    results: result,
                    more: false
                });
            }
        }).change(() => {
            $e.attr('value', $e.select2('val'));
        });
    });


    $('.child-voucher-name').not('.select2-container, .inited').addClass('inited').select2({
        initSelection: (element, callback) => {
            if (element.val() == '')
                callback({id: '', text: '-- Не выбрано --'});
            else
                callback({id: element.val(), text: getVoucherInfo(element.val())});
        },
        query: (query) => {
            let result = [{id: '', text: '-- Не выбрано --'}];
            let guids = $('#InformationVoucherDataRows .id');
            guids.each((i) => {
                let guid = $(guids[i]);

                result.push(
                    {
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
        initSelection: (element, callback) => {
            callback({id: element.attr('data-default-id'), text: element.attr('data-default-text')});
        },
        minimumInputLength: 1,
        ajax: {
            url: '../../api/WebSchools',
            dataType: 'json',
            quietMillis: 250,
            data: (term) => {
                return {
                    query: term
                };
            },
            results: (data) => {
                let result = [{id: '', text: '-- Не выбрано --'}];
                if (data) {
                    for (let i = 0; i < data.length; i++) {
                        result.push({id: data[i].id, text: data[i].name});
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
    for (let i = 0; i < placeOfRest.length; i++) {
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
    for (let i = 0; i < typeOfRest.length; i++) {
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

    for (let i = 0; i < benefitType.length; i++) {
        if (benefitType[i].Id == id) {
            return benefitType[i];
        }
    }

    return undefined;
}

window.onload = () => {
    $("#statusApplicant").select2().trigger('change'); // необходимо для того чтобы отобразился раздел "Сведения о представителе заявителя"

    $.each($(".benefit-type-id"), function (i, el) { // необходимо для того чтобы корректно отобразилась категория льготы ребенка
        var hiddenValue = $(el).val();
        if (hiddenValue.length > 0) {
            $(el).siblings("select.benefit-type-dropdown").select2().val(hiddenValue);
            $(el).siblings("select.benefit-type-dropdown").select2().trigger("change");
        } else {
            $(el).val("0");
        }
    })

    $("select.typeOfCamp").select2().find("option").each(function (i, option){
        var campId = $(option).val();
        var campName = $(option).text() ;
        typeOfCampOptions.push({id: campId, text: campName})
    })

    $("select.priorityTypeOfTransport").select2().find("option").each(function (i, option) {
        var typeOfTransportId = $(option).val();
        var typeOfTransportName = $(option).text();
        typeOfTransportOptions.push({ id: typeOfTransportId, text: typeOfTransportName })
    })

    ToggleTypeOfTransportBlock();
    ToggleTypeOfCampBlock();
};

function attendantChangeProxy($e) {
    let pb = $e.closest('fieldset.attendant-panel').find('.proxy-block');
    if ($e.prop('checked') && $('.TypeOfRestId').val() != CompensationEnum.toString()) {
        pb.removeClass('hidden');
    } else {
        pb.addClass('hidden');
        pb.find('input').val('');
    }
}

// Отображать или скрывать блок с приоритетныи и дополнительным видом транспорта в зависимости от цели обращения, направления отдыха и способа проезда
function ToggleTypeOfTransportBlock() {
    let typeOfRestId = parseInt($('select.type-of-rest').select2('val'));
    let placeOfRestId = parseInt($('.placeOfRestId').select2('val'));
    let transferFromVal = parseInt($('select.transferFrom').select2('val'));
    let transferToVal = parseInt($('select.transferTo').select2('val'));

    if (typesOfRestRequiringTransportSelection.indexOf(typeOfRestId) != -1
        && placesOfRestRequiringTransportSelection.indexOf(placeOfRestId) != -1
        && (transferFromVal == typeOfTransferRequiringTransportSelection || transferToVal == typeOfTransferRequiringTransportSelection)) {
        $("#TypeOfTransport").parent().removeClass('hidden');
        $("#TypeOfTransportLink").removeClass('hidden');
        $('#TypeOfTransportLink').html('<a href="#TypeOfTransport">Тип транспорта</a>');
    } else {
        $("#TypeOfTransport").parent().addClass('hidden');
        $('#TypeOfTransportLink').html('');
        $('.priorityTypeOfTransport').select2('val', null)
        $('.additionalTypeOfTransport').select2('val', null)
    }
}

// Отображать или скрывать блок с приоритетныи и дополнительным типом лагеря в зависимости от цели обращения
function ToggleTypeOfCampBlock() {
    let typeOfRestId = parseInt($('select.type-of-rest').select2('val'));
    var typeOfRest = getTypeOfRest(typeOfRestId);
    if (typeOfRestRequiringCampTypeSelection == typeOfRest?.ParentId) {
        $("#TypeOfCamp").parent().removeClass('hidden');
        $("#TypeOfCampLink").removeClass('hidden');
        $('#TypeOfCampLink').html('<a href="#TypeOfCamp">Тип лагеря</a>');
    } else {
        $("#TypeOfCamp").parent().addClass('hidden');
        $('#TypeOfCampLink').html('');
        $('.typeOfCamp').select2('val', '-1')
        $('.typeOfCampAddon').select2('val', '-1')
    }
}

$(() => {
    let childFn = doT.template($('#childTemplate').html());
    let voucherFn = doT.template($('#voucherTemplate').html());
    let voucherAttendantFn = doT.template($('#voucherTemplateAttendant').html());
    let fileRowFn = doT.template($('#fileRowTemplate').html());

    $('.datepicker-general').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY'});
    $('.datepicker-my').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date()});
    $('.datepicker-future').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY', minDate: new Date()});
    $('.datetimepicker').datetimepicker({showTodayButton: true, maxDate: new Date()});


    $('#Attendants').on('change',
        'input.is-proxy',
        (e) => {
            attendantChangeProxy($(e.target));
        });

    $('#Attendants').on('change',
        'select.applicantType',
        (e) => {
            let $e = $(e.target);
            let $cbx = $e.closest('.form-group').find('input.is-proxy');
            $cbx.prop('checked', $e.select2('val') === '3');
            attendantChangeProxy($cbx);
        });

    $('#addPlace').click((e) => {
        let target = $(e.target);
        let urlToGo = target.attr('urlToGo');
        let types = $('#typeOfRoomsSelect select');
        let typesString: String = '';
        types.each((i, em) => {
            let v = $(em).select2('val');
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
                } else {
                    $('#is-agent-accomp').select2('val', $("#is-agent-accomp").select2().val());
                }
            }

        } else {
            $('#Agent').parent('fieldset').addClass('hidden');
            $('#AgentLink').html('');
            $('[data-spy="scroll"]').each(function () {
                $(this).scrollspy('refresh');
            });

            $('#is-agent-accomp').select2('val', 'False');
            lockAttendantAddButton();
        }
    }

    function removeFile(e: JQueryEventObject) {
        let $item = $(e.target);
        let $tbody = $item.parent().parent().parent();
        let $row = $item.parent().parent();
        $row.remove();
        let childs = $tbody.find('tr');
        let regExp = new RegExp("\[[0-9]+\].D", "g");
        for (let i = 0; i < childs.length; i++) {
            $(childs[i]).find('input').each(function () {
                if ($(this).attr('name')) {
                    $(this).attr('name', $(this).attr('name').replace(regExp, '[' + i.toString() + '].D'));
                }
            });
        }
    }

    $('.request-file-remove').click(removeFile);

    $('.fileinput-button').each(function () {
        let realName = '';
        let fu = $(this);
        fu.fileupload({
            url: rootPath + '/Upload.ashx',
            dataType: 'json',
            pasteZone: null,
            dropZone: null,
            maxChunkSize: 1000000,
            beforeSend: (xhr) => {
                xhr.setRequestHeader("X-FileName", realName);
            },
            submit: (e) => {
                let target = $(e.target);
                let parent = $(target.parent().parent().parent()[0]);
                parent.find('.file-upload-div').addClass('hidden');
                parent.find('.file-uploading-div').removeClass('hidden');
            },
            always: (e) => {
                let target = $(e.target);
                let parent = $(target.parent().parent().parent()[0]);
                parent.find('.file-upload-div').removeClass('hidden');
                parent.find('.file-uploading-div').addClass('hidden');
            },
            done: (e, data) => {
                realName = '';
                $.each(data.result, (index, file) => {
                    let target = $(e.target);
                    let parent = $(target.parent().parent().parent()[0]);
                    let tbody = parent.find('table tbody');

                    let entity = {
                        fileTypesIndex: parent.find(".index-hidden").val(),
                        fileTitle: file.name,
                        fileName: file.realname,
                        fileIndex: parent.find('table tbody tr').length
                    };

                    let row = $(fileRowFn(entity));
                    row.find('.request-file-remove').click(removeFile);
                    tbody.append(row);
                });
            }
        });

        fu.on('fileuploadchunkdone', (e, data) => {
            $.each(data.result, (index, file) => {
                realName = file.realname;
            });
        });
    });
    let htmlBenefitTypeDropdown = '';
    let newChildId = 1;

    function initTypeOfSubrestrtiction($e) {
        let $restriction = $e.find('.restriction-select');
        let $subres = $e.find('.subrestriction-select');
        let $div = $e.find('.type-of-subrestriction');

        $subres.select2({
            initSelection: (element, callback) => {
                if ($subres.val() === '') {
                    callback({id: '', text: '-- Не выбрано --'});
                } else {
                    callback({id: $subres.val(), text: $subres.attr('titleText')});
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + 'Api/WebVocabulary/GetTypeOfSubRestriction',
                dataType: 'json',
                data: () => {
                    let t = $restriction.select2('val');
                    return {
                        id: t
                    };
                },
                results: (data) => {
                    let results = [{id: '', text: '-- Не выбрано --'}];

                    for (let j = 0; j < data.length; j++) {
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

        $restriction.on('change', () => {
            let val = $restriction.select2('val');
            $subres.select2('data', {id: '', text: '-- Не выбрано --'});
            if (containsInArray(typeOfRestrictionSubs, parseInt(val))) {
                $div.removeClass('hidden');
            } else {
                $div.addClass('hidden');
            }
        });
    }

    $('.child-info-block').each((i, e) => {
        initTypeOfSubrestrtiction($(e));
    });

    function addChild() {
        let model = addElement('Child', childFn);
        model.find('.id').val(String(-newChildId));
        newChildId++;
        let typeOfRest = getTypeOfRest($('#typeOfRest-select2').val());
        let priorityPlacement = getPlacement($('#priority-placement').val());
        let additionalPlacement = getPlacement($('#additional-placement').val());
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
        } else {
            $('.foreign-document').addClass('hidden');
        }

        if (typeOfRest.Id == CompensationEnum.toString()) {
            $('.RequestInformationVoucherChild').removeClass('hidden');
        } else {
            $('.RequestInformationVoucherChild').addClass('hidden');
        }

        if ($('#typeOfRest-select2').val() != 0) {
            $('.child-age-restriction').removeClass('hidden');
            $('.child-age-restriction-val').text('Допустимый возраст: ' + typeOfRest.MinAge + '-' + typeOfRest.MaxAge + ' лет');
        } else {
            $('.child-age-restriction').addClass('hidden');
        }

        if (htmlBenefitTypeDropdown) {
            $('select.benefit-type-dropdown').html(htmlBenefitTypeDropdown);
            $('select.benefit-type-dropdown').select2('val', 0);
            $('select.benefit-type-dropdown').trigger('change');
        }

        initTypeOfSubrestrtiction(model);
    }

    $('#AddChild').click(() => {
        addChild();
    });

    $('#AddAttendant').click(() => {
        addAttendant();
        lockAttendantAddButton();
    });

    let newVoucherId = 1;

    let addVoucherAttendantClick = (e: JQueryEventObject) => {
        let $p = $(e.target).parent();
        let model = $(voucherAttendantFn({}));
        inputMaskConfig(model);
        $p.find('table tbody').append(model);
        $p.find('table tbody .emptyRow').remove();
        changeIndexInNamesVoucher($('#InformationVoucherDataRows'), 'InformationVouchers');
        changeIndexInNamesVoucher($p.find('table tbody'), 'AttendantsPrice');
        configDropdowns();
    };

    $('#AddVoucher').click(() => {
        let model = $(voucherFn({}));
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
        } catch (e) {
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

    $(document).on('AddNewChild', () => {
        configDropdowns();
    });

    $('#Childs').children().each(function () {
        let self = this;
        bindAutoFill(self, 'Child');
    });

    refreshLinkOnChilds('Attendant');

    $('#Attendants').children().each(function () {
        let self = this;
        bindAutoFill(self, 'Attendant');
    });

    bindAutoFill($('#ApplicantFieldset'), 'Applicant');

    configDropdowns();

    function changeCountChildren() {
        let typeOfRest = $('select.type-of-rest').select2('val');
        if (!typeOfRest || typeOfRest === CompensationEnum.toString() || typeOfRest === CompensationYouthRestEnum.toString()) {
            return;
        }

        let countSetted = parseInt($('#mainPlaces').select2('val'));

        let childrens = $('#Childs').children('fieldset');

        let countChildren = childrens.length;

        if (countSetted > countChildren) {
            for (let i = 0; i < (countSetted - countChildren); i++) {
                addChild();
            }
        } else if (countChildren > countSetted) {
            for (let j = 0; j < (countChildren - countSetted); j++) {
                removeElement($('#Childs').children('fieldset:last').find('.remove-child-button'), 'Child');
            }
        }
    }

    let timeOfRestFn = doT.template($('#timeOfRestTemplate').html());

    let statuses = $('select#statusApplicant').html();

    // при выборе приоритетным наземный транспорт дополнительный воздушный
    function changeAdditionalTransport(target) {
        
        let additionalTransport = $(target).closest('fieldset').find('.additionalTypeOfTransport');

        var $typeOfadditional = $("select.additionalTypeOfTransport");
        if (target.selectedIndex == 2)
        {
            additionalTransport.find("option[value=2]").remove();

            $('.additionalTypeOfTransport').select2('val', 1);

        }
        else
        {
            var optionToAdd = typeOfTransportOptions.filter(obj => obj.id == 2)[0];
            if (optionToAdd && additionalTransport[1].childElementCount<3 ) {

                $typeOfadditional.append(new Option(optionToAdd.text, optionToAdd.id, false, false));

            }
            $('.additionalTypeOfTransport').select2('val', null);

        }
    }

    function changeTypeOfRest(target) {
        let savedInited = inited;
        let val = getTypeOfRest($(target).select2('val'));

        if (!val) {
            return;
        }

        if (savedInited) {
            $('input.placeOfRestId').each((i, e) => {
                $(e).select2('data', null);
            });
        }

        $('.time-of-rest')
            .each((i, e) => {
                $(e).select2('data', null);
            });

        $.ajax(rootPath + 'Api/WebVocabulary/GetBenefitTypeInternal', {
            data: {
                typeOfRestId: val.Id
            },
            success: (data) => {
                let res = {
                    data: [{id: 0, name: '-- Не выбрано --'}].concat(data)
                };

                htmlBenefitTypeDropdown = timeOfRestFn(res);
                $('select.benefit-type-dropdown')
                    .each((i, item) => {
                        let $item = $(item);
                        let selected = $item.select2('data');
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
            success: (data) => {
                if (data.length === 1 && savedInited) {
                    $('#YearOfRestId')
                        .select2('data',
                            {
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
        } else {
            $('#ApplicantFieldset .address-control').addClass('hidden');
        }

        let $placeRestLink = $('#PlaceRestLink');

        let $transfers = $('.transfers');

        if (val.NeedPlace) {
            $('#PlaceRest').parent().removeClass('hidden');
            $placeRestLink.html('<a href="#PlaceRest">Место отдыха</a>');
            $placeRestLink.show();
            $transfers.removeClass('hidden');
        } else {
            $('#PlaceRest').parent().addClass('hidden');
            $placeRestLink.html('');
            $placeRestLink.hide();
            $transfers.addClass('hidden');
            $transfers.find('select').each((i, e) => {
                $(e).select2('val', '');
            });
        }

        if (val.NotChildren) {
            $('#mainPlaces').select2('val', 0);
            $('select.is-accomp').select2('val', 'True');
            $('#is-agent-accomp').select2('val', 'False');

            let $selector = $('select#statusApplicant');

            let savedVal = $selector.select2('val');
            $selector.empty();
            $selector.append(statuses);
            $selector.find('*[value=1]').remove();
            $selector.find('*[value=2]').remove();
            $selector.find('*[value=3]').remove();
            if (inited) {
                $selector.select2('val', 5);
            } else {
                $selector.select2('val', savedVal);
            }


            changeAgent();
            let childrens = $('#Childs').children('fieldset');
            let countChildren = childrens.length;
            for (let j = 0; j < countChildren; j++) {
                removeElement($('#Childs').children('fieldset:last').find('.remove-child-button'), 'Child');
            }
            $('#ChildsReference').addClass('hidden');
            $('#ChildLinksHidden').append($('#ChildLinks').children());
            $('#Placements').addClass('hidden');
            $('#PlacesLink').html('');
        } else {

            let $selector = $('select#statusApplicant');
            let savedVal = $selector.select2('val');
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
                } else {
                    $selector.select2('val', savedVal);
                }

            } else {
                if (inited) {
                    $selector.select2('val', 1);
                } else {
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
                } else {
                    $('#PlacesAttendants').addClass('hidden');
                    $('#Data_CountAttendants').select2('val', null);
                    if (inited) {
                        if ($("select.is-accomp option[value='']").length !== 0) {
                            $('select.is-accomp').select2('val', null);
                        } else {
                            $('select.is-accomp').select2('val', 'False');
                        }
                        if ($("#is-agent-accomp option[value='']").length !== 0) {
                            $('#is-agent-accomp').select2('val', null);
                        } else {
                            $('#is-agent-accomp').select2('val', 'False');
                        }
                    }
                    $('#Placements').addClass('hidden');
                    $('#PlacesLink').html('');
                }
            } else {
                if (containsInArray(certificateTypeOFRests, val.Id)) {
                    $('#Placements').removeClass('hidden');
                    if (containsInArray(moneyAttendants, val.Id)) {
                        $("#PlacesAttendants").removeClass("hidden")
                    } else {
                        $("#PlacesAttendants").addClass("hidden")
                    }
                } else {
                    $('#Placements').addClass('hidden');
                    $('#PlacesLink').html('');
                    $('#mainPlaces').select2('val', "1");
                }
                if (containsInArray(moneyAttendants, val.Id)) {
                    $('#NeedPlacmentApplicant').removeClass('hidden');
                } else {
                    $('#NeedPlacmentApplicant').addClass('hidden');
                }

                if (inited) {
                    if (containsInArray(moneyAttendants, val.Id)) {
                        $('#Data_CountAttendants').select2('val', "1");
                    } else {
                        $('#Data_CountAttendants').select2('val', null);
                    }
                    if ($("select.is-accomp option[value='']").length !== 0) {
                        $('select.is-accomp').select2('val', null);
                    } else {
                        $('select.is-accomp').select2('val', 'False');
                    }
                    if ($("#is-agent-accomp option[value='']").length !== 0) {
                        $('#is-agent-accomp').select2('val', null);
                    } else {
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
        } else {
            if (containsInArray(moneyAttendants, val.Id)) {
                $('#NeedPlacmentApplicant').removeClass('hidden');
                $('#NeedPlacmentAgent').removeClass('hidden');
            } else {
                $('#NeedPlacmentApplicant').addClass('hidden');
                $('#NeedPlacmentAgent').addClass('hidden');
            }

            if (inited) {
                if ($("select.is-accomp option[value='']").length !== 0) {
                    $('select.is-accomp').select2('val', null);
                } else {
                    $('select.is-accomp').select2('val', 'False');
                }
                if ($("#is-agent-accomp option[value='']").length !== 0) {
                    $('#is-agent-accomp').select2('val', null);
                } else {
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
        } else {
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
        } else {
            $('.child-age-restriction').addClass('hidden');
        }

        let needBank = $('#Data_BankName').val() !== '';

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
            } else {
                $('#AddChild').addClass('hidden');
                $('.remove-child-button').addClass('hidden');
                $('#NeedPlacmentApplicant').addClass('hidden');
            }
        } else if (containsInArray(certificateTypeOFRests, val.Id)) {
            $('#TypeAndTime').html('Цель обращения');
            $('#TypeAndTimeLinkA').html('Цель обращения');
            $('#InformationVoucherLink').html('');
            if (needBank) {
                $('#BankLink').html('<a href="#Bank">Банковские реквизиты</a>');
                $('#Bank').removeClass('hidden');
            } else {
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
            } else {
                $('#NeedPlacmentApplicant').addClass('hidden');
                $('#NeedPlacmentAgent').addClass('hidden');
            }
        } else {
            $('#TypeAndTime').html('Цель обращения и время отдыха');
            $('#TypeAndTimeLinkA').html('Цель обращения и время отдыха');
            $('#InformationVoucherLink').html('');
            if ($('#requestOnMoney').val() === 'True' && $('#isFirstCompany').val() === 'True' && needBank) {
                $('#BankLink').html('<a href="#Bank">Банковские реквизиты</a>');
                $('#Bank').removeClass('hidden');
            } else {
                $('#BankLink').html('');
                $('#Bank').addClass('hidden');
            }

            $('.time-div').removeClass('hidden');
            $('#InformationVoucher').addClass('hidden');
            $('.RequestInformationVoucherChild').addClass('hidden');
            if (val.NeedPlacment) {
                $('#NeedPlacmentApplicant').removeClass('hidden');
            } else {
                $('#NeedPlacmentApplicant').addClass('hidden');
            }

            $('#AddChild').addClass('hidden');
            $('.remove-child-button').addClass('hidden');
        }
    }

    $('.type-of-rest').change((event) => {
        let target = event.target;
        changeTypeOfRest(target);
    });

    $('.priorityTypeOfTransport').change((event) => {
        let target = event.target;
        changeAdditionalTransport(target);
    });

    $('#mainPlaces')
        .change(() => {
            changeCountChildren();
        });
    $('#Data_CountAttendants')
        .change(() => {
            lockAttendantAddButton();
        });

    $('#is-agent-accomp')
        .change(() => {
            lockAttendantAddButton();
        });

    $('.is-accomp').change(() => {
        let id = $('#ApplicantFieldset').find('.id').val();
        if ($('.is-accomp').select2('val') === 'True' && ((getPlacement($('.PlaceOfRestId').val())) || {}).IsForegin) {
            $('.applicant-foreign-document').removeClass('hidden');
        } else {
            $('.applicant-foreign-document').addClass('hidden');
        }

        if ($('.is-accomp').select2('val') === 'True') {
            let name = '';
            $('#ApplicantFieldset').find('.name-input').each(function () {
                name += $(this).val() + ' ';
            });

            $('.applicant-offer-in-request').removeClass('hidden');
        } else {
            $('.applicant-offer-in-request').addClass('hidden');
            $('#AddonServicesTable tbody').find('tr:has(.service-link-applicant-id[value="' + id + '"])').remove();
        }
        lockAttendantAddButton();
    });

    $('select.beneficiariesId').change((e) => {
        let val = $(e.target).val();
        if (val === '3') {
            $('#AttendantInvalid').removeClass('hidden');
        } else {
            $('#AttendantInvalid').addClass('hidden');
        }
    });


    $(document).on('change', 'select.benefit-type-dropdown', (event) => {

        let val = getBenefitType($(event.target).val());
        if (inited) {
            $('.benefit-type-id').val(val.Id);
        }

        let parentIvalid = false;

        $('select.benefit-type-dropdown').each((i, e) => {
            let s = $(e).select2('val');
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
        } else {
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
        } else {
            $(event.target).closest('.request-block').find('.benefit').addClass('hidden');
        }

        if (val.NeedTypeOfRestriction) {
            $(event.target).closest('.request-block').find('.child-is-invalid-hidden').val('true');
            $(event.target).closest('.request-block').find('.child-is-invalid').attr('disabled', 'disabled');
            $(event.target).closest('.request-block').find('.child-is-invalid').prop('checked', true);
            $(event.target).closest('.request-block').find('.child-is-invalid').trigger('change');
        } else {
            $(event.target).closest('.request-block').find('.child-is-invalid').removeAttr('disabled');
        }

        let fieldset = $(event.target).closest('fieldset');
        if (val.Id != 0 && val.Id != 1 && val.Id != 12) {
            fieldset.find('.registered-in-Moscow').prop('checked', true);
            fieldset.find('.registered-in-Moscow').attr('disabled', 'disabled');
            fieldset.find('.registered-in-Moscow-hidden').val('true');
            fieldset.find('.registered-in-Moscow').trigger('change');
        } else {
            fieldset.find('.registered-in-Moscow').removeAttr('disabled');
            fieldset.find('.registered-in-Moscow').trigger('change');
        }

    });

    $(document).on('change', '.child-is-invalid', (event) => {
        let $target = $(event.target);
        let $e = $(event.target).closest('.request-block');

        let $restriction = $e.find('.restriction-select');
        let $subres = $e.find('.subrestriction-select');
        let $div = $e.find('.type-of-subrestriction');

        if ($target.is(':checked')) {
            $e.find('.type-of-restriction, .benefit-group-invalid').removeClass('hidden');
        } else {
            $e.find('.type-of-restriction, .benefit-group-invalid').addClass('hidden');
            $div.addClass('hidden');
        }
    });

    $('#statusApplicant').change((e) => {
        let val = $('#statusApplicant').select2('val');
        $('#AgentApplicant').prop('checked', val === '4');
        changeAgent();
        lockAttendantAddButton();
    });

    $(document).on('change', '#AgentApplicant', (event) => {
        changeAgent();
    });

    $(document).on('change', '.benefit-never-end', (event) => {
        if ($(event.target).is(':checked')) {
            $('.benefit-end-date').closest('.row').addClass('hidden');
        } else {
            $('.benefit-end-date').closest('.row').removeClass('hidden');
        }
    });

    $(document).on('change', '.school-not-present', (event) => {
        let prefix = $(event.target).attr('name').match(/^(.*)SchoolNotPresent/)[1];
        if ($(event.target).is(':checked')) {
            $('[name="' + prefix + 'SchoolId"').closest('.row').addClass('hidden');
        } else {
            $('[name="' + prefix + 'SchoolId"').closest('.row').removeClass('hidden');
        }
    });

    $(document).on('change', '.registered-in-Moscow', (event) => {
        if ($(event.target).is(':checked')) {
            $(event.target).closest('.request-block').find('.address-control').removeClass('hidden');
        } else {
            $(event.target).closest('.request-block').find('.address-control').addClass('hidden');
        }
    });

    configDropdowns();
    $('select.priority-placement').find('option').each((i, el) => {
        let val = getPlacement($(el).val());
        if (val.Id == 0)
            $(el).attr('data-sea-zone', '0');
        else
            $(el).attr('data-sea-zone', val.ZoneOfSea);
        $(el).attr('data-id', val.Id);
    });

    let historyAjax;
    $('.applicant-firstname, .applicant-lastname, .applicant-middlename,.agent-firstname, .agent-lastname, .agent-middlename, .attendant-firstname, .attendant-lastname, .attendant-middlename')
        .on('input',
            (e) => {
                let guid = $(e.target).closest('.request-block').find('.guid').val();

                $('input.child-attendant-fio[value="' + guid + '"]').select2("data", {id: guid, text: getFio(guid)});
            });

    $('.comment-button').click(() => {
        $('#CommentModal').modal();
    });

    $('.edit-save-button').click(() => {
        $('#Data_InternalCommentary').val($('#CommentModalValue').val());
        $('#CommentModal').modal('toggle');
    });

    $('.copy-request').click(() => {
        $('#copyRequestModal').modal('toggle');
    });


    $('.history-button').click(() => {
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
            success: (result) => {
                $('#HistoryModalLoading').addClass('hidden');
                $('#HistoryModalTable').removeClass('hidden');
                let template = doT.template($('#historyTableTemplate').html());
                $('#HistoryModalTable').find('tbody').html(template(result));
            },
            error: () => {
                $('#HistoryModalLoading').addClass('hidden');
                $('#HistoryModalError').addClass('hidden');
            }
        });
    });

    function documentInputType($target) {
        let self = $target;
        let $item = self.find("option:selected");
        let text = $item.text();
        let $fieldset = self.closest('fieldset');
        if (text === 'Паспорт гражданина РФ' || text === 'Паспорт РФ') {
            $fieldset.find('.input-mask-passport-series').inputmask('9999', {
                placeholder: "сссс",
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-passport-number').inputmask('999999', {
                placeholder: "нннннн",
                clearIncomplete: true
            });
        } else if (text === 'Свидетельство о рождении') {
            $fieldset.find('.input-mask-passport-series').inputmask('Regex', {
                regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]',
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-passport-number').inputmask('999999', {clearIncomplete: true});
        } else {
            $fieldset.find('.input-mask-passport-series').inputmask('Regex', {regex: '.*'});
            $fieldset.find('.input-mask-passport-number').inputmask('Regex', {regex: '.*'});
        }

        if ($item.val() !== '22' && $item.val() !== '23' && $('#isFirstCompany').val() === 'False') {
            $fieldset.find('.cert-birth-block').removeClass('hidden');
        } else {
            $fieldset.find('.cert-birth-block').addClass('hidden');
            $fieldset.find('.cert-birth-block>div>select').select2('val', 0);
            $fieldset.find('.cert-birth-block>div>input').val('');
        }
    }

    function documentCertInputType($target) {
        let self = $target;
        let $item = self.find("option:selected");
        let text = $item.text();
        let $fieldset = self.closest('fieldset');
        if (text === 'Паспорт гражданина РФ' || text === 'Паспорт РФ') {
            $fieldset.find('.input-mask-cert-series').inputmask('9999', {placeholder: "сссс", clearIncomplete: true});
            $fieldset.find('.input-mask-cert-number').inputmask('999999', {
                placeholder: "нннннн",
                clearIncomplete: true
            });
        } else if (text === 'Свидетельство о рождении') {
            $fieldset.find('.input-mask-cert-series').inputmask('Regex', {
                regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]',
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-cert-number').inputmask('999999', {clearIncomplete: true});
        } else {
            $fieldset.find('.input-mask-cert-series').inputmask('Regex', {regex: '.*'});
            $fieldset.find('.input-mask-cert-number').inputmask('Regex', {regex: '.*'});
        }
    }

    $(document).on('change', 'select.document-dropdown', (e) => {
        documentInputType($(e.target));
    });

    $(document).on('change', 'select.document-cert-dropdown', (e) => {
        documentCertInputType($(e.target));
    });

    $('select.document-dropdown').each((num, val) => {
        documentInputType($(val));
    });

    $('select.type-of-rest')
        .add('.placeOfRestId')
        .add('select.transferFrom')
        .add('select.transferTo')
        .on('change', ToggleTypeOfTransportBlock);

    $('select.type-of-rest').on('change', ToggleTypeOfCampBlock);

    var $typeOfCamp = $("select.typeOfCamp")
    var $typeOfCampAddon = $("select.typeOfCampAddon")

    $typeOfCamp.on("change", function () {

        if($(this).select2('val') == isCamping){
            $typeOfCampAddon.select2().find("option[value=" + isCamping + "]").remove()
            $typeOfCampAddon.select2("val","-1")
        }
        else{
            if($typeOfCampAddon.find("option[value=" + isCamping + "]").length == 0){
                var optionToAdd = typeOfCampOptions.filter(obj => obj.id == isCamping)[0];
                if (optionToAdd){
                    $typeOfCampAddon.append(new Option(optionToAdd.text, optionToAdd.id, false, false))
                }
                $typeOfCampAddon.select2("val","-1")
            }
        }
    });

    $('body').on('change', '.middlename-havenot', (e) => {
        let isChecked = $(e.target).is(':checked');
        let middlename = $(e.target).closest('fieldset').find('.middlename');
        if (isChecked) {
            middlename.val(null);
            middlename.attr('disabled', 'disabled');
        } else {
            middlename.removeAttr('disabled');
        }
    });

    let isExcludeChildConfirmed = false;
    $('#mainForm').submit(() => {
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

        let excludedChilds = $('.exclude-from-request, .exclude-from-request-a').filter((i, val: any) => {
            let hidden = $(val).closest('.checkbox').find('.exclude-from-request-hidden, .exclude-from-request-hidden-a');
            if (hidden.val() !== 'True' && $(val).is(':checked')) {
                return true;
            }
            return false;
        }).map((i, val) => {
            let block = $(val).closest('.request-block');
            let lastname = block.find('.info-lastname').val();
            let firstname = block.find('.info-firstname').val();
            let middlename = block.find('.info-middlename').val();
            let havenotMiddlename = block.find('.middlename-havenot').is(':checked');

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
                    action(dialogItself) {
                        isExcludeChildConfirmed = true;
                        $('#mainForm').submit();
                        dialogItself.close();
                    }
                }, {
                    label: 'Отмена',
                    action(dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
        return false;
    });

    $('.exclude-from-request').change((e) => {
        let block = $(e.target).closest('.request-block');
        let disabled = $(e.target).is(':checked');
        block.find('input, select').not('.exclude-from-request, .id, .disabled, input[type=hidden]').prop('disabled', disabled);
    });

    changeTypeOfRest($('select.type-of-rest'));
    inited = true;

    let $certificateCreateLink = $('#certificateCreateLink');
    $certificateCreateLink.on('click', (e) => {
        e.preventDefault();
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите создать сертификаты на выплату?',
            buttons: [
                {
                    label: "Создать сертификаты",
                    cssClass: 'btn-danger',
                    action: dialogItself => {
                        window.location.replace($certificateCreateLink.attr('href'));
                        dialogItself.close();
                    }
                }, {
                    label: 'Отмена',
                    action: dialogItself => {
                        dialogItself.close();
                    }
                }
            ]
        });
    });

    let $requestCreateLink = $('#requestCreateLink');
    $requestCreateLink.on('click', (e) => {
        e.preventDefault();
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите создать заявление на следующий год?',
            buttons: [
                {
                    label: "Создать заявление",
                    cssClass: 'btn-danger',
                    action: dialogItself => {
                        window.location.replace($requestCreateLink.attr('href'));
                        dialogItself.close();
                    }
                }, {
                    label: 'Отмена',
                    action: dialogItself => {
                        dialogItself.close();
                    }
                }
            ]
        });
    });

    $(".ApplicantAttendantChange").click(() => {
        let $item = $('#AttendantModal .ReplacingAccompanyBody');

        $(".ReplacingAccompanyErr").html('');

        AddAttendantPopup($item);

        $('#AttendantModal').modal();
    });

    $(".AttendantChange").click((event) => {
        let $item = $('#AttendantModal .ReplacingAccompanyBody');

        let aVal = $(event.target).closest("fieldset").find(".id").val();

        $(".ReplacingAccompanyErr").html('');

        AddAttendantPopup($item);

        $('#ReplacingAccompanyId').val(aVal);

        $('#AttendantModal').modal();
    });

    $(".attendantmodalsave").click(() => {
        $(".attendantmodalsave").prop("disabled", true)
        let $item = $('#AttendantModal #AttendantModalForm');

        let data = getFormData($item);

        data['HasNotMiddlename'] = $($item.find("input[name='HasNotMiddlename']")).is(':checked');
        //$("input[name='HasNotMiddlename']").is(':checked')

        $.ajax({
            method: "POST",
            url: attendantModalSaveUrl + '?RequestId=' + $("#Data_Id").val() + '&ReplacingAccompanyId=' + $("#ReplacingAccompanyId").val(),
            data: data
        })
            .done((msg) => {
                if (msg.IsError) {
                    $(".ReplacingAccompanyErr").html(msg.ErrorText);
                    $(".attendantmodalsave").prop("disabled", false)
                } else {
                    location.reload();
                }
            }).fail((msg) => {
            $(".ReplacingAccompanyErr").html("Ошибка при выполнении запроса");
            $(".attendantmodalsave").prop("disabled", false)
        });
    });
});

function getFormData($form) {
    let unindexedArray = $form.serializeArray();
    let indexed_array = {};

    $.map(unindexedArray, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}

function AddAttendantPopup($item) {
    $($item).html('');
    let model = $(attendantFn({}));

    $($item).append(model);

    $($item.find(".remove-attendant-button")).closest(".row").remove();
    $($item.find(".exclude-attendant")).closest(".row").remove();
    $($item.find("fieldset")).removeClass("bs-callout-info bs-callout");
    $($item.find(".type-violation")).closest(".row").remove();
    $($item.find(".AttendantChange")).closest(".row").remove();

    //applicantType

    $('#ReplacingAccompanyId').val('');

    let regExp = new RegExp("(.*\[[0-9]+\].)", "g");
    $($item.find("*")).each(function () {
        if ($(this).attr('name')) {
            $(this).attr('name', $(this).attr('name').replace(regExp, ''));
        }
    });

    try {
        $('.datepicker-my').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date()});

    } catch (e) {
    }

    try {
        $('.datepicker-future').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY', minDate: new Date()});
    } catch (e) {
    }

    try {
        $('.datepicker-general').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY'});
    } catch (e) {
    }

    inputMaskConfig($item);

    $($item.find(".select2")).select2();

    $item.on('change',
        'select.applicantType',
        (e) => {
            let $e = $(e.target);
            let $cbx = $e.closest('.form-group').find('input.is-proxy');
            $cbx.prop('checked', $e.select2('val') === '3');
            attendantChangeProxy($cbx);
        });


}

function changeIndexInNames(parent, name) {
    let childs = parent.children();
    let regExp = new RegExp("(.*\[[0-9]+\])", "g");
    for (let i = 0; i < childs.length; i++) {
        $(childs[i]).find('*').each(function () {
            if ($(this).attr('name')) {
                $(this).attr('name', $(this).attr('name').replace(regExp, name + '[' + i + ']'));
            }
        });

        $(childs[i]).find('.name-ref').attr("id", name + i);
        $(childs[i]).find('.name-title').attr("id", name + 'Title' + i);
    }
}

function changeIndexInNamesVoucher(parent, name) {
    let childs = parent.children();
    let regExp = new RegExp(name + "\[[0-9]+\].", "g");
    for (let i = 0; i < childs.length; i++) {
        $(childs[i]).find('*').each(function () {
            if ($(this).attr('name')) {
                $(this).attr('name', $(this).attr('name').replace(regExp, name + '[' + (i).toString() + '].'));
            }
        });

        $(childs[i]).find('.name-ref').attr("id", name + i);
        $(childs[i]).find('.name-title').attr("id", name + 'Title' + i);
    }
}

function changeGuids(parent, name) {
    let childs = parent.children();
    for (let i = 0; i < childs.length; i++) {
        let guidInputs = $(childs[i]).find('[name$=".Guid"]');
        guidInputs.each((j) => {
            let guid = newGuid();
            $(guidInputs[j]).val(guid);
        });
    }
}

function confirmButton(buttonName, actionCode, statusId = undefined) {
    if (statusId == '1050') {
        let res = true;
        $('.approve-checkboxs').find('input[type=checkbox]').each((i, e) => {
            res = res && $(e).prop('checked')
        });
        if (!res) {
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
                action: dialogItself => {
                    $('#action').val(actionCode);
                    $('#mainForm').submit();
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: dialogItself => {
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
    let $content;

    BootstrapDialog.show({
        title: 'Подтвердить действие',
        message: () => {
            let fn = doT.template($("#declineReasons" + statusId).html());
            $content = $(fn({name: "Вы действительно хотите " + buttonName.toLowerCase() + '?'}));
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
                action: (dialogItself) => {
                    $('#action').val(actionCode);
                    let declineReason = dialogItself.$modalContent.find('select.declineReasonsSelect').val();
                    if (declineReason != '0') {
                        $('#Data_DeclineReasonId').val(declineReason);
                        dialogItself.$modalContent.find('.declineReasonsSelectMessage').hide();
                    } else {
                        dialogItself.$modalContent.find('.declineReasonsSelectMessage').show();
                        return;
                    }

                    $('#mainForm').submit();
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action(dialogItself) {
                    dialogItself.close();
                }
            }
        ]
    });
}


var arr;

function CheckHandler(event, isParentNode) {
    var src = event.target;
    var cond = $(src).prop("checked");
    if (isParentNode) {
        if (cond) {
            $.each($(src).siblings("ul").find("[type='checkbox']"), function (i, val) {
                $(val).prop("checked", true);
            })
        } else {
            $.each($(src).siblings("ul").find("[type='checkbox']"), function (i, val) {
                $(val).prop("checked", false);
            })
        }
    } else {
        var total = $(src).parents("ul").first().find("[type='checkbox']").length;
        var checked = $(src).parents("ul").first().find("[type='checkbox']:checked").length;
        if (checked == total) {
            $(src).parents("ul").first().siblings("input[type='checkbox']").prop("checked", true);
        } else {
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
       
    } else {
            $.each($("#copyRequestModal").find("[type='checkbox']").not(":disabled"), function (i, val) {
            $(val).prop("checked", false);
        })
    }

}


const mapperDict = {
    "#GeneralInfo": {text: "Общие сведения", prop: "TransferGeneralData"},
    "#TypeAndTime": {text: "Цель обращения и время отдыха", prop: "TransferTargetAndTimeOfRestData"},
    "#PlaceRest": {text: "Место отдыха", blocked: "true"},
    "#Places": {text: "Размещение", blocked: "true"},
    "#Applicant": {text: "Сведения о заявителе", prop: "TransferApplicantData"},
    "#Agent": {text: "Сведения о представителе заявителя", prop: "TransferAgentData"},
    "#AttendantsReference": {text: "Сведения о сопровождающих", prop: "TransferAttendantData", list: "AttendantsIds"},
    "#InformationVoucher": {text: "Путевки", blocked: "true"},
    "#ChildsReference": {text: "Сведения о детях", prop: "TransferChildData", list: "ChildrenIds"},
    "#parentIvalid": {text: "Сведения о родителе-инвалиде", blocked: "true"},
    "#Bank": {text: "Банковские реквизиты", prop: "TransferBankData"},
    "#ChildrenRequests": {text: "Сведения о созданных заявлениях", blocked: "true"},
    "#ParentRequest": {text: "Сведения о заявлении на основе которого выдано текущее", blocked: "true"},
    "#RequestCertificates": {text: "Сведения о погашенном сертификате", blocked: "true"},
    "#FileReference": {text: "Документы", prop: "TransferFilesData"},
    "#TypeOfTransport": {text: "Тип транспорта", prop: "TransferTypeOfTransportData", blocked: "true"},
    "#TypeOfCamp": {text: "Тип лагеря", prop: "TransferTypeOfCampData"},

}
