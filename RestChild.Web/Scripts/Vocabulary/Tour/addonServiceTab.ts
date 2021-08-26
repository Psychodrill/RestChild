declare var rootPath: string;
var serviceEnumAddonPlace = 11;

$(() => {
	function addSpaces(nStr) {
		nStr += '';
		var x = nStr.split('.');
		var x1 = x[0];
		var x2 = x.length > 1 ? '.' + x[1] : '';
		var rgx = /(\d+)(\d{3})/;
		while (rgx.test(x1)) {
			x1 = x1.replace(rgx, '$1' + ' ' + '$2');
		}
		return x1 + x2;
	}

	// выбор услуги
	function initSelectService(control) {
		control.select2({
			minimumInputLength: 0,
			ajax: {
				url: rootPath + '/api/Commercial/GetAddonService',
				dataType: 'json',
				type: 'POST',
				contentType: "application/json; charset=utf-8",
				params: {
					contentType: 'application/json'
				},
				data: (term, page, e) => {
					var hotelsId = [];
					$('.main-hotelId').each((i, e) => {
						hotelsId.push($(e).select2('val'));
					});

					var typeService = $('input.base-service').attr('typeservice');
					return JSON.stringify({
						hotelId: hotelsId,
						servicesId: [],
						term: term,
						notTypeService: typeService
					});
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
		});
	}

	initSelectService($('.selectService:first'));

	function processTable(parent, newName) {
		var $tbody = parent.children('tbody');
		var $rows = $tbody.children('tr');
		var regexpServices = new RegExp('Services\\[\\d*\\].', 'g');
		var regexpPrices = new RegExp('Prices\\[\\d*\\].', 'g');

		if ($rows.length > 0) {
			$rows.each((num, val) => {
				var $td = $(val).children('td:first');
				$td.children('input').each((inputNum, input) => {

					var name = $(input).attr('name').replace(regexpServices, '');
					var n = newName + 'Services[' + num + '].' + name;
					$(input).attr('name', n);
				});

				var rowsPriceTable = $td.children('div.prices').children('div');
				rowsPriceTable.each((trNum, tr) => {
					$(tr).children('input').each((inputNum, input) => {
						var name = $(input).attr('name').replace(regexpServices, '').replace(regexpPrices, '');
						$(input).attr('name', newName + 'Services[' + num + '].Prices[' + trNum + '].' + name);
					});
				});

				var table = $td.children('div.selectServiceLinked').children('table.serviceTable');
				processTable(table, newName + 'Services[' + num + '].');
			});
		}

		parent.attr('parentname', newName);
	}

	function removeService(e) {
		var $row = $(e.target).closest('tr');
		$row.remove();
		processTable($('#mainServiceTable'), '');
	}

	var clasess = ['Id', 'AccountId', 'Code', 'Name', 'HistoryLinkId', 'ParentId', 'TourId', 'IsActive', 'Description', 'CuratorId', 'DirectoryFlightsId', 'TypeOfRoomsId', 'IsGroup', 'SizeMax', 'SizeMin', 'DateFrom', 'DateTo', 'DateBookingFrom', 'DateBookingTo', 'ByDefault', 'Requared', 'Hidden', 'NeedApprove', 'OnlyWithRequest'];
	var clasessPricess = ['prices-Id', 'prices-AddonServicesId', 'prices-LastUpdateTick', 'prices-AgeFrom', 'prices-AgeTo', 'prices-DateFrom', 'prices-DateTo', 'prices-Price', 'prices-PriceInternal'];
	$('#serviceDialog').find('.date>input').inputmask("d.m.y", { placeholder: "дд.мм.гггг", clearIncomplete: true });

	$('#serviceDialog').find('.CuratorId').select2({
		initSelection: (element, callback) => {
			callback({ id: element.val(), text: element.attr('accountname') });
		},
		minimumInputLength: 1,
		ajax: {
			url: rootPath + '/api/WebAccount/SearchAccount/',
			dataType: 'json',
			quietMillis: 250,
			data: (term, page) => {
				return {
					q: term,
				};
			},
			results: (data, page) => {
				var results = [];
				results.push({ id: 0, text: '-- Не выбрано --' });
				for (var i in data) {
					results.push({ id: data[i].Id, text: data[i].FullName });
				}
				return {
					results: results
				};
			},
			cache: true
		}
	});

	$('#serviceDialog').find('.DirectoryFlightsId').select2({
		initSelection: (element, callback) => {
			callback({ id: element.val(), text: element.attr('titletext') });
		},
		minimumInputLength: 0,
		ajax: {
			url: rootPath + '/api/WebDirectoryFlights/GetTourDirectoryFlights/',
			dataType: 'json',
			quietMillis: 250,
			data: (term, page) => {
				var parent = $('#serviceDialog').find('.hdn-parentid').val();
				var id = $('#serviceDialog').find('.hdn-id').val();
				return {
					addonServiceId: parent ? parent : id,
					yearOfRestId: $('#Data_YearOfRestId').select2('val'),
					q: term
				};
			},
			results: (data, page) => {
				var results = [];
				results.push({ id: 0, text: '-- Не выбрано --' });
				for (var i in data) {
					results.push({ id: data[i].id, text: data[i].name });
				}
				return {
					results: results
				};
			},
			cache: true
		}
	});

	$('#serviceDialog').find('.cbxIsGroup').change((e) => {
		var tr = $(e.target).closest('tr');
		if ($(e.target).prop('checked')) {
			tr.find('.divNeedSize2').removeClass('hidden');
		} else {
			tr.find('.divNeedSize2').addClass('hidden');
			tr.find('.size').val('');
		}
	});

	function prepareDialogPriceRow(template) {
		template.find('.decimal').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
		template.find('.integer').inputmask('integer', { allowMinus: false, rightAlign: false });
		template.find('.remove-serviceprice-btn').click((e) => {
			$(e.target).closest('tr').remove();
		});
	}

	function editService(e) {
		var $row = $(e.target).closest('tr').children('td');
		var $dialog = $('#serviceDialog');
		$dialog.find('.hdn-id').val($row.find('.hdn-id').val());
		$dialog.find('.hdn-parentid').val($row.find('.hdn-parentid').val());

		clasess.forEach((v) => {
			var item = $row.children('input.' + v);
			var target = $dialog.find('input.' + v);
			if (target.length === 1 && item.length === 1) {
				if (v === 'Description') {
					$('#serviceDialog').find('.text-editor-div').code(item.val());
				} else if (target.attr('type') === 'text') {
					target.val(item.val());
				} else if (target.attr('type') === 'checkbox') {
					target.prop('checked', item.val() === 'True');
				} else if (target.attr('type') === 'hidden') {
					if (item.val()) {
						target.select2('data', { id: item.val(), text: item.attr('titletext') });
					} else {
						target.select2('data', { id: '', text: '-- Не выбрано --' });
					}
				}
			}
		});

		//цены
		var items = $row.children('.prices').children('div');
		var $tbody = $dialog.find('table.prices-dialog>tbody');
		$tbody.empty();
		items.each((i, e) => {
			var $r = $(e);
			var template = $($('#servicePriceRowTemplate').html());
			clasessPricess.forEach((v) => {
				var item = $r.find('.' + v);
				var target = template.find('input.' + v);
				if (target.length === 1 && item.length === 1) {
					if (v === 'Description') {
						$('#serviceDialog').find('.text-editor-div').code(item.val());
					} else if (target.attr('type') === 'text' || target.attr('type') === 'hidden') {
						target.val(item.val());
					} else if (target.attr('type') === 'checkbox') {
						target.prop('checked', item.val() === 'True');
					}
				}
			});
			prepareDialogPriceRow(template);

			$tbody.append(template);
		});

		$.ajax({
			url: rootPath + 'Api/AddonService/GetSetService/' + $row.children('.ParentId').val(),
			type: 'GET',
			dataType: 'json',
			success: (r) => {
				$dialog.find('input[type=checkbox]').change((e) => {
					var $t = $(e.target);
					var $l = $t.closest('label');
					$l.find('input[type=hidden]').val($t.prop('checked') ? 'True' : 'False');
				});
				var serviceDescription = $row.children('div').children('div').children('div.service-description');

				var typeService = r.typeOfService || {};

				if (typeService.needAnnouncement) {
					$dialog.find('.divNeedAnnouncement').removeClass('hidden');
				} else {
					$dialog.find('.divNeedAnnouncement').addClass('hidden');
				}
				if (typeService.needConditions) {
					$dialog.find('.divNeedConditions').removeClass('hidden');
				} else {
					$dialog.find('.divNeedConditions').addClass('hidden');
				}
				if (typeService.needDescription) {
					$dialog.find('.divNeedDescription').removeClass('hidden');
				} else {
					$dialog.find('.divNeedDescription').addClass('hidden');
				}
				if (typeService.needDurationHour) {
					$dialog.find('.divNeedDurationHour').removeClass('hidden');
				} else {
					$dialog.find('.divNeedDurationHour').addClass('hidden');
				}
				if (typeService.needSize) {
					$dialog.find('.divNeedSize').removeClass('hidden');
					if (!r.isGroup) {
						$dialog.find('.divNeedSize2').addClass('hidden');
					} else {
						$dialog.find('.divNeedSize2').removeClass('hidden');
					}
				} else {
					$dialog.find('.divNeedSize').addClass('hidden');
					$dialog.find('.divNeedSize2').addClass('hidden');
				}

				if (typeService.needDurationHour) {
					$dialog.find('.divNeedDurationHour').removeClass('hidden');
				} else {
					$dialog.find('.divNeedDurationHour').addClass('hidden');
				}

				if (typeService.mayByDefault) {
					$dialog.find('.divMayByDefault').removeClass('hidden');
				} else {
					$dialog.find('.divMayByDefault').addClass('hidden');
				}

				if (typeService.mayRequared) {
					$dialog.find('.divMayRequared').removeClass('hidden');
				} else {
					$dialog.find('.divMayRequared').addClass('hidden');
				}
				if (typeService.needTransport) {
					$dialog.find('.divDirectoryFlightsId').removeClass('hidden');
				} else {
					$dialog.find('.divDirectoryFlightsId').addClass('hidden');
				}

				if (typeService.mayWithAccomodation) {
					$dialog.find('.divMayWithAccomodation').removeClass('hidden');
				} else {
					$dialog.find('.divMayWithAccomodation').addClass('hidden');
				}

				if (typeService.mayMustApprove) {
					$dialog.find('.divMayMustApprove').removeClass('hidden');
				} else {
					$dialog.find('.divMayMustApprove').addClass('hidden');
				}

				var isGroup = $dialog.find('.cbxIsGroup');
				if (isGroup.prop('checked')) {
					$dialog.find('.divNeedSize2').removeClass('hidden');
				} else {
					$dialog.find('.divNeedSize2').addClass('hidden');
					$dialog.find('.size').val('');
				}

				if (r.typeOfServiceId != serviceEnumAddonPlace) {
					$dialog.find('.serviceEnumAddonPlace').addClass('hidden');
				} else {
					$dialog.find('.serviceEnumAddonPlace').removeClass('hidden');
				}

				if (serviceDescription.find('.hdn-has-price').val() === 'True') {
					$dialog.find('.nav-tabs a:last').addClass('hidden');
				} else {
					$dialog.find('.nav-tabs a:last').removeClass('hidden');
				}

				if (r.typePriceCalculationId == 3) {
					$dialog.find('.dateService-label').html('Дата услуги');
					$dialog.find('.dateService-to').addClass('hidden');
				} else {
					$dialog.find('.dateService-label').html('Срок оказания услуги с');
					$dialog.find('.dateService-to').removeClass('hidden');
				}

				var sb = $dialog.find('.save-button');
				sb.unbind();
				sb.click(() => {

					var errorMessage = '';
					var dateFrom = moment($dialog.find('.DateFrom').val(), 'DD.MM.YYYY');
					var dateTo = moment($dialog.find('.DateTo').val(), 'DD.MM.YYYY');
					var bookingFrom = moment($dialog.find('.DateBookingFrom').val(), 'DD.MM.YYYY');
					var bookingTo = moment($dialog.find('.DateBookingTo').val(), 'DD.MM.YYYY');
					if (dateFrom.isValid() && dateTo.isValid()) {
						if (dateFrom.isAfter(dateTo)) {
							errorMessage = errorMessage + '<li>Срок оказания услуги с должен быть не позже чем срок оказания услуги по</li>';
						}
					}

					if (bookingFrom.isValid() && bookingTo.isValid()) {
						if (bookingFrom.isAfter(bookingTo)) {
							errorMessage = errorMessage + '<li>Дата бронирования с должна быть не позже чем дата бронирования по</li>';
						}
					}

					if (bookingTo.isValid() && dateFrom.isValid()) {
						if (bookingTo.isAfter(dateFrom)) {
							errorMessage = errorMessage + '<li>"Дата услуги" не может быть до даты "Бронирование по" </li>';
						}
					}

					if (errorMessage !== '') {
						ShowAlert('<ul>' + errorMessage + '</ul>', "alert-danger", '', true);
						return;
					}

					clasess.forEach((v) => {
						var item = $row.children('input.' + v);
						var descrItem = serviceDescription.find('span.' + v);
						var target = $dialog.find('input.' + v);
						if (target.length === 1 && item.length === 1) {
							if (v === 'Description') {
								item.val($('#serviceDialog').find('.text-editor-div').code());
							} else if (target.attr('type') === 'text') {
								item.val(target.val());
							} else if (target.attr('type') === 'checkbox') {
								item.val(target.prop('checked') ? 'True' : 'False');
								if (v === 'Requared') {
									if (target.prop('checked')) {
										$row.find('.descr-flag-Requared').html('Обязательная');
									} else {
										$row.find('.descr-flag-Requared').html('Не обязательная');
									}
								}

								if (v === 'ByDefault') {
									if (target.prop('checked')) {
										$row.find('.descr-flag-ByDefault').removeClass('hidden');
									} else {
										$row.find('.descr-flag-ByDefault').addClass('hidden');
									}
								}
								if (v === 'NeedApprove') {
									if (target.prop('checked')) {
										$row.find('.descr-flag-NeedApprove').removeClass('hidden');
									} else {
										$row.find('.descr-flag-NeedApprove').addClass('hidden');
									}
								}
								if (v === 'Hidden') {
									if (target.prop('checked')) {
										$row.find('.descr-flag-Hidden').removeClass('hidden');
									} else {
										$row.find('.descr-flag-Hidden').addClass('hidden');
									}
								}
								if (v === 'OnlyWithRequest') {
									if (target.prop('checked')) {
										$row.find('.descr-flag-OnlyWithRequest').removeClass('hidden');
									} else {
										$row.find('.descr-flag-OnlyWithRequest').addClass('hidden');
									}
								}
							} else if (target.attr('type') === 'hidden') {
								var d = target.select2('data');
								item.val(d.id);
								item.attr('titletext', d.text);

								if (v === 'CuratorId') {
									if (d.id) {
										$row.find('.CuratorTitle').html(d.text);
										$row.find('.descr-curator-div').removeClass('hidden');
									} else {
										$row.find('.descr-curator-div').addClass('hidden');
									}
								}
								if (v === 'TypeOfRoomsId') {
									if (d.id) {
										$row.find('.TypeRoomTitle').html(d.text);
										$row.find('.descr-TypeRoom-div').removeClass('hidden');
									} else {
										$row.find('.descr-TypeRoom-div').addClass('hidden');
									}
								}
								if (v === 'DirectoryFlightsId') {
									if (d.id) {
										$row.find('.divDirectoryFlightsSpan').html(d.text);
										$row.find('.divDirectoryFlightsDiv').removeClass('hidden');
									} else {
										$row.find('.divDirectoryFlightsDiv').addClass('hidden');
									}
								}
							}
						}

						if (descrItem.length === 1 && item.length === 1) {
							descrItem.html(item.val());


							if (item.val()) {
								descrItem.parent().removeClass('hidden');
							} else {
								descrItem.parent().addClass('hidden');
							}
						}
					});
					var $prices = $row.children('.prices');
					$prices.empty();
					var $rows = $dialog.find('table.prices-dialog>tbody').children('tr');
					var minPrice = 0;
					var maxPrice = 0;
					var minPriceStr = "";
					var maxPriceStr = "";
					$rows.each((i, e) => {
						var $r = $(e);
						var template = $($('#servicePriceRowTemplateRow').html());
						clasessPricess.forEach((v) => {
							var item = $r.find('.' + v);
							var target = template.find('input.' + v);
							if (target.length === 1 && item.length === 1) {
								if (v === "prices-Price" && item.val()) {
									var val = parseFloat(item.val());
									if (maxPrice < val || maxPriceStr === '') {
										maxPriceStr = item.val();
										maxPrice = val;
									}
									if (minPrice > val || minPriceStr === '') {
										minPriceStr = item.val();
										minPrice = val;
									}
								}
								if (item.attr('type') === 'text' || item.attr('type') === 'hidden') {
									target.val(item.val());
								} else if (item.attr('type') === 'checkbox') {
									target.prop('checked', item.val() === 'True');
								}
							}
						});

						$prices.append(template);
					});

					if (serviceDescription.find('.hdn-has-price').val() !== 'True') {
						if (minPriceStr === '') {
							serviceDescription.find('.descr-price-div').addClass('hidden');
						} else {
							serviceDescription.find('span.priceMin').html(addSpaces(minPriceStr));
							serviceDescription.find('span.priceMax').html(addSpaces(maxPriceStr));
							serviceDescription.find('.descr-price-div').removeClass('hidden');
						}
					}
					processTable($('#mainServiceTable'), '');
					$dialog.modal('hide');
				});
				$dialog.find('.nav-tabs a:first').tab('show');
				$dialog.modal('show');
			}
		});
	}

	$('#serviceDialog').find('.addprice').click(() => {
		var template = $($('#servicePriceRowTemplate').html());
		var $tbody = $('#serviceDialog').find('table.prices-dialog>tbody');
		prepareDialogPriceRow(template);

		$tbody.append(template);
	});

	function serviceAddPrice(e) {
		var $t = $(e.target);
		var $tmpl = $($('#servicePriceRowTemplate').html());

		$tmpl.find('.decimal').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
		$tmpl.find('.integer').inputmask('integer', { allowMinus: false, rightAlign: false });

		$tmpl.find('.remove-service-btn').click(removeService);

		var $tabl = $t.closest('tr').find('.prices').find('tbody');
		$tabl.append($tmpl);

		processTable($('#mainServiceTable'), '');
	}

	function addServiceClick(e) {
		var $t = $(e.target);
		var $select = $t.closest('.form-group').find('input.selectService');
		var $parent = $t.closest('.selectServiceLinked');
		var serviceId = $t.closest('.form-group').find('.selectService').select2('val');
		if (!serviceId || serviceId <= 0) {
			ShowAlert("Необходимо указать услугу для добавления", "alert-danger", "glyphicon-remove", true);
			return;
		}

		$.ajax({
			url: rootPath + 'CommercialTour/GetServiceDescription/' + serviceId.toString(),
			type: 'GET',
			dataType: 'html',
			success: (description) => {
				var $table = $parent.children('.serviceTable');
				var $tbody = $table.children('tbody');
				var $row = $($('#serviceRowTemplate').html());
				$row.find('.service-description').append(description);
				$row.find('.addon-service-id').val(serviceId);

				$.ajax({
					url: rootPath + 'Api/AddonService/GetSetService/' + serviceId.toString(),
					type: 'GET',
					dataType: 'json',
					success: (r) => {
						prepareRow($row);
						$row.find('.Description').val(r.description);
						$row.find('.AgeFrom').val(r.ageFrom);
						$row.find('.AgeTo').val(r.ageTo);
						$row.find('.SizeMin').val(r.sizeMin);
						$row.find('.SizeMax').val(r.sizeMax);
						$row.find('.IsGroup').val(r.isGroup ? "True" : "False");
						$row.find('.ByDefault').val(r.byDefault ? "True" : "False");
						$row.find('.Requared').val(r.requared ? "True" : "False");
						$row.find('.NeedApprove').val(r.needApprove ? "True" : "False");
						$row.find('.ByDefault').val(r.byDefault ? "True" : "False");
						$row.find('.OnlyWithRequest').val(r.onlyWithRequest ? "True" : "False");
						$row.find('.Name').val(r.name);

						$row.find('.Hidden').prop('checked', r.hidden).trigger('change');

						if (r.prices && r.prices.length > 0) {
							$row.find('.addprice').closest('h4').hide();
							$row.find('table.prices').hide();
						}

						$tbody.append($row);
						processTable($('#mainServiceTable'), '');
						$select.select2('data', null);
					}
				});
			}
		});

		return;
	};

	$('.addServiceDialogButton:first').click(addServiceClick);

	function prepareRow($row) {
		$row.find('.date>input').inputmask("d.m.y", { placeholder: "дд.мм.гггг", clearIncomplete: true });
		$row.find('.decimal').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
		$row.find('.integer').inputmask('integer', { allowMinus: false, rightAlign: false });
		$row.find('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
		$row.find('.remove-service-btn').click(removeService);
		$row.find('.edit-service-btn').click(editService);
		$row.find('.addprice').click(serviceAddPrice);
		initSelectService($row.find('.selectService'));
		$row.find('.addServiceDialogButton').click(addServiceClick);
		$row.find('.curator-id').select2({
			initSelection: (element, callback) => {
				callback({ id: element.val(), text: element.attr('accountname') });
			},
			minimumInputLength: 1,
			ajax: {
				url: rootPath + '/api/WebAccount/SearchAccount/',
				dataType: 'json',
				quietMillis: 250,
				data: (term, page) => {
					return {
						q: term,
					};
				},
				results: (data, page) => {
					var results = [];
					results.push({ id: 0, text: '-- Не выбрано --' });
					for (var i in data) {
						results.push({ id: data[i].Id, text: data[i].FullName });
					}
					return {
						results: results
					};
				},
				cache: true
			}
		});
	}

	prepareRow($('#mainServiceTable'));
	processTable($('#mainServiceTable'), '');

	function getTypeOfRooms() {
		var typeOfRooms = [];

		var ids = {};

		$('.fond-TypeOfRoomsId').each((i, e) => {
			var $element = $(e);
			var $row = $element.closest('tr');
			if (!ids['c' + $element.val()]) {
				typeOfRooms.push({
					id: $element.val(), text: $row.find('.TourVolumeName').html() });
				ids['c' + $element.val()] = true;
			}
		});

		return typeOfRooms;
	}

	function bindTypeOfRooms($row) {
		$row.find('.typeOfRooms-service-Id').select2({
			minimumInputLength: 0,
			initSelection: (element, callback) => {
				if ($(element).val() === '') {
					callback({ id: '', text: '-- Не выбрано --' });
				} else {
					var types = getTypeOfRooms();
					types.forEach((e) => {
						if (e.id === $(element).val()) {
							callback(e);
						}
					});
				}
			},
			query: (query) => {
				var data = getTypeOfRooms();
				query.callback({ results: getTypeOfRooms() });
			}
		});
	}

	bindTypeOfRooms($('#serviceTable'));
	bindTypeOfRooms($('#serviceDialog'));

	$('.cbxIsGroup').change((e) => {
		var tr = $('#serviceDialog');
		if ($(e.target).prop('checked')) {
			tr.find('.divNeedSize2').removeClass('hidden');
		} else {
			tr.find('.divNeedSize2').addClass('hidden');
			tr.find('.size').val('');
		}
	});
});
