declare var rootPath: string;

function loadRegions(val, regionSelector) {
	var apiUrl = '';
	regionSelector.empty();
	if (val && val != '0') {
		apiUrl = rootPath + '/api/WebBtiDistricts/' + val + '/regions';
	} else {
		apiUrl = rootPath + '/api/WebBtiDistricts/regions';
	}
	regionSelector.append($('<option value="" data-district="" data-region="">Загрузка</option>'));
	regionSelector.parent().find('.loading').removeClass('invisible');
	$.ajax({
		dataType: 'json',
		url: apiUrl,
		data: null,
		success: (result) => {
			regionSelector.empty();
			regionSelector.append($('<option value="" data-district="" data-region="">-- Не выбрано --</option>'));
			for (var i in result) {
				regionSelector.append($('<option value="' + result[i].id + '" ' + (result[i].id == 0 ? 'selected' : '') + '>' + result[i].name + '</option>'));
			}
			regionSelector.parent().find('.loading').addClass('invisible');
			regionSelector.change();
		},
		error: () => {
			regionSelector.empty();
			regionSelector.append($('<option value="" data-district="" data-region="">Ошибка загрузки данных</option>'));
			regionSelector.parent().find('.loading').addClass('invisible');
		}
	});
}


$(() => {
	function formatTypeRest(state) {
		if (state.main) {
			return '<div style=\'padding-left:' + (state.level - 1) * 15 + 'px\'><b>' + state.text + '</b></div>';
		} else {
			return '<div style=\'padding-left:' + (state.level - 1) * 15 + 'px\'>' + state.text + '</div>';
		}
    }

    $('.type-of-rest').select2({
		query: (q) => {
			var res = [];
            typeOfRest.forEach((i) => {
                if (i.Name.toLowerCase().indexOf(q.term.toLowerCase()) > -1) {
                    res.push({ id: i.Id, text: i.Name, main: i.IsActive, level: i.MaxAge });
                }
			});

			var d = {
				results: res
			};

			q.callback(d);
		},
		initSelection: (element, callback) => {
			var res = { id: 0, text: '-- Не выбрано --', main: '', level: '' };
			typeOfRest.forEach((i) => {
				if ($(element).val() === i.Id.toString()) {
                    res = { id: i.Id, text: i.Name, main: i.IsActive, level: i.MaxAge };
				}
			});

			callback(res);
		},
		formatResult: formatTypeRest
	}).change(() => {
		var val = $('input.type-of-rest').select2('val');
		$('.TypeOfRestId').val(val);
	});

	$('#HotelsId').select2({
		initSelection: (element, callback) => {
			if ($('#_HotelsId').val() == '') {
				callback({ id: '', text: '-- Не выбрано --' });
			} else {
				callback({ id: $('#_HotelsId').val(), text: $('#_HotelsName').val() });
			}
		},
		minimumInputLength: 0,
		ajax: {
			url: rootPath + '/api/WebHotels',
			dataType: 'json',
			data: (term, page) => {
				return {
					name: term,
					onlyApproved: 'True'
				};
			},
			results: (data, page) => {
				var results = [{ id: '', text: '-- Не выбрано --' }];
				results = results.concat($.map(data, (item) => {
					return {
						text: item.name,
						id: item.id
					}
				}));
				return {
					results: results
				};
			},
			cache: true
		}
	});
	var timeOfRestFn = doT.template($('#timeOfRestTemplate').html());
	$('select.type-of-rest, select.year-of-rest').change((event, _) => {
		var val = $('select.type-of-rest').val();
		var yearOfRest = $('select.year-of-rest').val();
		var e = <any>event;
		$('select.time-of-rest').empty();
		$('select.time-of-rest').parent().find('.loading').removeClass('invisible');
		$('select.time-of-rest').append($('<option value="" data-district="" data-region="">Загрузка</option>'));
		$.ajax(rootPath + 'Api/WebVocabulary/GetTimesOfRestWithoutFilter?typeOfRestId=' + val + '&yearOfRestId=' + yearOfRest, {
			success: (data) => {
				$('select.time-of-rest').empty();
				var res = {
					selected: $('select.time-of-rest').val(),
					data: [{ id: 0, name: '-- Не выбрано --' }].concat(data)
				};

				$('select.time-of-rest').html(timeOfRestFn(res));

				if (e.val && val) {
					var selectedTime = $(e.target.selectedOptions[0]).attr('data-selected-time');
					if (val.Id && val.Id != 0 && selectedTime) {
						$('select.time-of-rest').select2('val', selectedTime);
					} else {
						$('select.time-of-rest').select2('val', '0');
					}
				} else {
					var timeOfRest = $('select.time-of-rest').val();
					$('select.type-of-rest').find('option:selected').attr('data-selected-time', timeOfRest);
				}
				$('select.time-of-rest').parent().find('.loading').addClass('invisible');
			},
			error: () => {
				$('select.time-of-rest').empty();
				$('select.time-of-rest').parent().find('.loading').addClass('invisible');
				$('select.time-of-rest').append($('<option value="" data-district="" data-region="">Ошибка загрузки данных</option>'));
			}
		});
	});
	$('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
	$('select.select2').select2();

	$('.district').change((event) => {
		loadRegions($(event.target).val(), $('select.region'));
	});

	$('.create-user-id').select2({
		initSelection: function (element, callback) {
			callback({ id: element.attr('data-id'), text: element.attr('data-text')});
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
				results.push({ id: 0, text: '-- Не выбрано --'});
				for (var i in data) {
					results.push({ id: data[i].Id, text: data[i].FullName })
					}
				return {
					results: results
				};
			},
			cache: true
		}
	});
});

function openRequest(action) {
	window.open(action, '_blank');
}
