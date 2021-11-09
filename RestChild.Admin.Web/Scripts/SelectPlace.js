/// <reference path="typings/select2/select2.d.ts" />
/// <reference path="InputMaskConfig.ts" />
function configPlaceDropdowns() {
    $('.select2').not('.select2-container, .inited').addClass('inited').select2();
}
$(function () {
    configPlaceDropdowns();
    var timeOfRestFn = doT.template($('#timeOfRestTemplate').html());
    function formatTypeRest(state) {
        if (state.disabled) {
            return '<div style=\'padding-left:' + (state.level - 1) * 15 + 'px\'><b>' + state.text + '</b></div>';
        }
        else {
            return '<div style=\'padding-left:' + (state.level - 1) * 15 + 'px\'>' + state.text + '</div>';
        }
    }
    $('.type-of-rest').select2({
        query: function (q) {
            var res = [];
            typeOfRest.forEach(function (i) {
                res.push({ id: i.Id, text: i.Name, disabled: i.IsActive, level: i.MaxAge });
            });
            var d = {
                results: res
            };
            q.callback(d);
        },
        initSelection: function (element, callback) {
            var res = { id: 0, text: '-- Не выбрано --' };
            typeOfRest.forEach(function (i) {
                if ($(element).val() === i.Id.toString()) {
                    res = { id: i.Id, text: i.Name };
                }
            });
            callback(res);
        },
        formatResult: formatTypeRest
    }).change(function () {
        var val = $('input.type-of-rest').select2('val');
        $('.TypeOfRestId').val(val);
    });
    function changeYearOfType() {
        var val = $('input.type-of-rest').val();
        var yr = $('select.year-of-rest').select2('val');
        var e = event;
        $.ajax('../../Api/WebVocabulary/GetTimesOfRest?typeOfRestId=' + val + '&yearOfRestId=' + yr, {
            success: function (data) {
                var res = {
                    selected: $('select.time-of-rest').val(),
                    data: [{ id: 0, name: '-- Не выбрано --' }].concat(data)
                };
                $('select.time-of-rest').html(timeOfRestFn(res));
                if (e.val) {
                    var selectedTime = $(e.target.selectedOptions[0]).attr('data-selected-time');
                    if (val.Id && val.Id != 0 && selectedTime) {
                        $('select.time-of-rest').select2('val', selectedTime);
                    }
                    else {
                        $('select.time-of-rest').select2('val', '0');
                    }
                }
                else {
                    var timeOfRest = $('select.time-of-rest').val();
                    $('select.type-of-rest').find('option:selected').attr('data-selected-time', timeOfRest);
                }
            }
        });
    }
    $('select.year-of-rest').change(changeYearOfType);
    $('select.type-of-rest').change(changeYearOfType);
});
//# sourceMappingURL=SelectPlace.js.map