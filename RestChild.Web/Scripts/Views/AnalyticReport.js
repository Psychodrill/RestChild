$(function () {
    moment.locale('ru');
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
    defaultYearItem = $("#yearOfRestSelect option:default");
    function setText(id, select2Element) {
        var str = null;
        if (select2Element != null)
            str = select2Element.text;
        $(id).val(str);
    }
    $('#timeOfRestSelect').on("change", function (event) {
        var selectedElement = $("#timeOfRestSelect").select2('data');
        setText("#timeOfRest", selectedElement);
    });
    $('#yearOfRestSelect').on("change", function (event) {
        var selectedElement = $("#yearOfRestSelect option:selected").text();
        setText("#yearOfRest", selectedElement);
    });
    $("#districtName").on("change", function (event) {
        var selectedElement = $("#districtName").select2('data');
        setText("#districtName", selectedElement);
    });
    $("#hotelSelect").on("change", function (event) {
        var selectedElement = $("#hotelSelect").select2('data');
        setText("#hotelName", selectedElement);
    });
    $("#benefitSelect").on("change", function (event) {
        var selectedElement = $("#benefitSelect").select2('data');
        setText("#benefitName", selectedElement);
    });
    $("#vedomstvoSelect").on("change", function (event) {
        var selectedElement = $("#vedomstvoSelect").select2('data');
        setText("#vedomstvoName", selectedElement);
    });
    $("#agencySelect").on("change", function (event) {
        var selectedElement = $("#agencySelect").select2('data');
        setText("#agencyName", selectedElement);
    });
    $("#supplierSelect").on("change", function (event) {
        var selectedElement = $("#supplierSelect").select2('data');
        setText("#supplierName", selectedElement);
    });
    $("#placeOfRestSelect").on("change", function (event) {
        var selectedElement = $("#placeOfRestSelect").select2('data');
        setText("#placeOfRestName", selectedElement);
    });
    $("#placeOfRestSelect").on("change", function (event) {
        var selectedElement = $("#placeOfRestSelect").select2('data');
        setText("#placeOfRestName", selectedElement);
    });
    $('select.select2').select2();
    $("#btnClearFields").click(function () {
        //#region Ids
        var select2DropDownIds = [
            { nameId: '#timeOfRest', selectId: '#timeOfRestSelect' },
            { nameId: '#districtName', selectId: '#districtSelect' },
            { nameId: '#hotelName', selectId: '#hotelSelect' },
            { nameId: '#benefitName', selectId: '#benefitSelect' },
            { nameId: '#vedomstvoName', selectId: '#vedomstvoSelect' },
            { nameId: '#agencyName', selectId: '#agencySelect' },
            { nameId: '#supplierName', selectId: '#supplierSelect' },
            { nameId: '#placeOfRestName', selectId: '#placeOfRestSelect' },
        ];
        var textboxIds = [
            '#YearOfBirthDateBegin',
            '#YearOfBirthDateEnd',
            '#DateStartBegin',
            '#DateStartEnd',
            '#DateFormingBegin',
            '#DateFormingEnd',
            '#YearOfBDateFormingEndirthDateBegin',
            '#FlightNumber'
        ];
        var dropdownListIds = [
            '#SupplierId',
            '#DepartureId',
            '#ArrivalId',
            '#TypeOfTransportId',
            '#ExchangeBaseRegistryTypeId',
            //'#yearOfRestSelect'
        ];
        //#endregion        
        var checkBoxIds = [
            '#NextYearsIncluded'
        ];
        checkBoxIds.forEach(function (id) { return setDefaultCheckBoxValue(id); });
        textboxIds.forEach(function (id) { return setDefaultTextboxValue(id); });
        dropdownListIds.forEach(function (id) { return setDefaultDropDownListValue(id); });
        select2DropDownIds.forEach(function (element) { return setDefaultSelect2DropDownValue(element.nameId, element.selectId); });
        setDefaultDropDownListYearValue('#yearOfRest', '#yearOfRestSelect');
        //#region Helpers
        function setDefaultSelect2DropDownValue(nameId, selectId) {
            setText(nameId, { id: '', text: '' });
            $(selectId).select2('data', { id: '', text: '-- Не выбрано --' });
        }
        function setDefaultDropDownListValue(id) {
            $(id).select2('data', { id: '', text: '-- Не выбрано --' });
        }
        function setDefaultTextboxValue(id) {
            $(id).val('');
        }
        function setDefaultCheckBoxValue(id) {
            $(id).val('false');
        }
        function setDefaultDropDownListYearValue(nameId, selectId) {
            setText(nameId, { id: defaultYearItem.val(), text: defaultYearItem.text() });
            $(selectId).select2('data', { id: defaultYearItem.val(), text: defaultYearItem.text() });
        }
        //#endregion        
    });
});
//# sourceMappingURL=AnalyticReport.js.map