function countrySelect2(selector, selectorId, selectorName, rootPath) {
    $(selector).select2({
        initSelection: function (element, callback) {
            if ($(selectorId).val() == '') {
                callback({ id: '', text: '-- Не выбрано --' });
            }
            else {
                callback({ id: $(selectorId).val(), text: $(selectorName).val() });
            }
        },
        minimumInputLength: 0,
        ajax: {
            url: rootPath + '/api/WebCountry/Get',
            dataType: 'json',
            data: function (term, page) {
                return {
                    query: term
                };
            },
            results: function (data, page) {
                var results = [{ id: '', text: '-- Не выбрано --' }];
                results = results.concat($.map(data, function (item) {
                    return {
                        text: item.fullName,
                        id: item.id
                    };
                }));
                return {
                    results: results
                };
            }
        }
    });
}
//# sourceMappingURL=Country.js.map