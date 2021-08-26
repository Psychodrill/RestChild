function initMobileHistoryElements(area) {
    area.find('.btn-hystory-link').click((e) => {
        $('#HistoryModalDialog').remove();
        $('body').append($('#HistoryModalDialogTemplate').html());
        let $dialog =$('#HistoryModalDialog');
        $dialog.modal('show');
        $.ajax({
            url: rootPath + 'api/HistoryMobile/GetByHistoryLink',
            data: { historyLinkId: $(e.target).closest('a').attr('data-history-id') }
        }).done((data) => {
            if (data == null || data.length == 0 || data.length == null) {
                $dialog.find('.modal-body').html('<p class="text-center">История отсутствует </p>');
                return;
            }
            let tempFn = doT.template($('#HistoryModalTableTemplate').html());
            $dialog.find('.modal-body').html(tempFn($.map(data, (val) => {
                let dateChange = moment(val.dateChange, "YYYY-MM-DDTHH:mm:ss");
                return {
                    eventCode: val.eventCode != null ? val.eventCode : '-',
                    account: val.authorString,
                    commentary: val.commentary != null ? val.commentary : '-',
                    dateChange: dateChange.isValid() ? dateChange.format('DD.MM.YYYY HH:mm') : '-'
                };
            })));
        }).fail(() => {
            $dialog.find('.modal-body').html('<p class="text-center">Ошибка получения данных</p>');
        });
    });
}
$(() => {
    initMobileHistoryElements($(document));
});
