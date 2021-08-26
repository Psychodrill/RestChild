﻿$(()=>{
    $('select').select2();
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });

    $('.time-only').inputmask("h:s", {
        placeholder: "чч:мм",
        clearIncomplete: true
    });

    $('.ts-type').on('change', (e)=>{
        let $e = $(e.target).closest('input');
        if (!$e.prop('checked'))
        {
            return;
        }

        if ($e.val()==='Simple'){
            $('.Simple').addClass('hidden');
        } else {
            $('.Simple').removeClass('hidden');
        }

        if ($e.val()==='EveryWeek'){
            $('.EveryWeek').removeClass('hidden');
        } else {
            $('.EveryWeek').addClass('hidden');
        }

        if ($e.val()==='EveryDay'){
            $('.EveryDay').removeClass('hidden');
        } else {
            $('.EveryDay').addClass('hidden');
        }
    });

    initPhotoUploader(undefined, undefined);
});
