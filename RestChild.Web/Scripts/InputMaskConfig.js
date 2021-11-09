/// <reference path="jquery.inputmask/jquery.inputmask.d.ts"/>
/// <reference path="typings/moment/moment.d.ts"/>
function inputMaskDateAnytime(range) {
    range.inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    });
}
function inputMaskDateTimeAnytime(range) {
    range.inputmask("d.m.y h:s", {
        placeholder: "дд.мм.гггг чч:мм",
        clearIncomplete: true
    });
}
function inputMaskConfig(scope) {
    $(scope.find('.input-mask-passport-series-active')).inputmask('9999', { placeholder: "сссс", clearIncomplete: true });
    $(scope.find('.input-mask-passport-number-active')).inputmask('999999', { placeholder: "нннннн", clearIncomplete: true });
    $(scope.find('.input-mask-phone')).inputmask('(999) 999-99-99', { clearIncomplete: true });
    $(scope.find('.input-mask-phone-new')).inputmask('(999)999-99-99', { clearIncomplete: true });
    $(scope.find('.input-mask-email')).inputmask({
        mask: "*{1,200}[.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}][.*{1,200}]@*{1,200}[.*{2,6}]",
        greedy: false,
        placeholder: " ",
        clearIncomplete: true,
        onBeforePaste: function (pastedValue, opts) {
            return pastedValue.replace("mailto:", "");
        },
        definitions: {
            '*': {
                validator: "[0-9A-Za-zА-Я-Ёа-я-ё!#$%&'*+/=?^_`{|}~\-]",
                cardinality: 1
                //,casing: "lower"
            }
        }
    });
    $(scope.find('.input-mask-date')).inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    }).focusout(function (e) {
        var now = moment().startOf('day');
        var val = moment($(e.target).val(), 'DD.MM.YYYY');
        if (now.diff(val, 'days') < 0) {
            $(e.target).val('');
        }
    });
    $(scope.find('.input-mask-date-future')).inputmask("d.m.y", {
        placeholder: "дд.мм.гггг",
        clearIncomplete: true
    }).focusout(function (e) {
        var now = moment().startOf('day');
        var val = moment($(e.target).val(), 'DD.MM.YYYY');
        if (now.diff(val, 'days') >= 0) {
            $(e.target).val('');
        }
    });
    $(scope.find('.input-mask-age')).inputmask('integer', { min: 0, max: 100, rightAlign: false });
    inputMaskDateAnytime($(scope.find('.input-mask-date-anytime')));
    $(scope.find('.age, .integer')).inputmask("integer", { allowMinus: false, rightAlign: false });
    $(scope.find('.procent')).inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',', min: 0, max: 100 }).focusout(function (e) {
        var val = parseFloat($(e.target).val());
        if (val > 100) {
            $(e.target).val('');
        }
    });
    $(scope.find('.price, .decimal')).inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 2, radixPoint: ',' });
    $(scope.find('.coordinates')).inputmask('decimal', { allowMinus: false, rightAlign: false, digits: 8, radixPoint: ',' });
    $(scope.find('.year-inputmask')).inputmask('9999', { placeholder: "гггг", clearIncomplete: true });
    $(scope.find('.snils')).inputmask('999-999-999 99', { clearIncomplete: true });
    $(scope.find('.document-code')).inputmask('999-999', { clearIncomplete: true });
}
$(function () {
    setTimeout(function () {
        if (!document['disableInputMaskConfig']) {
            inputMaskConfig($);
        }
    }, 10);
});
//# sourceMappingURL=InputMaskConfig.js.map