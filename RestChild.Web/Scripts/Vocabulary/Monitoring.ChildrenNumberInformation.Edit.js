$(function () {
    $('.select2').select2();
    $('#Data_YearOfRestId').on('change', function (e) {
        Relocate();
    });
    $('#Data_OrganisationId').on('change', function (e) {
        Relocate();
    });
    function Relocate() {
        var yar = $('#Data_YearOfRestId').val();
        var org = $('#Data_OrganisationId').val();
        window.location.href = '/Monitoring/ChildrenNumberInformation/' + org + '/' + yar;
    }
    $(document).on("click", ".add-hotel", function () {
        $.ajax({
            url: "/Monitoring/ChildrenNumberInformation/HotelAdd",
            type: "GET",
            //data: { },
            success: function (results) {
                var $results = $(results);
                var $cpor = $results.find(".ChooseRegion");
                SetRegions($cpor);
                $results.find(".ChooseHotel").select2({
                    minimumInputLength: 0,
                    ajax: {
                        url: rootPath + 'Api/WebMonitoring/GetMonitoringHotels',
                        dataType: 'json',
                        data: function (term, page) {
                            return {
                                regionId: $cpor.val()
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
                });
                SetDateTimes($results);
                $(".hotels-list").prepend($results);
            }
        });
    });
    $(document).on("click", ".add-tour", function () {
        var $this = $(this);
        var pfx = $this.closest('.hotel').find('.prefix').val();
        $.ajax({
            url: "/Monitoring/ChildrenNumberInformation/TourAdd",
            type: "GET",
            data: { prefix: pfx },
            success: function (results) {
                var $results = $(results);
                SetDateTimes($results);
                $this.closest(".tours").find(".tours-list").prepend($results);
            }
        });
    });
    SetDateTimes($(document));
    function SetDateTimes($input) {
        $input.find('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
        inputMaskDateAnytime($input.find('.input-mask-date-anytime'));
        $input.find(".input-mask-integer").inputmask('integer', { min: 0, max: 100000, rightAlign: false, allowMinus: false });
    }
    ;
    function SetRegions($input) {
        $input.select2({
            minimumInputLength: 0,
            ajax: {
                url: rootPath + 'Api/WebVocabulary/GetStateDistricts',
                dataType: 'json',
                data: function (term, page) {
                    return {};
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
        });
    }
    ;
    $(document).on("click", ".remove-tour", function () {
        $(this).closest(".tour").remove();
    });
    $(document).on("click", ".remove-hotel", function () {
        $(this).closest(".hotel").remove();
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
//# sourceMappingURL=Monitoring.ChildrenNumberInformation.Edit.js.map