$(() => {
	$('#timeOfRestSelect').select2({
		initSelection: (element, callback) => {
			if (element.val() == '' || element.val() == 0)
				callback({ id: '', text: '-- Не выбрано --' });
			else
				callback({ id: $('#_timeOfRestId').val(), text: $('#_timeOfRestName').val() });
		},
		allowClear: true,
		language: "ru",
		ajax: {
			url: rootPath + '/api/GroupedTimeOfRest/Get',
			dataType: 'json',
			quietMillis: 250,
			data: (term, page) => {
				return {
					query: term,
				};
			},
			results: (data, page) => {
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
}
)
