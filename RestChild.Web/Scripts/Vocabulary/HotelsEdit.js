$(function () {
    $('#Data_addFileBtn').click(function () {
        $('#Data_addFileModalLoading').addClass('hidden');
        $('#Data_addFileModal').modal("show");
        $('#Data_addFileModal input').val(null);
        $('#Data_addFileModalImg').attr('src', null);
    });
    $(document).on('click', ".photo", function (event) {
        var el = $(event.currentTarget);
        var item = el.closest('.photo');
        var input = $(item.find("input.IsMainPhoto"));
        var curentValue = input.val() && input.val().toString().toLowerCase() === "true";
        var newValue = !curentValue;
        input.val((newValue).toString());
        if (newValue) {
            el.addClass("isMainPhoto");
        }
        else {
            el.removeClass("isMainPhoto");
        }
    });
    $('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('.datepicker').inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    });
    $('#Data_addTypeOfRoomBasePlaces').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('#Data_addTypeOfRoomAddonPlaces').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('#Data_addTypeOfRoomRoomSizePerPerson').inputmask('decimal', {
        allowMinus: false,
        rightAlign: false,
        digits: 2,
        radixPoint: ','
    });
    $('#Data_NumberHousing').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('#Data_addTypeOfRoomMaximumCount').inputmask('integer', { allowMinus: false, rightAlign: false });
    $('#Data_Squere').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
    $('#Data_Phone').inputmask('(999)999-99-99', { clearIncomplete: true });
    $('select').not(".hprices-select").select2();
    //номерной фонд
    $('#RoomPhotoModelUploadedPhotos').on('click', function (event) {
        var target = event.currentTarget;
        var currentTarget = event.target;
        var divPhoto = $(currentTarget).parents('div.photo');
        var curentValue = divPhoto.attr("data-isMainPhoto") === "true";
        var newValue = !curentValue;
        divPhoto.attr("data-isMainPhoto", (newValue).toString());
        if (newValue) {
            divPhoto.addClass("isMainPhoto");
        }
        else {
            divPhoto.removeClass("isMainPhoto");
        }
        divPhoto.next('div.photo-hidden').find('input[name="IsMainPhoto"]').val(newValue.toString());
    });
    $('#Data_addRoomPhotoModalUpload').fileupload({
        dataType: 'json',
        url: '/UploadHotelFile.ashx',
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        maxFileSize: 1000000,
        add: function (e, data) {
            var uploadErrors = [];
            var acceptFileTypes = /^image\/(gif|jpe?g|png)$/i;
            if (data.originalFiles[0]['type'].length && !acceptFileTypes.test(data.originalFiles[0]['type'])) {
                uploadErrors.push('Нельзя загружать файлы выбранного типа');
            }
            if (data.originalFiles[0]['size'] > 1000000) {
                uploadErrors.push('Файл слишком большой. Нельзя загружать файлы больше 1 Мб');
            }
            if (uploadErrors.length > 0) {
                ShowAlert(uploadErrors.join("<br/>"), "alert-danger", "glyphicon-ok", true);
            }
            else {
                $('#Data_addRoomPhotoModalLoading').removeClass('hidden');
                data.submit();
            }
        },
        done: function (e, data) {
            $('#Data_addRoomPhotoModalLoading').addClass('hidden');
            $('#Data_addFileModalSave').removeAttr('disabled');
            var tempFn = doT.template($('#Data_roomPhotoModalTemplate').html());
            var newPhoto = tempFn({
                FileUrl: data.result[0].realname,
                Id: 0,
                FileTypeId: 10,
                HotelId: 0,
                TypeOfRoomsId: 0,
                IsMainPhoto: false
            });
            $('#RoomPhotoModelUploadedPhotos').append(newPhoto);
            $(newPhoto).bind("click", function (event) {
                var el = $(event.currentTarget);
                var photo = el.parent("div.phone");
                var curentValue = photo.attr("data-isMainPhoto") === "true";
                var newValue = !curentValue;
                photo.attr("data-isMainPhoto", (newValue).toString());
                if (newValue) {
                    $(photo).addClass("isMainPhoto");
                }
                else {
                    $(photo).removeClass("isMainPhoto");
                }
            });
        }
    });
    $('#Data_addFileModalUpload').fileupload({
        dataType: 'json',
        url: '/UploadHotelFile.ashx',
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        maxFileSize: 1000000,
        add: function (e, data) {
            var uploadErrors = [];
            var acceptFileTypes = /^image\/(gif|jpe?g|png)$/i;
            if (data.originalFiles[0]['type'].length && !acceptFileTypes.test(data.originalFiles[0]['type'])) {
                uploadErrors.push('Нельзя загружать файлы выбранного типа');
            }
            if (data.originalFiles[0]['size'] > 1000000) {
                uploadErrors.push('Файл слишком большой. Нельзя загружать файлы больше 1 Мб');
            }
            if (uploadErrors.length > 0) {
                ShowAlert(uploadErrors.join("<br/>"), "alert-danger", "glyphicon-ok", true);
            }
            else {
                $('#Data_addFileModalLoading').removeClass('hidden');
                data.submit();
            }
        },
        done: function (e, data) {
            $('#Data_addFileModalLoading').addClass('hidden');
            $('#Data_addFileModalSave').removeAttr('disabled');
            $('#Data_uploadedFileName').val(data.result[0].realname);
            $('#Data_addFileModalImg').attr('src', '/DownloadHotelFile.ashx/' + data.result[0].realname);
        }
    });
    $('#Data_addFileModalSave').click(function () {
        var filetypeInfrastructure = 'Инфраструктура';
        var filetypeInteriors = 'Интерьеры';
        var filetypeTechObjects = 'Технические объекты';
        var filetypeExteriors = 'Экстерьеры';
        var target = '';
        if (!$('#Data_addFileModalForm').valid()) {
            return;
        }
        var group = $('#Data_addFileModalObject option:selected').parent();
        if (group.attr('label') == filetypeInfrastructure) {
            target = '#Data_infrastructurePhotoSet';
        }
        else if (group.attr('label') == filetypeInteriors) {
            target = '#Data_interiorsPhotoSet';
        }
        else if (group.attr('label') == filetypeTechObjects) {
            target = '#Data_techobjPhotoSet';
        }
        else if (group.attr('label') == filetypeExteriors) {
            target = '#Data_exteriorsPhotoSet';
        }
        ;
        var tempFn = doT.template($('#Data_photoTemplate').html());
        $(target).append(tempFn({
            FileUrl: $('#Data_uploadedFileName').val(),
            FileName: $('#Data_addFileModalName').val(),
            FileTypeId: $('#Data_addFileModalObject').val(),
            FileTypeParentId: 0,
            FileTypeName: '',
            Id: 0
        }));
        $('#Data_addFileModal').modal("hide");
    });
    $('#hotelsForm').submit(function () {
        var photos = $('#Files .photo-hidden');
        $.each(photos, function (i, photo) {
            var hiddens = $(photo).find('input[type="hidden"]');
            $.each(hiddens, function (j, hidden) {
                var dotPosition = $(hidden).attr('name').lastIndexOf('.');
                var name = dotPosition != -1 ? $(hidden).attr('name').substring(dotPosition + 1) : $(hidden).attr('name');
                $(hidden).attr('name', 'Data.Files[' + i + '].' + name);
            });
        });
        $('#addContactBtn, .edit-contact, .delete-contact').removeClass('hidden');
        $('#ContactsTable .for-remove').removeClass('for-remove').removeClass('hidden');
        $('#ContactsTable .new-contact').remove();
        var contacts = $('#ContactsTable tbody tr');
        $.each(contacts, function (i, type) {
            var hiddens = $(type).find('input[type="hidden"]');
            $.each(hiddens, function (j, hidden) {
                var dotPosition = $(hidden).attr('name').lastIndexOf('.');
                var name = dotPosition !== -1 ? $(hidden).attr('name').substring(dotPosition + 1) : $(hidden).attr('name');
                $(hidden).attr('name', 'Data.Contacts[' + i + '].' + name);
            });
        });
        var tableRows = $('#Data_typeOfRoomsTable tbody tr');
        var i = 0;
        for (i; i <= tableRows.length; i++) {
            var hiddens = $(tableRows[i]).find('div.room-hidden input[type="hidden"]');
            $.each(hiddens, function (j, hidden) {
                var dotPosition = $(hidden).attr('name').lastIndexOf('.');
                var name = dotPosition !== -1 ? $(hidden).attr('name').substring(dotPosition + 1) : $(hidden).attr('name');
                $(hidden).attr('name', 'Data.TypeOfRooms[' + i + '].' + name);
            });
            var roomPhotos = $(tableRows[i]).find('div.photo-hidden');
            $.each(roomPhotos, function (photoCnt, photo) {
                var photoHiddens = $(photo).find('input[type = "hidden"]');
                $.each(photoHiddens, function (photoHiddenCnt, hidden) {
                    var dotPosition = $(hidden).attr('name').lastIndexOf('.');
                    var name = dotPosition !== -1 ? $(hidden).attr('name').substring(dotPosition + 1) : $(hidden).attr('name');
                    $(hidden).attr('name', 'Data.TypeOfRooms[' + i + '].Files[' + photoCnt + '].' + name);
                });
            });
        }
        if ($(".isCampingCheck.visibleCheckbox").prop("disabled")) {
            $(".isCampingCheck.hidden").prop("checked", false);
        }
        else {
            if ($(".isCampingCheck.visibleCheckbox").prop("checked")) {
                $(".isCampingCheck.hidden").prop("checked", true);
            }
            else {
                $(".isCampingCheck.hidden").prop("checked", false);
            }
        }
        $('#hotelsForm').unbind();
        $('#hotelsForm').submit();
    });
    $('body').on('click', '.remove-photo', function (e) {
        var photo = $(e.target).closest('.photo');
        $(photo.remove());
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
    var $editTypeOfRooms = null;
    $('#Data_addRoomBtn').click(function () {
        $('#Data_addTypeOfRoomModal').modal("show");
        $('#Data_addTypeOfRoomModal input').val(null);
        $('#Data_addTypeOfRoomModal input:checkbox').prop('checked', false);
        $editTypeOfRooms = null;
        $('#RoomPhotoModelUploadedPhotos').empty();
    });
    function createHiddenFieldsObject(elem) {
        var e = $(elem);
        return {
            Id: e.find('input[name="Id"]').val(),
            FileTypeId: e.find('input[name="FileTypeId"]').val(),
            FileUrl: e.find('input[name="FileUrl"]').val(),
            IsMainPhoto: e.find('input.IsMainPhoto').val(),
            HotelId: e.find('input[name="HotelId"]').val(),
            TypeOfRoomsId: e.find('input[name="TypeOfRoomsId"]').val()
        };
    }
    $('body').on('click', '.updateTypeOfRooms', function (e) {
        $editTypeOfRooms = $(e.target).closest('tr');
        $('#Data_addTypeOfRoomName').val($editTypeOfRooms.find('input[name="Name"]').val());
        $('#Data_addTypeOfRoomBasePlaces').val($editTypeOfRooms.find('input[name="CountBasePlace"]').val());
        $('#Data_addTypeOfRoomAddonPlaces').val($editTypeOfRooms.find('input[name="CountAddonPlace"]').val());
        $('#Data_addTypeOfRoomRoomSizePerPerson').val($editTypeOfRooms.find('input[name="RoomSizePerPerson"]').val());
        $('#Data_addTypeOfRoomMaximumCount').val($editTypeOfRooms.find('input[name="MaximumCount"]').val());
        var modalRoomUploadedPhotos = $('#RoomPhotoModelUploadedPhotos');
        modalRoomUploadedPhotos.empty();
        $editTypeOfRooms.find('div.photo-hidden').each(function (i, elem) {
            var tFunc = doT.template($('#Data_roomPhotoModalTemplate').html());
            var obj = createHiddenFieldsObject($(elem));
            obj.ClassName = obj.IsMainPhoto == 'true' ? 'isMainPhoto' : '';
            modalRoomUploadedPhotos.append(tFunc(obj));
        });
        var checkboxes = $('#Data_addTypeOfRoomModal input[type="checkbox"]');
        $.each(checkboxes, function (i, val) {
            $('#Data_addTypeOfRoom' + $(val).attr('name')).prop('checked', $editTypeOfRooms.find('input[name="' + $(val).attr('name') + '"]').val() == 'True');
        });
        $('#Data_addTypeOfRoomModal').modal("show");
    });
    $('#Data_addTypeOfRoomModal').on("hidden.bs.modal", function () {
        $editTypeOfRooms = null;
    });
    $('#Data_addTypeOfRoomSave').click(function () {
        var addPhotosToMainWindow = function (row) {
            var lastTd = row.find('td:last-child');
            lastTd.find('div.photo-hidden').remove();
            var tFunc = doT.template($('#Data_roomPhotoTemplate').html());
            $('#RoomPhotoModelUploadedPhotos div.photo-hidden').each(function (i, elem) {
                var query = $(elem);
                var isMainPhotoAttr = query.closest("[data-isMainPhoto]").attr('data-isMainPhoto');
                var obj = createHiddenFieldsObject(query);
                var content1 = tFunc(obj);
                lastTd.append($(content1));
            });
        };
        if (!$('#Data_addTypeOfRoomModal input').valid()) {
            return;
        }
        var conveniences = $('#Data_addTypeOfRoomConveniences input:checked');
        var conveniencesStr = '';
        var conviencesArray = [];
        $.each(conveniences, function (i, val) {
            if (conveniencesStr != '') {
                conveniencesStr += ', ';
            }
            conveniencesStr += $(val).attr('data-text');
            conviencesArray.push({
                Name: $(val).attr('name'),
                Value: 'True'
            });
        });
        var tempFn = doT.template($('#Data_typeOfRoomsTemplate').html());
        var newRow = $(tempFn({
            Id: $editTypeOfRooms ? $editTypeOfRooms.find('input[name="Id"]').val() : '0',
            Name: $('#Data_addTypeOfRoomName').val(),
            CountBasePlace: $('#Data_addTypeOfRoomBasePlaces').val(),
            CountAddonPlace: $('#Data_addTypeOfRoomAddonPlaces').val(),
            RoomSizePerPerson: $('#Data_addTypeOfRoomRoomSizePerPerson').val(),
            MaximumCount: $('#Data_addTypeOfRoomMaximumCount').val(),
            Conveniences: conveniencesStr,
            ConviencesArray: conviencesArray
        }));
        if ($editTypeOfRooms) {
            $editTypeOfRooms.replaceWith(newRow);
        }
        else {
            $('#Data_typeOfRoomsTable tbody').append(newRow);
        }
        $('#Data_addTypeOfRoomModal').modal("hide");
        addPhotosToMainWindow(newRow);
        $('#RoomPhotoModelUploadedPhotos').empty();
    });
    $('body').on('click', '.deleteTypeOfRooms', function (e) {
        $($(e.target).closest('tr')).remove();
    });
    $('#Data_HotelTypeId').change(function (e) {
        if ($(e.target).select2('val') != hotelTypeId_Hotel) {
            $('#RoomsTab').addClass('hidden');
        }
        else {
            $('#RoomsTab').removeClass('hidden');
        }
    });
    $(document).on('shown.bs.tab', '#MainTabs a[data-toggle="tab"]', function (e) {
        $('#ActiveTab').val($(e.target).attr('href').substring(1));
    });
    $('#addContactBtn').click(function (e) {
        $(e.target).addClass('hidden');
        $('.edit-contact, delete-contact').addClass('hidden');
        var tempFn = doT.template($('#ContactEditRowTemplate').html());
        $('#ContactsTable tbody').append(tempFn({
            id: '0',
            lastName: '',
            firstName: '',
            middleName: '',
            position: '',
            phone: ''
        }));
        $('#ContactsTable tbody .new-contact .contact-edit-phone').inputmask('(999)999-99-99', { clearIncomplete: true });
    });
    $(document).on('click', '.edit-contact', function (e) {
        $('#addContactBtn, .edit-contact, .delete-contact').addClass('hidden');
        var tr = $(e.target).closest('tr');
        var tempFn = doT.template($('#ContactEditRowTemplate').html());
        tr.before(tempFn({
            id: tr.find('.contact-edit-id').val(),
            lastName: tr.find('.contact-edit-lastname').val(),
            firstName: tr.find('.contact-edit-firstname').val(),
            middleName: tr.find('.contact-edit-middlename').val(),
            position: tr.find('.contact-edit-position').val(),
            phone: tr.find('.contact-edit-phone').val()
        }));
        $('#ContactsTable tbody .new-contact .contact-edit-phone').inputmask('(999)999-99-99', { clearIncomplete: true });
        tr.addClass('for-remove').addClass('hidden');
    });
    $(document).on('click', '.cancel-edit-contact', function (e) {
        var tr = $(e.target).closest('tr');
        tr.closest('table').find('.for-remove').removeClass('for-remove').removeClass('hidden');
        tr.remove();
        $('#addContactBtn, .edit-contact, .delete-contact').removeClass('hidden');
    });
    $(document).on('click', '.update-contact', function (e) {
        var tr = $(e.target).closest('tr');
        if (!tr.find('input').valid()) {
            return;
        }
        var tempFn = doT.template($('#ContactRowTemplate').html());
        tr.closest('table').find('.for-remove').remove();
        tr.replaceWith(tempFn({
            id: tr.find('.contact-edit-id').val(),
            lastName: tr.find('.contact-edit-lastname').val(),
            firstName: tr.find('.contact-edit-firstname').val(),
            middleName: tr.find('.contact-edit-middlename').val(),
            position: tr.find('.contact-edit-position').val(),
            phone: tr.find('.contact-edit-phone').val()
        }));
        $('#addContactBtn, .edit-contact, .delete-contact').removeClass('hidden');
    });
    $(document).on('click', '.delete-contact', function (e) {
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: 'Вы действительно хотите удалить контактное лицо?',
            buttons: [
                {
                    label: 'Удалить',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        $(e.target).closest('tr').remove();
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
    function fixAccomodationTable() {
        $('#AccomodationTable .accomodation-tr').each(function (num, val) {
            $(val).find('input[name^="Data.Accommodation["]').each(function (inputNum, input) {
                var regexp = new RegExp('Data.Accommodation\\[.*?\\](.*)');
                var matched = $(input).attr('name').match(regexp);
                if (matched != null && matched.length > 1) {
                    var name = matched[1];
                    $(input).attr('name', 'Data.Accommodation[' + num + ']' + name);
                }
            });
            $(val).find('.accomodation-child-tr').each(function (trNum, tr) {
                $(tr).find('input[name^="Data.Accommodation["]').each(function (inputNum, input) {
                    var regexp = new RegExp('Data.Accommodation\\[.*?\\].AccommodationChildren\\[.*\\](.*)');
                    var childMatch = $(input).attr('name').match(regexp);
                    if (childMatch != null && childMatch.length > 1) {
                        var childName = childMatch[1];
                        $(input).attr('name', 'Data.Accommodation[' + num + '].AccommodationChildren[' + trNum + ']' + childName);
                    }
                });
            });
        });
    }
    $(document).on('click', '.add-accomodation-child', function (e) {
        var div = $(e.target).closest('.accomodation-tr');
        var table = div.find('.accoomodation-child-table');
        var templateFn = doT.template($('#AccomodationChildTemplate').html());
        var newElem = templateFn({
            id: '0',
            accomodationId: div.find('.accomodation-id').val(),
            ageFrom: '0',
            ageTo: '0',
            countChildren: '0'
        });
        table.find('tbody').append(newElem);
        table.removeClass('hidden');
        table.find('tbody').last().find('.age, .count').inputmask("integer", { allowMinus: false, rightAlign: false });
        fixAccomodationTable();
    });
    $(document).on('click', '.add-accomodation', function (e) {
        var table = $('#AccomodationTable');
        var templateFn = doT.template($('#AccomodationTemplate').html());
        var newElem = templateFn({
            id: '0',
            hotelId: $('#Data_Id').val(),
            name: '',
            adult: '0'
        });
        table.append(newElem);
        table.last().find('.age, .count').inputmask("integer", { allowMinus: false, rightAlign: false });
        fixAccomodationTable();
    });
    $(document).on('click', '.remove-accomodation', function (e) {
        $(e.target).closest('.accomodation-tr').remove();
        fixAccomodationTable();
    });
    $(document).on('click', '.remove-accomodation-child', function (e) {
        var table = $(e.target).closest('table');
        if (table.find('.accomodation-child-tr').length === 1) {
            table.addClass('hidden');
        }
        $(e.target).closest('tr').remove();
        fixAccomodationTable();
    });
    function fixDiningTable() {
        $('#DiningOptionsTable tbody tr').each(function (num, val) {
            $(val).find('input[name^="Data.DiningOptions["]').each(function (inputNum, input) {
                var regexp = new RegExp('Data.DiningOptions\\[.*?\\](.*)');
                var matched = $(input).attr('name').match(regexp);
                if (matched != null && matched.length > 1) {
                    var name = matched[1];
                    $(input).attr('name', 'Data.DiningOptions[' + num + ']' + name);
                }
            });
        });
    }
    $(document).on('click', '#DiningOptionAdd', function (e) {
        var tempFn = doT.template($('#DiningOptionTemplate').html());
        $('#DiningOptionsTable').removeClass('hidden');
        $('#DiningOptionsTable tbody').append(tempFn({
            name: '',
            id: '0',
            hotelId: $('#Data_Id').val()
        }));
        fixDiningTable();
    });
    $(document).on('click', '.remove-dining', function (e) {
        var tbody = $(e.target).closest('tbody');
        $(e.target).closest('tr').remove();
        if (tbody.find('tr').length === 0) {
            tbody.closest('table').addClass('hidden');
        }
        fixDiningTable();
    });
    function IsCampingCheck() {
        if ($("select.hotelType").select2("val") == hotelTypeId_Hotel) {
            $(".isCampingCheckContainer").addClass("hidden");
            $(".isCampingCheck.hidden").prop("checked", false);
            $(".isCampingCheck.visibleCheckbox").prop("checked", false);
        }
        else {
            $(".isCampingCheckContainer").removeClass("hidden");
        }
    }
    IsCampingCheck();
    $("select.hotelType").on("change", function () {
        IsCampingCheck();
    });
    $('.age, .count').inputmask("integer", { allowMinus: false, rightAlign: false });
});
//# sourceMappingURL=HotelsEdit.js.map