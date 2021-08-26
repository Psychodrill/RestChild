declare var formActionString : string;

class ResultSearch {
	searchString: string;
	parentIndexes: any;
}
class ParentsNeed {
	item: any;
	visibled: boolean;
	collapseed: boolean;
	panelCollapse: boolean;
}

document['disableInputMaskConfig'] = true;

$(() => {


	var currentSearchString = '';
	var result: ResultSearch[] = [];
	var indexParent = 0;
	var parentsList = {};

	function hasClass(element, cls) {
		return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
	}

	function getParents(element) {
		var current = element;
		var parents = [];
		while (current.tagName.toLowerCase() !== 'form') {
			if (hasClass(current, 'search-to-hide')) {
				parents.push(current);
			}

			current = current.parentNode;
		}

		return parents;
	}

	$('.transport-info-fio, .camper-name').each((index, element) => {
		setTimeout(() => {
			var parentsJs = getParents(element);
			var parentIndexs = [];
			parentsJs.forEach((parentElement, i) => {
				if (!parentElement.attributes["parent-index"]) {
					var att = document.createAttribute("parent-index");
					att.value = 'i' + indexParent;
					parentElement.setAttributeNode(att);
					var hc = hasClass(parentElement, 'panel-collapse');
					var obj: ParentsNeed = {
						item: $(parentElement),
						visibled: true,
						collapseed: true,
						panelCollapse: hc
					};

					parentsList['i' + indexParent] = obj;
					indexParent++;
				}

				parentIndexs.push(parentElement.attributes["parent-index"].value);

			});
			result.push(<any>{
				searchString: (<HTMLElement>element).innerHTML.toLowerCase(),
				parentIndexes: parentIndexs
			});
		});
	});

	var searchMode = false;
	function onlyUnique(value, index, self) {
		return self.indexOf(value) === index;
	}

	function searchPerson(e) {
		var item = $(e.target).val();
		setTimeout(() => {
			var curVal = $(e.target).val();
			if (currentSearchString !== curVal && !item && !curVal) {
				currentSearchString = curVal;
				for (var prop in parentsList) {
					if (parentsList.hasOwnProperty(prop)) {
						var i: ParentsNeed = parentsList[prop];
						if (i && !i.visibled && !i.panelCollapse) {
							i.item.show();
							i.visibled = true;
						}
						if (i && !i.collapseed && i.panelCollapse) {
							i.collapseed = true;
							i.item.collapse('hide');
						}
					}
				}

				$('.not-searched-rn').show();
				$('.searched-rn').hide();
				searchMode = false;
				return;
			}

			if (!searchMode) {
				$('.not-searched-rn').hide();
				$('.searched-rn').show();
				searchMode = true;
			}

			if (currentSearchString !== curVal && item === curVal) {
				var searchString = curVal.toLowerCase().trim();
				var itemToHide = [];
				var itemToShow = [];
				result.forEach((j) => {
					var i = <any>j;
					if (i.searchString.indexOf(searchString) > -1) {
						itemToShow = itemToShow.concat(i.parentIndexes);
					} else {
						itemToHide = itemToHide.concat(i.parentIndexes);
					}
				});

				curVal = $(e.target).val();
				if (item === curVal) {
					currentSearchString = curVal;

					itemToHide.filter(onlyUnique).forEach((el) => {
						setTimeout(() =>
						{
							if (itemToShow.indexOf(el) < 0) {
								var i: ParentsNeed = parentsList[el];
								if (i && i.visibled && !i.panelCollapse) {
									i.item.hide();
									i.visibled = false;
								}
								if (i && !i.collapseed && i.panelCollapse) {
									i.collapseed = true;
									i.item.collapse('hide');
								}
							}
						});
				});

					itemToShow.filter(onlyUnique).forEach((el) => {
						setTimeout(() => {
							var i: ParentsNeed = parentsList[el];
							if (i && !i.visibled && !i.panelCollapse) {
								i.item.show();
								i.visibled = true;
							}
							if (i && i.collapseed && i.panelCollapse) {
								i.collapseed = false;
								i.item.collapse('show');
							}
						});
					});
				}
			}
		}, 50);
	}

	$('#searchTextInput').keyup(searchPerson);
	$('#searchTextInput').on('paste', searchPerson);

	//var items = document.getElementsByTagName('SELECT');
	//setTimeout(() => {
	//	$('select').select2({ width: '100%' });
	//});

	$('.panel-collapse').on('show.bs.collapse', (e) => {
		if ($(e.target).hasClass('setuped')) {
			return;
		}
		$(e.target).addClass('setuped');
		var selects = $(e.target).find('.info:first select');
		var datePickers = $(e.target).find('.info:first .input-mask-date-anytime');
		function setupSelects(index) {
			setTimeout(() => {
				$(selects[index]).select2({ width: '100%' });
			});
		};
		function setupInputmask(index) {
			setTimeout(() => {
				inputMaskDateAnytime($(datePickers[index]));
			});
		};

		for (var i = 0; i < Math.max(selects.length, datePickers.length); i++) {
			if (i < selects.length) {
				setupSelects(i);
			}

			if (i < datePickers.length) {
				setupInputmask(i);
			}
		}

	});

	 $('#DirectoryFlightButtonForAll').click(() => {
		 var selected = $('#DirectoryFlightSelectForAll').select2('data');
		 var date = $('#DepartureDateForAll').val();
		 if (date) {
			 $('.departure-date').not('[disabled="disabled"]').val(date);
		 }
		 if (selected.id) {
			 $('select.transport-info-directory-flight-select').not('[disabled="disabled"]').each((i, e) => {
				 var item = $(e);
				 if (item.data('select2')) {
					 item.select2('data', selected).trigger('change');
				 } else {
					 item.find("option[value=" + selected.id + "]").attr('selected', 'true');
				 }
			 });

		 }
		 if (!date && !selected.id) {
			 $('.departure-date').not('[disabled="disabled"]').val('');
			 $('select.transport-info-directory-flight-select').not('[disabled="disabled"]').each((i, e) => {
				 var item = $(e);
				 if (item.data('select2')) {
					 item.select2('data', selected).trigger('change');
				 } else {
					 item.find("option[value='']").attr('selected', 'true');
				 }
			 });
		 }
	 });

	$('.directory-flight-set-button').click((e) => {
		var row = $(e.target).closest('.directory-flight-set');
		var panelGroup = $(e.target).closest('.panel-group');
		var selected = row.find('.directory-flight-set-select').first().select2('data');
		var countVal = row.find('.integer').val();
		var date = row.find('.departure-date-hotel-set').val();
		var rows = panelGroup.find('tbody>tr');
		var count = countVal ? parseInt(countVal) :	rows.length;
		var onlyEmpty = panelGroup.find('input[type=checkbox]').prop('checked');
		var index = 0;
		var lastRowToProcessed = -1;
		var countRowToProcessed = 0;
		while (index < rows.length && count>0) {
			var $r = $(rows[index]);
			var rspan = $r.find('td.not-searched-rn').attr('rowspan');
			if (rspan) {
				countRowToProcessed = parseInt(rspan)-1;
			}

			var $ddate = $r.find('.departure-date');
			if ($ddate.attr('disabled') !=='disabled') {
				var $s = $r.find('select.transport-info-directory-flight-select');
				$s.select2();
				var er = isNaN(parseInt($s.select2('data').id)) && $ddate.val() === "";
				if (er || !onlyEmpty) {
					count--;
				}
			}

			if (countRowToProcessed > 0) {
				countRowToProcessed--;
			} else if (count>=0) {
				lastRowToProcessed = index;
			}
			index++;
		}

		index = 0;
		while (index <= lastRowToProcessed) {
			var $row = $(rows[index]);
			var $dd = $row.find('.departure-date');
			if ($dd.attr('disabled') !== 'disabled') {
				var $select = $row.find('select.transport-info-directory-flight-select');
				var emptyRow = isNaN(parseInt($select.select2('data').id)) && $dd.val() === "";
				if (emptyRow || !onlyEmpty) {
					$dd.val(date);
					$select.select2('data', selected).trigger('change');
				}
			}

			index++;
		}
	 });

	//$('.not-need-ticket').change((e) => {
	//	if ($(e.target).is(':checked')) {
	//		$(e.target).closest('tr').find('.transport-info-directory-flight-select').select2('data', { value: '', text: '-- Не выбрано --' }).attr('disabled', 'disabled');
	//		$(e.target).closest('tr').find('.transport-info-directory-wagon-input').val('').attr('disabled', 'disabled');
	//		$(e.target).closest('tr').find('.transport-info-directory-placenumber-input').val('').attr('disabled', 'disabled');
	//		$(e.target).closest('tr').find('.departure-date').val('').attr('disabled', 'disabled');
	//		$(e.target).closest('tr').find('.transport-info-directory-placement').addClass('hidden');
	//	} else {
	//		$(e.target).closest('tr').find('.transport-info-directory-flight-select').removeAttr('disabled');
	//		$(e.target).closest('tr').find('.transport-info-directory-wagon-input').removeAttr('disabled');
	//		$(e.target).closest('tr').find('.transport-info-directory-placenumber-input').removeAttr('disabled');
	//		$(e.target).closest('tr').find('.departure-date').removeAttr('disabled');
	//	}

	//	var needTicket = !$(e.target).is(':checked');

	//});

	$('select.transport-info-directory-flight-select-for-hotels').change((e) => {
		var tr = $(e.target).closest('tr');
		if ($(e.target).find(':selected').attr('data-need-placement') === 'True') {
			tr.find('.transport-info-directory-placement').removeClass('hidden');
		} else {
			tr.find('.transport-info-directory-placement').addClass('hidden');
		}
	});

	$('#transportForm').submit(() => {
		if (typeof (Storage) !== 'undefined') {
			window.sessionStorage['TransportInfo_Id'] = $('#Data_Id').val();
			window.sessionStorage['TransportInfo_CampRestOpen'] = $('#campsAccordionCollapse').hasClass('in') ? 'true' : 'false';
			window.sessionStorage['TransportInfo_HotelRestOpen'] = $('#hotelsAccordionCollapse').hasClass('in') ? 'true' : 'false';
			window.sessionStorage['TransportInfo_CampsOpen'] = JSON.stringify($('.camp-accordion.in').map((i, val) => { return $(val).attr('data-camp-id'); }).get());
			window.sessionStorage['TransportInfo_HotelsOpen'] = JSON.stringify($('.hotel-accordion.in').map((i, val) => { return $(val).attr('data-hotel-id'); }).get());
			window.sessionStorage['TransportInfo_PartiesOpen'] = JSON.stringify($('.party-accordion.in').map((i, val) => { return $(val).attr('data-party-id'); }).get());
			window.sessionStorage['TransportInfo_RequestsOpen'] = JSON.stringify($('.request-accordion.in').map((i, val) => { return $(val).attr('data-request-id'); }).get());
		}
		return true;
	});

	$('.datetime').inputmask("d.m.y h:s", {
		placeholder: "дд.мм.гггг чч:мм",
		clearIncomplete: true
	});
	$('.datetime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });
	$('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm' });
	inputMaskDateAnytime($('#DepartureDateForAll'));
	$('#DirectoryFlightSelectForAll').select2();
	restoreAccordionStates();
});

