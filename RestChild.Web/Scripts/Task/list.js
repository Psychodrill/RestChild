$(function () {
    $('select').select2({ dropdownAutoWidth: true });
    $('.CampId').select2({
        initSelection: function (element, callback) {
            if (element.val() == '' || element.val() == 0)
                callback({ id: '', text: '-- Не выбрано --' });
            else
                callback({ id: element.val(), text: element.attr('titletext') });
        },
        allowClear: true,
        language: "ru",
        ajax: {
            url: rootPath + '/api/NewBout/GetCamps',
            dataType: 'json',
            quietMillis: 250,
            data: function (term) {
                return {
                    query: term
                };
            },
            results: function (data, page) {
                var res = [{ id: '', text: '-- Не выбрано --' }];
                for (var i = 0; i < data.length; i++) {
                    if (data[i].id) {
                        res.push({ id: data[i].id, text: data[i].name });
                    }
                }
                return {
                    results: res
                };
            },
            cache: true
        }
    });
});
//# sourceMappingURL=list.js.map