var infoPanelNum = 0;
function showInfo(info: string) {
	var num = (++infoPanelNum);
	var newItem = '<div id=InfoPanelItem'+num+' class="alert alert-info infoPanel-fade" role="alert"><span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true"></span><span> ' + info + '</span></div>';
	$('.infoPanel').append(newItem);

	window.setTimeout(() => {
		$('#InfoPanelItem' + num).addClass('infoPanel-fade-in');
	}, 1000*num);

	window.setTimeout(() => {
		$('#InfoPanelItem' + num).removeClass('infoPanel-fade-in');
	}, 3000 + 1000 * num);
};
