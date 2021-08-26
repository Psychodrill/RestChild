$(()=>{
    $('select').select2();
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });

    let template = doT.template($('#templateParameter').html());

    let index = 1;

    let $rows = $('#rows');

    let $parameterText = $('#parameterText');

    $rows.on('click', '.gp-remove', (e)=>{
       $(e.target).closest('div.form-group').remove();
    });

    $('#btnParameter').on('click', ()=>{
        let text = $parameterText.val();
        if (!text) {
            ShowAlert('Для добавления укажите параметр подарка', "alert-danger", "glyphicon-ok", true);
            return;
        }

        let $r = $(template(-(index++)));
        $rows.append($r);
        inputMaskConfig($r);

        $r.find('.gp-name').val(text);
        $r.find('.gp-name-text').html(text);
        $parameterText.val('');
    });

    initPhotoUploader(undefined, undefined);
});
