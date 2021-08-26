declare var typeOfRestrictionSubs;
declare var checkSNILSUrl;

let initedDuo = false;

$(() => {
    $(".datepicker-anytime").datetimepicker({ showTodayButton: true, format: "DD.MM.YYYY" });
    $("select").select2();
    setStreetAR($(".street-autocompleteAR").not("inited").addClass("inited"));
    setSchoolAR($(".school-autocompleteAR").not("inited").addClass("inited"));
    documentTypeSetMask($('#Data_Child_DocumentTypeId').select2('val'));
    drugsTableHeadVisibility();

    $(".OrphanagePupilFormSave").click((e) => {
        $(e.target).prop("disabled", true);
        $("#OrphanagePupilForm").submit();
    });

    $(".restriction-block").each((i, e) => {
        initTypeOfSubrestrtiction($(e));
    });

    $("form").on("change", ".No-Middle-Name", function () {
        const closest_midname = $(this).closest(".form-horizontal").find(".Middle-Name");
        if (this.checked) {
            $(closest_midname).val("");
            $(closest_midname).prop("readonly", true);
        } else {
            $(closest_midname).prop("readonly", false);
        }
        });

    $("form").on("change", ".school-not-present", function () {
        const closest_SchoolId = $(this).closest("fieldset").find(".SchoolId");
        const closest_SchoolManual = $(this).closest("fieldset").find(".SchoolManual");
        const closest_SchoolManual_g = $(this).closest("fieldset").find(".school-manual");
        //let $target = $(event.target);

        if ($(this).is(":checked")) {
            closest_SchoolId.select2("data", { id: "", text: "-- Не выбрано--" });
            closest_SchoolManual_g.removeClass("hidden");
        } else {
            closest_SchoolManual.val("");
            closest_SchoolManual_g.addClass("hidden");
        }
    });

    $("form").on("change", ".PupilIsOut", (e) => {
        let DateOut = $(e.target).closest("fieldset").find("#Data_DateOut");
        let OrganisationOut = $(e.target).closest("fieldset").find("#Data_OrganisationOut");
        let $target = $(e.target);

        if ($target.is(":checked")) {
            DateOut.closest(".form-group").removeClass("hidden");
            OrganisationOut.closest(".form-group").removeClass("hidden");
        } else {
            DateOut.val("");
            OrganisationOut.val("");
            DateOut.closest(".form-group").addClass("hidden");
            OrganisationOut.closest(".form-group").addClass("hidden");
        }
    });

    $("form").on("change", ".child-is-invalid", (event) => {
            const $target = $(event.target);
            const $e = $(event.target).closest(".restriction-block");

            const $restriction = $e.find(".restriction-select");
            const $subres = $e.find(".subrestriction-select");
            const $div = $e.find(".type-of-subrestriction");

            if (initedDuo) {
                $restriction.select2("data", { id: "", text: "-- Не выбрано--" });
                $subres.select2("data", { id: "", text: "-- Не выбрано--" });
            }

            if ($target.is(":checked")) {
                $e.find(".type-of-restriction, .benefit-group-invalid").removeClass("hidden");
            } else {
                $e.find(".type-of-restriction, .benefit-group-invalid").addClass("hidden");
                $div.addClass("hidden");
            }
    });

    $("form").on("click", ".request-file-remove", function () {
        $(this).closest("tr").remove();
    });

    $("form").on("click", ".btn-snils-check-link", function () {
        let button = $(this);
        let pid = button.attr('data-id');
        $.ajax({
            url: checkSNILSUrl,
            type: "GET",
            data: { pupilId: pid },
            success: function (results) {
                button.parent().find("button").first().trigger("click");
                //button.remove();
            }
        });
    });

    $("form").on("click", ".fileImageinput-button", function () {

        let realName = "";

        let tmpl = doT.template(templateImage);

        const fu = $(this).fileupload({
            url: rootPath + "/UploadPupilFile.ashx",
            dataType: "json",
            pasteZone: null,
            dropZone: null,
            maxChunkSize: 1000000,
            beforeSend: (xhr) => {
                xhr.setRequestHeader("X-FileName", realName);
            },
            done: (e, data) => {
                realName = "";
                $.each(data.result,
                    (index, file) => {
                        var target = $(e.target);
                        var parent = $(target.parent().parent());
                        var entity = {
                            fileTitle: file.name,
                            fileName: file.realname,
                            tableName: 'Files',
                        };

                        var row = $(tmpl(entity));
                        parent.find(".thumbnail").replaceWith(row);
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

    $("form").on("click", ".add-drug", function () {
        $.ajax({
            url: "/Orphanage/Pupils/AddDrug",
            type: "POST",
            data: {},
            success: function (results) {
                const $results = $(results);
                $(".pupil-drugs").append($results);
                $results.find("select").select2();
                drugsTableHeadVisibility()
            }
        });
    });

    $("form").on("click", ".remove-drug", function () {
        $(event.target).closest(".form-group").remove();
        drugsTableHeadVisibility()
    });

    $("form").on("change", "#Data_FoulRegionRestriction", function() {
        if (this.checked) {
            $(".restrictionPeriod").removeClass("hidden");
        } else {
            $(".restrictionPeriod").addClass("hidden");
            $(".restrictionPeriod").find(".request-period-start").val("");
        }
        
    });

    function setStreetAR(element) {
        element.select2({
            initSelection: function (element, callback) {
                const data = {
                    id: element.attr("data-default-id"),
                    text: element.attr("data-default-text"),
                    district: element.attr("data-default-district"),
                    region: element.attr("data-default-region")
                };
                callback(data);
            },
            minimumInputLength: 3,
            formatResult: function (data, container, query) {
                if (data.district) {
                    $(container).attr("data-default-district", data.district);
                }
                if (data.region) {
                    $(container).attr("data-default-region", data.region);
                }
                return data.text;
            },
            ajax: {
                url: "/api/WebFIAS/SearchHome",
                quietMillis: 250,
                type: "GET",
                data: function (term, page) {
                    return { Query: term };
                },
                results: function (data, page) {
                    const result = [];
                    for (let i = 0; i < data.suggestions.length; i++) {
                        if (data.suggestions[i].data.fias_id) {
                            result.push({
                                id: data.suggestions[i].data.fias_id,
                                text: data.suggestions[i].value,
                                district: data.suggestions[i].data.district,
                                region: data.suggestions[i].data.adm_area
                            });
                        }
                    }
                    return {
                        results: result
                    };
                },
                cache: true
            }
        })
            .on("change",
                function (e) {
                    const block = $(this).closest(".form-horizontal");
                    block.find("#Data_Child_Address_District").val(e.added.district);
                    block.find("#Data_Child_Address_Region").val(e.added.region);
                    block.find("#Data_Child_Address_Name").val(e.added.text);
                    //Data_Child_Address_Region
                });
    }

    function initTypeOfSubrestrtiction($e) {
        const $restriction = $e.find(".restriction-select");
        const $subres = $e.find(".subrestriction-select");
        const $div = $e.find(".type-of-subrestriction");

        $subres.select2({
            initSelection: (element, callback) => {
                if ($subres.val() === "") {
                    callback({ id: "", text: "-- Не выбрано --" });
                } else {
                    callback({ id: $subres.val(), text: $subres.attr("titleText") });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + "Api/WebVocabulary/GetTypeOfSubRestriction",
                dataType: "json",
                data: () => {
                    const t = $restriction.select2("val");
                    return {
                        id: t
                    };
                },
                results: (data) => {
                    const results = [{ id: "", text: "-- Не выбрано --" }];

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

        function contains(a, obj) {
            for (let i = 0; i < a.length; i++) {
                if (a[i] == obj) {
                    return true;
                }
            }
            return false;
        }

        $restriction.on("change",
            () => {
                const val = $restriction.select2("val");
                $subres.select2("data", { id: "", text: "-- Не выбрано --" });
                if (contains(typeOfRestrictionSubs, val)) {
                    $div.removeClass("hidden");
                } else {
                    $div.addClass("hidden");
                }
            });
    }

    function setSchoolAR(element) {
        element.select2({
            initSelection: function (element, callback) {
                const data = {
                    id: element.attr("data-default-id"),
                    text: element.attr("data-default-text"),
                    district: element.attr("data-default-district"),
                    region: element.attr("data-default-region")
                };
                callback(data);
            },
            minimumInputLength: 3,
            formatResult: function (data, container, query) {
                return data.text;
            },
            ajax: {
                url: "/api/WebEkisSchools",
                quietMillis: 250,
                type: "GET",
                data: function (term, page) {
                    return { query: term };
                },
                results: (data, page) => {
                    var result = [];
                    for (let i = 0; i < data.length; i++) {
                        result.push({ id: data[i].id, text: data[i].name });
                    }
                    return {
                        results: result
                    };
                },
                cache: true
            }
        })
            .on("change",
                function (e) {
                    //var block = $(this).closest(".form-horizontal");
                    //block.find("#Child_SchoolId").val(e.added.id);
                    //block.find("#Child_School_Name").val(e.added.id);
                });
    }

    var templateImage = $("#fileImageRowTemplate").html();

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

    $('#Data_Child_DocumentTypeId').change((e) => {
        var val = $(e.target).select2('val');
        documentTypeSetMask(val);
    });

    function documentTypeSetMask(val) {
        var birthCertId = '22';
        var passport = '50001';
        var attendantPassport = '60001';
        var selectorDialog = '#dialogPerson';

        if (val === passport || val === attendantPassport) {
            $(selectorDialog + ' .document-seria').inputmask('9999', { clearIncomplete: true });
            $(selectorDialog + ' .document-number').inputmask('999999', { clearIncomplete: true });
        } else if (val === birthCertId) {
            $(selectorDialog + ' .document-seria').inputmask('Regex', { regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]', clearIncomplete: true });
            $(selectorDialog + ' .document-number').inputmask('999999', { clearIncomplete: true });
        } else if (!val) {
            $(selectorDialog + ' .document-number').inputmask('remove');
            $(selectorDialog + ' .document-seria').inputmask('remove');
        } else {
            $(selectorDialog + ' .document-number').inputmask('remove');
            $(selectorDialog + ' .document-seria').inputmask('remove');
        }
    }

    function drugsTableHeadVisibility() {
        let count = $(".pupil-drugs").find(".form-group").length;
        if (count > 2) {
            $(".c-thead").show();
        } else {
            $(".c-thead").hide();
        }
    }
});
