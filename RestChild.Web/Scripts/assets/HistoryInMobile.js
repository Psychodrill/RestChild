function initMobileHistoryElements(area) {
    area.find('.btn-hystory-link').click(function (e) {
        $('#HistoryModalDialog').remove();
        $('body').append($('#HistoryModalDialogTemplate').html());
        var $dialog = $('#HistoryModalDialog');
        $dialog.modal('show');
        $.ajax({
            url: rootPath + 'api/HistoryMobile/GetByHistoryLink',
            data: { historyLinkId: $(e.target).closest('a').attr('data-history-id') }
        }).done(function (data) {
            if (data == null || data.length == 0 || data.length == null) {
                $dialog.find('.modal-body').html('<p class="text-center">История отсутствует </p>');
                return;
            }
            var tempFn = doT.template($('#HistoryModalTableTemplate').html());
            $dialog.find('.modal-body').html(tempFn($.map(data, function (val) {
                var dateChange = moment(val.dateChange, "YYYY-MM-DDTHH:mm:ss");
                return {
                    eventCode: val.eventCode != null ? val.eventCode : '-',
                    account: val.authorString,
                    commentary: val.commentary != null ? val.commentary : '-',
                    dateChange: dateChange.isValid() ? dateChange.format('DD.MM.YYYY HH:mm') : '-'
                };
            })));
        }).fail(function () {
            $dialog.find('.modal-body').html('<p class="text-center">Ошибка получения данных</p>');
        });
    });
}
$(function () {
    initMobileHistoryElements($(document));
});
//# sourceMappingURL=HistoryInMobile.js.map