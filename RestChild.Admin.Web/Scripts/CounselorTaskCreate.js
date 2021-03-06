var appended = {};
var countWorker = 0;
function getKeyRow(row) {
    return 'ct' + row.find('.CoworkerType').val() + 'pi' + row.find('.PerformerId').val();
}
function processRows(body, startIndex) {
    var childs = body.find('tr');
    var regExp = new RegExp("\[[0-9]+\].", "g");
    for (var i = 0; i < childs.length; i++) {
        $(childs[i]).find('input').each(function () {
            if ($(this).attr('name')) {
                $(this).attr('name', $(this).attr('name').replace(regExp, '[' + (startIndex + i).toString() + '].'));
            }
        });
    }
    if (childs.length === 1) {
        body.find('.empty-row').show();
    }
    else {
        body.find('.empty-row').hide();
    }
}
function removeWorker(e) {
    var target = $(e.target);
    var parent = target.parent().parent();
    var key = getKeyRow(parent);
    parent.remove();
    processRows($('#PerformersTable tbody'), -1);
    delete appended[key];
    countWorker--;
}
function addWorker(e) {
    var target = $(e.target);
    addWorkerInteranl(target);
}
function addWorkerAll(e) {
    var target = $(e.target).parent().parent();
    target.find('button.add-one').each(function (i, e) {
        addWorkerInteranl($(e));
    });
}
function addWorkerInteranl(target) {
    var parent = target.parent().parent();
    var row = $($('#rowPefomance').html());
    row.find('.fio').html(parent.find('.coworker-dialog-name').html());
    row.find('.type').html(parent.find('.coworker-type-str').val());
    row.find('.bout').html(parent.find('.coworker-bout').val());
    row.find('.boutId').val(parent.find('.bout-id').val());
    row.find('.partyId').val(parent.find('.party-id').val());
    row.find('.CoworkerType').val(parent.find('.coworker-type').val());
    row.find('.PerformerId').val(parent.find('.coworker-id').val());
    row.find('.remove-button').click(removeWorker);
    var key = getKeyRow(row);
    if (!appended[key]) {
        $('#PerformersTable tbody').append(row);
        appended[key] = true;
        processRows($('#PerformersTable tbody'), -1);
        countWorker++;
    }
    else {
        ShowAlert('?????????????????? ?????? ???????????????? ?? ????????????', "alert-danger", "glyphicon-ok", true);
    }
}
function searchCoworkers(pageIndex, notHide) {
    var data = {
        HotelTypeId: $('#searchFilter .HotelTypeId').select2('val'),
        HotelId: $('#searchFilter .HotelId').select2('val'),
        YearOfRestId: $('#searchFilter .YearOfRestId').select2('val'),
        GroupedTimeOfRestId: $('#searchFilter .GroupedTimeOfRestId').select2('val'),
        Name: $('#searchFilter .Name').val(),
        SubjectOfRestId: $('#searchFilter .SubjectOfRestId').select2('val'),
        CoworkerType: $('#searchFilter .CoworkerType').select2('val'),
        PageNumber: pageIndex + 1,
        ParentTaskId: $('#searchFilter .ParentTaskId').val()
    };
    $("#coworkersDiv").html("<div align=\"center\"><img src=\"/Content/images/spinner.gif\" /> ????????????????</div>");
    $.ajax({
        type: "POST",
        url: rootPath + "/CounselorTask/GetCoworkers",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (!notHide) {
            $('#searchFilter').collapse('hide');
        }
        $("#coworkersDiv").html(data);
        $("#coworkersDiv").find('button.add-one').click(addWorker);
        $("#coworkersDiv").find('button.add-all').click(addWorkerAll);
    }).fail(function (data) {
        $("#coworkersDiv").html("???????????? ????????????????");
    });
}
$(function () {
    $('select').select2();
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $("#coworkersDiv").find('button.add-one').click(addWorker);
    $("#coworkersDiv").find('button.add-all').click(addWorkerAll);
    var fileRowFn = doT.template($('#fileRowTemplate').html());
    $('#btnCreateTask').click(function (e) {
        var isError = false;
        if (countWorker === 0) {
            ShowAlert('?????? ???????????????? ?????????????? ???????????????????? ?????????????? ??????????????????????', "alert-danger", "glyphicon-ok", true);
            isError = true;
        }
        var el = $('#body');
        var code = el.code();
        if (!$('#mainFieldSet .DateTaskFld').val()) {
            ShowAlert('?????? ???????????????? ?????????????? ???????????????????? ?????????????? ???????? ???????????????????? ????????????', "alert-danger", "glyphicon-ok", true);
            isError = true;
        }
        if (!$('#mainFieldSet .TitleFld').val()) {
            ShowAlert('?????? ???????????????? ?????????????? ???????????????????? ?????????????? ???????? ????????????', "alert-danger", "glyphicon-ok", true);
            isError = true;
        }
        if (!code) {
            ShowAlert('?????? ???????????????? ?????????????? ???????????????????? ?????????????? ????????????', "alert-danger", "glyphicon-ok", true);
            isError = true;
        }
        if ($('#mainFieldSet .DeadlineFld').length > 0 && !$('#mainFieldSet .DeadlineFld').val()) {
            ShowAlert('?????? ???????????????? ?????????????? ???????????????????? ?????????????? ???????? ???????????????????? ????????????', "alert-danger", "glyphicon-ok", true);
            isError = true;
        }
        var dtp = $('.DateTaskFld').data("DateTimePicker");
        var dlp = $('.DeadlineFld').data('DateTimePicker');
        if (dtp && dlp && dtp.date().isAfter(dlp.date())) {
            ShowAlert('???????? ???????????????????? ???? ?????????? ???????? ???????????? ?????? ???????? ???????????????????? ????????????', "alert-danger", "glyphicon-ok", true);
            isError = true;
        }
        if (!isError) {
            $('#mainFieldSet .ContentFld').val(code);
            $('form').submit();
        }
    });
    function removeFile(e) {
        var $tbody = $(this).parent().parent().parent();
        var $row = $(this).parent().parent();
        $row.remove();
        processRows($('#filesTable tbody'), 0);
    }
    $('.request-file-remove').click(removeFile);
    $('.fileinput-button').each(function () {
        var dropZone = $(this).find('.panel-file');
        var realName = '';
        var fu = $(this).fileupload({
            url: rootPath + '/UploadTaskFile.ashx',
            dataType: 'json',
            pasteZone: null,
            dropZone: null,
            maxChunkSize: 1000000,
            beforeSend: function (xhr, data) {
                xhr.setRequestHeader("X-FileName", realName);
            },
            submit: function (e, data) {
                var target = $(e.target);
                var parent = $(target.parent().parent().parent()[0]);
                parent.find('.file-upload-div').addClass('hidden');
                parent.find('.file-uploading-div').removeClass('hidden');
            },
            always: function (e, data) {
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
                    processRows($('#filesTable tbody'), 0);
                });
            },
        });
        fu.on('fileuploadchunkdone', function (e, data) {
            $.each(data.result, function (index, file) {
                realName = file.realname;
            });
        });
    });
    $('#searchFilter .HotelId').select2({
        initSelection: function (element, callback) {
            if ($('#_HotelsId').val() == '') {
                callback({ id: '', text: '-- ???? ?????????????? --' });
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
                var hotelType = $('#searchFilter .HotelTypeId').select2('val');
                return {
                    name: term,
                    onlyApproved: 'True',
                    hotelType: hotelType ? hotelType : null
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- ???? ?????????????? --' }];
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
    });
    var yearOfRestId = $('#searchFilter .YearOfRestId').select2('val');
    $(document).on('click', '.dialog-search-button', function () { searchCoworkers(0, false); });
    $(document).on('click', '.dialog-clear-button', function () {
        $('#searchFilter .HotelTypeId').select2('val', null);
        $('#searchFilter .HotelId').select2('data', { text: '-- ???? ?????????????? --' });
        $('#searchFilter .YearOfRestId').select2('val', yearOfRestId);
        $('#searchFilter .GroupedTimeOfRestId').select2('val', null);
        $('#searchFilter .Name').val('');
        $('#searchFilter .SubjectOfRestId').select2('val', null);
        $('#searchFilter .CoworkerType').select2('val', null);
        searchCoworkers(0, true);
    });
});
//# sourceMappingURL=CounselorTaskCreate.js.map