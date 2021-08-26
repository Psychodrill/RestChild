function changeJournalEvent(e, boutEventId) {
    $.ajax({
        dataType: 'json',
        url: rootPath + '/api/WebBout/ChangeJournalActive?id=' + boutEventId + '&state=' + $(e).prop('checked'),
        data: null,
        method: 'POST',
        success: function () {
            $('#forSiteCheckbox').trigger('change');
        },
        error: function () {
        }
    });
}
;
function loadJournal(boutId) {
    var journalRowTemplate = doT.template($('#journalRowTemplate').html());
    var forSite = $('#forSiteCheckbox').prop('checked');
    var forEvent = $('#forEvent').prop('checked');
    var forReport = $('#forReport').prop('checked');
    var forIncident = $('#forIncident').prop('checked');
    var $table = $('#boutJournalList');
    $.ajax({
        dataType: 'json',
        url: rootPath + '/api/WebBout/GetBoutJournal?boutId=' + boutId + '&viewOnSite=' + forSite + '&forEvent=' + forEvent + '&forReport=' + forReport + '&forIncident=' + forIncident,
        data: null,
        success: function (result) {
            if (result.length !== 0) {
                $('#boutJournalEmpty').addClass('hidden');
                var $tbody = $table.find('tbody');
                $tbody.empty();
                result.forEach(function (c, i) {
                    var html = journalRowTemplate(c);
                    $tbody.append($(html));
                });
                $table.removeClass('hidden');
            }
            else {
                $table.addClass('hidden');
                $('#boutJournalEmpty').removeClass('hidden');
            }
        },
        error: function () {
        }
    });
}
function setNotComeInPlaceOfRest($target, resp) {
    var $tr = $target.closest('tr');
    var $select = $tr.find('.not-need-ticket-select');
    var val = $select.select2('val');
    $target.prop('checked', !resp);
    if (!resp) {
        $tr.find('.label-notComeInPlaceOfRest').addClass('hidden');
        $select.prop("disabled", false);
        if (val === 3 || val === '3') {
            $select.select2('val', '');
        }
    }
    else {
        $tr.find('.label-notComeInPlaceOfRest').removeClass('hidden');
        if (!val) {
            $select.select2('val', 3);
        }
        $select.prop("disabled", true);
    }
    return $select.select2('val');
}
function refreshCountIn(resp, count) {
    var countIn = parseInt($('#countIn' + count).html());
    var countNotIn = parseInt($('#countNotIn' + count).html());
    if (!resp) {
        countIn++;
        countNotIn--;
    }
    else {
        countIn--;
        countNotIn++;
    }
    $('#countIn' + count).html(countIn.toString());
    $('#countNotIn' + count).html(countNotIn.toString());
}
function recalcCountIn($target, resp) {
    var $partyCollapse = $target.closest('.party-collapse');
    if ($partyCollapse.length > 0) {
        var count = $partyCollapse.attr('party-number');
        refreshCountIn(resp, count);
    }
    refreshCountIn(resp, '');
}
$(function () {
    $('select').select2();
    $(document).on('change', '.notComeInPlaceOfRest', function (e) {
        var $target = $(e.target);
        var $tr = $(e.target).closest('tr');
        var name = $tr.find('.camper-name').html();
        var val = $target.prop('checked');
        $.ajax({
            method: 'GET',
            url: rootPath + '/api/WebBout/SetNotComeInPlaceOfRest',
            data: {
                linkToPeopleId: $target.attr('data-link-id'),
                linkToPeopleFromId: $target.attr('data-from-link-id'),
                notCome: !val
            },
            success: function (resp) {
                var status = setNotComeInPlaceOfRest($target, resp);
                $target.prop('checked', !resp);
                recalcCountIn($target, resp);
                if (!resp) {
                    changeRowTicketReason($target, parseInt(status));
                }
                else {
                    changeRowTicketReason($target, parseInt(status));
                }
                ShowAlert(name + ". Информация о явке в место отдыха обновлена.", "alert-success", "glyphicon-ok", true);
            },
            error: function () {
                $target.prop('checked', !val);
                ShowAlert(name + ". Ошибка обновления информации", "alert-danger", "glyphicon-remove");
            }
        });
    });
    $(document).on('change', '.deliveredParents', function (e) {
        var $target = $(e.target);
        var $tr = $(e.target).closest('tr');
        var name = $tr.find('.camper-name').html();
        var val = $target.prop('checked');
        $.ajax({
            method: 'GET',
            url: rootPath + '/api/WebBout/SetDeliveredParents',
            data: {
                linkToPeopleId: $target.attr('data-link-id'),
                notCome: val
            },
            success: function (resp) {
                $target.prop('checked', resp);
                ShowAlert(name + ". Информация о передаче ребёнка родителям обновлена.", "alert-success", "glyphicon-ok", true);
            },
            error: function () {
                $target.prop('checked', !val);
                ShowAlert(name + ". Ошибка обновления информации", "alert-danger", "glyphicon-remove");
            }
        });
    });
    $(document).on('shown.bs.tab', '#MainTabs a[data-toggle="tab"]', function (e) {
        $('#ActiveTab').val($(e.target).attr('href').substring(1));
    });
    function getPersonalForAdd(data, url, dialogId, title) {
        $("#" + dialogId + " .modal-body").html("<div align=\"center\"><img src=\"/Content/images/spinner.gif\" /> Загрузка</div>");
        $("#" + dialogId).modal("show");
        $.ajax({
            type: "get",
            data: data,
            url: url
        })
            .done(function (result) {
            $("#" + dialogId + " .counselors-modal-title").html(title);
            $("#" + dialogId + " .modal-body").html(result);
            $("#" + dialogId + " .modal-body select").select2();
        })
            .fail(function (result) {
            $("#" + dialogId + " .modal-body").html("<div align=\"center\">Ошибка загрузки</div>");
        });
    }
    $(document).on("click", ".btn-add-senior-counselor-dialog", function () {
        getPersonalForAdd({
            //onlyVacant: true,
            vacantForBoutId: $('#Data_Id').val(),
            addButtonClass: 'btn-senior-counselor-add'
        }, rootPath + "/Counselors/CounselorsForAdd", "CounselorsModal", "Вожатые");
    });
    $(document).on("click", ".btn-add-swing-counselor-dialog", function () {
        getPersonalForAdd({
            //onlyVacant: true,
            vacantForBoutId: $('#Data_Id').val(),
            addButtonClass: 'btn-swing-counselor-add'
        }, rootPath + "/Counselors/CounselorsForAdd", "CounselorsModal", "Вожатые");
    });
    $(document).on("click", ".btn-add-administrator-dialog", function () {
        getPersonalForAdd({
            //onlyVacant: true,
            vacantForBoutId: $('#Data_Id').val(),
            addButtonClass: 'btn-administrator-add'
        }, rootPath + "/AdministratorTour/AdministratorsForAdd", "CounselorsModal", "Администраторы");
    });
    function loadPersonal() {
        $("#BoutPersonal").html("<div align=\"center\"><img src=\"/Content/images/spinner.gif\" /> Загрузка</div>");
        $.ajax({
            type: "get",
            url: rootPath + "/Bout/GetPersonal",
            data: {
                id: $('#Data_Id').val()
            }
        })
            .done(function (data) {
            $("#BoutPersonal").html(data);
        })
            .fail(function (data) {
            $("#BoutPersonal").html("Ошибка загрузки");
        });
    }
    function processPersonal(data, action) {
        $.ajax({
            url: rootPath + "/api/WebBout/" + action,
            type: "GET",
            dataType: "json",
            data: data,
            cache: false
        })
            .done(function (result) {
            if (result.HasError) {
                BootstrapDialog.show({
                    title: "Ошибка",
                    message: result.ErrorMessage
                });
            }
            else if (result.BoutLastUpdateTick) {
                $('#lut').val(result.BoutLastUpdateTick);
            }
        })
            .fail(function () {
            BootstrapDialog.show({
                title: "",
                message: "Ошибка"
            });
        })
            .always(function () {
            loadPersonal();
        });
    }
    $(document).on('click', '.btn-senior-counselor-add', function (e) {
        processPersonal({
            boutId: $('#Data_Id').val(),
            counselorId: $(e.target).attr("data-id")
        }, 'AddSeniorCounselor');
    });
    $(document).on('click', '.btn-swing-counselor-add', function (e) {
        processPersonal({
            boutId: $('#Data_Id').val(),
            counselorId: $(e.target).attr("data-id")
        }, 'AddSwingCounselor');
    });
    $(document).on('click', '.btn-administrator-add', function (e) {
        processPersonal({
            boutId: $('#Data_Id').val(),
            administratorId: $(e.target).attr("data-id")
        }, 'AddAdministartorTour');
    });
    $(document).on('click', '.remove-senior-counselor', function (e) {
        processPersonal({
            boutId: $('#Data_Id').val(),
            counselorId: $(e.target).attr("data-id")
        }, 'RemoveSeniorCounselor');
    });
    $(document).on('click', '.remove-swing-counselor', function (e) {
        processPersonal({
            boutId: $('#Data_Id').val(),
            counselorId: $(e.target).attr("data-id")
        }, 'RemoveSwingCounselor');
    });
    $(document).on('click', '.remove-administrator', function (e) {
        processPersonal({
            boutId: $('#Data_Id').val(),
            administratorId: $(e.target).attr("data-id")
        }, 'RemoveAdministratotTour');
    });
    $(document).on('change', '.child-not-come-in-place-of-rest-checkbox', function (e) {
        var tr = $(e.target).closest('tr');
        var isChecked = $(e.target).is(':checked');
        var childId = $(e.target).attr('data-child-id');
        var name = $(e.target).closest('tr').find('.camper-name').html();
        $.ajax({
            method: 'GET',
            url: rootPath + '/api/WebBout/SetChildNotComeInPlaceOfRest',
            data: {
                childId: childId,
                notCome: isChecked
            },
            success: function (resp) {
                $(e.target).prop('checked', resp);
                if (resp) {
                    tr.addClass('has-error');
                }
                else {
                    tr.removeClass('has-error');
                }
                ShowAlert(name + ". Информация о явке в место отдыха обновлена.", "alert-success", "glyphicon-ok", true);
            },
            error: function () {
                $(e.target).prop('checked', !isChecked);
                ShowAlert(name + ". Ошибка обновления информации", "alert-danger", "glyphicon-remove");
            }
        });
    });
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });
});
//# sourceMappingURL=BoutEdit.js.map