 $(() => {
	 $('select').select2();
	 $('#AgeFrom, #AgeTo').inputmask('99', { allowMinus: false, rightAlign: false, clearIncomplete: true });
 });