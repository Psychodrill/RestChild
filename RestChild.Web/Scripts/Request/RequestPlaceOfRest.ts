$(() => {
	$('.placeOfRestId')
		.each((i, e) => {
			$(e).select2({
				initSelection: (element, callback) => {
					if ($(e).val() === '') {
						callback(null);
					} else {
						callback({ id: $(e).val(), text: $(e).attr('placeName') });
					}
				},
				minimumInputLength: 0,
				ajax: {
					url: rootPath + 'Api/WebRestPlace/GetPlaceOfRests',
					dataType: 'json',
					type: 'POST',
					contentType: "application/json; charset=utf-8",
					params: {
						contentType: 'application/json'
					},
					data: (t) => {
						let selected = [];
						$('input.placeOfRestId')
							.each((i, s) => {
								let v = $(s).select2('val');
								if (v && !$(e).is($(s))) {
									selected.push(parseInt(v));
								}
							});
						return JSON.stringify({
                            presentIds : selected,
                            tid: $('#typeOfRest-select2').val(),
                            t: t
                        });
					},
					results: (data) => {
						let results = [];
						let selected = [];
						$('input.placeOfRestId')
							.each((i, s) => {
								let v = $(s).select2('val');
								if (v && !$(e).is($(s))) {
									selected.push(parseInt(v));
								}
							});

						for (let j = 0; j < data.length; j++) {
							if ($.inArray(data[j].id, selected) < 0) {
								results.push({
									text: data[j].name,
									id: data[j].id
								});
							}
						}

						return {
							results: results
						};
					},
					cache: true
				}
			});
		});
});
