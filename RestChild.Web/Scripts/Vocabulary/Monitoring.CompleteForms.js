$(function () {
    $('.select2').select2();
    var $formType = $('#formType');
    $('#btnExcel').on('click', function () {
        var subUrl = $formType.val();
        window.location.replace(rootPath + subUrl.substr(1) + '?yearOfRestId=' + $('#year').val());
    });
});
//# sourceMappingURL=Monitoring.CompleteForms.js.map