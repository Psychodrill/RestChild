$(() => {
    $("select").select2();

    $('.organisationSelect').select2({
        initSelection: (element, callback) => {
            const data = {
                id: element.attr("data-default-id"),
                text: element.attr("data-default-text"),
            };

            if (data.id && data.id.length > 0) {
                callback(data);
            }
            else {
                callback({ id: -1, text: '-- Не выбрано --' });
            }
        },
        minimumInputLength: 1,
        ajax: {
            url: rootPath + 'api/Prefectures',
            dataType: 'json',
            quietMillis: 250,
            data: (term, page) => {
                return {
                    query: term,
                };
            },
            results: (data, page) => {
                var results = [];
                results.push({ id: 0, text: '-- Не выбрано --' });
                for (var i in data) {
                    results.push({ id: data[i].id, text: data[i].name });
                }

                return {
                    results: results
                };
            },
            cache: true
        }
    });
});
