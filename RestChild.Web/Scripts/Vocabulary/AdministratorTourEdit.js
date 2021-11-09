$(function () {
    $('.user-control').select2({
        initSelection: function (element, callback) {
            callback({ id: element.attr('data-id'), text: element.attr('data-text') });
        },
        minimumInputLength: 1,
        ajax: {
            url: rootPath + '/api/WebAccount/SearchAccount/',
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                return {
                    q: term,
                };
            },
            results: function (data, page) {
                var results = [];
                results.push({ id: 0, text: '-- Не выбрано --' });
                for (var i in data) {
                    results.push({ id: data[i].Id, text: data[i].FullName });
                }
                return {
                    results: results
                };
            },
            cache: true
        }
    });
});
//# sourceMappingURL=AdministratorTourEdit.js.map