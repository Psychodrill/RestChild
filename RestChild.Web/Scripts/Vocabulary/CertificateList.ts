$(() => {
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('.datetimepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });
    inputMaskDateTimeAnytime($('.input-mask-datetime-anytime'));
    $('select').select2();
});

function openCertificate(action) {
    window.open(action, '_blank');
}
