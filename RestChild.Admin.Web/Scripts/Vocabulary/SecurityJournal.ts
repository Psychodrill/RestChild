$(() => {
   $('.datepicker-anytime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY' });
   $('select').select2();
   $('.stop-session').click(StopSession);

});

function StopSession() {
   $('#ModalSessionStop .modal-body').html('<div class="text-center">Получение данных <img class="text-center" width="20" height="20" src="/Content/images/spinner.gif" /> </div>');
   var SessionUid = $(this).attr("sessionUid");
   var row = this;
   $('#ModalSessionStop').modal('show');
   $.ajax({
      url: rootPath + 'api/SecurityJournalApi/SpotSession',
      data: { SessionUid: SessionUid }
   }).done((data) => {
      $('#ModalSessionStop .modal-body').html('<p class="text-center">Сессия остановлена</p>');
      $(row).closest('tr').remove();
   }).fail(() => {
      $('#ModalSessionStop .modal-body').html('<p class="text-center">Ошибка получения данных</p>');
   });
}


