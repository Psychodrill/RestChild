$(function () {
    $('select').select2();
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    var template = doT.template($('#templateParameter').html());
    var index = 1;
    var $rows = $('#rows');
    var $parameterText = $('#parameterText');
    $rows.on('click', '.gp-remove', function (e) {
        $(e.target).closest('div.form-group').remove();
    });
    $('#btnParameter').on('click', function () {
        var text = $parameterText.val();
        if (!text) {
            ShowAlert('Для добавления укажите параметр подарка', "alert-danger", "glyphicon-ok", true);
            return;
        }
        var $r = $(template(-(index++)));
        $rows.append($r);
        inputMaskConfig($r);
        $r.find('.gp-name').val(text);
        $r.find('.gp-name-text').html(text);
        $parameterText.val('');
    });
    initPhotoUploader(undefined, undefined);
});
//# sourceMappingURL=manage.js.map