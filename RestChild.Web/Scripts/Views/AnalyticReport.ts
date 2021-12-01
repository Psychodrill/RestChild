
$(() => {
	moment.locale('ru');
	$('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });

    var defaultYearItem = $("#yearOfRestSelect option:default");
	function setText(id: string, select2Element: any) {
		var str=null;
		if (select2Element != null)
			str = select2Element.text;

		$(id).val(str);
	}

	$('#timeOfRestSelect').on("change", (event) => {
		var selectedElement = $("#timeOfRestSelect").select2('data');
		setText("#timeOfRest", selectedElement);
	});

    $('#yearOfRestSelect').on("change", (event) => {
        var selectedElement = $("#yearOfRestSelect option:selected").text();
        setText("#yearOfRest", selectedElement);
	});

	$("#districtName").on("change", (event) => {
		var selectedElement = $("#districtName").select2('data');
		setText("#districtName",selectedElement);
	});

	$("#hotelSelect").on("change", (event) => {
		var selectedElement = $("#hotelSelect").select2('data');
		setText("#hotelName",selectedElement);
	});

	$("#benefitSelect").on("change", (event) => {
		var selectedElement = $("#benefitSelect").select2('data');
		setText("#benefitName",selectedElement);
	});

	$("#vedomstvoSelect").on("change", (event) => {
		var selectedElement = $("#vedomstvoSelect").select2('data');
		setText("#vedomstvoName", selectedElement);
	});

	$("#agencySelect").on("change", (event) => {
		var selectedElement = $("#agencySelect").select2('data');
		setText("#agencyName", selectedElement);
	});

	$("#supplierSelect").on("change", (event) => {
		var selectedElement = $("#supplierSelect").select2('data');
		setText("#supplierName", selectedElement);
	});

	$("#placeOfRestSelect").on("change", (event) => {
		var selectedElement = $("#placeOfRestSelect").select2('data');
		setText("#placeOfRestName", selectedElement);
	});

	$("#placeOfRestSelect").on("change", (event) => {
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

        checkBoxIds.forEach((id) => setDefaultCheckBoxValue(id));
        textboxIds.forEach((id) => setDefaultTextboxValue(id));
        dropdownListIds.forEach((id) => setDefaultDropDownListValue(id));
        select2DropDownIds.forEach((element) => setDefaultSelect2DropDownValue(element.nameId, element.selectId));

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
