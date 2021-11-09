function addressControl(rootPath) {
    function loadHouses(val, prefix, selectedId) {
        var adrressSelector = '[name="' + prefix + 'BtiAddressId"]';
        $(adrressSelector).empty();
        if (val) {
            $(adrressSelector).append($('<option value="" data-district="" data-region="">Загрузка</option>'));
            $(adrressSelector).parent().find('.loading').removeClass('invisible');
            $.ajax({
                dataType: 'json',
                url: rootPath + '/api/WebBtiStreets/' + val + '/houses',
                data: null,
                success: function (result) {
                    $(adrressSelector).empty();
                    $(adrressSelector).append($('<option value="" data-district="" data-region="">Выберите дом</option>'));
                    for (var i in result) {
                        $(adrressSelector).append($('<option value="' + result[i].AddressId + '" data-district="' + (result[i].District ? result[i].District.replace(/'/g, "&#39;").replace(/\"/g, "&#34;") : result[i].District) + '" data-region="' + (result[i].Region ? result[i].Region.replace(/'/g, "&#39;").replace(/\"/g, "&#34;") : result[i].Region) + '" ' + (selectedId == result[i].AddressId ? 'selected' : '') + '>' + (result[i].ShortAddress ? result[i].ShortAddress.replace(/'/g, "&#39;").replace(/\"/g, "&#34;") : result[i].ShortAddress) + '</option>'));
                    }
                    $(adrressSelector).parent().find('.loading').addClass('invisible');
                    $(adrressSelector).change();
                    $(adrressSelector).attr('data-selectedId', null);
                },
                error: function () {
                    $(adrressSelector).empty();
                    $(adrressSelector).append($('<option value="" data-district="" data-region="">Ошибка загрузки данных</option>'));
                    $(adrressSelector).parent().find('.loading').addClass('invisible');
                }
            });
        }
    }
    function loadRegions(val, prefix, selectedId) {
        var regionSelector = '[name="' + prefix + 'BtiRegionId"]';
        $(regionSelector).empty();
        if (val) {
            $(regionSelector).append($('<option value="" data-district="" data-region="">Загрузка</option>'));
            $(regionSelector).parent().find('.loading').removeClass('invisible');
            $.ajax({
                dataType: 'json',
                url: rootPath + '/api/WebBtiDistricts/' + val + '/regions',
                data: null,
                success: function (result) {
                    $(regionSelector).empty();
                    $(regionSelector).append($('<option value="" data-district="" data-region="">-- Не выбрано --</option>'));
                    for (var i in result) {
                        $(regionSelector).append($('<option value="' + result[i].id + '" ' + (selectedId == result[i].id ? 'selected' : '') + '>' + result[i].name + '</option>'));
                    }
                    $(regionSelector).parent().find('.loading').addClass('invisible');
                    $(regionSelector).change();
                    $(regionSelector).attr('data-selectedId', null);
                },
                error: function () {
                    $(regionSelector).empty();
                    $(regionSelector).append($('<option value="" data-district="" data-region="">Ошибка загрузки данных</option>'));
                    $(regionSelector).parent().find('.loading').addClass('invisible');
                }
            });
        }
        else {
            $(regionSelector).append($('<option value="" data-district="" data-region="">-- Не выбрано --</option>'));
        }
    }
    function setStreet(element) {
        element.select2({
            initSelection: function (element, callback) {
                var prefix = element.attr('name').match(/^(.*)StreetAutocomplete/)[1];
                var addressSelector = '[name="' + prefix + 'BtiAddressId"]';
                var selectedId = $(addressSelector).attr('data-selectedId');
                var data = { id: element.attr('data-default-id'), text: element.attr('data-default-text') };
                if (element.attr('data-default-id')) {
                    loadHouses(element.attr('data-default-id'), prefix, selectedId);
                    element.attr('data-default-id', 0);
                    element.attr('data-default-text', '');
                }
                else {
                    $('[name="' + prefix + 'BtiAddressId"]').append($('<option value="" data-district="" data-region="">Выберите дом</option>'));
                }
                callback(data);
            },
            minimumInputLength: 1,
            ajax: {
                url: rootPath + '/api/WebBtiStreets',
                dataType: 'json',
                quietMillis: 250,
                data: function (term, page) {
                    return {
                        query: term,
                    };
                },
                results: function (data, page) {
                    for (var i in data) {
                        data[i] = { id: data[i].id, text: data[i].name };
                    }
                    return {
                        results: data
                    };
                },
                cache: true
            }
        })
            .on('change', function (e) {
            var prefix = $(this).attr('name').match(/^(.*)StreetAutocomplete/)[1];
            var addressSelector = '[name="' + prefix + 'BtiAddressId"]';
            var selectedId = $(addressSelector).attr('data-selectedId');
            loadHouses($(this).select2('val'), prefix, selectedId);
        });
    }
    function setStreetAR(element) {
        element.select2({
            initSelection: function (element, callback) {
                var data = {
                    id: element.attr('data-default-id'),
                    text: element.attr('data-default-text'),
                    district: element.attr('data-default-district'),
                    region: element.attr('data-default-region')
                };
                callback(data);
            },
            minimumInputLength: 3,
            formatResult: function (data, container, query) {
                if (data.district) {
                    $(container).attr('data-default-district', data.district);
                }
                if (data.region) {
                    $(container).attr('data-default-region', data.region);
                }
                return data.text;
            },
            ajax: {
                url: "/api/WebFIAS/SearchHome",
                quietMillis: 250,
                type: 'GET',
                data: function (term, page) {
                    return { Query: term };
                },
                results: function (data, page) {
                    var result = [];
                    for (var i = 0; i < data.suggestions.length; i++) {
                        if (data.suggestions[i].data.fias_id) {
                            result.push({
                                id: data.suggestions[i].data.fias_id,
                                text: data.suggestions[i].value,
                                district: data.suggestions[i].data.district,
                                region: data.suggestions[i].data.adm_area
                            });
                        }
                    }
                    return {
                        results: result
                    };
                },
                cache: true
            }
        })
            .on('change', function (e) {
            var prefix = $(this).attr('name').match(/^(.*)FiasId/)[1];
            var aSelector = '[name="' + prefix + 'DistrictAR"]';
            $(aSelector).html(e.added.district);
            var aSelector = '[name="' + prefix + 'District"]';
            $(aSelector).val(e.added.district);
            var aSelector = '[name="' + prefix + 'RegionAR"]';
            $(aSelector).html(e.added.region);
            var aSelector = '[name="' + prefix + 'Region"]';
            $(aSelector).val(e.added.region);
            var aSelector = '[name="' + prefix + 'Name"]';
            $(aSelector).val(e.added.text);
            $('.bti-district-id').select2('val', 0);
        });
    }
    function setAddress(element) {
        element.on('change', function (event) {
            var prefix = $(event.target).attr('name').match(/^(.*)BtiAddressId/)[1];
            var adrressSelector = '[name="' + prefix + 'BtiAddressId"]';
            var districtSelector = '[name="' + prefix + 'District"]';
            var regionSelector = '[name="' + prefix + 'Region"]';
            $(districtSelector).html($(adrressSelector + ' option:selected').attr('data-district'));
            $(regionSelector).html($(adrressSelector + ' option:selected').attr('data-region'));
        });
    }
    function setDistrict(element) {
        element.on('change', function (event) {
            var prefix = $(event.target).attr('name').match(/^(.*)BtiDistrictId/)[1];
            var regionSelector = '[name="' + prefix + 'BtiRegionId"]';
            loadRegions($(event.target).val(), prefix, $(regionSelector).attr('data-selectedId'));
        });
        element.each(function (i) {
            var prefix = $(element[i]).attr('name').match(/^(.*)BtiDistrictId/)[1];
            var regionSelector = '[name="' + prefix + 'BtiRegionId"]';
            loadRegions($(element[i]).val(), prefix, $(regionSelector).attr('data-selectedId'));
        });
    }
    function initStreetNotFoundCheckbox(scope) {
        $(scope.find('.StreetNotFoundCheckbox')).on('change', function (event) {
            var check = $(event.target);
            var prefix = $(check).attr('name').match(/^(.*)ManualType/)[1];
            var autocompleteDiv = $('[name="' + prefix + 'StreetAutocompleteDiv"]');
            var manualDiv = $('[name="' + prefix + 'StreetWithoutAutocompleteDiv"]');
            if ($(check).is(':checked')) {
                autocompleteDiv.addClass('hidden');
                manualDiv.removeClass('hidden');
                autocompleteDiv.find('*').attr('disabled', 'disabled');
                manualDiv.find('*').removeAttr('disabled');
            }
            else {
                autocompleteDiv.removeClass('hidden');
                manualDiv.addClass('hidden');
                autocompleteDiv.find('*').removeAttr('disabled');
                manualDiv.find('*').attr("disabled", 'disabled');
            }
            if ($(event.target).closest('.address-control-body').find('.address-control-disabled').val() === 'True') {
                autocompleteDiv.find('*').not('option').attr('disabled', 'disabled');
                manualDiv.find('*').not('option').attr('disabled', 'disabled');
            }
        });
    }
    function setSelects(scope) {
        scope.select2();
    }
    $(document).ready(function () {
        setStreet($('.street-autocomplete').not('inited').addClass('inited'));
        setStreetAR($('.street-autocompleteAR').not('inited').addClass('inited'));
        setAddress($('select.bti-address').not('inited').addClass('inited'));
        setDistrict($('.bti-district-id').not('inited').addClass('inited'));
        setSelects($('.address-control-select2').not('inited').addClass('inited'));
    });
    $(document).on('AddNewChild', function (event, control) {
        setStreet($(control).find('.street-autocomplete').not('inited').addClass('inited'));
        setStreetAR($('.street-autocompleteAR').not('inited').addClass('inited'));
        setAddress($(control).find('select.bti-address').not('inited').addClass('inited'));
        setDistrict($(control).find('.bti-district-id').not('inited').addClass('inited'));
        setSelects($(control).find('.address-control-select2').not('inited').addClass('inited'));
        initStreetNotFoundCheckbox($(control));
        $(control).find('.StreetNotFoundCheckbox').prop('checked', false).trigger('change');
    });
    initStreetNotFoundCheckbox($);
    $('.StreetNotFoundCheckbox').trigger('change');
    return {
        setSelect2: setStreet,
        setAddress: setAddress
    };
}
//# sourceMappingURL=AddressControlFIAS.js.map