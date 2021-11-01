function validateChild($form) {
    var foreignPassportId = '50005';
    var foreignBirthCertId = '23';
    var isValid = true;
    for (var i in listOfChildsDialogEditRequired) {
        var $element = $form.find(listOfChildsDialogEditRequired[i]);
        if ($element.val() == null || $element.val() == '') {
            isValid = false;
            $element.parent().addClass('has-error');
        }
        else {
            $element.parent().removeClass('has-error');
        }
    }
    if (!$form.find('.middleName-present').is(':checked') && $form.find('.middleName').val() == '') {
        isValid = false;
        $form.find('.middleName').parent().addClass('has-error');
    }
    else {
        $form.find('.middleName').parent().removeClass('has-error');
    }
    if ($form.find('.male:checked').val() == null) {
        isValid = false;
        $form.find('.male').parent().addClass('has-error');
    }
    else {
        $form.find('.male').parent().removeClass('has-error');
    }
    if ($form.find('select.document-type').val() !== foreignPassportId && $form.find('select.document-type').val() !== foreignBirthCertId && $form.find('.document-seria').val() === '') {
        isValid = false;
        $form.find('.document-seria').parent().addClass('has-error');
    }
    else {
        $form.find('.document-seria').parent().removeClass('has-error');
    }
    if (!$form.find('.attendant-middleName-present').is(':checked') && $form.find('.attendant-middleName').val() == '') {
        isValid = false;
        $form.find('.attendant-middleName').parent().addClass('has-error');
    }
    else {
        $form.find('.attendant-middleName').parent().removeClass('has-error');
    }
    if (moment($form.find('.datebirth').val(), "DD.MM.YYYY") > moment($form.find('.document-date-issue').val(), "DD.MM.YYYY")) {
        isValid = false;
        $form.find('.datebirth').parent().addClass('has-error');
        $form.find('.document-date-issue').parent().addClass('has-error');
        $form.find('.dialog-validation-alert-doc-date').removeClass('hidden');
    }
    else {
        $form.find('.datebirth').parent().removeClass('has-error');
        $form.find('.document-date-issue').parent().removeClass('has-error');
        $form.find('.dialog-validation-alert-doc-date').addClass('hidden');
    }
    if ($form.find('#ManualType').is(':checked')) {
        if ($form.find('select.bti-district-id').val() === '0') {
            isValid = false;
            $form.find('.bti-district-id').parent().addClass('has-error');
        }
        else {
            $form.find('.bti-district-id').parent().removeClass('has-error');
        }
        if ($form.find('select.bti-region-id').val() === '') {
            isValid = false;
            $form.find('.bti-region-id').parent().addClass('has-error');
        }
        else {
            $form.find('.bti-region-id').parent().removeClass('has-error');
        }
        if ($form.find('input.street').val() === '') {
            isValid = false;
            $form.find('.street').parent().addClass('has-error');
        }
        else {
            $form.find('.street').parent().removeClass('has-error');
        }
        if ($form.find('input.house').val() === '') {
            isValid = false;
            $form.find('.house').parent().addClass('has-error');
        }
        else {
            $form.find('.house').parent().removeClass('has-error');
        }
        if ($form.find('input.appartment').val() === '') {
            isValid = false;
            $form.find('.appartment').parent().addClass('has-error');
        }
        else {
            $form.find('.appartment').parent().removeClass('has-error');
        }
    }
    else {
        if ($form.find('input.street-autocomplete').val() === '') {
            isValid = false;
            $form.find('.street-autocomplete').parent().addClass('has-error');
        }
        else {
            $form.find('.street-autocomplete').parent().removeClass('has-error');
        }
        if ($form.find('input.appartment-simple').val() === '') {
            isValid = false;
            $form.find('.appartment-simple').parent().addClass('has-error');
        }
        else {
            $form.find('.appartment-simple').parent().removeClass('has-error');
        }
        if ($form.find('select.bti-address').val() === '') {
            isValid = false;
            $form.find('.bti-address').parent().addClass('has-error');
        }
        else {
            $form.find('.bti-address').parent().removeClass('has-error');
        }
    }
    var birthdate = moment($form.find('.datebirth').val(), 'DD.MM.YYYY');
    var checkDate = moment($('#DateIncome').val(), 'DD.MM.YYYY');
    var age = checkDate.diff(birthdate, 'years');
    if (!familyListOfChildsEdit) {
        if (age < 7 || age > 17) {
            isValid = false;
            $form.find('.datebirth').parent().addClass('has-error');
            $form.find('.dialog-validation-alert-child-age').removeClass('hidden');
        }
        else {
            $form.find('.datebirth').parent().removeClass('has-error');
            $form.find('.dialog-validation-alert-child-age').addClass('hidden');
        }
    }
    else {
        if ((age >= 2 && age <= 23)) {
            $form.find('.datebirth').parent().removeClass('has-error');
            $form.find('.dialog-validation-alert-child-age').addClass('hidden');
        }
        else {
            isValid = false;
            $form.find('.datebirth').parent().addClass('has-error');
            $form.find('.dialog-validation-alert-child-age').removeClass('hidden');
        }
    }
    if (!isValid) {
        $('.dialog-validation-alert').removeClass('hidden');
    }
    else {
        $('.dialog-validation-alert').addClass('hidden');
    }
    return isValid;
}
//# sourceMappingURL=ChildDialogValidation.js.map