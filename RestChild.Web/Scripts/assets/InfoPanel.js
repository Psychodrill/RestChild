var infoPanelNum = 0;
function showInfo(info) {
    var num = (++infoPanelNum);
    var newItem = '<div id=InfoPanelItem' + num + ' class="alert alert-info infoPanel-fade" role="alert"><span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true"></span><span> ' + info + '</span></div>';
    $('.infoPanel').append(newItem);
    window.setTimeout(function () {
        $('#InfoPanelItem' + num).addClass('infoPanel-fade-in');
    }, 1000 * num);
    window.setTimeout(function () {
        $('#InfoPanelItem' + num).removeClass('infoPanel-fade-in');
    }, 3000 + 1000 * num);
}
;
//# sourceMappingURL=InfoPanel.js.map