$(function () {
    $('select').select2();
    $('#HotelId').select2({
        initSelection: function (element, callback) {
            if ($('#_HotelsId').val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $('#_HotelsId').val(), text: $('#_HotelsName').val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebHotels',
            dataType: 'json',
            data: function (term, page) {
                return {
                    name: term,
                    onlyApproved: 'True'
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                results = results.concat($.map(data, function (item) {
                    return {
                        text: item.name,
                        id: item.id
                    };
                }));
                return {
                    results: results
                };
            },
            cache: true
        }
    }).select2('val', []);
});
//# sourceMappingURL=AddonServiceList.js.map