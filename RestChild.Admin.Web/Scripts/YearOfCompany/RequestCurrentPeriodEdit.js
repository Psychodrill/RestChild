function sendEvent(codeEvent) {
    $('#askDateTime').modal({ backdrop: 'static' });
    $('#ok-button').unbind();
    $('#ok-button').click(function () {
        $.ajax({
            method: 'post',
            url: rootPath + '/Api/WebRequestCurrentPeriod/SendEvent?id=' + $('.yearOfRestId').val() + '&eventCode=' + codeEvent + '&planDate=' + $('#planSendEventDate').val(),
            success: function (result) {
                if (result === true || result === 'true') {
                    $('#btn' + codeEvent).remove();
                    $('#askDateTime').modal('hide');
                    ShowAlert("Запрошена отправка статусов. Статусы будут отправлены в ЕЛК в указанное время.", "alert-success", "glyphicon-ok", true);
                }
                else {
                    ShowAlert('Нет прав на отправку статусов', "alert-danger", "glyphicon-ok", true);
                }
            }
        });
    });
}
$(function () {
    $('.datetimepicker').datetimepicker({
        showTodayButton: true,
        format: 'DD.MM.YYYY'
    });
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });
    inputMaskConfig($);
    $('[data-toggle="tooltip"]').tooltip();
    $('form').on('submit', function (e) {
        $('#yearName').val($('#yearNumber').val());
    });
    inputMaskDateTimeAnytime($('.input-mask-datetime-anytime'));
});
//# sourceMappingURL=RequestCurrentPeriodEdit.js.map