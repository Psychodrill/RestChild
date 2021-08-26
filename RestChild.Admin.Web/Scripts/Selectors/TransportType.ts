declare var rootPath: string;

$(() => {
	$('#typeOfTransport').select2({
		initSelection: (element, callback) => {
			var data = [];
			$('div.selectedTypeOfTransport').each((i, elem) => {
				data.push({
					id: $(elem).find("input[name=\"typeId\"]").val(),
					text: $(elem).find("input[name=\"typeName\"]").val()
				});

				callback(data);
			});

		},
		allowClear: true,
		language: "ru",
		multiple:true,
		ajax: {
			url: rootPath + 'api/typeOfTransport',
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
	})
	.select2('val',[]);
});
