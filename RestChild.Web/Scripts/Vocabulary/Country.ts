function countrySelect2(selector: string, selectorId: string, selectorName: string, rootPath : string) {
	$(selector).select2({
		initSelection: (element, callback) => {
			if ($(selectorId).val() == '') {
				callback({ id: '', text: '-- Не выбрано --' });
			} else {
				callback({ id: $(selectorId).val(), text: $(selectorName).val() });
			}
		},
		minimumInputLength: 0,
		ajax: {
			url: rootPath + '/api/WebCountry/Get',
			dataType: 'json',
			data: (term, page) => {
				return {
					query: term
				};
			},
			results: (data, page) => {
				var results = [{ id: '', text: '-- Не выбрано --' }];
				results = results.concat($.map(data, (item) => {
					return {
						text: item.fullName,
						id: item.id
					}
				}));
				return {
					results: results
				};
			}
		}
	});
} 