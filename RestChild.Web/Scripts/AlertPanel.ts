var _AlertPanelItemNum = 1;
function ShowAlert(text: string, className: string, glyphicon: string, autoClose : boolean = false) {
	var newItem = '<div class="alert ' + className + ' alert-item-fade" id="AlertItem-' + _AlertPanelItemNum + '" role="alert">' +
						'<div><button type="button" class="close alert-item-close"><span>&times;</span></button></div>' +
						'<div><span class="glyphicon glyphicon ' + glyphicon + '">&nbsp;</span>' + text + '' +'</div>' +
					'</div >';
	$('#AlertContainer').append(newItem);
	var currentItemNum = _AlertPanelItemNum++;

	window.setTimeout(() => {
		$('#AlertItem-' + currentItemNum).addClass('alert-item-fade-in');
	}, 400);

	if (autoClose) {
		window.setTimeout(() => {
			CloseAlert($('#AlertItem-' + currentItemNum));
		}, 7000);
	}
};

$(() => {
	$(document).on('click', '.alert-item-close', (e) => {
		var alert = $(e.target).closest('.alert');
		CloseAlert(alert);
	});
});

function CloseAlert(alert: JQuery) {
	alert.removeClass('alert-item-fade-in');
	window.setTimeout(() => {
		alert.remove();
	}, 400);
};