function restoreAccordionStates() {
	if (typeof (Storage) !== 'undefined') {
		if (window.sessionStorage['TransportInfo_Id'] == $('#Data_Id').val()) {

			if (window.sessionStorage['TransportInfo_CampRestOpen'] == 'true') {
				$('#campsAccordionCollapse').addClass('in');
			}
			if (window.sessionStorage['TransportInfo_HotelRestOpen'] == 'true') {
				$('#hotelsAccordionCollapse').addClass('in');
			}

			try {
				var camps = JSON.parse(window.sessionStorage['TransportInfo_CampsOpen']);
				for (var camp in camps) {
					$('.camp-accordion[data-camp-id="' + camps[camp] + '"]').addClass('in');
				}
			} catch (e) {
				console.log('Ошибка разбора JSON TransportInfo_CampsOpen');
			}

			try {
				var hotels = JSON.parse(window.sessionStorage['TransportInfo_HotelsOpen']);
				for (var hotel in hotels) {
					$('.hotel-accordion[data-hotel-id="' + hotels[hotel] + '"]').addClass('in');
				}
			} catch (e) {
				console.log('Ошибка разбора JSON TransportInfo_HotelsOpen');
			}

			try {
				var parties = JSON.parse(window.sessionStorage['TransportInfo_PartiesOpen']);
				for (var party in parties) {
					$('.party-accordion[data-party-id="' + parties[party] + '"]').addClass('in');
				}
			} catch (e) {
				console.log('Ошибка разбора JSON TransportInfo_PartiesOpen');
			}

			try {
				var requests = JSON.parse(window.sessionStorage['TransportInfo_RequestsOpen']);
				for (var request in requests) {
					$('.request-accordion[data-request-id="' + requests[request] + '"]').addClass('in');
				}
			} catch (e) {
				console.log('Ошибка разбора JSON TransportInfo_PartiesOpen');
			}

		}

		window.sessionStorage.removeItem('TransportInfo_Id');
		window.sessionStorage.removeItem('TransportInfo_CampRestOpen');
		window.sessionStorage.removeItem('TransportInfo_HotelRestOpen');
		window.sessionStorage.removeItem('TransportInfo_CampsOpen');
		window.sessionStorage.removeItem('TransportInfo_HotelsOpen');
		window.sessionStorage.removeItem('TransportInfo_PartiesOpen');
		window.sessionStorage.removeItem('TransportInfo_RequestsOpen');
	}
}

function overrideConfirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector) {
	if (actionCode === formActionString) {
		var emptyWagonInputs = $('.transport-info-directory-placement:not(.hidden) .transport-info-directory-wagon-input:blank:not(:disabled)');
		var emptyPlaceNumberInputs = $('.transport-info-directory-placement:not(.hidden) .transport-info-directory-placenumber-input:blank:not(:disabled)');
		var emptyFlightSelects = $('.transport-info-directory-placement:not(.hidden) select.transport-info-directory-flight-select:blank:not(:disabled)');
		var emptyDepartureDates = $('.departure-date:blank:not(:disabled)');
		 if (emptyWagonInputs.length !== 0 || emptyPlaceNumberInputs.length !== 0 || emptyFlightSelects.length !== 0 || emptyDepartureDates.length !== 0) {
			 BootstrapDialog.show({
				 title: 'Предупреждение',
				 message: 'Внимание! Информация о транспорте внесена не в полном объеме',
				 buttons: [
					 {
						 label: 'Закрыть',
						 action: dialogItself => {

							 dialogItself.close();
						 }
					 }
				 ],
				 onhide: dialogRef => {
					 confirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector);
				 }
			 });

			 emptyWagonInputs.each((num, val) => {
				 $(val).closest('.form-group').addClass('has-error');
			 });

			 emptyPlaceNumberInputs.each((num, val) => {
				 $(val).closest('.form-group').addClass('has-error');
			 });

			 emptyFlightSelects.each((num, val) => {
				 $(val).closest('.form-group').addClass('has-error');
			 });

			 emptyDepartureDates.each((num, val) => {
				 $(val).closest('.input-group').addClass('has-error');
			 });
		 } else {
			 $('.transport-info-directory-wagon.form-group').removeClass('has-error');
			 $('.transport-info-directory-placeNum.form-group').removeClass('has-error');
			 $('.transport-info-directory-flight.form-group').removeClass('has-error');
			 $('.departure-date').each((num, val) => {
				 $(val).closest('.input-group').removeClass('has-error');
			 });

			 confirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector);
		 }
	 } else {
		 confirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector);
	 }

 }
