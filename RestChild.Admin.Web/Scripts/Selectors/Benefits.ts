$(() => $('#benefitSelect').select2({
	initSelection: (element, callback) => {
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
		data: (term, page) => {
			return {
				query: term,
			};
		},
		results: (data, page) => {
			var res = [];

			for (let i = 0; i < data.length; i++) {
				if (data[i].name) {
					data[i].text = data[i].name;
					res.push({ id: data[i].id, text: data[i].name });
					data[i].children = [];
				};
			};

			return {
				results: res
			};
		},
		cache: true
	}
}));

