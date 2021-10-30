$(function () {
    var getSnilsAction = '0DC806DA-6C5C-4B56-B410-3F2E1765AAFB';
    var getRelativesAction = '4AFEE4AC-9251-45F7-ADC1-17F0AC41345A';
    var getPassportBySnilsInBr = 'C1C55BEA-A6D9-4AE4-A5FD-03190E4A2B86';
    //  Проверка ЦПМПК
    var getCPMPK = 'EDC78633-AC2A-41E6-BD7E-5E6FFF695346';
    //  Проверка адреса регистрации
    var getRegistrationAddress = '38D6E2D8-CE98-4916-A267-D4469FDE6295';
    var getExtractFromFGISFRI = '3e8fe0c1-1501-477a-b492-5fa1037d1d97';
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
            $fieldset.find('.input-mask-passport-series').inputmask('9999', {
                placeholder: "сссс",
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-passport-number').inputmask('999999', {
                placeholder: "нннннн",
                clearIncomplete: true
            });
        }
        else if (text === 'Свидетельство о рождении') {
            $fieldset.find('.input-mask-passport-series').inputmask('Regex', {
                regex: '[a-zA-Z]{1,7}-[а-яА-Я][а-яА-Я]',
                clearIncomplete: true
            });
            $fieldset.find('.input-mask-passport-number').inputmask('999999', { clearIncomplete: true });
        }
        else {
            $fieldset.find('.input-mask-passport-series').inputmask('Regex', { regex: '.*' });
            $fieldset.find('.input-mask-passport-number').inputmask('Regex', { regex: '.*' });
        }
    }
    var $sendButton = $('#sendButton');
    var $snilsBlock = $('#snilsBlock');
    var $parent = $('#parent');
    var $otherBlock = $('#otherBlock');
    var $addonSnils = $('#addonSnils');
    var $CPMPK = $('#CPMPK');
    var $registrationAddress = $('#registrationAddress');
    $sendButton.click(function () {
        var errorMsg = '';
        var uid = $sendButton.attr('uid');
        if (!$('#dialogLastName').val()) {
            errorMsg = errorMsg + '<li>Не заполнена фамилия</li>';
        }
        if (!$('#dialogDateBirth').val()) {
            errorMsg = errorMsg + '<li>Не заполнена дата рождения</li>';
        }
        if (uid === getSnilsAction) {
            // это запрос СНИЛС
            if (!$('#dialogNumber').val()) {
                errorMsg = errorMsg + '<li>Не заполнен номер документа</li>';
            }
        }
        else if (uid === getRelativesAction) {
            // это запрос родства москвы
            if (!$('#dialogLastNameParent').val()) {
                errorMsg = errorMsg + '<li>Не заполнена фамилия родителя</li>';
            }
            if (!$('#dialogDateBirthParent').val()) {
                errorMsg = errorMsg + '<li>Не заполнена дата рождения родителя</li>';
            }
        }
        else if (uid == getPassportBySnilsInBr) {
            if (!$('#snils').val() && getSnilsAction !== uid) {
                errorMsg = errorMsg + '<li>Не заполнен СНИЛС</li>';
            }
        }
        else {
            if (!$('#dialogNumber').val() && uid !== getCPMPK) {
                errorMsg = errorMsg + '<li>Не заполнен номер документа</li>';
            }
            if (!$('#snils').val() && getSnilsAction !== uid && uid !== getCPMPK) {
                errorMsg = errorMsg + '<li>Не заполнен СНИЛС</li>';
            }
        }
        if (errorMsg) {
            ShowAlert('<ul>' + errorMsg + '</ul>', 'alert-danger', "", true);
            return;
        }
        $('#actionString').val(uid);
        $('.mainForm').submit();
    });
    var $sendDialog = $('#sendDialog');
    $sendDialog.find('select').change(function (e) {
        documentInputType($(e.target));
    });
    $('.create-request').click(function (e) {
        var $t = $(e.target).closest('button');
        $sendDialog.find('input').val('');
        $sendDialog.find('select').select2('val', 22);
        documentInputType($sendDialog.find('select'));
        var uid = $t.attr('uid');
        $parent.addClass('hidden');
        $registrationAddress.addClass('hidden');
        $otherBlock.removeClass('hidden');
        if (uid === getSnilsAction) {
            // это запрос СНИЛС
            $snilsBlock.addClass('hidden');
            $addonSnils.removeClass('hidden');
        }
        else if (uid === getRelativesAction) {
            // это запрос родства москвы
            $parent.removeClass('hidden');
            $snilsBlock.addClass('hidden');
            $otherBlock.addClass('hidden');
            $addonSnils.addClass('hidden');
        }
        else if (uid == getPassportBySnilsInBr) {
            $otherBlock.addClass('hidden');
            $addonSnils.addClass('hidden');
        }
        else if (uid == getCPMPK) {
            // ЦПМПК
            $otherBlock.addClass('hidden');
            $snilsBlock.addClass('hidden');
            $addonSnils.addClass('hidden');
            $otherBlock.addClass('hidden');
        }
        else if (uid == getRegistrationAddress) {
            // Получить адрес регистрации
            $snilsBlock.addClass('hidden');
            $addonSnils.addClass('hidden');
            $registrationAddress.removeClass('hidden');
        }
        else {
            $snilsBlock.removeClass('hidden');
            $addonSnils.addClass('hidden');
        }
        $sendButton.attr('uid', uid);
        $sendDialog.modal('show');
    });
});
//# sourceMappingURL=List.js.map