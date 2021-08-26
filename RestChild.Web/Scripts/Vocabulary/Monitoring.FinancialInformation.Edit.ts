$(() => {
    $('.select2').select2();


    $('#Data_YearOfRestId').on('change', function (e) {
        Relocate();
    });
    $('#Data_OrganisationId').on('change', function (e) {
        Relocate();
    });

    function Relocate() {
        console.log("works");
        let yar = $('#Data_YearOfRestId').val();
        let org = $('#Data_OrganisationId').val();
        window.location.href = '/Monitoring/FinanceInformation/' + org + '/' + yar;
    }

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
