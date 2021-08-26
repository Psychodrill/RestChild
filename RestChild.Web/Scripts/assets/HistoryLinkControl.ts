function initHistoryElements(area) {
	area.find('.btn-hystory-link').click((e) => {
		$('#HistoryModalDialog').remove();
		$('body').append($('#HistoryModalDialogTemplate').html());
		$('#HistoryModalDialog').modal('show');
		$.ajax({
			url: rootPath + 'api/WebHistory/GetByHistoryLink',
			data: { historyLinkId: $(e.target).closest('a').attr('data-history-id') }
		}).done((data) => {
			if (data == null || data.length == 0 || data.length == null) {
				$('#HistoryModalDialog .modal-body').html('<p class="text-center">История отсутствует </p>');
				return;
			}
			var tempFn = doT.template($('#HistoryModalTableTemplate').html());
			$('#HistoryModalDialog .modal-body').html(tempFn($.map(data, (val) => {
				var dateChange = moment(val.dateChange, "YYYY-MM-DDTHH:mm:ss");
				return {
					eventCode: val.eventCode != null ? val.eventCode : '-',
					account: val.account ? val.account.name : !val.authorString ? '-' : val.authorString,
					commentary: val.commentary != null ? val.commentary : '-',
					dateChange: dateChange.isValid() ? dateChange.format('DD.MM.YYYY HH:mm') : '-'
				};
			})));
		}).fail(() => {
			$('#HistoryModalDialog .modal-body').html('<p class="text-center">Ошибка получения данных</p>');
		});
	});

}
$(() => {
	initHistoryElements($(document));
});
