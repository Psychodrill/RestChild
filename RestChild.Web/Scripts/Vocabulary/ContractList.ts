 $(() => {

	 $('#SignDate').inputmask("d.m.y", {
		 placeholder: "дд.мм.гггг",
		 clearIncomplete: true
	 });
	 $('#SignDate').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
	 $('select').select2();

 });
