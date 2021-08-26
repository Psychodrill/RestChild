$(() => {
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
        let yar = $('#Data_YearOfRestId').val();
        let org = $('#Data_OrganisationId').val();
        let mon = $('#Data_Month').val();
        window.location.href = '/Monitoring/SmallLeisureInfo/' + org + '/' + yar + '/' + mon;
    }

    $(document).on("click",
        ".add-slid",
        function () {
            var gbu = $("#GBUs").val();

            var ek = $('.slid-gbu').map(function () {
                return $(this).val();
            }).get();

            if (parseInt(gbu) > 0 && (ek.indexOf(gbu) < 0)) {
                $.ajax({
                    url: "/Monitoring/SmallLeisureInfo/GBUAdd",
                    type: "GET",
                    data: {GBUId: gbu},
                    success: function (results) {
                        const $results = $(results);

                        $(".slid-list").prepend($results);


                    }
                });
            }
            $("#GBUs").val('-1').trigger('change');
        });

    $(document).on("click", ".remove-slid", function () {
        $(this).closest(".slid").remove();
    });

    let $fileInput = $('#fileUploader');
    $fileInput.on('change', (e) => {
        let t;
        t = e.target;
        if (t.files && t.files.length > 0) {
            const data = new FormData()
            for (let i = 0; i < t.files.length; i++) {
                data.append('files[' + i.toString() + ']', t.files[i]);
            }
            $('#LoadResultDialog').remove();
            $('body').append($('#LoadResultDialogTemplate').html());
            let $loadResultDialog =$('#LoadResultDialog');
            $loadResultDialog.modal('show');
            $loadResultDialog.on('hidden.bs.modal', ()=> {
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
                    let tempFn = doT.template($('#LoadResultDialogRowTemplate').html());
                    $('#LoadResultDialog .modal-body').html(tempFn(data));
                }
            });
        } else {
            ShowAlert("Укажите файлы для загрузки", "alert-danger", "glyphicon-remove");
        }

        let $newItem = $fileInput.val('').clone(true);
        $fileInput.replaceWith($newItem);
        $fileInput = $newItem;
    });

    $('#fileToUpload').on('click', (e) => {
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
            let realName = "";
            const fu = $(this).fileupload({
                url: rootPath + "/UploadPupilFile.ashx",
                dataType: "json",
                pasteZone: null,
                dropZone: null,
                maxChunkSize: 1000000,
                beforeSend: (xhr) => {
                    xhr.setRequestHeader("X-FileName", realName);
                },
                submit: (e) => {
                    var target = $(e.target);
                    var parent = $(target.parent().parent().parent()[0]);
                    parent.find(".file-upload-div").addClass("hidden");
                    parent.find(".file-uploading-div").removeClass("hidden");
                },
                always: (e) => {
                    var target = $(e.target);
                    var parent = $(target.parent().parent().parent()[0]);
                    parent.find(".file-upload-div").removeClass("hidden");
                    parent.find(".file-uploading-div").addClass("hidden");
                },
                done: (e, data) => {
                    realName = "";
                    $.each(data.result,
                        (index, file) => {
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

            fu.on("fileuploadchunkdone",
                (e, data) => {
                    $.each(data.result,
                        (index, file) => {
                            realName = file.realname;
                        });
                });
        });
    }
});
