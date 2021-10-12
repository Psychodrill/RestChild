declare var BookingMosgorturReestrAddChildUrl: string;
declare var BookingMosgorturReestrChooseGridUrl: string;

$(() => {
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date(new Date().setDate(new Date().getDate() + 15)) });
    $('select').select2();


    $("form").on("click",
        ".delete-child",
        function() {
            $(this).closest('div.child').hide();
            $(this).closest('div.child').find('input.childdeleted').val('true');
        });

    $("form").on("click",
        ".add-child",
        function() {
            var nextIndex = $("div.child").length;
            $.ajax({
                url: BookingMosgorturReestrAddChildUrl,
                type: 'POST',
                data: { index: nextIndex },
                success: function(results) {
                    $(results).insertBefore($("div.add-child-row").first());
                    $("div.child").last().find(".select2").select2();
                    $("div.child").last().find(".date").datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
                }
            });
        });

    $("form").on("change",
        ".No-Middle-Name",
        function() {
            let closest_midname = $(this).closest(".form-group").find(".Middle-Name");
            if (this.checked) {
                $(closest_midname).val('');
                $(closest_midname).prop('readonly', true);
            } else {
                $(closest_midname).prop('readonly', false);
            }
        });

    if ($("#PinCode").val() == "") {
        GetGrid();
    }

    $("form").on("click", ".visitGrid-time", function () {
        $(this).closest(".visitGrid").find(".visitGrid-time").each(function () {
            $(this).find(":first-child").removeClass("btn-success");
            $("#Time").val('');
        });
        let div = $(this).find(":first-child");
        div.addClass("btn-success");
        $("#Time").val(div.attr("s-time"));
    });

    let changed = false;
    $("form").on("dp.change", ".visitGrid .date", function () {
        if (!changed) {
            $("#Time").val('');
            GetGrid();
        }
    });

    $("form").on("change", ".visitGrid .select2", function () {
        if (!changed) {
            $("#Time").val('');
            GetGrid();
        }
    });

    function GetGrid() {
        let v1 = $("form .visitGrid #SelectedTarget").val();
        let v2 = $("form .visitGrid #Date").val();
        let v3 = $("form .visitGrid #SlotsCount").val();

        let m = moment(v2, 'DD.MM.YYYY', true);

        if (v1 > 0 && m.isValid() && v3 > 0) {
            changed = true;
            $(".grid").html('');

            $.ajax({
                url: BookingMosgorturReestrChooseGridUrl,
                type: 'POST',
                data: { selectedTarget: v1, slotsCount: v3, date: v2 },
                success: function (results) {
                    $(".grid").html(results);
                    changed = false;
                    let tval = $("#Time").val();
                    if (tval) {
                        let dvs = $(".grid").find(".visitGrid-time").find(`div[s-time='${tval}']`);
                        dvs.addClass("btn-success");
                    }
                }
            });
        }
    }
});
