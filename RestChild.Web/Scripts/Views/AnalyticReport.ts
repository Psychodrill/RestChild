$(() => {
	moment.locale('ru');
	$('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });


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
		setText("#yearOfRest",selectedElement);
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
});
