﻿$(() => {
	$('select').select2();
	$('.time').datetimepicker({
		format: 'HH:mm',
		extraFormats: ['DD.MM.YYYY HH:mm']
	});

	$('.contract-select').select2({
		initSelection: (element, callback) => {
			if ($('#_ContractId').val() === '') {
				callback({ id: '', text: '-- Не выбрано --' });
			} else {
				callback({ id: $('#_ContractId').val(), text: $('#_ContractName').val() });
			}
		},
		minimumInputLength: 0,
		ajax: {
			url: rootPath + '/api/WebContract/GetByOrganization',
			dataType: 'json',
			data: (term, page) => {
				return {
					signNumber: term,
					yearOfRestId: $('#yearOfRestId').val(),
					onTransport: true,
					organizationId: null,
					onRest:null
				};
			},
			results: (data, page) => {
				var results = [{ id: '', text: '-- Не выбрано --' }];
				results = results.concat($.map(data, (item) => {
					return {
						text: item.signNumber + (item.supplier ? (' (' + item.supplier.shortName + ')') : ''),
						id: item.id
					}
				}));
				return {
					results: results
				};
			},
			cache: true
		}
	}).select2('val', []);

});
