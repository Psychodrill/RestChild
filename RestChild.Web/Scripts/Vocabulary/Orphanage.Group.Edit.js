$.validator.setDefaults({
    ignore: ""
});
$(function () {
    var phsindex = 1;
    var rfprindex = 1;
    $(".main select").select2();
    setPeriod($(".requestst-fpr"));
    $(".datepicker-anytime").datetimepicker({ showTodayButton: true, format: "DD.MM.YYYY" });
    $(".pupil-health-status").each(function (i, e) {
        initTypeOfSubrestrtiction($(e));
    });
    $("#groupForm").on("click", ".add-pupil-health-status", function () {
        $.ajax({
            url: "/Orphanage/Groups/PupilHealthStatusAdd",
            type: "POST",
            data: { index: (phsindex * -1) },
            success: function (results) {
                var $results = $(results);
                $(".pupil-health-statuses").append($results);
                $results.find("select").select2();
                initTypeOfSubrestrtiction($results);
                phsindex++;
            }
        });
    });
    $("#groupForm").on("click", ".remove-pupil-health-status", function () {
        var block = $(event.target).closest(".pupil-health-status");
        block.remove();
    });
    $("#groupForm").on("click", ".add-request-for-period-of-rest", function () {
        var vpi = $("#Data_VacationPeriodId").val();
        $.ajax({
            url: "/Orphanage/Groups/RequestsForPeriodOfRestAdd",
            type: "POST",
            data: { index: (rfprindex * -1), vacationPeriodId: vpi },
            success: function (results) {
                $(".requestst-fpr").append(results);
                setPeriod($(".requestst-fpr div.request-for-period-of-rest").last());
                rfprindex++;
                totalPeriodsReCount();
            }
        });
    });
    $("#groupForm").on("click", ".request-for-period-of-rest-remove", function () {
        var block = $(event.target).closest(".request-for-period-of-rest");
        block.remove();
        totalPeriodsReCount();
    });
    function initTypeOfSubrestrtiction($e) {
        var $restriction = $e.find(".restriction-select");
        var $subres = $e.find(".subrestriction-select");
        var $div = $e.find(".type-of-subrestriction");
        $subres.select2({
            initSelection: function (element, callback) {
                var $el = $(element);
                if ($el.val() === "") {
                    callback({ id: "", text: "-- Не выбрано --" });
                }
                else {
                    callback({ id: $subres.val(), text: $subres.attr("titleText") });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: rootPath + "Api/WebVocabulary/GetTypeOfSubRestriction",
                dataType: "json",
                data: function () {
                    var t = $restriction.select2("val");
                    return {
                        id: t
                    };
                },
                results: function (data) {
                    var results = [{ id: "", text: "-- Не выбрано --" }];
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
        $restriction.on("change", function () {
            var val = $restriction.select2("val");
            $subres.select2("data", { id: "", text: "-- Не выбрано --" });
            if (contains(typeOfRestrictionSubs, val)) {
                $div.removeClass("hidden");
            }
            else {
                $div.addClass("hidden");
            }
        });
    }
    $("#PupilsList").on("click", ".choose-pupil", function () {
        $(document).find("#PupilsToAdd").remove();
        var orphanageId = $(document).find("#PupilsList").find("#OrphanageId").first().val();
        var groupId = $(document).find("#PupilsList").find("#GroupId").first().val();
        $.ajax({
            url: "/Orphanage/Groups/PupilsChoose",
            type: "POST",
            data: { orphanageId: orphanageId, groupId: groupId },
            success: function (results) {
                var $results = $(results);
                $results.find("select").select2();
                $("body").append($results);
                $(document).find("#PupilsToAdd").closest(".modal").modal("show");
            }
        });
    });
    $("#PupilsList").on("click", ".pupil-remove", function () {
        var pupilId = $(event.target).closest("tr").find(".pupilId").val();
        var groupId = $(document).find("#PupilsList").find("#GroupId").first().val();
        $.ajax({
            url: "/Orphanage/Groups/PupilRemove",
            type: "POST",
            data: { groupId: groupId, pupilId: pupilId },
            success: function (results) {
                $("#PupilsList").submit();
            }
        });
    });
    $(document).on("click", ".pupil-add", function () {
        var pupilId = $(event.target).closest("tr").find(".pupilId").val();
        var groupId = $(event.target).closest("form").find("#GroupId").first().val();
        $.ajax({
            url: "/Orphanage/Groups/PupilAdd",
            type: "POST",
            data: { groupId: groupId, pupilId: pupilId },
            success: function (results) {
                $("#PupilsList").submit();
                $("#PupilsToAdd").find(".PupilId_" + pupilId).closest("tr").remove();
            }
        });
    });
    $('#groupForm').on('submit', function (e) {
        e.preventDefault();
        var return_val = true;
        var elem = "#Data_Name";
        if ($(elem).val().trim() == "") {
            $(elem).next('span').show();
            return_val = false;
        }
        else {
            $(elem).next('span').hide();
        }
        elem = "#YearOfRestId";
        if ($(elem).val().trim() == "") {
            $(elem).next('span').show();
            return_val = false;
        }
        else {
            $(elem).next('span').hide();
        }
        elem = "#FormOfRestId";
        if ($(elem).val().trim() == "") {
            $(elem).next('span').show();
            return_val = false;
        }
        else {
            $(elem).next('span').hide();
        }
        if (return_val) {
            $('#groupForm').unbind().submit();
        }
    });
    function setPeriod(element) {
        var groupId = $("#Data_Id").val();
        inputMaskDateAnytime($(element.find('.input-mask-date-anytime')));
        element.find('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
        element.find(".placesofrest").select2({
            initSelection: function (element, callback) {
                if (element.val() == '') {
                    callback({ id: '', text: '-- Не выбрано --' });
                }
                else {
                    callback({ id: element.val(), text: element.next('input:hidden').val() });
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
        element.find(".timesofrest").select2({
            initSelection: function (element, callback) {
                if (element.val() == '') {
                    callback({ id: '', text: '-- Не выбрано --' });
                }
                else {
                    callback({ id: element.val(), text: element.next('input:hidden').val() });
                }
            },
            minimumInputLength: 0,
            ajax: {
                url: "/api/WebOrphan/GetPupilGroupTimesOfRest",
                quietMillis: 250,
                type: "GET",
                data: function (term, page) {
                    return { orphanagePupilGroupId: groupId, query: term };
                },
                results: function (data, page) {
                    var results = [{ id: '', text: '-- Не выбрано --' }];
                    results = results.concat($.map(data, function (item) {
                        return {
                            id: item.id,
                            text: item.name
                        };
                    }));
                    $(".timesofrest").each(function () {
                        var evalue = $(this).val();
                        if (evalue != "") {
                            results = $.grep(results, function (e) {
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
            var groupPeriodId = $(this).closest(".request-for-period-of-rest").find(".rfporId").val();
            $(this).select2({
                initSelection: function (element, callback) {
                    if (element.val() == '') {
                        callback({ id: '', text: '-- Не выбрано --' });
                    }
                    else {
                        callback({ id: element.val(), text: element.next('input:hidden').val() });
                    }
                },
                minimumInputLength: 0,
                ajax: {
                    url: "/api/WebOrphan/GetTours",
                    quietMillis: 250,
                    type: "GET",
                    data: function (term, page, context) {
                        return { orphanageRequestForPeriodOfRestId: groupPeriodId, query: term };
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
        });
        element.find(".pcount")
            .inputmask("numeric", { min: 0, max: 300 })
            .on('input propertychange paste', function () {
            totalPupilsCount();
        });
        element.find(".ccount")
            .inputmask("numeric", { min: 0, max: 15 })
            .on('input propertychange paste', function () {
            totalCollaboratorsCount();
        });
        element.find(".mcount")
            .inputmask("numeric", { min: 0, max: 15 })
            .on('input propertychange paste', function () {
            totalMGTCollaboratorsCount();
        });
    }
    $("body").on("click", ".clear-form", function () {
        var block = $(event.target).closest("form");
        block.find("input.form-control").val('');
        block.find("#IsNotInGroup").prop('checked', false);
        block.find("#IsMale").val('');
        block.find("#IsMale").select2().trigger('change');
        block.submit();
    });
    function totalPupilsCount() {
        var total = 0;
        $(document).find(".pcount").each(function () {
            total += parseInt(this.value, 10);
        });
        $("#Data_PupilsCount").val('' + total);
    }
    function totalCollaboratorsCount() {
        var total = 0;
        $(document).find(".ccount").each(function () {
            total += parseInt(this.value, 10);
        });
        $("#Data_CollaboratorsCount").val('' + total);
    }
    function totalMGTCollaboratorsCount() {
        var total = 0;
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
//# sourceMappingURL=Orphanage.Group.Edit.js.map