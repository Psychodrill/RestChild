/// <reference path="InputMaskConfig.ts" />
$(function () {
    $('select').not('.not-select2').select2();
    $('.datepicker').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    $('#CheckAllCheckbox').click(function () {
        if ($('#CheckAllCheckbox').is(":checked"))
            $('.benefit-type-checkbox').prop("checked", true);
        else
            $('.benefit-type-checkbox').prop("checked", false);
    });
    $('.benefit-type-checkbox').change(function () {
        if ($('.benefit-type-checkbox:not(:checked)').length == 0)
            $('#CheckAllCheckbox').prop("checked", true);
        else
            $('#CheckAllCheckbox').prop("checked", false);
    });
    $('#AllChildrenApproved').click(function () {
        $('.benefit-select').val(benefitTypeApproved);
    });
    $('#AllChildrenRejected').click(function () {
        $('.benefit-select').val(benefitTypeRejected);
    });
    inputMaskConfig($);
    var historyAjax;
    $('.history-button').click(function (event) {
        $('#HistoryModalError').addClass('hidden');
        $('#HistoryModalTable').addClass('hidden');
        $('#HistoryModalLoading').removeClass('hidden');
        $('#HistoryModal').modal();
        if (historyAjax) {
            historyAjax.abort();
        }
        historyAjax = $.ajax({
            type: 'GET',
            url: '/api/WebFirstRequestCompany/LoadInteragencyRequestHistory/' + $('#RequestId').val(),
            success: function (result) {
                $('#HistoryModalLoading').addClass('hidden');
                $('#HistoryModalTable').removeClass('hidden');
                var template = doT.template($('#historyTableTemplate').html());
                $('#HistoryModalTable').find('tbody').html(template(result));
            },
            error: function () {
                $('#HistoryModalLoading').addClass('hidden');
                $('#HistoryModalError').addClass('hidden');
            }
        });
    });
    $('#Data_ForAllRegion').change(function (e) {
        if ($(e.target).is(':checked')) {
            $('#Data_BtiRegionId').attr('disabled', 'disabled');
        }
        else {
            $('#Data_BtiRegionId').removeAttr('disabled');
        }
    });
    var benefitsAjax;
    $('input#Data_BtiRegionId').change(function () {
        var regionId = $('#Data_BtiRegionId:checked').val();
        var allRegions;
        if ($('.radio-all-regions').is(':checked')) {
            $('#Data_ForAllRegion').val('True');
            allRegions = true;
        }
        else {
            $('#Data_ForAllRegion').val('False');
            allRegions = false;
        }
        $('#TableBenefitsTypes tbody').html('<tr><td><img src="/Content/images/spinner.gif" /> Загрузка</td></tr>');
        benefitsAjax = $.ajax({
            type: 'GET',
            url: '/api/WebInteragencyRequest/GetBenefitTypesForRegion',
            data: {
                regionId: regionId,
                allRegions: allRegions
            },
            success: function (result) {
                var template = doT.template($('#BeneftitTypesTemplate').html());
                $('#TableBenefitsTypes').find('tbody').html(template(result));
                $('#CheckAllCheckbox').prop('checked', 'checked');
            },
            error: function () {
                $('#TableBenefitsTypes tbody').html('<tr><td>Ошибка загрузки</td></tr>');
            }
        });
    });
});
function openIntergencyRequest(action) {
    window.open(action, '_blank');
}
//# sourceMappingURL=InteragencyRequest.js.map