$(() => {
	$('select').select2();
	$('.date input, input.date').inputmask("d.m.y", {
		placeholder: "дд.мм.гггг",
		clearIncomplete: true
	});
	$('.date').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });

	function documentInputType($target) {
		var self = $target;
		var $item = self.find("option:selected");
		var text = $item.text();
		var $fieldset = $('#sendDialog');
        if (text === 'Паспорт гражданина РФ' || text === 'Паспорт РФ') {
			$fieldset.find('.input-mask-passport-series').inputmask('9999', { placeholder: "сссс", clearIncomplete: true });
			$fieldset.find('.input-mask-passport-number').inputmask('999999', { placeholder: "нннннн", clearIncomplete: true });
		} else if (text === 'Свидетельство о рождении') {
			$fieldset.find('.input-mask-passport-series').inputmask('Regex', { regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]', clearIncomplete: true });
			$fieldset.find('.input-mask-passport-number').inputmask('999999', { clearIncomplete: true });
		} else {
			$fieldset.find('.input-mask-passport-series').inputmask('Regex', { regex: '.*' });
			$fieldset.find('.input-mask-passport-number').inputmask('Regex', { regex: '.*' });
		}
	}

	$('#sendButton').click(() => {
		var errorMsg = '';

		if (!$('#dialogLastName').val()) {
			errorMsg = errorMsg + '<li>Не заполнена фамилия</li>';
		}
		if (!$('#dialogDateBirth').val()) {
			errorMsg = errorMsg + '<li>Не заполнена дата рождения</li>';
		}
		if (!$('#dialogNumber').val()) {
			errorMsg = errorMsg + '<li>Не заполнен номер документа</li>';
		}

		if (errorMsg) {
			ShowAlert('<ul>' + errorMsg + '</ul>', 'alert-danger', "", true);
			return;
		}

		$('#actionString').val('9B109AD2-B3CE-4776-85C4-D7A8F76AA8E3');
		$('.mainForm').submit();
	});

	$('#sendDialog').find('select').change((e) => {
		documentInputType($(e.target));
	});

	$('#btnCreateRequest').click(() => {
		$('#sendDialog').find('input').val('');
		$('#sendDialog').find('select').select2('val', 22);
		documentInputType($('#sendDialog').find('select'));
		$('#sendDialog').modal('show');
	});
});
