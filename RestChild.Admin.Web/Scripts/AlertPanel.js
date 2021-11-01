var _AlertPanelItemNum = 1;
function ShowAlert(text, className, glyphicon, autoClose) {
    if (autoClose === void 0) { autoClose = false; }
    var newItem = '<div class="alert ' + className + ' alert-item-fade" id="AlertItem-' + _AlertPanelItemNum + '" role="alert">' +
        '<div><button type="button" class="close alert-item-close"><span>&times;</span></button></div>' +
        '<div><span class="glyphicon glyphicon ' + glyphicon + '">&nbsp;</span>' + text + '' + '</div>' +
        '</div >';
    $('#AlertContainer').append(newItem);
    var currentItemNum = _AlertPanelItemNum++;
    window.setTimeout(function () {
        $('#AlertItem-' + currentItemNum).addClass('alert-item-fade-in');
    }, 400);
    if (autoClose) {
        window.setTimeout(function () {
            CloseAlert($('#AlertItem-' + currentItemNum));
        }, 7000);
    }
}
;
$(function () {
    $(document).on('click', '.alert-item-close', function (e) {
        var alert = $(e.target).closest('.alert');
        CloseAlert(alert);
    });
});
function CloseAlert(alert) {
    alert.removeClass('alert-item-fade-in');
    window.setTimeout(function () {
        alert.remove();
    }, 400);
}
;
//# sourceMappingURL=AlertPanel.js.map