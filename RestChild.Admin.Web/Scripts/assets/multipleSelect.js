//TODO: использовать requierjs

$('.multiple-select').multiselect({
	includeSelectAllOption: true,
	enableFiltering: true,
	enableCaseInsensitiveFiltering: true,
	selectAllText: 'Выбрать всё',
	buttonText: function(options, select) {
		return "Назначить";
	}
});
