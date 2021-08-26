function sendEvent(codeEvent) {

	$('#askDateTime').modal({ backdrop: 'static'});

	$('#ok-button').unbind();
	$('#ok-button').click(() => {
		$.ajax({
			method: 'post',
			url: rootPath + '/Api/WebRequestCurrentPeriod/SendEvent?id=' + $('.yearOfRestId').val() + '&eventCode=' + codeEvent + '&planDate=' + $('#planSendEventDate').val(),
			success: (result) => {
				if (result === true || result === 'true') {
					$('#btn' + codeEvent).remove();
					$('#askDateTime').modal('hide');
					ShowAlert("Запрошена отправка статусов. Статусы будут отправлены в ЕЛК в указанное время.", "alert-success", "glyphicon-ok", true);
				} else {
					ShowAlert('Нет прав на отправку статусов', "alert-danger", "glyphicon-ok", true);
				}
			}
		});	
	});
}

function sendToERL(CurrentPeriodId) {
    $.ajax({
        method: 'post',
        url: rootPath + '/RequestCurrentPeriod/Send24ToERL?CurrentPeriodId=' + CurrentPeriodId,
        success: (result) => {
            if (result === true || result === 'true') {
                ShowAlert('Данные по заявочной компании будут отправлены в ИС Социум', "alert-success", "glyphicon-ok", true);
            } else {
                ShowAlert('Во время отправки данных по заявочной компании произошла ошибка', "alert-danger", "glyphicon-ok", true);
            }
        }
    });	
}

function L2020DM(CurrentPeriodId) {
    $.ajax({
        method: 'post',
        url: rootPath + '/Api/WebFirstRequestCompany/Lok2020DecisionMake?currentPeriodId=' + CurrentPeriodId,
        success: (result) => {
            if (result === true || result === 'true') {
                ShowAlert('"Запрошена отправка статусов. Статусы будут отправлены.', "alert-success", "glyphicon-ok", true);
            } else {
                ShowAlert('Нет прав на отправку статусов', "alert-danger", "glyphicon-ok", true);
            }
        }
    });
}


$(() => {
	$('.datetimepicker').datetimepicker({
		showTodayButton: true,
		format: 'DD.MM.YYYY'
	});

	$('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });

	inputMaskConfig($);

	$('[data-toggle="tooltip"]').tooltip();

	$('form').on('submit',
		(e) => {
			$('#yearName').val($('#yearNumber').val());
		});
	inputMaskDateTimeAnytime($('.input-mask-datetime-anytime'));

});

