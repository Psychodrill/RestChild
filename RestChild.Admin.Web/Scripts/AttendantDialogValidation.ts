function validateAttendant($form: JQuery): boolean {
	var foreignPassportId = '60005';

	var isValid = true;
	var required = ['.lastName', '.firstName', '.datebirth', 'select.document-type', '.document-number', '.document-date-issue', '.document-subject-issue', '.position'];

	for (var i in required) {
		var $element = $form.find(required[i]);
		if ($element.val() == null || $element.val() === '') {
			isValid = false;
			$element.closest('.form-group').addClass('has-error');
		} else {
			$element.closest('.form-group').removeClass('has-error');
		}
	}

	if (!$form.find('.middleName-present').is(':checked') && $form.find('.middleName').val() == '') {
		isValid = false;
		$form.find('.middleName').closest('.form-group').addClass('has-error');
	} else {
		$form.find('.middleName').closest('.form-group').removeClass('has-error');
	}

	if ($form.find('.male:checked').val() == null) {
		isValid = false;
		$form.find('.male').closest('.form-group').addClass('has-error');
	} else {
		$form.find('.male').closest('.form-group').removeClass('has-error');
	}

	if ($form.find('select.document-type').val() !== foreignPassportId && $form.find('.document-seria').val() === '') {
		isValid = false;
		$form.find('.document-seria').closest('.form-group').addClass('has-error');
	}

	if ($form.find('select.document-type').val() !== foreignPassportId && $form.find('.document-seria').val() === '') {
		isValid = false;
		$form.find('.document-seria').closest('.form-group').addClass('has-error');
	}

	if (!isValid) {
		$('.dialog-validation-alert').removeClass('hidden');
	} else {
		$('.dialog-validation-alert').addClass('hidden');
	}

	return isValid;
 }