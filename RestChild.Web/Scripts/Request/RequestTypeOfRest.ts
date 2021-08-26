$(() => {
	$('#YearOfRestId')
		.select2({
			initSelection: (element, callback) => {
                let $yearOfRestId = $('#YearOfRestId');
                if ($yearOfRestId.val() === '') {
					callback(null);
				} else {
					callback({id: $yearOfRestId.val(), text: $yearOfRestId.attr('YearName')});
				}
			},
			minimumInputLength: 0,
			ajax: {
				url: rootPath + '/api/WebVocabulary/GetYearsForTypeOfRest',
				dataType: 'json',
				data: (term) => {
					return {
						name: term,
						typeOfRestId: $('.TypeOfRestId').val()
					};
				},
				results: (data) => {
					let results = [];
					results = results.concat($.map(data,
						(item) => {
							return {
								text: item.name,
								id: item.id
							};
						}));
					return {
						results: results
					};
				},
				cache: true
			}
		});

	$('#representInterest').select2({
		initSelection: (element, callback) => {
			let $e = $('#representInterest');
			if ($e.val() === '') {
				callback({id: '', text: '-- Не выбрано --'});
			} else {
				callback({id: $e.val(), text: $e.attr('titleText')});
			}
		},
		minimumInputLength: 0,
		ajax: {
			url: rootPath + 'Api/WebVocabulary/GetRepresentInterest',
			dataType: 'json',
			data: () => {
				let t = $('.TypeOfRestId').val();
				return {
					id: t
				};
			},
			results: (data) => {
				let results = [{id: '', text: '-- Не выбрано --'}];

				for (let j = 0; j < data.length; j++) {
					results.push({
						text: data[j].name,
						id: data[j].id
					});
				}

				return {
					results: results
				};
			},
			cache: false
		}
	});

	$('.time-of-rest')
		.each((i, e) => {
			$(e).select2({
				initSelection: (element, callback) => {
					if ($(e).val() === '') {
						callback(null);
					} else {
						callback({id: $(e).val(), text: $(e).attr('TimeName')});
					}
				},
				minimumInputLength: 0,
				ajax: {
					url: rootPath + 'Api/WebVocabulary/GetTimesOfRestWithoutFilter',
					dataType: 'json',
					data: () => {
						let y = $('#YearOfRestId').val();
						if (!y) {
							y = '-1';
						}
						let t = $('.TypeOfRestId').val();
						if (!t) {
							t = '-1000';
						}
						return {
							yearOfRestId: y,
							typeOfRestId: t
						};
					},
					results: (data) => {
						let results = [];
						let selected = [];
						$('input.time-of-rest')
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

	$('#SaveButton')
		.click((e) => {
			if ($('#manualTypeOfRest').val() === '1') {
				let allSelectedSelected = true;
				$('.select-must-selected').each((i, e) => {
					let $e = $(e);
					let val = $e.select2('val');
					if ((!val || val === '0') && $e.closest('.hidden').length === 0) {
						allSelectedSelected = false;
					}
				});

				if (!allSelectedSelected) {
					e.preventDefault();
					ShowAlert("Необходимо заполнить все обязательные поля", "alert-danger", "glyphicon-remove", true);
					return;
				}

				if (!$('.TypeOfRestId').val() || !$('#YearOfRestId').select2('val')) {
					e.preventDefault();
					ShowAlert("Для сохранения заявления необходимо указать цель обращения и год кампании", "alert-danger", "glyphicon-remove", true);
				}

			}
		});
});
