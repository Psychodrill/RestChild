$(function () {
    $('.select2').select2();
    $(document).find('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    inputMaskDateAnytime($(document).find('.input-mask-date-anytime'));
    var $modal = $('#SendEventModal');
    $(document).on("click", ".sendEventModalShow", function () {
        $('#EventSendDate').val('');
        $('#EventSendText').val('');
        $modal.modal('show');
    });
    $(document).on("click", ".sendEvent", function () {
        var $date = $('#EventSendDate').val();
        var $msg = $('#EventSendText').val();
        if ($msg.length > 0) {
            $.ajax({
                url: SendMessageEventActtion,
                type: 'POST',
                data: { sendEventDate: $date, message: $msg },
                success: function (results) {
                    $modal.modal('hide');
                }
            });
        }
    });
});
//# sourceMappingURL=Monitoring.Lists.js.map