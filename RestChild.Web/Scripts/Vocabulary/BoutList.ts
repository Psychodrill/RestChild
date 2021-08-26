$(() => {
	 $('select').select2();
	 $('#HotelsId').select2({
		 initSelection: (element, callback) => {
			 if ($('#_HotelsId').val() == '') {
				 callback({ id: '', text: '-- Не выбрано --' });
			 } else {
				 callback({ id: $('#_HotelsId').val(), text: $('#_HotelsName').val() });
			 }
		 },
		 minimumInputLength: 0,
		 ajax: {
			 url: rootPath + '/api/WebHotels',
			 dataType: 'json',
			 data: (term, page) => {
				 var hotelType = $('#HotelTypeId').select2('val');
				 return {
					 name: term,
					 onlyApproved: 'True',
					 hotelType: hotelType ? hotelType : null
				 };
			 },
			 results: (data, page) => {
				 var results = [{ id: '', text: '-- Не выбрано --' }];
				 results = results.concat($.map(data, (item) => {
					return {
						 text: item.name,
						 id: item.id
					 }
				}));
				 return {
					 results: results
				 };
			 },
			 cache: true
		 }
	 });
 });
