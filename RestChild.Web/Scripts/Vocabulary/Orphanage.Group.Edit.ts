$.validator.setDefaults({
    ignore: ""
});

$(() => {
    let phsindex = 1;
    let rfprindex = 1;
    $(".main select").select2();

    setPeriod($(".requestst-fpr"));

    $(".datepicker-anytime").datetimepicker({showTodayButton: true, format: "DD.MM.YYYY"});

    $(".pupil-health-status").each((i, e) => {
        initTypeOfSubrestrtiction($(e));
    });

    $("#groupForm").on("click", ".add-pupil-health-status", () => {
        $.ajax({
            url: "/Orphanage/Groups/PupilHealthStatusAdd",
            type: "POST",
            data: {index: (phsindex * -1)},
            success: (results) => {
                const $results = $(results);
                $(".pupil-health-statuses").append($results);
                $results.find("select").select2();
                initTypeOfSubrestrtiction($results);
                phsindex++;
            }
        });
    });

    $("#groupForm").on("click", ".remove-pupil-health-status", () => {
        const block = $(event.target).closest(".pupil-health-status");
        block.remove();
    });

    $("#groupForm").on("click", ".add-request-for-period-of-rest", () => {
        const vpi = $("#Data_VacationPeriodId").val();

        $.ajax({
            url: "/Orphanage/Groups/RequestsForPeriodOfRestAdd",
            type: "POST",
            data: { index: (rfprindex * -1), vacationPeriodId: vpi},
            success: (results) => {
                $(".requestst-fpr").append(results);
                setPeriod($(".requestst-fpr div.request-for-period-of-rest").last());
                rfprindex++;

                totalPeriodsReCount();
            }
        });
    });

    $("#groupForm").on("click", ".request-for-period-of-rest-remove", () => {
        const block = $(event.target).closest(".request-for-period-of-rest");
        block.remove();

        totalPeriodsReCount();
    });

    function initTypeOfSubrestrtiction($e) {
        const $restriction = $e.find(".restriction-select");
        const $subres = $e.find(".subrestriction-select");
        const $div = $e.find(".type-of-subrestriction");

        $subres.select2({
            initSelection: (element, callback) => {
                const $el = $(element);
                if ($el.val() === "") {
                    callback({id: "", text: "-- Не выбрано --"});
                } else {
                    callback({id: $subres.val(), text: $subres.attr("titleText")});
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
                    const results = [{id: "", text: "-- Не выбрано --"}];

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
                $subres.select2("data", {id: "", text: "-- Не выбрано --"});
                if (contains(typeOfRestrictionSubs, val)) {
                    $div.removeClass("hidden");
                } else {
                    $div.addClass("hidden");
                }
            });
    }

    $("#PupilsList").on("click", ".choose-pupil",
        () => {
            $(document).find("#PupilsToAdd").remove();
            var orphanageId = $(document).find("#PupilsList").find("#OrphanageId").first().val();
            var groupId = $(document).find("#PupilsList").find("#GroupId").first().val();

            $.ajax({
                url: "/Orphanage/Groups/PupilsChoose",
                type: "POST",
                data: {orphanageId: orphanageId, groupId: groupId},
                success: (results) => {
                    const $results = $(results);
                    $results.find("select").select2();
                    $("body").append($results);
                    $(document).find("#PupilsToAdd").closest(".modal").modal("show");
                }
            });
        });

    $("#PupilsList").on("click", ".pupil-remove",
        () => {
            var pupilId = $(event.target).closest("tr").find(".pupilId").val();
            var groupId = $(document).find("#PupilsList").find("#GroupId").first().val();

            $.ajax({
                url: "/Orphanage/Groups/PupilRemove",
                type: "POST",
                data: {groupId: groupId, pupilId: pupilId},
                success: (results) => {
                    $("#PupilsList").submit();
                }
            });
        });

    $(document).on("click", ".pupil-add",
        () => {
            var pupilId = $(event.target).closest("tr").find(".pupilId").val();
            var groupId = $(event.target).closest("form").find("#GroupId").first().val();
            $.ajax({
                url: "/Orphanage/Groups/PupilAdd",
                type: "POST",
                data: {groupId: groupId, pupilId: pupilId},
                success: (results) => {
                    $("#PupilsList").submit();
                    $("#PupilsToAdd").find(".PupilId_" + pupilId).closest("tr").remove();
                }
            });
        });

    $('#groupForm').on('submit', (e) => {
        e.preventDefault();

        let return_val = true;
        let elem = "#Data_Name";
        if ($(elem).val().trim() == "") {
            $(elem).next('span').show();
            return_val = false;
        } else {
            $(elem).next('span').hide();
        }

        elem = "#YearOfRestId";
        if ($(elem).val().trim() == "") {
            $(elem).next('span').show();
            return_val = false;
        } else {
            $(elem).next('span').hide();
        }

        elem = "#FormOfRestId";
        if ($(elem).val().trim() == "") {
            $(elem).next('span').show();
            return_val = false;
        } else {
            $(elem).next('span').hide();
        }

        if (return_val) {
            $('#groupForm').unbind().submit();
        }
    });

    function setPeriod(element: JQuery) {
        let groupId = $("#Data_Id").val();
        inputMaskDateAnytime($(element.find('.input-mask-date-anytime')));
        element.find('.datepicker-anytime').datetimepicker({showTodayButton: true, format: 'DD.MM.YYYY'});
        element.find(".placesofrest").select2({
            initSelection: function (element, callback) {
                if (element.val() == '') {
                    callback({id: '', text: '-- Не выбрано --'});
                } else {
                    callback({id: element.val(), text: element.next('input:hidden').val()});
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: "/api/WebVocabulary/GetPlacesOfRestForPupilsRequestOfPeriodRest",
                quietMillis: 250,
                type: "GET",
                data: function (term, page) {
                    return { groupId: groupId, query: term };
                },
                results: function (data, page) {
                    var results = [{id: '', text: '-- Не выбрано --'}];
                    results = results.concat($.map(data, (item) => {
                        return {
                            text: item.name,
                            id: item.id
                        }
                    }));

                    return {
                        results: results
                    };
                },
                cache: true
            }
        });

        element.find(".timesofrest").select2({
            initSelection: function (element, callback) {
                if (element.val() == '') {
                    callback({id: '', text: '-- Не выбрано --'});
                } else {
                    callback({id: element.val(), text: element.next('input:hidden').val()});
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: "/api/WebOrphan/GetPupilGroupTimesOfRest",
                quietMillis: 250,
                type: "GET",
                data: function (term, page) {
                    return {orphanagePupilGroupId: groupId, query: term};
                },
                results: function (data, page) {
                    let results = [{id: '', text: '-- Не выбрано --'}];
                    results = results.concat($.map(data, (item) => {
                        return {
                            id: item.id,
                            text: item.name
                        }
                    }));

                    $(".timesofrest").each(function () {
                        let evalue = $(this).val();
                        if (evalue != "") {
                            results = $.grep(results, (e) => {
                                return e.id != evalue;
                            });
                        }
                    });

                    return {
                        results: results
                    };
                },
                cache: false
            }
        });

        element.find(".tours").each(function () {
            let groupPeriodId = $(this).closest(".request-for-period-of-rest").find(".rfporId").val();
            $(this).select2({
                initSelection: function (element, callback) {
                    if (element.val() == '') {
                        callback({id: '', text: '-- Не выбрано --'});
                    } else {
                        callback({id: element.val(), text: element.next('input:hidden').val()});
                    }
                },
                minimumInputLength: 0,
                ajax: {
                    url: "/api/WebOrphan/GetTours",
                    quietMillis: 250,
                    type: "GET",
                    data: function (term, page, context) {
                        return {orphanageRequestForPeriodOfRestId: groupPeriodId, query: term};
                    },
                    results: function (data, page) {
                        var results = [{id: '', text: '-- Не выбрано --'}];
                        results = results.concat($.map(data, (item) => {
                            return {
                                text: item.name,
                                id: item.id
                            }
                        }));
                        return {
                            results: results
                        };
                    },
                    cache: true
                }
            });
        });

        element.find(".pcount")
            .inputmask("numeric", {min: 0, max: 300})
            .on('input propertychange paste', () => {
                totalPupilsCount();
            });
        element.find(".ccount")
            .inputmask("numeric", {min: 0, max: 15})
            .on('input propertychange paste', () => {
                totalCollaboratorsCount();
            });
        element.find(".mcount")
            .inputmask("numeric", {min: 0, max: 15})
            .on('input propertychange paste', () => {
                totalMGTCollaboratorsCount();
            });
    }

    $("body").on("click", ".clear-form", () => {
        const block = $(event.target).closest("form");
        block.find("input.form-control").val('');
        block.find("#IsNotInGroup").prop('checked', false);
        block.find("#IsMale").val('');
        block.find("#IsMale").select2().trigger('change');
        block.submit();
    });

    function totalPupilsCount() {
        let total = 0;
        $(document).find(".pcount").each(function () {
            total += parseInt(this.value, 10);
        });
        $("#Data_PupilsCount").val('' + total);
    }

    function totalCollaboratorsCount() {
        let total = 0;
        $(document).find(".ccount").each(function () {
            total += parseInt(this.value, 10);
        });
        $("#Data_CollaboratorsCount").val('' + total);
    }

    function totalMGTCollaboratorsCount() {
        let total = 0;
        $(document).find(".mcount").each(function () {
            total += parseInt(this.value, 10);
        });
        $("#Data_MGTCollaboratorsCount").val('' + total);
    }

    function totalPeriodsReCount() {
        totalPupilsCount();
        totalCollaboratorsCount();
        totalMGTCollaboratorsCount();
    }
});







