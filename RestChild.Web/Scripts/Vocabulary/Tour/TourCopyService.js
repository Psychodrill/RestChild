$(function () {
    $('#selectAll').change(function () {
        $('#tourTableCopyServices input[type=checkbox]').prop('checked', $('#selectAll').prop('checked'));
    });
    $('#selectAllService').change(function () {
        $('#serviceTableCopyServices input[type=checkbox]').prop('checked', $('#selectAllService').prop('checked'));
    });
    $('.btn-copy-service').click(function () {
        $('#copyServices input[type=checkbox]').prop('checked', false);
        $('#copyServices').modal('show');
    });
    $('#copyServicesBtn').click(function () {
        var toursIds = [];
        $('#tourTableCopyServices input[type=checkbox]').each(function (i, e) {
            var $e = $(e);
            if ($e.prop('checked') && $e.attr('tourId')) {
                toursIds.push($e.attr('tourId'));
            }
        });
        var servicesIds = [];
        $('#serviceTableCopyServices input[type=checkbox]').each(function (i, e) {
            var $e = $(e);
            if ($e.prop('checked') && $e.attr('serviceId')) {
                servicesIds.push($e.attr('serviceId'));
            }
        });
        var error = '';
        if (toursIds.length === 0) {
            error = error + '<li>Не выбрано ни одно размещение для копирования услуг</li>';
        }
        if (servicesIds.length === 0) {
            error = error + '<li>Не выбрана ни одна услуга для копирования</li>';
        }
        if (error) {
            ShowAlert('<ul>' + error + '</ul>', "alert-danger", "glyphicon-ok", true);
            return;
        }
        $.ajax({
            url: rootPath + 'Api/Commercial/CopyServices',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({
                id: $('#Data_Id').val(),
                replaceServices: $('#replaceService').prop('checked'),
                tourIds: toursIds,
                servicesIds: servicesIds
            }),
            success: function (e) {
                if (e.hasError) {
                    ShowAlert('Ошибка копирования услуг: ' + e.errorMessage, "alert-danger", "glyphicon-ok", true);
                    return;
                }
                else {
                    ShowAlert('Услуги успешно скопированы', "alert-success", "glyphicon-ok", true);
                    $('#copyServices').modal('hide');
                }
            }
        });
    });
});
//# sourceMappingURL=TourCopyService.js.map