var Filter = /** @class */ (function () {
    function Filter() {
    }
    return Filter;
}());
function getFilter() {
    var filter = new Filter();
    filter.boutsId = $("#Data_Id").val();
    filter.hotelsId = $("#Data_HotelsId").val();
    filter.groupedTimeOfRestId = $("#Data_GroupedTimeOfRestId").val();
    filter.onlyNotAdded = $("#OnlyNotAdded").length !== 0 ? $("#OnlyNotAdded").is(":checked") : null;
    filter.onlyBenefits = $("#OnlyBenefits").is(":checked");
    filter.onlyCommerce = $("#OnlyCommerce").is(":checked");
    filter.onlySpecilized = $("#OnlySpecilized").is(":checked");
    filter.isMale = $("#IsMale:checked").val();
    filter.ageFrom = $("#AgeFrom").val();
    filter.ageTo = $("#AgeTo").val();
    filter.name = $("#Name").val();
    filter.isGrouped = $("#Grouping:checked").val() === "Grouped";
    filter.subjectOfRestid = $("#SubjectOfRestid").val();
    filter.openedPartyId = parseInt($(".party-collapse.in").first().attr("data-id"));
    filter.orderBy = $('#OrderBy').val();
    return filter;
}
function loadParties(filter) {
    $("#PartiesBlock").html("<div align=\"center\"><img src=\"/Content/images/spinner.gif\" /> Загрузка</div>");
    $.ajax({
        type: "get",
        url: rootPath + "/Party/GetParties",
        data: filter
    })
        .done(function (data) {
        $("#PartiesBlock").html(data);
        $('#PartiesBlock select').select2();
    })
        .fail(function (data) {
        $("#PartiesBlock").html("Ошибка загрузки");
    });
}
function loadChilds(filter) {
    $("#ChildsBlock").html("<div align=\"center\"><img src=\"/Content/images/spinner.gif\" /> Загрузка</div>");
    $.ajax({
        type: "get",
        url: rootPath + "/Party/GetChilds",
        data: filter
    })
        .done(function (data) {
        $("#ChildsBlock").html(data);
        $('#countTotal').html($('#countTotalSrc').html());
        $('#countUngrouped').html($('#countUngroupedSrc').html());
        $("#ChildsBlock select").select2();
    })
        .fail(function (data) {
        $("#ChildsBlock").html("Ошибка загрузки");
    });
}
$(function () {
    $("select").select2();
    $("#HotelId").select2({
        initSelection: function (element, callback) {
            callback({ id: "", text: "-- Не выбрано --" });
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + "/api/WebHotels",
            dataType: "json",
            data: function (term, page) {
                return {
                    name: term,
                    onlyApproved: "True"
                };
            },
            results: function (data, page) {
                var results = [{ id: "", text: "-- Не выбрано --" }];
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
    }).on("change", function () {
        var tourGroup = $("#TourGroupId").select2("data");
        if (tourGroup == null || tourGroup.id === "") {
            return;
        }
        $("#TourGroupId").select2("data", { id: "", text: "-- Не выбрано --" });
        var filter = getFilter();
        $("#PartiesBlock").html(null);
        $("#ChildsBlock").html(null);
    });
    $("#TourGroupId").select2({
        initSelection: function (element, callback) {
            callback({ id: "", text: "-- Не выбрано --" });
        },
        minimumResultsForSearch: Infinity,
        ajax: {
            url: rootPath + "/api/WebTours/GetGroupByDates",
            dataType: "json",
            data: function () {
                return {
                    hotelId: $("#HotelId").select2("val")
                };
            },
            results: function (data, page) {
                var results = [{ id: "", text: "-- Не выбрано --" }];
                results = results.concat($.map(data, function (item) {
                    return {
                        dateIncome: item.DateIncome,
                        dateOutcome: item.DateOutcome,
                        id: item.Text,
                        text: item.Text
                    };
                }));
                return {
                    results: results
                };
            },
            cache: true
        }
    }).on("change", function () {
        if ($("#TourGroupId").val() == "") {
            $("#PartiesBlock").html(null);
            $("#ChildsBlock").html(null);
        }
        else {
            var filter = getFilter();
            loadParties(filter);
            loadChilds(filter);
        }
    });
    $(document).on("click", "#CreateParty", function () {
        $.ajax({
            type: "post",
            url: rootPath + "/api/WebParty/AddParty",
            data: getFilter()
        })
            .done(function (result) {
            if (result.HasError) {
                BootstrapDialog.show({
                    title: "Ошибка",
                    message: result.ErrorMessage
                });
            }
            else {
                var filter = getFilter();
                filter.openedPartyId = result.OpenedPartyId;
                loadParties(filter);
                loadChilds(filter);
            }
        })
            .fail(function () {
            BootstrapDialog.show({
                title: "",
                message: "Ошибка"
            });
            var filter = getFilter();
            loadParties(filter);
            loadChilds(filter);
        });
    });
    $(document).on("click", "#AddToParty", function () {
        var parties = $(".party-collapse.in");
        var childs = $(".child-checkbox:checked").map(function (i, val) {
            return $(val).attr("data-id");
        }).get();
        if (parties.length === 0 || childs.length === 0) {
            return;
        }
        var party = parties.first();
        $.ajax({
            type: "post",
            url: rootPath + "/api/WebParty/AddToParty?partyId=" + party.attr("data-id"),
            dataType: "json",
            data: {
                "": childs
            }
        })
            .done(function (result) {
            if (result.HasError) {
                BootstrapDialog.show({
                    title: "Ошибка",
                    message: result.ErrorMessage
                });
            }
        })
            .fail(function () {
            BootstrapDialog.show({
                title: "",
                message: "Ошибка"
            });
        })
            .always(function () {
            var filter = getFilter();
            loadParties(filter);
            loadChilds(filter);
        });
    });
    $(document).on("click", "#ExcludeFromParty", function () {
        var childs = $(".party-collapse.in .child-in-party:checked").map(function (i, val) {
            return $(val).attr("data-id");
        }).get();
        if (childs.length === 0) {
            return;
        }
        $.ajax({
            type: "get",
            url: rootPath + "/api/WebParty/ExcludeFromParty",
            data: {
                childs: childs
            }
        })
            .done(function (result) {
            if (result.HasError) {
                BootstrapDialog.show({
                    title: "Ошибка",
                    message: result.ErrorMessage
                });
            }
        })
            .fail(function () {
            BootstrapDialog.show({
                title: "",
                message: "Ошибка"
            });
        })
            .always(function () {
            var filter = getFilter();
            loadParties(filter);
            loadChilds(filter);
        });
    });
    $(document).on("click", "#RemoveParty", function () {
        BootstrapDialog.show({
            title: "Подтвердить действие",
            message: "Вы действительно хотите удалить отряд?",
            buttons: [
                {
                    label: "Удалить",
                    cssClass: "btn-danger",
                    action: function (dialogItself) {
                        var parties = $(".party-collapse.in");
                        if (parties.length === 0) {
                            dialogItself.close();
                            return;
                        }
                        var party = parties.first();
                        $.ajax({
                            type: "get",
                            url: rootPath + "/api/WebParty/RemoveParty",
                            data: {
                                partyId: party.attr("data-id")
                            }
                        })
                            .done(function (result) {
                            if (result.HasError) {
                                BootstrapDialog.show({
                                    title: "Ошибка",
                                    message: result.ErrorMessage
                                });
                            }
                        })
                            .fail(function () {
                            BootstrapDialog.show({
                                title: "",
                                message: "Ошибка"
                            });
                        })
                            .always(function () {
                            var filter = getFilter();
                            loadParties(filter);
                            loadChilds(filter);
                        });
                        dialogItself.close();
                    }
                }, {
                    label: "Отмена",
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
    });
    $(document).on("change", "#Grouping", function () {
        loadChilds(getFilter());
    });
    $(document).on("click", ".btn-filter", function () {
        loadChilds(getFilter());
    });
    $(document).on("click", ".btn-counselor-add-dialog", function () {
        $("#CounselorsModal .modal-body").html("<div align=\"center\"><img src=\"/Content/images/spinner.gif\" /> Загрузка</div>");
        $("#CounselorsModal").modal("show");
        $.ajax({
            type: "get",
            data: {
                onlyVacant: true,
                vacantForPartyId: $(".party-collapse.in:first").attr('data-id'),
                addButtonClass: "btn-counselor-add"
            },
            url: rootPath + "/Counselors/CounselorsForAdd"
        })
            .done(function (data) {
            $("#CounselorsModal .modal-body").html(data);
            $("#CounselorsModal .modal-body select").select2();
        })
            .fail(function (data) {
            $("#counselorsModal .modal-body").html("<div align=\"center\">Ошибка загрузки</div>");
        });
    });
    $(document).on("click", ".dialog-search-button", function (e) {
        $.ajax({
            url: $("#CounselorsUrl").val(),
            data: {
                name: $('#searchCriteriaPanel input[name="Name"]').val(),
                ageFrom: $('#searchCriteriaPanel input[name="AgeFrom"]').val(),
                ageTo: $('#searchCriteriaPanel input[name="AgeTo"]').val(),
                isMale: $('#searchCriteriaPanel select[name="IsMale"]').select2("val"),
                vacantForBoutId: $('#searchCriteriaPanel input[name="VacantForBoutId"]').val(),
                onlyVacant: $('#searchCriteriaPanel input[name="OnlyVacant"]').val(),
                addButtonClass: $('#searchCriteriaPanel input[name="AddButtonClass"]').val()
            },
            type: "GET",
            cache: false,
            success: function (result) {
                $("#CounselorsDialogBody").html(result);
                $("#CounselorsDialogBody select").select2();
            }
        });
    });
    $(document).on("click", ".dialog-clear-button", function (e) {
        $.ajax({
            url: $("#CounselorsUrl").val(),
            data: {
                vacantForBoutId: $('#searchCriteriaPanel input[name="VacantForBoutId"]').val(),
                onlyVacant: $('#searchCriteriaPanel input[name="OnlyVacant"]').val(),
                addButtonClass: $('#searchCriteriaPanel input[name="AddButtonClass"]').val()
            },
            type: "GET",
            cache: false,
            success: function (result) {
                $("#CounselorsDialogBody").html(result);
                $("#CounselorsDialogBody select").select2();
            }
        });
    });
    $(document).on("click", ".btn-counselor-add", function (e) {
        var parties = $(".party-collapse.in");
        var party = parties.first();
        $.ajax({
            url: rootPath + "/api/WebParty/AddCounselorToParty",
            type: "GET",
            dataType: "json",
            data: {
                partyId: party.attr("data-id"),
                counselorId: $(e.target).attr("data-id")
            },
            cache: false
        })
            .done(function (result) {
            if (result.HasError) {
                BootstrapDialog.show({
                    title: "Ошибка",
                    message: result.ErrorMessage
                });
            }
        })
            .fail(function () {
            BootstrapDialog.show({
                title: "",
                message: "Ошибка"
            });
        })
            .always(function () {
            loadParties(getFilter());
        });
    });
    $(document).on("click", ".btn-counselor-remove", function (e) {
        BootstrapDialog.show({
            title: "Подтвердить действие",
            message: "Вы действительно хотите удалить вожатого из отряда?",
            buttons: [
                {
                    label: "Удалить",
                    cssClass: "btn-danger",
                    action: function (dialogItself) {
                        $.ajax({
                            url: rootPath + "/api/WebParty/RemoveCounselorFromParty",
                            type: "GET",
                            data: {
                                partyId: $(e.target).attr("data-party-id"),
                                counselorId: $(e.target).attr("data-counselor-id")
                            },
                            cache: false
                        })
                            .done(function (result) {
                            if (result.HasError) {
                                BootstrapDialog.show({
                                    title: "Ошибка",
                                    message: result.ErrorMessage
                                });
                            }
                        })
                            .fail(function () {
                            BootstrapDialog.show({
                                title: "",
                                message: "Ошибка"
                            });
                        })
                            .always(function () {
                            loadParties(getFilter());
                        });
                        dialogItself.close();
                    }
                }, {
                    label: "Отмена",
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
    });
    $(document).on("click", "#CounselorsDialogBody .pagination a", function (e) {
        e.preventDefault();
        $.ajax({
            url: $(e.target).attr("href"),
            type: "GET",
            cache: false
        })
            .done(function (result) {
            $("#CounselorsDialogBody").html(result);
            $("#CounselorsDialogBody select").select2();
        });
        return false;
    });
    $(document).on("click", ".btn-party-change-state", function (e) {
        $.ajax({
            url: rootPath + "/api/WebParty/ChangeState",
            type: "GET",
            data: {
                partyId: $(e.target).attr("data-id"),
                actionCode: $(e.target).attr("data-action-code")
            },
            cache: false
        })
            .done(function (result) {
            if (result.HasError) {
                BootstrapDialog.show({
                    title: "Ошибка",
                    message: result.ErrorMessage
                });
            }
        })
            .fail(function () {
            BootstrapDialog.show({
                title: "",
                message: "Ошибка"
            });
        })
            .always(function () {
            loadParties(getFilter());
        });
    });
    $(document).on("click", ".group-checkbox", function (e) {
        if (e.data == "raw") {
            return;
        }
        if ($(e.target).is(":checked")) {
            $(e.target).closest(".panel-group").find(".child-checkbox:not(:disabled)").prop("checked", true);
        }
        else {
            $(e.target).closest(".panel-group").find(".child-checkbox:not(:disabled)").prop("checked", false);
        }
        if ($("#ChildsBlock").find(".child-checkbox:not(:checked)").length == 0) {
            $(".check-all-childs").prop("checked", true);
        }
        else {
            $(".check-all-childs").prop("checked", false);
        }
    });
    $(document).on("click", ".child-checkbox", function (e) {
        if ($(e.target).closest(".panel-group").find(".child-checkbox:not(:checked)").length == 0) {
            $(e.target).closest(".panel-group").find(".group-checkbox").prop("checked", true);
        }
        else {
            $(e.target).closest(".panel-group").find(".group-checkbox").prop("checked", false);
        }
        if ($("#ChildsBlock").find(".child-checkbox:not(:checked)").length == 0) {
            $(".check-all-childs").prop("checked", true);
        }
        else {
            $(".check-all-childs").prop("checked", false);
        }
    });
    $(document).on('click', '.btn-child-ticket', function (e) {
        $.ajax({
            url: rootPath + "/api/WebParty/NeedTicket",
            type: "GET",
            data: {
                childId: $(e.target).attr("data-child-id"),
                forward: $(e.target).attr("data-forward"),
                need: $(e.target).attr("data-need")
            },
            cache: false
        })
            .done(function (result) {
            if (result.HasError) {
                BootstrapDialog.show({
                    title: "Ошибка",
                    message: result.ErrorMessage
                });
            }
        })
            .fail(function () {
            BootstrapDialog.show({
                title: "",
                message: "Ошибка"
            });
        })
            .always(function () {
            loadParties(getFilter());
        });
    });
    $(document).on('hidden.bs.collapse', '#partyAccordion', function () {
        if ($('#partyAccordion .in').length === 0) {
            $('#ExcludeFromParty').attr('disabled', 'disabled');
            $('#RemoveParty').attr('disabled', 'disabled');
            $('#AddToParty').attr('disabled', 'disabled');
        }
    });
    $(document).on('shown.bs.collapse', '#partyAccordion', function (e) {
        if ($(e.target).find('.table-childs-inparty tbody tr').length !== 0) {
            $('#ExcludeFromParty').removeAttr('disabled');
        }
        $('#RemoveParty').removeAttr('disabled');
        $('#AddToParty').removeAttr('disabled');
    });
    $(document).on('click', '.child-order-ungrouped', function (e) {
        var type = $(e.target).attr('data-sort-type');
        $('#OrderBy').val(type);
        loadChilds(getFilter());
    });
    $(document).on('change', '.check-all-childs', function (e) {
        var val = false;
        if ($(e.target).is(':checked')) {
            val = true;
        }
        $('#ChildsBlock .group-checkbox:not(:disabled)').prop('checked', val);
        $('#ChildsBlock .child-checkbox:not(:disabled)').prop('checked', val);
    });
    $(document).on('click', '.check-all-childs-in-party', function (e) {
        var val = false;
        if ($(e.target).is(':checked')) {
            val = true;
        }
        $('#PartiesBlock .child-in-party:not(:disabled)').prop('checked', val);
    });
    $(document).on('click', '.child-in-party', function (e) {
        if ($("#PartiesBlock").find(".child-in-party:not(:checked)").length == 0) {
            $(".check-all-childs-in-party").prop("checked", true);
        }
        else {
            $(".check-all-childs-in-party").prop("checked", false);
        }
    });
    loadParties(getFilter());
    loadChilds(getFilter());
});
//# sourceMappingURL=PartyForm.js.map