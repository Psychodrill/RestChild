declare var rootPath: string;

$(() => $('#vedomstvoSelect').select2({
	initSelection: (element, callback) => {
		if (element.val() == '' || element.val() == 0)
			callback({ id: '', text: '-- Не выбрано --' });
	},
	minimumInputLength: 1,
	allowClear: true,
	language: "ru",
	ajax: {
		url: rootPath + 'api/orgsAndVed',
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
				if (data[i].parentId == null) {
					res.push(data[i]);
					data[i].children = [];
				};
			};

			for (let i = 0; i < data.length; i++) {
				if (data[i].parentId != null) {

					var gr = null;
					for (var j = 0; j < res.length; j++) {
						if (res[j].id == data[i].parentId) {
							gr = res[j];
						}
					}
					gr.children.push({ id: data[i].id, text: data[i].name});
				}
			}

			var result = [];
			for (let i = 0; i < res.length; i++) {
				if (res[i].id) {
					if (res[i].children) {
						result.push({ id: res[i].id, text: res[i].name, children: res[i].children, disabled:false});
					}

				}
			}

			return {
				results: result
			};
		},
		cache: true
	}
}));

