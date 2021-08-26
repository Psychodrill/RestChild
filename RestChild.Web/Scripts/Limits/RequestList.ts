$(() => {
	$('#btnExcelExport').click(() => {
		var action = $('#mainForm').attr('action');
		$('#mainForm').attr('action', rootPath + 'Limits/RequestExcel');
		$('#mainForm').submit();
		$('#mainForm').attr('action', action);
	});

	$('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
	inputMaskDateAnytime($('.input-mask-date-anytime'));
	$('.price, .decimal').inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });

	$('.select2').select2();
	$('#orgId').select2({
		initSelection: (element, callback) => {
			var e = $(element);
			if (e.val() === '') {
				callback({ id: '', text: '-- Не выбрано --' });
			} else {
				callback({ id: e.val(), text: e.attr('orgName') });
			}
		},
		minimumInputLength: 0,
		ajax: {
			url: rootPath + 'Api/Limits/GetOrganizationForRequest',
			dataType: 'json',
			data: (term, page) => {
				return {
					oivId: $('#oivId').length >0 ? $('#oivId').select2('val') : null,
					yearOfRestId: $('#yearOfRestId').select2('val'),
					name: term
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
			cache: false
		}
	}).on('change', e => {
		$('#mainForm').submit();
	});

	$('#orgIdSelect.org-id').select2({
		initSelection: (element, callback) => {
			var e = $(element);
			if (e.val() === '') {
				callback({ id: '', text: '-- Не выбрано --' });
			} else {
				callback({ id: e.val(), text: e.attr('orgName') });
			}
		},
		minimumInputLength: 0,
		ajax: {
			url: rootPath + 'Api/Limits/GetOrganizationForRequest',
			dataType: 'json',
			data: (term, page) => {
				return {
					oivId: $('#oivIdSelect').length > 0 ? $('#oivIdSelect').val() : null,
					yearOfRestId: $('#yearOfRestId').select2('val'),
					name: term
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
			cache: false
		}
	});

	$('#placeOfRestId').select2({
		minimumInputLength: 0,
		ajax: {
			url: rootPath + 'Api/WebVocabulary/GetPlacesOfRest',
			dataType: 'json',
			data: (term, page) => {
				return {
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

	function checkRequest(request) {
		var errors = '';

		if (!request.limitOnVedomstvoId || request.limitOnVedomstvoId === "0") {
			errors = errors + '<li>Не указано ведомство</li>';
		}

		if (!request.organizationId || request.organizationId === "0") {
			errors = errors + '<li>Не указана организация</li>';
		}

		if (!request.placeOfRestId || request.placeOfRestId==="0") {
			errors = errors + '<li>Не указан регион отдыха</li>';
		}

		if (!request.groupedTimeOfRestId || request.groupedTimeOfRestId === "0") {
			errors = errors + '<li>Не указано желаемое время отдыха</li>';
		}

		if (!request.volume || request.volume === "0") {
			errors = errors + '<li>Не указано количество детей</li>';
		}

		if (errors !== '') {
			ShowAlert('<ul>' + errors + '</ul>', "alert-danger", "", true);
			return false;
		}

		return true;
	}

	$('.edit').click((e) => {
		var row = $(e.target).closest('tr');
		var id = row.find('.request-id').val();
		$.ajax({
			type: 'POST',
			url: rootPath + 'Api/Limits/GetOrganizationRequest?reqId=' + id,
			contentType: 'application/json; charset=utf-8',
			success: (result) => {
				$('#add-request-div').addClass('hidden');
				$('#edit-request-div').removeClass('hidden');
				if (result.organization) {
					$('#org-dialog-name').html(result.organization.name);
				} else {
					$('#org-dialog-name').html('-');
				}
				if (result.limitOnVedomstvo && result.limitOnVedomstvo.organization) {
					$('#oiv-dialog-name').html(result.limitOnVedomstvo.organization.name);
				} else {
					$('#oiv-dialog-name').html('-');
				}
				$('#nameInput').val(result.name);
				$('#comment').val(result.comment);
				$('#volume').val(result.volume);
				$('#volumeCounselor').val(result.volumeCounselor);
				$('#volumeAttendant').val(result.volumeAttendant);
				$('#timeOfRestInput').select2('val', result.groupedTimeOfRestId ? result.groupedTimeOfRestId : 0);
				$('#categoryInput').select2('val', result.listOfChildsCategoryId ? result.listOfChildsCategoryId : 0);
				$('#placeOfRestId').select2('data', result.placeOfRest ? { id: result.placeOfRest.id, text: result.placeOfRest.name } : { id: '', text: '-- Не выбрано --' });
				$('#btnModalUpdate').removeClass('hidden');
				$('#btnModalAdd').addClass('hidden');
				$('#btnModalUpdate').unbind();
				$('#btnModalUpdate').click(() => {
					result.name = $('#nameInput').val();
					result.volume = $('#volume').val();
					result.placeOfRestId = $('#placeOfRestId').select2('val');
					result.volumeCounselor = $('#volumeCounselor').val();
					result.volumeAttendant = $('#volumeAttendant').val();
					result.groupedTimeOfRestId = $('#timeOfRestInput').select2('val');
					result.listOfChildsCategoryId = $('#categoryInput').select2('val');

					result.comment = $('#comment').val();

					if (!checkRequest(result)) {
						return;
					}

					$.ajax({
						type: 'POST',
						url: rootPath + 'Api/Limits/UpdateOrganizationRequest',
						contentType: 'application/json; charset=utf-8',
						data: JSON.stringify(result),
						success: (result) => {
							if (result) {
								BootstrapDialog.show({
									title: 'Ошибка',
									message: result,
									buttons: [{ label: 'Закрыть', action: (dialogItself) => { dialogItself.close(); } }]
								});
							} else {
								$('#mainForm').submit();
							}
						},
						error: () => {
							BootstrapDialog.show({
								title: 'Ошибка',
								message: 'Произошла ошибка при добавлении заявления на квоту',
								buttons: [{ label: 'Закрыть', action: (dialogItself) => { dialogItself.close(); } }]
							});
						}
					});
				});
				$('#appendDialog').modal('show');
			}
		});
	});

	$('#appendDialogButton').click(() => {
		$('#add-request-div').removeClass('hidden');
		$('#edit-request-div').addClass('hidden');
		$('#nameInput').val('');
		$('#comment').val('');
		$('#volume').val('');
		$('#volumeCounselor').val('');
		$('#volumeAttendant').val('');
		$('#timeOfRestInput').select2('val', 0);
		$('#categoryInput').select2('val', 0);
		var orgs = $('#orgIdSelect.org-id');
		if (orgs.length > 0) {
			orgs.select2('data', { id: '', text: '-- Не выбрано --' });
		}

		var oiv = $('#oivIdSelect.select2');
		if (oiv.length > 0) {
			oiv.select2('data', { id: '', text: '-- Не выбрано --' });
		}

		$('#placeOfRestId').select2('data', { id: '', text: '-- Не выбрано --' });
		$('#btnModalUpdate').addClass('hidden');
		$('#btnModalAdd').removeClass('hidden');

		$('#appendDialog').modal('show');
	});

	$('#yearOfRestId').change(() => {
		$('#mainForm').submit();
	});
	$('#btnModalAdd').click(() => {

		var result = {
			stateId: $('#yearOfRestId').val(),
			limitOnVedomstvoId: $('#oivIdSelect').val(),
			organizationId: $('#orgIdSelect').val(),
			name: $('#nameInput').val(),
			volume: $('#volume').val(),
			volumeCounselor: $('#volumeCounselor').val(),
			volumeAttendant: $('#volumeAttendant').val(),
			placeOfRestId: $('#placeOfRestId').select2('val'),
			groupedTimeOfRestId: $('#timeOfRestInput').select2('val'),
			listOfChildsCategoryId: $('#categoryInput').select2('val'),
			comment: $('#comment').val()
		};

		if (!checkRequest(result)) {
			return;
		}

		$.ajax({
			type: 'POST',
			url: rootPath + 'Api/Limits/AddOrganizationRequest',
			contentType: 'application/json; charset=utf-8',
			data: JSON.stringify(result),
			success: (result) => {
				if (result) {
					BootstrapDialog.show({
						title: 'Ошибка',
						message: result,
						buttons: [{ label: 'Закрыть', action: (dialogItself) => { dialogItself.close(); } }]
					});
				} else {
					$('#mainForm').submit();
				}
			},
			error: () => {
				BootstrapDialog.show({
					title: 'Ошибка',
					message: 'Произошла ошибка при добавлении заявления на квоту',
					buttons: [{ label: 'Закрыть', action: (dialogItself) => { dialogItself.close(); } } ]
				});
			}
		});
	});

	$('.actions').click((e) => {
		var $button = $(e.target).closest('a');
		var row = $(e.target).closest('tr');
		var action = $button.attr('code');
		var needComment = $button.attr('needComment');
		var buttonName = $button.attr('title');
		var descr = $button.attr('description');
		var $content;
		BootstrapDialog.show({
			title: buttonName ? buttonName : 'Подтверждение',
			message: (dialog) => {
				var fn = doT.template($("#stateDialogBody").html());
				$content = $(fn({ name: ((descr) ? descr : ('Вы действительно хотите ' + buttonName.toLowerCase() + '?')), needComment: needComment ==='True' }));
				return $content;
			},
			buttons: [
				{
					label: buttonName,
					action: dialogItself => {
						var commentary = $content.find('.stateDialogComment').val();
						$.ajax({
							type: 'POST',
							url: rootPath + 'Api/Limits/RequestAction?reqId=' + row.find('.request-id').val() + '&actionCode=' + action + '&commentary=' + encodeURIComponent(commentary ? commentary : '-'),
							data: null,
							success: result => {
								dialogItself.close();
								if (result.isError) {
									var text = "<ul>";
									for (var i = 0; i < result.errors.length; i++) {
										text = text + '<li>' + result.errors[i] + '</li>';
									}

									text = text + '</ul>';
									BootstrapDialog.show({
										title: 'Ошибка',
										message: 'Произошла ошибка при изменении статуса заявки по учреждению ' + text,
										buttons: [
											{ label: 'Закрыть', action: dialogItself => { dialogItself.close(); } }
										]
									});
								} else {
									$('#mainForm').submit();
								}

							}
						});
					}
				},
				{
					label: 'Закрыть',
					action: (dialogItself) => { dialogItself.close(); }
				}
			]
		});
	});

	$('.remove').click((e) => {
		var $button = $(e.target).closest('a');
		var row = $(e.target).closest('tr');

		BootstrapDialog.show({
			title: 'Удалить',
			message: 'Удалить заявку?',
			buttons: [
				{
					label: 'Удалить',
					action: dialogItself => {
						$.ajax({
							type: 'POST',
							url: rootPath + 'Api/Limits/RemoveOrganizationRequest?reqId=' + row.find('.request-id').val(),
							data: null,
							success: result => {
								dialogItself.close();
								if (result.isError) {
									var text = "<ul>";
									for (var i = 0; i < result.errors.length; i++) {
										text = text + '<li>' + result.errors[i] + '</li>';
									}

									text = text + '</ul>';
									BootstrapDialog.show({
										title: 'Ошибка',
										message: 'Произошла ошибка при удалении заявки по учреждению ' + text,
										buttons: [
											{ label: 'Закрыть', action: dialogItself => { dialogItself.close(); } }
										]
									});
								} else {
									$('#mainForm').submit();
								}

							}
						});
					}
				},
				{
					label: 'Закрыть',
					action: (dialogItself) => { dialogItself.close(); }
				}
			]
		});
	});
});
