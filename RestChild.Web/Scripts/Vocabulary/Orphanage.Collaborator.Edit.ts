$(() => {
    $(".datepicker-anytime").datetimepicker({ showTodayButton: true, format: "DD.MM.YYYY" });
    $("select").select2();
    setStreetAR($(".street-autocompleteAR").not("inited").addClass("inited"));

    documentTypeSetMask($('#Applicant_DocumentTypeId').select2('val'));

    $("form").on("change", ".No-Middle-Name", function() {
            const closest_midname = $(this).closest(".form-group").find(".Middle-Name");
            if (this.checked) {
                $(closest_midname).val("");
                $(closest_midname).prop("readonly", true);
            } else {
                $(closest_midname).prop("readonly", false);
            }
        });

    $(".OrphanageCollaboratorFormDeleteSave").click((e) => {
        return confirmStateButton("#OrphanageCollaboratorForm", "#Data_Applicant_IsDeleted", "true", "Удалить", "Удалить сотрудника?", null);
    });

    $(".OrphanageCollaboratorFormRestoreSave").click((e) => {
        return confirmStateButton("#OrphanageCollaboratorForm", "#Data_Applicant_IsDeleted", "false", "Восстановить", "Восстановить сотрудника?", null);
    });

    const $sendDialog = $(".sendDialog");
    $sendDialog.find("select").change((e) => {
        documentInputType($(e.target));
    });

    function setStreetAR(element) {
        element.select2({
                initSelection: function(element, callback) {
                    const data = {
                        id: element.attr("data-default-id"),
                        text: element.attr("data-default-text"),
                        district: element.attr("data-default-district"),
                        region: element.attr("data-default-region")
                    };
                    callback(data);
                },
                minimumInputLength: 3,
                formatResult: function(data, container, query) {
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
                    data: function(term, page) {
                        return { Query: term };
                    },
                    results: function(data, page) {
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
                function(e) {
                    const block = $(this).closest(".form-horizontal");
                    block.find("#Data_Applicant_Address_District").val(e.added.district);
                    block.find("#Data_Applicant_Address_Region").val(e.added.region);
                    block.find("#Data_Applicant_Address_Name").val(e.added.text);
                });
    }

    function documentInputType($target) {
        const self = $target;
        const $item = self.find("option:selected");
        const text = $item.text();
        const $fieldset = $("#sendDialog");
        if (text === "Паспорт гражданина РФ") {
            $fieldset.find(".input-mask-passport-series").inputmask("9999",
                {
                    placeholder: "сссс",
                    clearIncomplete: true
                });
            $fieldset.find(".input-mask-passport-number").inputmask("999999",
                {
                    placeholder: "нннннн",
                    clearIncomplete: true
                });
        } else if (text === "Свидетельство о рождении") {
            $fieldset.find(".input-mask-passport-series").inputmask("Regex",
                {
                    regex: "[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]",
                    clearIncomplete: true
                });
            $fieldset.find(".input-mask-passport-number").inputmask("999999", { clearIncomplete: true });
        } else {
            $fieldset.find(".input-mask-passport-series").inputmask("Regex", { regex: ".*" });
            $fieldset.find(".input-mask-passport-number").inputmask("Regex", { regex: ".*" });
        }
    }

    $('#Applicant_DocumentTypeId').change((e) => {
        var val = $(e.target).select2('val');
        documentTypeSetMask(val);
    });

    function documentTypeSetMask(val) {
        var birthCertId = '22';
        var passport = '20001';
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
});
