declare var addressControlClear;
declare var addressControlSetValue;
declare var addressControlGetValue;
declare var addressControlValid;

// Очистка формы адреса, приведение в "исходное" состояние.
addressControlClear = () => {

};

// Метод динамической установки значения адреса
addressControlSetValue = (val, needVladenie = true) => {
    if (!val) {
        val = { id: 0 };
    }

    var noBtiFilled = val.street || val.house || val.corpus || val.stroenie || val.vladenie;
    if (val.fiasId) {
        noBtiFilled = false;
    }

    $('.StreetNotFoundCheckbox').prop('checked', noBtiFilled && !val.btiAddressId).trigger('change');
    $('.StreetNotFoundCheckbox').change((e) => {
        if ($('.StreetNotFoundCheckbox').prop('checked')) {
            $('.bti-address').select2('data', { id: null, text: '' });
            $('input.street-autocomplete').select2('data', { id: null, text: 'Выберите дом' });
        }
    });
    $('#DialogChildAddressId').val(val.id);

    if (val.btiAddressId) {
        $('input.appartment').val("");
        $('input.appartment-simple').val(val.appartment);

        $('.bti-address').val(val.btiAddressId);
        $('.bti-address').attr('data-selectedId', val.btiAddressId);

        $('input.street-autocomplete').attr('data-default-id', val.btiAddressBtiStreetId);
        $('input.street-autocomplete').attr('data-default-text', val.btiAddressBtiStreetName);
        $('input.street-autocomplete').select2('data', { id: parseInt(val.btiAddressBtiStreetId), text: val.btiAddressBtiStreetName });
        $('input.street-autocomplete').trigger('change');

        $('input.street').val('');
        $('input.house').val('');
        $('input.corpus').val('');
        $('input.stroenie').val('');
        $('input.vladenie').val('');
    } else if (val.fiasId) {
        $('input.street-autocompleteAR').select2('data', { id: val.fiasId, text: val.street });
        $('input.appartment-simple').val(val.appartment);
        $('.Data-District').html(val.district);
        $('.Data-Region').html(val.region);
    } else {
        $('input.appartment').val(val.appartment);
        $('input.appartment-simple').val("");

        $('.bti-address').select2('data', null);
        $('.bti-address').trigger('change');

        $('input.street-autocomplete').select2('data', null);
        $('input.street-autocomplete').trigger('change');

        $('input.street-autocompleteAR').select2('data', { id: null, text: "" });

        $('input.street').val(val.street);
        $('input.house').val(val.house);
        $('input.corpus').val(val.corpus);
        $('input.stroenie').val(val.stroenie);
        $('input.vladenie').val(val.vladenie);
    }

    if (!needVladenie) {
        $('input.vladenie').addClass('hidden');
    }

    // Магия! Работает, скопировано с C:\Projects\RestChild\RestChild.Web\Views\Limits\ListOfChildsEdit.cshtml 608...
    if (val.btiRegionId) {
        $('.bti-region-id').attr('data-selectedId', val.btiRegionId);
        $('.bti-region-id').trigger('change');
        $('select.bti-district-id').select2('val', val.btiDistrictId);
    } else {
        $('select.bti-region-id').select2('val', '');
        $('select.bti-district-id').select2('val', '0');
    }

    $('select.bti-district-id').trigger('change');
}

// Метод получения значения выбранного адреса
addressControlGetValue = function () {
    var apt;
    var btiStrId = null;
    var btiStrName = '';
    var fiasId = null;
    var street = $('input.street').val();
    var region = null;
    var district = null;


    if ($(".StreetNotFoundCheckbox").prop('checked')) { // $('.StreetNotFoundCheckbox-h').val() === 'True' ???
        apt = $("input.appartment").val();
    }
    else {
        apt = $("input.appartment-simple").val();
        var exists = $(".street-autocompleteAR").length;
        if (exists) {
            let data = $("input.street-autocompleteAR").select2("data");
            if (data) {
                fiasId = data.id;
                street = data.text;
                region = $(".Data-Region").html();
                district = $(".Data-District").html();
            }
        } else {
            let data = $("input.street-autocomplete").select2("data");
            if (data) {
                btiStrId = data.id;
                btiStrName = data.text;
            }
        }
    }
    return {
        id: $('#DialogChildAddressId').val(),
        btiAddressId: $('select.bti-address').val(),
        btiAddressBtiStreetId: btiStrId,
        btiAddressBtiStreetName: btiStrName,
        street: street,
        house: $('input.house').val(),
        corpus: $('input.corpus').val(),
        stroenie: $('input.stroenie').val(),
        vladenie: $('input.vladenie').val(),
        appartment: apt,
        btiRegionId: $('select.bti-region-id').val(),
        btiDistrictId: $('select.bti-district-id').val(),
        fiasId: fiasId,
        region: region,
        district: district
    };
};
// Валидация на заполнение значения контрола.
addressControlValid = function () {
    var val = addressControlGetValue();
    return val.appartment && (val.fiasId || val.btiAddressId || (val.btiRegionId && val.street));
};
//# sourceMappingURL=AddressExt.js.map

