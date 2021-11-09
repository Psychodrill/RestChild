var typeOfRestRestWithParents = '2';
var typeOfRestRestCamps = '1';
var typeOfRestSpecializedCampsFamily = '15';
var typeOfRestSpecializedCamps = '-1';
$(function () {
    $('.text-editor-div').summernote({
        lang: 'ru-RU',
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'italic', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']],
            ['table', ['table']],
            ['insert', ['link', 'hr']]
        ],
        maxHeight: 300,
        height: 300
    });
    $(document).on('click', 'input.childs-payed', function (e) {
        var $target = $(e.target);
        var id = $target.attr('childid');
        var tourId = $target.attr('tourid');
        var value = $target.prop('checked');
        if (value) {
            $target.closest('tr').find('.addon-service').removeClass('hidden');
        }
        else {
            $target.closest('tr').find('.addon-service').addClass('hidden');
        }
        $.ajax({
            url: rootPath + '/api/WebTours/ChangePaidChild?id=' + tourId + '&childId=' + id + '&value=' + value,
            type: 'POST',
            success: function () {
                ShowAlert("Информация об оплате обновлена", "alert-success", "glyphicon-ok", true);
            },
            error: function () {
                ShowAlert("Ошибка обновления информации", "alert-danger", "glyphicon-remove");
            }
        });
        if ($('input.childs-payed').not(':checked').length === 0 && $('input.attendants-payed').not(':checked').length === 0) {
            $('input.list-paid').prop('checked', true);
        }
        else {
            $('input.list-paid').prop('checked', false);
        }
    });
    $(document).on('click', 'input.attendants-payed', function (e) {
        var id = $(e.target).attr('attendantid');
        var tourId = $(e.target).attr('tourid');
        var value = $(e.target).prop('checked');
        $.ajax({
            url: rootPath + '/api/WebTours/ChangePaidAttendant?id=' + tourId + '&attendantId=' + id + '&value=' + value,
            type: 'POST'
        });
        if ($('input.childs-payed').not(':checked').length === 0 && $('input.attendants-payed').not(':checked').length === 0) {
            $('input.list-paid').prop('checked', true);
        }
        else {
            $('input.list-paid').prop('checked', false);
        }
    });
    $(document).on('click', 'input.list-paid', function (e) {
        var target = $(e.target);
        var parent = target.parent().parent().parent().parent();
        var listId = target.attr('data-list-id');
        var value = target.prop('checked');
        $.ajax({
            url: rootPath + '/api/WebTours/ChangePaidChildList',
            data: {
                id: listId,
                paid: value
            },
            method: 'GET'
        });
        parent.find('input.childs-payed').prop('checked', value);
        if (value) {
            parent.find('button.addon-service').removeClass('hidden');
        }
        else {
            parent.find('button.addon-service').addClass('hidden');
        }
        parent.find('input.attendants-payed').prop('checked', value);
    });
    $('.countPlace').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('.date').inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    });
    $('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('.datetime').inputmask("d.m.y h:s", {
        placeholder: "дд.мм.гггг чч:мм",
        clearIncomplete: true
    });
    $('.datetime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });
    $('#Data_TourPrice').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
    $('#summa').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
    $('#RoomRateRowTemplate .price').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
    $('#Data_TourPriceAttendant').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
    $('#Data_ChildListsAddFilterChildsFrom').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('#Data_ChildListsAddFilterChildsTo').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('.count-places, .count-rooms').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('select').select2();
    $('#Data_TypeOfRestId').select2().on('change', function (e) {
        if (e['val'] === typeOfRestRestWithParents) {
            $('#TourVolumeForCamps').addClass('hidden');
            $('#TourVolumeWithParents').removeClass('hidden');
        }
        else {
            $('#TourVolumeForCamps').removeClass('hidden');
            $('#TourVolumeWithParents').addClass('hidden');
        }
        if (e['val'] === typeOfRestSpecializedCamps || e['val'] === typeOfRestSpecializedCampsFamily) {
            $('#Data_ForList').val('True');
            $('#listForSpecializedCamps').removeClass('hidden');
            $('#listForCooperative').addClass('hidden');
            $('#listForCamps').addClass('hidden');
            $('#limitOnVedomstvoId').removeClass('hidden');
        }
        else if (e['val'] === typeOfRestRestCamps) {
            $('#Data_ForList').val('True');
            $('#listForCamps').removeClass('hidden');
            $('#listForSpecializedCamps').addClass('hidden');
            $('#listForCooperative').addClass('hidden');
            $('#limitOnVedomstvoId').addClass('hidden');
        }
        else {
            $('#Data_ForList').val('False');
            $('#listForSpecializedCamps').addClass('hidden');
            $('#listForCooperative').removeClass('hidden');
            $('#listForCamps').addClass('hidden');
            $('#limitOnVedomstvoId').addClass('hidden');
        }
        if (e['val'] === typeOfRestRestCamps || e['val'] === typeOfRestRestWithParents) {
            $('#Payments, #ChildAge').removeClass('hidden');
        }
        else {
            $('#Payments, #ChildAge').addClass('hidden');
        }
        if (e['val'] === typeOfRestSpecializedCamps || e['val'] === typeOfRestSpecializedCampsFamily) {
            $('.specialized-camp').removeClass('hidden');
        }
        else {
            $('.specialized-camp').addClass('hidden');
        }
        $.ajax({
            url: rootPath + 'Api/WebRestType/GetById/' + e['val'],
            type: 'GET',
            dataType: 'json',
            success: function (typeOfRest) {
                if (!typeOfRest.needBookingDate) {
                    $('#BookingDates').addClass('hidden');
                }
                else {
                    $('#BookingDates').removeClass('hidden');
                }
                if (typeOfRest.needSubject) {
                    $('.subject-of-rest').removeClass('hidden');
                }
                else {
                    $('.subject-of-rest').addClass('hidden');
                }
                if (typeOfRest.firstRequestCompanySelect) {
                    $('#forMultipleStageCompany').closest('div').removeClass('hidden');
                }
                else {
                    $('#forMultipleStageCompany').prop('checked', false);
                    $('#forMultipleStageCompany').closest('div').addClass('hidden');
                }
            }
        });
        $('#Data_TimeOfRestId').select2('data', { id: '', text: '-- Не выбрано --' });
        $('#Data_HotelsId').select2('data', { id: '', text: '-- Не выбрано --' });
        $('#Data_LimitOnVedomstvoId').select2('data', { id: '', text: '-- Не выбрано --' });
    });
    $('#Data_LimitOnVedomstvoId').select2({
        initSelection: function (element, callback) {
            if ($('#_LimitOnVedomstvoId').val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $('#_LimitOnVedomstvoId').val(), text: $('#_LimitOnVedomstvoName').val(), vedomstvoId: $('#_VedomstvoId').val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/Limits/GetLimitsOnYear',
            dataType: 'json',
            data: function (term, page) {
                return {
                    yearId: $('#Data_YearOfRestId').select2('val')
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                results = results.concat($.map(data, function (item) {
                    return {
                        text: item.organizationName + (item.tl ? ' (' + item.tl + ')' : ''),
                        id: item.id,
                        tlId: item.tlId,
                        vedomstvoId: item.organizationId
                    };
                }));
                return {
                    results: results
                };
            },
            cache: true
        }
    }).on('change', function () {
        $('#Data_ContractId').select2('data', { id: '', text: '-- Не выбрано --' });
        var data = $('#Data_LimitOnVedomstvoId').select2('data');
        if (data && data.tlId == 2) {
            $('.specialized-camp').addClass('hidden');
        }
        else {
            $('.specialized-camp').removeClass('hidden');
        }
    });
    $('#Data_TimeOfRestId').select2({
        initSelection: function (element, callback) {
            if ($('#_TimeOfRestId').val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $('#_TimeOfRestId').val(), text: $('#_TimeOfRestName').val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebRestTime/GetByTypeAndYear',
            dataType: 'json',
            data: function (term, page) {
                return {
                    name: term,
                    typeOfRestId: $('#Data_TypeOfRestId').select2('val'),
                    yearOfRestId: $('#Data_YearOfRestId').select2('val')
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                results = results.concat($.map(data, function (item) {
                    var dateStart = moment([item.year, item.month - 1, item.dayOfMonth]);
                    var dateEnd = moment(dateStart).add(item.periodLength - 1, 'days');
                    return {
                        text: item.name,
                        id: item.id,
                        dateStart: dateStart,
                        dateEnd: dateEnd
                    };
                }));
                return {
                    results: results
                };
            },
            cache: true
        }
    }).select2('val', [])
        .on('change', function (e) {
        var data = $(e.target).select2('data');
        if (data != null) {
            var dateStart = data.dateStart;
            var dateEnd = data.dateEnd;
            $('#Data_DateIncome').val(dateStart.format('DD.MM.YYYY'));
            $('#Data_DateOutcome').val(dateEnd.format('DD.MM.YYYY'));
        }
    });
    $('#toursForm').on('submit', function (e) {
        e.preventDefault();
        var tourVolumes = $('#TourVolumeTable tr, #TourVolumeForCamps');
        $.each(tourVolumes, function (i, volume) {
            var hiddens = $(volume).find('input');
            $.each(hiddens, function (j, hidden) {
                var dotPosition = $(hidden).attr('name').lastIndexOf('.');
                var name = dotPosition != -1 ? $(hidden).attr('name').substring(dotPosition + 1) : $(hidden).attr('name');
                $(hidden).attr('name', 'Data.Volumes[' + (i - 1) + '].' + name);
            });
        });
        var tourChildList = $('#ChildListsTable tr.child-list-row');
        $.each(tourChildList, function (i, volume) {
            var hiddens = $(volume).find('input');
            $.each(hiddens, function (j, hidden) {
                var dotPosition = $(hidden).attr('name').lastIndexOf('.');
                var name = dotPosition != -1 ? $(hidden).attr('name').substring(dotPosition + 1) : $(hidden).attr('name');
                $(hidden).attr('name', 'Data.ChildLists[' + i + '].' + name);
            });
        });
        $('.HotelsId').val($('.main-hotelId').select2('val'));
        $('#toursForm').unbind().submit();
    });
    $('.main-hotelId').on("change", function (e) {
        var val = $(e.target).select2('val');
        if (val && val != '') {
            $('#showHotel').attr('href', rootPath + '/Hotels/Update/' + val);
            $('#showHotel').removeClass('hidden');
        }
        else {
            $('#showHotel').addClass('hidden');
        }
    });
    $('#Data_HotelsId').select2({
        initSelection: function (element, callback) {
            if ($('#_HotelsId').val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $('#_HotelsId').val(), text: $('#_HotelsName').val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebHotels',
            dataType: 'json',
            data: function (term, page) {
                return {
                    name: term,
                    typeOfRest: $('#Data_TypeOfRestId').val(),
                    onlyApproved: 'True'
                };
            },
            results: function (data, page) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.name,
                            id: item.id
                        };
                    })
                };
            },
            cache: true
        }
    }).select2('val', []);
    $('#ChildListsAddFilterVedomstvo').select2({
        initSelection: function (element, callback) {
            callback({ id: '', text: '-- Не выбрано --' });
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + 'api/Vedomstvo',
            dataType: 'json',
            data: function (term, page) {
                return {
                    query: term
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
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
    }).select2('val', []);
    $('#addTourVolume').click(function () {
        if (!$('#Data_HotelsId').valid()) {
            return;
        }
        $('#typeOfRoomsChooseModal .modal-body').html('<div class="text-center">Получение данных <img class="text-center" width="20" height="20" src="/Content/images/spinner.gif"/> </div>');
        $('#typeOfRoomsChooseModal').modal('show');
        $.ajax({
            url: rootPath + '/api/WebTypeOfRooms',
            data: { hotelId: $('#Data_HotelsId').val() }
        }).done(function (data) {
            if (data == null || data.length == 0 || data.length == null) {
                $('#typeOfRoomsChooseModal .modal-body').html('<p class="text-center">Номерной фонд отсутствует </p>');
                return;
            }
            var tempFn = doT.template($('#typeOfRoomsChooseTemplate').html());
            $('#typeOfRoomsChooseModal .modal-body').html(tempFn(data));
        }).fail(function () {
            $('#typeOfRoomsChooseModal .modal-body').html('<p class="text-center">Ошибка получения данных</p>');
        });
    });
    function prepareRateTable() {
        if ($('#roomRateTable tbody').children('tr').length > 0) {
            $('#roomRateTable').show();
            $('#roomRateI').hide();
            $('#roomRateTable tbody tr').each(function (num, val) {
                $(val).find('input[name^="RoomRates["]').each(function (inputNum, input) {
                    var regexp = new RegExp('RoomRates\\[.*?\\](.*)');
                    var matched = $(input).attr('name').match(regexp);
                    if (matched != null && matched.length > 1) {
                        var name = matched[1];
                        $(input).attr('name', 'RoomRates[' + num + ']' + name);
                    }
                });
            });
        }
        else {
            $('#roomRateTable').hide();
            $('#roomRateI').show();
        }
    }
    $('#addRoomRate').click(function () {
        if (!$('#Data_HotelsId').valid()) {
            return;
        }
        ;
        $('#summa').val('');
        $('#TypeOfRoomsId').select2("data", { id: -1, text: "-- Не выбрано --" });
        ;
        $('#AccommodationsId').select2("data", { id: -1, text: "-- Не выбрано --" });
        $('#DiningOptionsId').select2("data", { id: -1, text: "-- Не выбрано --" });
        $('#isAddonPlace').prop('checked', false);
        $('#roomRateDialog').modal('show');
    });
    function removeRoomrate(e) {
        var row = $(e.currentTarget).parents('tr');
        row.remove();
        prepareRateTable();
    }
    $('#roomRateTable .remove-roomrate-btn').on('click', removeRoomrate);
    $('#saveRoomRateClick').click(function () {
        var row = $($('#RoomRateRowTemplate').html());
        var tr = $('#TypeOfRoomsId').select2("data");
        var a = $('#AccommodationsId').select2("data");
        var dinOp = $('#DiningOptionsId').select2("data");
        row.find('.typeOfRooms-name').html(tr.text);
        row.find('.accommodation-name').html(a.text);
        row.find('.diningOptions-name').html(dinOp.text);
        row.find('.typeOfRooms').val(tr.id);
        row.find('.accommodation').val(a.id);
        row.find('.diningOptions').val(dinOp.id);
        row.find('.price').val($('#summa').val());
        row.find('.price').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
        row.find('.remove-roomrate-btn').on('click', removeRoomrate);
        row.find('.isAddonPlace').val($('#isAddonPlace').is(':checked') ? 'True' : 'False');
        row.find('.roomrate-for-addon').html($('#isAddonPlace').is(':checked') ? '<span class="glyphicon glyphicon-ok text-success"></span>' : '<span class="glyphicon glyphicon-remove text-danger"></span>');
        $('#roomRateTable tbody').append(row);
        prepareRateTable();
    });
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).parent().addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).parent().removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
        }
    });
    $('body').on('click', '.typeOfRoomChoose', function (e) {
        var chosenItem = $(e.target).closest('tr');
        var id = chosenItem.find('.typeOfRoomId').val();
        if ($('#TourVolumeTable input[name="TypeOfRoomsId"][value="' + id + '"]').length != 0) {
            return;
        }
        var tempFn = doT.template($('#typeOfRoomsTemplate').html());
        $('#TourVolumeTable tbody').append(tempFn({
            id: id,
            name: chosenItem.find('.typeOfRoomName').html(),
            conviences: chosenItem.find('.typeOfRoomConviences').html()
        }));
        $('#TourVolumeTable').removeClass('hidden');
        $.each($('.newTr'), function (i, val) {
            $(val).find('.countPlace').inputmask('integer', { allowMinus: false, rightAlign: false });
            $(val).find('input[name="CountRooms"]').inputmask('integer', { allowMinus: false, rightAlign: false });
            $(val).removeClass('.newTr');
        });
    });
    $('body').on('click', '.removeTourVolume', function (e) {
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите удалить номерной фонд?',
            buttons: [
                {
                    label: 'Удалить',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        $(e.target).closest('tr').remove();
                        if ($('#Data_TourVolumeTable tbody tr').length == 0) {
                            $('#Data_TourVolumeTable').addClass('hidden');
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
    });
    $('#AddChildLists').click(function () {
        $('#ChildListsAddFilter').addClass('hidden');
        $('#ChildListsAddFilterCollapseOne').collapse('hide');
        $('#ChildListsAddFilterVedomstvo').select2('val', { id: '', text: '-- Не выбрано --' });
        $('#ChildListsAddFilterChildsFrom').val(null);
        $('#ChildListsAddFilterChildsTo').val(null);
        if ($('#Data_YearOfRestId').select2('val') == '') {
            $('#ChildListsModal .modal-body').html('<div class="text-center">Выберите год отдыха</div>');
            $('#ChildListsModal').modal("show");
            return;
        }
        $('#ChildListsAddBody').html('<div class="text-center">Получение данных <img class="text-center" width="20" height="20" src="/Content/images/spinner.gif"/> </div>');
        $('#ChildListsModal').modal("show");
        loadAvailableChildLists({ yearId: $('#Data_YearOfRestId').select2('val') });
    });
    $('body').on('click', '.btn-childlist-select', function (e) {
        var tr = $(e.target).closest('tr');
        var id = tr.find('.child-list-id').val();
        if ($('.child-list-table-id[value="' + id + '"]').length != 0) {
            return;
        }
        var tempFn = doT.template($('#ChildListsTemplate').html());
        $('#ChildListsTableBody').append(tempFn({
            Id: id,
            Vedomstvo: tr.find('.childlist-table-vedomstvo').html(),
            Organization: tr.find('.childlist-table-organization').html(),
            Name: tr.find('.childlist-table-name').html(),
            ChildsCount: tr.find('.childlist-table-childscount').html(),
            AttendantsCount: tr.find('.childlist-table-attendantscount').html(),
            State: tr.find('.childlist-table-state').html()
        }));
        $('#ChildListsTable').removeClass('hidden');
    });
    $('body').on('click', '.childlist-exclude', function (e) {
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите удалить список?',
            buttons: [
                {
                    label: 'Удалить',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        $(e.target).closest('tr').next().remove();
                        $(e.target).closest('tr').remove();
                        if ($('#Data_ChildListsTableBody tr').length == 0) {
                            $('#Data_ChildListsTable').addClass('hidden');
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
    });
    $('body').on('click', '.childlist-expand', function (e) {
        var childTr = $(e.target).closest('tr').next('tr.collapse');
        var listId = $(e.target).closest('tr').find('[name="Id"]').val();
        childTr.toggleClass('out').toggleClass('in');
        $(e.target).closest('tr').find('.childlist-expand i').toggleClass('glyphicon-rotate');
        if (!childTr.hasClass('loaded')) {
            childTr.addClass('loaded');
            childTr.find('.childs-in-list').html('<div class="text-center">Получение данных <img class="text-center" width="20" height="20" src="/Content/images/spinner.gif" /></div>');
            $.ajax({
                url: rootPath + '/api/Limits/GetChildsAndAttendantsInList',
                data: { listId: listId },
                dataType: 'json'
            }).done(function (received) {
                if (received == null || (received.childs == null || received.childs.length == 0 || received.childs.length == null) && (received.attendants == null || received.attendants.length == 0 || received.attendants.length == null)) {
                    childTr.find('.childs-in-list').html('<p class="text-center">Отдыхающие в списке не найдены </p>');
                    return;
                }
                var tempFn = doT.template($('#ChildsInListTemplate').html());
                var childs = null;
                if (received.childs != null && received.childs.length > 0) {
                    childs = $.map(received.childs, function (val, i) {
                        return {
                            num: i + 1,
                            lastName: val.lastName != null ? val.lastName : '-',
                            firstName: val.firstName != null ? val.firstName : '',
                            middleName: val.middleName != null ? val.middleName : '',
                            dateOfBirth: val.dateOfBirth != null ? moment(val.dateOfBirth).format('DD.MM.YYYY') : '-',
                            documentType: val.documentType != null && val.documentType.name != null ? val.documentType.name : '-',
                            documentSeria: val.documentSeria != null && val.documentSeria != null ? val.documentSeria : '',
                            documentNumber: val.documentNumber != null && val.documentNumber != null ? val.documentNumber : '',
                            paymentFileUrl: val.paymentFileUrl != null ? rootPath + '/DownloadPaymentFileHandler.ashx/' + val.paymentFileUrl : null,
                            paymentFileTitle: val.paymentFileTitle,
                            id: val.id,
                            payed: val.payed
                        };
                    });
                }
                ;
                var attendants = null;
                if (received.attendants != null && received.attendants.length > 0) {
                    attendants = $.map(received.attendants, function (val, i) {
                        return {
                            num: i + 1,
                            lastName: val.lastName != null ? val.lastName : '-',
                            firstName: val.firstName != null ? val.firstName : '',
                            middleName: val.middleName != null ? val.middleName : '',
                            dateOfBirth: val.dateOfBirth != null ? moment(val.dateOfBirth).format('DD.MM.YYYY') : '-',
                            documentType: val.documentType != null && val.documentType.name != null ? val.documentType.name : '-',
                            documentSeria: val.documentSeria != null && val.documentSeria != null ? val.documentSeria : '',
                            documentNumber: val.documentNumber != null && val.documentNumber != null ? val.documentNumber : '',
                            paymentFileUrl: val.paymentFileUrl != null ? rootPath + '/DownloadPaymentFileHandler.ashx/' + val.paymentFileUrl : null,
                            paymentFileTitle: val.paymentFileTitle,
                            id: val.id,
                            payed: val.payed
                        };
                    });
                }
                ;
                var listPaid = true;
                if (childs != null) {
                    for (var child in childs) {
                        if (!childs[child].payed) {
                            listPaid = false;
                            break;
                        }
                    }
                }
                if (listPaid && attendants != null) {
                    for (var attendant in attendants) {
                        if (!attendants[attendant].payed) {
                            listPaid = false;
                            break;
                        }
                    }
                }
                childTr.find('.childs-in-list').html(tempFn({
                    listPaid: listPaid,
                    listId: listId,
                    childs: childs,
                    attendants: attendants
                }));
            }).fail(function () {
                childTr.find('.childs-in-list').html('<p class="text-center">Ошибка получения данных</p>');
                childTr.removeClass('loaded');
            });
            childTr.find('childs-in-list');
        }
    });
    $('#ChildListsAddFilterSearch').click(function () {
        $('#ChildListsAddBody').html('<div class="text-center">Получение данных <img class="text-center" width="20" height="20" src="/Content/images/spinner.gif"/> </div>');
        loadAvailableChildLists({
            yearId: $('#Data_YearOfRestId').select2('val'),
            vedomstvoId: $('#ChildListsAddFilterVedomstvo').val(),
            childsCountFrom: $('#ChildListsAddFilterChildsFrom').val(),
            childsCountTo: $('#ChildListsAddFilterChildsTo').val()
        });
    });
    $('#ChildListsAddFilterClear').click(function () {
        $('#ChildListsAddFilterVedomstvo').select2('val', { id: '', text: '-- Не выбрано --' });
        $('#ChildListsAddFilterChildsFrom').val(null);
        $('#ChildListsAddFilterChildsTo').val(null);
        $('#ChildListsAddBody').html('<div class="text-center">Получение данных <img class="text-center" width="20" height="20" src="/Content/images/spinner.gif"/> </div>');
        loadAvailableChildLists({
            yearId: $('#Data_YearOfRestId').select2('val'),
            vedomstvoId: $('#ChildListsAddFilterVedomstvo').val(),
            childsCountFrom: $('#ChildListsAddFilterChildsFrom').val(),
            childsCountTo: $('#ChildListsAddFilterChildsTo').val()
        });
    });
    function fixPartyNums() {
        $('.party').each(function (partyNum, party) {
            $(party).find('input[name^="Data.Partys["]').each(function (inputNum, input) {
                var regexp = new RegExp('Data.Partys\\[.*?\\](.*)');
                var matched = $(input).attr('name').match(regexp);
                if (matched != null && matched.length > 1) {
                    var name = matched[1];
                    $(input).attr('name', 'Data.Partys[' + partyNum + ']' + name);
                }
            });
            $(party).find('.child-in-party-table tbody tr').each(function (trNum, tr) {
                $(tr).find('input[name^="Data.Partys["]').each(function (inputNum, input) {
                    var regexp = new RegExp('Data.Partys\\[.*?\\].Childs\\[.*\\](.*)');
                    var childMatch = $(input).attr('name').match(regexp);
                    if (childMatch != null && childMatch.length > 1) {
                        var childName = childMatch[1];
                        $(input).attr('name', 'Data.Partys[' + partyNum + '].Childs[' + trNum + ']' + childName);
                    }
                });
            });
        });
    }
    function updateChildrenCount($party) {
        var len = $party.find('tr.child-in-party').length;
        $party.find('.children-count').html(len);
    }
    $('body').on('click', '.btn-add-child-in-party', function (e) {
        var party = $(e.target).closest('.party');
        if (party.find('.childsByParty').select2('val') === '') {
            return;
        }
        var addingChildId = party.find('.childsByParty').select2('val');
        var childInNotParty = $('.children-not-in-parties-id[value="' + addingChildId + '"]').closest('tr');
        var childInNotPartyFio = childInNotParty.find('.children-not-in-parties-fio').html();
        var childInNotPartyAge = childInNotParty.find('.children-not-in-parties-age').html();
        var childInNotPartyGender = childInNotParty.find('.children-not-in-parties-gender').html();
        var tempFn = doT.template($('#ChildInPartyRowTemplate').html());
        var childTable = party.find('.child-in-party-table tbody');
        childTable.append(tempFn({
            fio: childInNotPartyFio,
            age: childInNotPartyAge,
            gender: childInNotPartyGender,
            id: addingChildId
        }));
        party.find('.child-in-party-table').removeClass('hidden');
        childInNotParty.remove();
        $('.childsByParty option[value="' + addingChildId + '"').remove();
        $('.childsByParty').select2('data', { id: '', text: "-- Не выбрано --" });
        if ($('.children-not-in-parties-id').length === 0) {
            $('.children-not-in-parties-table').addClass('hidden');
            $('.children-not-in-parties-empty').removeClass('hidden');
        }
        updateChildrenCount(party);
        fixPartyNums();
    });
    function removeChildFromparty($child) {
        $child.closest('.child-in-party').remove();
        var tr = $child.closest('tr');
        if (tr.length == 0) {
            return;
        }
        var addingChildId = tr.find('.child-in-party-id').val();
        var childInNotPartyFio = tr.find('.child-in-party-fio').html();
        var childInNotPartyAge = tr.find('.child-in-party-age').html();
        var childInNotPartyGender = tr.find('.child-in-party-gender').html();
        var rowFn = doT.template($('#ChildNotInPartyRowTemplate').html());
        var childTable = $('.children-not-in-parties-table tbody');
        childTable.append(rowFn({
            fio: childInNotPartyFio,
            age: childInNotPartyAge,
            gender: childInNotPartyGender,
            id: addingChildId
        }));
        var optionFn = doT.template($('#ChildNotInPartyOptionTemplate').html());
        $('select.childsByParty').append(optionFn({
            fio: childInNotPartyFio,
            id: addingChildId
        }));
        $('.children-not-in-parties-empty').addClass('hidden');
        $('.children-not-in-parties-table').removeClass('hidden');
    }
    $('body').on('click', '.child-in-party-remove', function (e) {
        var party = $(e.target).closest('.party');
        removeChildFromparty($(e.target));
        updateChildrenCount(party);
        if (party.find('.child-in-party').length === 0) {
            party.find('.child-in-party-table').addClass('hidden');
        }
        else {
            party.find('.child-in-party-table').removeClass('hidden');
        }
        fixPartyNums();
    });
    $('.btn-add-party').click(function () {
        var partyFn = doT.template($('#PartyTemplate').html());
        $('.parties').append(partyFn());
        var optionFn = doT.template($('#ChildNotInPartyOptionTemplate').html());
        var newPartyChildSelect = $('.parties .party').last().find('select.childsByParty');
        newPartyChildSelect.append(optionFn({
            fio: '-- Не выбрано --',
            id: ''
        }));
        $('.children-not-in-parties').each(function (num, val) {
            newPartyChildSelect.append(optionFn({
                fio: $(val).find('.children-not-in-parties-fio').html(),
                id: $(val).find('.children-not-in-parties-id').val()
            }));
        });
        newPartyChildSelect.select2();
        fixPartyNums();
    });
    $('body').on('click', '.remove-party', function (e) {
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите удалить отряд?',
            buttons: [
                {
                    label: 'Удалить',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        var party = $(e.target).closest('.party');
                        party.find('.child-in-party-id').each(function (num, val) {
                            removeChildFromparty($(val));
                        });
                        party.remove();
                        fixPartyNums();
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
    $('#Data_YearOfRestId').change(function () {
        $('#Data_TimeOfRestId').select2('data', { id: '', text: '-- Не выбрано --' });
        $('#Data_LimitOnVedomstvoId').select2('data', { id: '', text: '-- Не выбрано --' });
    });
    $('#Data_ContractId').select2({
        initSelection: function (element, callback) {
            if ($('#_ContractId').val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $('#_ContractId').val(), text: $('#_ContractName').val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebContract/GetByOrganization',
            dataType: 'json',
            data: function (term, page) {
                var limitOnVedomstvo = $('#Data_LimitOnVedomstvoId').select2('data');
                return {
                    signNumber: term,
                    organizationId: null,
                    yearOfRestId: null,
                    onTransport: null,
                    onRest: null
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                results = results.concat($.map(data, function (item) {
                    return {
                        text: item.signNumber + (item.supplier ? (' (' + (item.supplier.shortName ? item.supplier.shortName : item.supplier.name) + ')') : ''),
                        id: item.id
                    };
                }));
                return {
                    results: results
                };
            },
            cache: true
        }
    }).select2('val', [])
        .on('change', function (e) {
        var data = $(e.target).select2('data');
        if (data != null) {
            var dateStart = data.dateStart;
            var dateEnd = data.dateEnd;
            $('#Data_DateIncome').val(dateStart.format('DD.MM.YYYY'));
            $('#Data_DateOutcome').val(dateEnd.format('DD.MM.YYYY'));
        }
    });
    $(document).on('shown.bs.tab', '#MainTabs>ul>li>a[data-toggle="tab"]', function (e) {
        $('#ActiveTab').val($(e.target).attr('href').substring(1));
    });
    $('.service-date-from').focusout(function (e) {
        var tr = $(e.target).closest('tr');
        var dateFrom = $(e.target).val();
        if (!dateFrom) {
            tr.find('.input-group:has(.service-date-from), .input-group:has(.service-date-to)').removeClass('has-error');
            return;
        }
        var dateTo = tr.find('.service-date-to').val();
        if (moment(dateFrom, "DD.MM.YYYY") > moment(dateTo, "DD.MM.YYYY")) {
            tr.find('.input-group:has(.service-date-from), .input-group:has(.service-date-to)').addClass('has-error');
        }
        else {
            tr.find('.input-group:has(.service-date-from), .input-group:has(.service-date-to)').removeClass('has-error');
        }
    });
    $('.service-date-to').focusout(function (e) {
        var tr = $(e.target).closest('tr');
        var dateTo = $(e.target).val();
        if (!dateTo) {
            tr.find('.input-group:has(.service-date-from), .input-group:has(.service-date-to)').removeClass('has-error');
            return;
        }
        var dateFrom = tr.find('.service-date-from').val();
        if (moment(dateFrom, "DD.MM.YYYY") > moment(dateTo, "DD.MM.YYYY")) {
            tr.find('.input-group:has(.service-date-from), .input-group:has(.service-date-to)').addClass('has-error');
        }
        else {
            tr.find('.input-group:has(.service-date-from), .input-group:has(.service-date-to)').removeClass('has-error');
        }
    });
    initTypeOfSubrestrtiction();
});
function loadAvailableChildLists(args) {
    $.ajax({
        url: rootPath + '/api/WebTours/GetAvailableChildLists',
        data: args,
        dataType: 'json'
    }).done(function (received) {
        if (received == null || received.length == 0 || received.length == null) {
            $('#ChildListsAddBody').html('<p class="text-center">Списки детей не найдены </p>');
            return;
        }
        var tempFn = doT.template($('#ChildListsModalTemplate').html());
        var data = $.map(received, function (val) {
            return {
                Id: val.id,
                Vedomstvo: val.limitOnOrganization != null && val.limitOnOrganization.limitOnVedomstvo != null && val.limitOnOrganization.limitOnVedomstvo.organization != null && val.limitOnOrganization.limitOnVedomstvo.organization.name != null ? val.limitOnOrganization.limitOnVedomstvo.organization.name : '-',
                Organization: val.limitOnOrganization != null && val.limitOnOrganization.organization != null && val.limitOnOrganization.organization.name != null ? val.limitOnOrganization.organization.name : '-',
                Name: val.name != null ? val.name : '-',
                ChildsCount: val.countChild != null ? val.countChild : '-',
                AttendantsCount: val.countAttendants != null ? val.countAttendants : '-',
                State: val.state != null && val.state.name != null ? val.state.name : '-'
            };
        });
        $('#ChildListsAddBody').html(tempFn(data));
        $('#ChildListsAddFilter').removeClass('hidden');
    }).fail(function () {
        $('#ChildListsAddBody').html('<p class="text-center">Ошибка получения данных</p>');
    });
}
;
function initTypeOfSubrestrtiction() {
    var $restriction = $('.restriction-select');
    var $subres = $('.subrestriction-select');
    var $div = $('.type-of-subrestriction');
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
//# sourceMappingURL=ToursEdit.js.map