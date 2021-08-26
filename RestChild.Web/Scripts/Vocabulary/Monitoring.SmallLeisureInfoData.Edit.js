$(function () {
    $('.select2').select2();
    $('#Data_YearOfRestId').on('change', function (e) {
        Relocate();
    });
    $('#Data_Month').on('change', function (e) {
        Relocate();
    });
    $('#Data_OrganisationId').on('change', function (e) {
        Relocate();
    });
    function Relocate() {
        var yar = $('#Data_YearOfRestId').val();
        var org = $('#Data_OrganisationId').val();
        var mon = $('#Data_Month').val();
        window.location.href = '/Monitoring/SmallLeisureInfo/' + org + '/' + yar + '/' + mon;
    }
    $(document).on("click", ".add-slid", function () {
        var gbu = $("#GBUs").val();
        var ek = $('.slid-gbu').map(function () {
            return $(this).val();
        }).get();
        if (parseInt(gbu) > 0 && (ek.indexOf(gbu) < 0)) {
            $.ajax({
                url: "/Monitoring/SmallLeisureInfo/GBUAdd",
                type: "GET",
                data: { GBUId: gbu },
                success: function (results) {
                    var $results = $(results);
                    $(".slid-list").prepend($results);
                }
            });
        }
        $("#GBUs").val('-1').trigger('change');
    });
    $(document).on("click", ".remove-slid", function () {
        $(this).closest(".slid").remove();
    });
    var $fileInput = $('#fileUploader');
    $fileInput.on('change', function (e) {
        var t;
        t = e.target;
        if (t.files && t.files.length > 0) {
            var data = new FormData();
            for (var i = 0; i < t.files.length; i++) {
                data.append('files[' + i.toString() + ']', t.files[i]);
            }
            $('#LoadResultDialog').remove();
            $('body').append($('#LoadResultDialogTemplate').html());
            var $loadResultDialog = $('#LoadResultDialog');
            $loadResultDialog.modal('show');
            $loadResultDialog.on('hidden.bs.modal', function () {
                Relocate();
            });
            $.ajax({
                url: rootPath + '/Monitoring/SaveSmallLeisureInfoFiles?yearOfRestId='
                    + $('#Data_YearOfRestId').select2('val')
                    + '&month=' + $('#Data_Month').select2('val')
                    + '&organisationId=' + $('#Data_OrganisationId').select2('val'),
                data: data,
                processData: false,
                contentType: false,
                type: 'POST',
                success: function (data) {
                    var tempFn = doT.template($('#LoadResultDialogRowTemplate').html());
                    $('#LoadResultDialog .modal-body').html(tempFn(data));
                }
            });
        }
        else {
            ShowAlert("Укажите файлы для загрузки", "alert-danger", "glyphicon-remove");
        }
        var $newItem = $fileInput.val('').clone(true);
        $fileInput.replaceWith($newItem);
        $fileInput = $newItem;
    });
    $('#fileToUpload').on('click', function (e) {
        $fileInput.trigger('click');
    });
    $("form").on("click", ".request-file-remove", function () {
        $(this).closest("tr").remove();
    });
    var template = $("#fileRowTemplate").html();
    if (template && template !== "") {
        var fileRowFn = doT.template(template);
        function removeFile(e) {
            $(e.target).closest("li").remove();
        }
        $("#filesTable .file-remove").click(removeFile);
        $(".fileinput-button").each(function () {
            var realName = "";
            var fu = $(this).fileupload({
                url: rootPath + "/UploadPupilFile.ashx",
                dataType: "json",
                pasteZone: null,
                dropZone: null,
                maxChunkSize: 1000000,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("X-FileName", realName);
                },
                submit: function (e) {
                    var target = $(e.target);
                    var parent = $(target.parent().parent().parent()[0]);
                    parent.find(".file-upload-div").addClass("hidden");
                    parent.find(".file-uploading-div").removeClass("hidden");
                },
                always: function (e) {
                    var target = $(e.target);
                    var parent = $(target.parent().parent().parent()[0]);
                    parent.find(".file-upload-div").removeClass("hidden");
                    parent.find(".file-uploading-div").addClass("hidden");
                },
                done: function (e, data) {
                    realName = "";
                    $.each(data.result, function (index, file) {
                        var target = $(e.target);
                        var parent = $(target.parent().parent().parent()[0]);
                        var tbody = parent.find("#filesTable tbody");
                        var $table = parent.find("#filesTable");
                        var entity = {
                            fileTypesIndex: parent.find(".index-hidden").val(),
                            fileTitle: file.name,
                            fileName: file.realname,
                            tableName: $table.attr("tablename"),
                            fileIndex: parent.find("table tbody tr").length
                        };
                        var row = $(fileRowFn(entity));
                        row.find(".request-file-remove").click(removeFile);
                        tbody.append(row);
                    });
                }
            });
            fu.on("fileuploadchunkdone", function (e, data) {
                $.each(data.result, function (index, file) {
                    realName = file.realname;
                });
            });
        });
    }
});
//# sourceMappingURL=Monitoring.SmallLeisureInfoData.Edit.js.map