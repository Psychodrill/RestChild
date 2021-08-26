function changeRowTicketReason($target, resp) {
	var tr = $target.closest('tr');

	tr.removeClass('gray');
	tr.removeClass('has-error');
	tr.removeClass('has-warning');
	if (resp === 0) {
		$target.select2('val', null);

		$target.closest('tr').find('.transport-info-directory-flight-select').removeAttr('disabled');
		$target.closest('tr').find('.transport-info-directory-wagon-input').removeAttr('disabled');
		$target.closest('tr').find('.transport-info-directory-placenumber-input').removeAttr('disabled');
		$target.closest('tr').find('.departure-date').removeAttr('disabled');
	} else {
		if (resp === 1) {
			tr.addClass('gray');
		}
		else if (resp === 2) {
			tr.addClass('has-error');
		}
		else if (resp === 3) {
			tr.addClass('has-warning');
			var cbx = $target.closest('tr').find('.notComeInPlaceOfRest');
			cbx.prop('checked', false);
		}

		$target.select2('val', resp);
		$target.closest('tr').find('.transport-info-directory-flight-select').select2('data', { value: '', text: '-- Не выбрано --' }).attr('disabled', 'disabled');
		$target.closest('tr').find('.transport-info-directory-wagon-input').val('').attr('disabled', 'disabled');
		$target.closest('tr').find('.transport-info-directory-placenumber-input').val('').attr('disabled', 'disabled');
		$target.closest('tr').find('.departure-date').val('').attr('disabled', 'disabled');
		$target.closest('tr').find('.transport-info-directory-placement').addClass('hidden');
	}
}

$(() => {
	$(document).on('change', 'select.not-need-ticket-select', (e) => {
		var $target = $(e.target);
		var tr = $target.closest('tr');
		var value = $target.select2('val');
		var id = $target.attr('data-id');
		var name = tr.find('.camper-name, .transport-info-fio').html();
		var url = rootPath + '/api/WebTransportInfo/SetNotNeedTicketReason';

		//if ($(e.target).hasClass('not-need-ticket-child')) {
		//	url = rootPath + '/api/WebTransportInfo/SetNotNeedTicketReasonForChild';
		//} else if ($(e.target).hasClass('not-need-ticket-attendant')) {
		//	url = rootPath + '/api/WebTransportInfo/SetNotNeedTicketReasonForAttendant';
		//}

		$.ajax({
			method: 'GET',
			url: url,
			data: {
				id: id,
				reasonId: value
			},
			success: (resp) => {
				changeRowTicketReason($target, resp);
				if (resp == 3 && (typeof setNotComeInPlaceOfRest === 'function')) {
					setNotComeInPlaceOfRest($target, true);
					recalcCountIn($target, true);
				}
				ShowAlert(name + ". Информация об отказе от билета обновлена", "alert-success", "glyphicon-ok", true);
			},
			error: () => {
				ShowAlert(name + ". Ошибка обновления информации", "alert-danger", "glyphicon-remove");
			}
		});
	});

	$(document).on('change', 'input.not-need-ticket', (e) => {
		var tr = $(e.target).closest('tr');
		var id = $(e.target).attr('data-id');
		var needTicket = !$(e.target).is(':checked');
		var name = tr.find('.camper-name, .transport-info-fio').html();

		$.ajax({
			method: 'GET',
			url: rootPath + '/api/WebTransportInfo/SetNeedTicket',
			data: {
				id: id,
				value: needTicket
			},
			success: (resp) => {
				if (!resp) {
					$(e.target).closest('tr').find('.transport-info-directory-flight-select').select2('data', { value: '', text: '-- Не выбрано --' }).attr('disabled', 'disabled');
					$(e.target).closest('tr').find('.transport-info-directory-wagon-input').val('').attr('disabled', 'disabled');
					$(e.target).closest('tr').find('.transport-info-directory-placenumber-input').val('').attr('disabled', 'disabled');
					$(e.target).closest('tr').find('.departure-date').val('').attr('disabled', 'disabled');
					$(e.target).closest('tr').find('.transport-info-directory-placement').addClass('hidden');
				} else {
					$(e.target).closest('tr').find('.transport-info-directory-flight-select').removeAttr('disabled');
					$(e.target).closest('tr').find('.transport-info-directory-wagon-input').removeAttr('disabled');
					$(e.target).closest('tr').find('.transport-info-directory-placenumber-input').removeAttr('disabled');
					$(e.target).closest('tr').find('.departure-date').removeAttr('disabled');
				}
				ShowAlert(name + ". Информация о билете обновлена", "alert-success", "glyphicon-ok", true);
			},
			error: () => {
				ShowAlert(name + ". Ошибка обновления информации", "alert-danger", "glyphicon-remove");
			}
		});
	});
});
