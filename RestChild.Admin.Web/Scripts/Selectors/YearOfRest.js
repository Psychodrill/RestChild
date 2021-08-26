$(function () {
    $('#yearOfRestSelect').select2({
        initSelection: function (element, callback) {
            if (element.val() == '' || element.val() == 0)
                callback({ id: '', text: '-- Не выбрано --' });
            else
                callback({ id: $('#_yearOfRestId').val(), text: $('#_yearOfRestName').val() });
        },
        allowClear: true,
        language: "ru",
        ajax: {
            url: rootPath + '/api/WebRestYear/Get',
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                return {
                    query: term,
                };
            },
            results: function (data, page) {
                var res = [];
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
//# sourceMappingURL=YearOfRest.js.map