$(function () {
    moment.locale('ru');
    $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
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
});
//# sourceMappingURL=AnalyticReport.js.map