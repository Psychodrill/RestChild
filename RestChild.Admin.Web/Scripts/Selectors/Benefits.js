$(function () { return $('#benefitSelect').select2({
    initSelection: function (element, callback) {
        if (element.val() == '' || element.val() == 0)
            callback({ id: '', text: '-- Не выбрано --' });
        else
            callback({ id: $('#_benefitId').val(), text: $('#_benefitName').val() });
    },
    allowClear: true,
    language: "ru",
    ajax: {
        url: rootPath + 'api/WebBenefitType/Get',
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
                if (data[i].name) {
                    data[i].text = data[i].name;
                    res.push({ id: data[i].id, text: data[i].name });
                    data[i].children = [];
                }
                ;
            }
            ;
            return {
                results: res
            };
        },
        cache: true
    }
}); });
//# sourceMappingURL=Benefits.js.map