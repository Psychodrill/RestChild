declare var rootPath: string;

declare var isServiceEditable: boolean;
$(() => {
	$('.age').inputmask("integer", { allowMinus: false, rightAlign: false });
	$('select').select2();

	jQuery.validator.methods["date"] = function (value, element) { return true; }

	$('.date>input').inputmask("d.m.y", {
		placeholder: "дд.мм.гггг",
		clearIncomplete: true
	});
	$('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });

	$('.datetime>input').inputmask("d.m.y h:s", {
		placeholder: "дд.мм.гггг чч:мм",
		clearIncomplete: true
	});
	$('.datetime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });

	$('#cbxIsGroup').change((e) => {
		if ($('#cbxIsGroup').prop('checked')) {
			$('#divNeedSize2').removeClass('hidden');
		} else {
			$('#divNeedSize2').addClass('hidden');
			$('.size').val('');
		}
	});

	$('#Data_TypeOfServiceId').change((e) => {
		$.ajax({
			url: rootPath + 'Api/AddonService/GetTypeService/' + $('#Data_TypeOfServiceId').select2('val').toString(),
			type: 'GET',
			dataType: 'json',
			success: (r) => {
				if (r.needAnnouncement) {
					$('#divNeedAnnouncement').removeClass('hidden');
				} else {
					$('#divNeedAnnouncement').addClass('hidden');
				}
				if (r.needConditions) {
					$('#divNeedConditions').removeClass('hidden');
				} else {
					$('#divNeedConditions').addClass('hidden');
				}
				if (r.needDescription) {
					$('#divNeedDescription').removeClass('hidden');
				} else {
					$('#divNeedDescription').addClass('hidden');
				}
				if (r.needDurationHour) {
					$('#divNeedDurationHour').removeClass('hidden');
				} else {
					$('#divNeedDurationHour').addClass('hidden');
				}
				if (r.needTransport) {
					$('.transport-div').removeClass('hidden');
				} else {
					$('.transport-div').addClass('hidden');
				}

				if (r.needSize) {
					$('#divNeedSize').removeClass('hidden');
				} else {
					$('#divNeedSize').addClass('hidden');
					$('#divNeedSize2').addClass('hidden');
					$('#cbxIsGroup').prop('checked', false);
					$('.size').val('');
				}
			}
		});
	});

	$('#Data_PartnerId').select2({
		initSelection: (element, callback) => {
			if (element.val() == '')
				callback({ id: '', text: '-- Не выбрано --' });
			else
				callback({ id: $('#_PartnerId').val(), text: $('#_PartnerName').val() });
		},
		minimumInputLength: 1,
		allowClear: true,
		language: "ru",
		ajax: {
			url: rootPath + '/api/Orgs',
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

	$('.main-hotelId').select2({
		initSelection: (element, callback) => {
			callback({ id: '', text: '-- Не выбрано --' });
		},
		minimumInputLength: 1,
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
				return {
					results: $.map(data, (item) => {
						return {
							text: item.name,
							id: item.id
						}
					})
				};

			},
			cache: true
		}
	}).select2('val', []);

	function processHotelTable() {
		if ($('#hotelsTable>tbody').children('tr').length > 0) {
			$('#hotelsTable>tbody>tr').each((num, val) => {
				$(val).find('input[name^="Hotels["],select[name^="Hotels["]').each((inputNum, input) => {
					var regexp = new RegExp('Hotels\\[.*?\\](.*)');
					var matched = $(input).attr('name').match(regexp);
					if (matched != null && matched.length > 1) {
						var name = matched[1];
						$(input).attr('name', 'Hotels[' + num + ']' + name);
						$(input).parent().find('span.field-validation-valid').attr('data-valmsg-for', 'Hotels[' + num + ']' + name);
					}
				});
			});
			$('#hotelsDiv').removeClass('hidden');
		} else {
			$('#hotelsDiv').addClass('hidden');
		}
	}

	function processServiceTable() {
		$("form")
			.removeData("validator")
			.removeData("unobtrusiveValidation");

		if ($('#prices>tbody').children('tr').length > 0) {
			$('#prices').show();
			$('#prices>tbody>tr').each((num, val) => {
				$(val).find('input[name^="Prices["],select[name^="Prices["],textarea[name^="Prices["]').each((inputNum, input) => {
					var regexp = new RegExp('Prices\\[.*?\\](.*)');
					var matched = $(input).attr('name').match(regexp);
					if (matched != null && matched.length > 1) {
						var name = matched[1];
						$(input).attr('name', 'Prices[' + num + ']' + name);
						$(input).parent().find('span.field-validation-valid').attr('data-valmsg-for', 'Prices[' + num + ']' + name);
					}
				});
			});
		}

		$.validator
			.unobtrusive
			.parse("form");
	}

	function removeService(e) {
		var $row = $(e.target).closest('tr');
		$row.remove();
		processServiceTable();
	}

	$('#addprice').click((e) => {
		var $tmpl = $($('#servicePriceRowTemplate').html());

		$tmpl.find('.decimal').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
		$tmpl.find('.integer').inputmask('integer', { allowMinus: false, rightAlign: false });

		$tmpl.find('.remove-price-btn').click(removeService);

		$tmpl.find('.date>input').inputmask("d.m.y", {
			placeholder: "дд.мм.гггг",
			clearIncomplete: true
		});
		$tmpl.find('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });

		var $tabl = $('#prices').find('tbody');
		$tabl.append($tmpl);
		processServiceTable();
	});

	$('.remove-price-btn').click(removeService);


	$('#hotelsTable').on('click', '.remove-hotel-btn', (e) => {
		var row = $(e.target).closest('tr');
		row.remove();
		processHotelTable();
	});

	$('#hotelAdd').click(() => {
		var data = $('.main-hotelId').select2('data');
		if (data.id <= 0) {
			ShowAlert('Укажите место отдыха для добавления', "alert-danger", "glyphicon-remove", true);
			return;
		}

		var row = $($('#hotelRow').html());
		var table = $('#hotelsTable');
		var tbody = table.find('tbody');
		row.find('.hotel-title').html(data.text);
		row.find('input').val(data.id);
		var hl = row.find('a.hotel-link');
		hl.attr('href', hl.attr('href') + data.id);
		$('.main-hotelId').select2('data', { id: '', text: '-- Не выбрано --' });

		tbody.append(row);
		processHotelTable();
	});

	$('#Data_addFileBtn').click(() => {
		$('#Data_addFileModalLoading').addClass('hidden');
		$('#Data_addFileModal').modal("show");
		$('#Data_addFileModal input').val(null);
		$('#Data_addFileModalImg').attr('src', null);
	});

	$('#Data_addFileModalUpload').fileupload({
		dataType: 'json',
		url: '/UploadHotelFile.ashx',
		add: (e, data) => {
			$('#Data_addFileModalLoading').removeClass('hidden');
			data.submit();
		},
		done: (e, data) => {
			$('#Data_addFileModalLoading').addClass('hidden');
			$('#Data_addFileModalSave').removeAttr('disabled');
			$('#Data_uploadedFileName').val(data.result[0].realname);
			$('#Data_addFileModalImg').attr('src', '/DownloadHotelFile.ashx/' + data.result[0].realname);
		}
	});

	$('#Data_addFileModalSave').click(() => {
		if (!$('#Data_addFileModalForm').valid()) {
			return;
		}
		var target = '#Data_photoSet';

		var tempFn = doT.template($('#Data_photoTemplate').html());
		$(target).append(tempFn({
			FileTitle: $('#Data_uploadedFileName').val(),
			FileName: $('#Data_addFileModalName').val(),
			Id: 0
		}));
		$('#Data_addFileModal').modal("hide");
	});

	$('body').on('click', '.remove-photo', (e) => {
		var photo = $(e.target).closest('.photo');
		$(photo.next()).remove();
		$(photo.remove());
	});

	$('#addonServiceForm').submit(() => {
		var photos = $('.photo-hidden');
		$.each(photos, (i, photo) => {
			var hiddens = $(photo).find('input[type="hidden"]');
			$.each(hiddens, (j, hidden) => {
				var dotPosition = $(hidden).attr('name').lastIndexOf('.');
				var name = dotPosition != -1 ? $(hidden).attr('name').substring(dotPosition + 1) : $(hidden).attr('name');
				$(hidden).attr('name', 'Data.Photos[' + i + '].' + name);
			});
		});

		return true;
	});

	$(document).on('shown.bs.tab', '#MainTabs a[data-toggle="tab"]', (e) => {
		$('#ActiveTab').val($(e.target).attr('href').substring(1));
	});

	$('.reset-hotel').click(() => {
		$('#Data_HotelsId').select2('data', { text: '-- Не выбрано --', id: null });
	});

	$('.text-editor').each((n, e) => {
		var $e = $(e);
		var div = $e.find('div.text-editor-div');
		var hdn = $e.find('input.text-editor-hdn');
		if (isServiceEditable) {
			div.summernote({
				lang: 'ru-RU',
				toolbar: [
					['style', ['style']],
					['font', ['bold', 'italic', 'underline', 'clear']],
					['fontname', ['fontname']],
					['color', ['color']],
					['para', ['ul', 'ol', 'paragraph']],
					['height', ['height']],
					['table', ['table']],
					['insert', ['link', 'hr']]
				],
				maxHeight: 300,
				height: 300
			});
			div.code(hdn.val());
		} else {
			div.html(hdn.val());
		}
	});
});



function confirmStateButtonAddonService(formSelector, actionSelector, actionCode, buttonName, description, commentSelector) {
	var ageFrom = $('#Data_AgeFrom');
	var ageTo = $('#Data_AgeTo');
	var typeOfService = $('.typeOfServiceId');

	function checkOnDate($eDateFrom, $eDateTo) {
		var hasError = false;
		if ($eDateFrom.is(':disabled') && $eDateTo.is(':disabled')) {
			return hasError;
		}

		var dateFrom = moment($eDateFrom.val(), 'DD.MM.YYYY');
		var dateTo = moment($eDateTo.val(), 'DD.MM.YYYY');
		if (dateFrom.isValid() && dateTo.isValid() && dateFrom.isAfter(dateTo)) {
			$eDateFrom.closest('.col-md-2').find('.field-validation-error').html('Дата с должна быть не позже даты по');
			$eDateFrom.closest('.date').addClass('has-error');
			$eDateTo.closest('.date').addClass('has-error');
			hasError = true;
		} else {
			$eDateFrom.closest('.date').removeClass('has-error');
			$eDateTo.closest('.date').removeClass('has-error');
			$eDateFrom.closest('.col-md-2').find('.field-validation-error').html('');
		}

		return hasError;
	}

	var hasError = false;

	if (typeOfService.select2('val') === '0') {
		typeOfService.closest('div.col-md-10').find('.field-validation-error').html('Заполните поле');
		typeOfService.addClass('input-validation-error');
		hasError = true;
	} else {
		typeOfService.removeClass('input-validation-error');
		typeOfService.closest('div.col-md-10').find('.field-validation-error').html('');
	}

	$('#prices').children('tbody').children('tr').each((i, e) => {
		var $e = $(e);
		var $eDateFrom = $e.find('.date-from');
		var $eDateTo = $e.find('.date-to');
		hasError = checkOnDate($eDateFrom, $eDateTo) || hasError;
	});

	hasError = checkOnDate($('#dateFrom'), $('#dateTo')) || hasError;

	if (hasError) {
		return undefined;
	}

	$('.text-editor').each((n, e) => {
		var $e = $(e);
		var div = $e.find('div.text-editor-div');
		var hdn = $e.find('input.text-editor-hdn');
		if (isServiceEditable) {
			hdn.val(div.code());
		}
	});

	if (!ageFrom.is(':disabled') && !ageTo.is(':disabled')) {
		var ageFromVal = parseInt(ageFrom.val());
		var ageToVal = parseInt(ageTo.val());
		if (ageFromVal !== NaN && ageToVal !== NaN && ageFromVal > ageToVal) {
			BootstrapDialog.show({
				title: "Ошибка",
				message: "Некорректно введен возраст"
			});
			return undefined;
		}
	}

	return confirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector);
}
