$(function () {
    $("#listForm").on("click", ".add-pupil", function () {
        var orphanageId = $("#OrganizationId").val();
        $(document).find("#PupilsToAdd").closest(".modal").remove();
        $.ajax({
            url: "/Orphanage/PupilGroupLists/PupilsChoose",
            type: "POST",
            data: { orphanageId: orphanageId },
            success: function (results) {
                var $results = $(results);
                $results.find("select").select2();
                $("body").append($results);
                $(document).find("#PupilsToAdd").closest(".modal").modal("show");
            }
        });
    });
    $("#listForm").on("click", ".add-collaborator", function () {
        var orphanageId = $("#OrganizationId").val();
        $(document).find("#CollaboratorsToAdd").closest(".modal").remove();
        $.ajax({
            url: "/Orphanage/PupilGroupLists/CollaboratorsChoose",
            type: "POST",
            data: { orphanageId: orphanageId },
            success: function (results) {
                var $results = $(results);
                $results.find("select").select2();
                $("body").append($results);
                $(document).find("#CollaboratorsToAdd").closest(".modal").modal("show");
            }
        });
    });
    CollaboratorsNumberCount();
    PupilsNumberCount();
    $(document).on("click", ".pupil-add", function () {
        var pupilId = $(event.target).closest("tr").find(".pupilId").val();
        var values = $("#listForm .pupils input.pkey").map(function () { return $(this).val(); }).get();
        var pupils = $("#listForm .pupils input.ppkey").map(function () { return $(this).val(); }).get();
        var pupilsE = $("#listForm .pupils input.pekey").map(function () { return $(this).val(); }).get();
        var min = Math.min.apply(Math, values);
        if (min >= 0)
            min = 0;
        min = min - 1;
        if (jQuery.inArray(pupilId, pupils) === -1 && jQuery.inArray(pupilId, pupilsE) === -1) {
            $.ajax({
                url: "/Orphanage/PupilGroupLists/PupilAdd",
                type: "POST",
                data: { pupilId: pupilId, dictKey: min - 1 },
                success: function (results) {
                    var zResult = $(results);
                    zResult.find("input.pkey").val(min);
                    zResult.find('.select2').select2();
                    zResult.find('.select2').on('change', function (e) {
                        TransferAddressPeopleRecount();
                    });
                    $("#listForm .pupils > tbody:last-child").append(zResult);
                    TransferAddressPeopleRecount();
                }
            });
        }
    });
    $(document).on("click", ".collaborator-add", function () {
        var сollaboratorIdId = $(event.target).closest("tr").find(".collaboratorId").val();
        var values = $("#listForm .collaborators input.pkey").map(function () { return $(this).val(); }).get();
        var сollaborators = $("#listForm .collaborators input.ppkey").map(function () { return $(this).val(); }).get();
        var сollaboratorsE = $("#listForm .collaborators input.pekey").map(function () { return $(this).val(); }).get();
        var min = Math.min.apply(Math, values);
        if (min >= 0)
            min = 0;
        min = min - 1;
        if (jQuery.inArray(сollaboratorIdId, сollaborators) === -1 && jQuery.inArray(сollaboratorIdId, сollaboratorsE) === -1) {
            $.ajax({
                url: "/Orphanage/PupilGroupLists/CollaboratorAdd",
                type: "POST",
                data: { сollaboratorId: сollaboratorIdId, dictKey: min },
                success: function (results) {
                    var zResult = $(results);
                    zResult.find("input.pkey").val(min);
                    zResult.find('.select2').select2();
                    zResult.find('.select2').on('change', function (e) {
                        TransferAddressPeopleRecount();
                    });
                    $("#listForm .collaborators > tbody:last-child").append(zResult);
                    TransferAddressPeopleRecount();
                }
            });
        }
    });
    $("form").on("click", ".remove-pupil", function () {
        $(event.target).closest("tr").remove();
        TransferAddressPeopleRecount();
    });
    $("form").on("click", ".remove-сollaborator", function () {
        $(event.target).closest("tr").remove();
        TransferAddressPeopleRecount();
    });
    $("body").on("click", ".clear-form", function () {
        var block = $(event.target).closest("form");
        block.find("input.form-control").val('');
        block.find("#IsNotInGroup").prop('checked', false);
        block.find("#IsMale").val('');
        block.find("#IsMale").select2().trigger('change');
        block.submit();
    });
    $('.select2').select2();
    $('.select2').on('change', function (e) {
        TransferAddressPeopleRecount();
    });
    function TransferAddressPeopleRecount() {
        $(".transferAdressId").each(function () {
            var cur = $(this);
            var cur_val = cur.val();
            var cur_count = 0;
            $(document).find("select.possibleTransferOrganisatonAddresId").each(function () {
                if ($(this).val() == cur_val) {
                    cur_count = cur_count + 1;
                }
            });
            cur.closest("tr").find('.CountPeople').val(cur_count + '');
        });
        CollaboratorsNumberCount();
        PupilsNumberCount();
    }
    function PupilsNumberCount() {
        var pc = $(".pupils tr").length - 1;
        $(".pupilsCount").html('(' + pc + ')');
        var count = 0;
        $('.pupils tr').each(function () {
            $(this).find('.numerator').html('(' + (count++) + ')');
        });
    }
    function CollaboratorsNumberCount() {
        var cc = $(".collaborators tr").length - 1;
        $(".collaboratorsCount").html('(' + cc + ')');
        var count = 0;
        $('.collaborators tr').each(function () {
            $(this).find('.numerator').html('(' + (count++) + ')');
        });
    }
});
function UdpForm() {
    $("body").find("#IsMale").select2();
}
//# sourceMappingURL=Orphanage.Group.List.Edit.js.map