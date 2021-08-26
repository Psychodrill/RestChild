declare var rootPath: string;

$(() => $('#hotelSelect').select2({
	initSelection: (element, callback) => {
		if (element.val() == '' || element.val() == 0)
			callback({ id: '', text: '-- Не выбрано --' });
		else
			callback({ id: $('#_hotelId').val(), text: $('#_hotelName').val() });
	},
	minimumInputLength: 1,
	allowClear: true,
	language: "ru",
	ajax: {
		url: rootPath + 'api/WebHotels',
		dataType: 'json',
		quietMillis: 250,
		data: (term, page) => {
			return {
				name: term,
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
}));

