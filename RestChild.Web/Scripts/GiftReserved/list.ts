$(() => {
    $('select').select2({dropdownAutoWidth: true});

    $('.datetimepicker').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY', maxDate: new Date() });

    $('.refused').on('click', (e)=>{
        let $t = $(e.target).closest('button');

        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: () => {
                return $('<span>Вы уверены что хотите зафиксировать отказ от подарка?</span>');
            },
            buttons: [
                {
                    label: 'Подтвердить',
                    cssClass: 'btn-danger',
                    action: dialogItself => {

                        dialogItself.close();

                        $.ajax({
                            url: rootPath + '/api/NewBout/GiftCancel?uid=' + $t.attr('dataid'),
                            type: 'POST',
                            success: () => {
                                ShowAlert("Был зафиксирован отказ от выдачи подарка.", "alert-success", "glyphicon-ok", true);
                                let interval = setInterval(()=>{
                                    clearInterval(interval);
                                    $('.mainForm').submit();
                                }, 1000);
                            },
                            error: function () {
                                ShowAlert('Ошибка выполнения отказа от подарка', "alert-danger", "glyphicon-remove", true);
                            }
                        });

                    }
                }, {
                    label: 'Отмена',
                    action: dialogItself => {
                        dialogItself.close();
                    }
                }
            ]
        });

    });

    $('.agree').on('click', (e)=>{
        let $t = $(e.target).closest('button');

        $.ajax({
            url: rootPath + '/api/NewBout/GiftRequestToIssued?uid=' + $t.attr('dataid'),
            type: 'POST',
            success: () => {
                let $content;
                BootstrapDialog.show({
                    title: 'Подтвердить действие',
                    message: () => {
                        let body = '\t<div>\n' +
                            '<label class="control-label">Укажите код для получения подарка:</label>\n' +
                            '<input type="text" class="form-control accountCode" maxlength="500" />\n' +
                            '</div>';
                        $content = $(body);
                        return $content;
                    },
                    buttons: [
                        {
                            label: 'Подтвердить',
                            cssClass: 'btn-danger',
                            action: dialogItself => {
                                let code = $content.find('.accountCode').val();

                                if (!code){
                                    ShowAlert('Укажите код пользователя приложения для установки связи', "alert-danger", "glyphicon-remove", true);
                                    return;
                                }

                                dialogItself.close();

                                $.ajax({
                                    url: rootPath + '/api/NewBout/GiftIssued?uid=' + $t.attr('dataid') + '&code=' + code,
                                    type: 'POST',
                                    success: () => {
                                        ShowAlert("Был зафиксирован факт выдачи подарка.","alert-success", "glyphicon-ok", true);
                                        let interval = setInterval(()=>{
                                            clearInterval(interval);
                                            $('.mainForm').submit();
                                        }, 1000);
                                    },
                                    error: function () {
                                        ShowAlert('Ошибка фиксации факта выдачи подарка', "alert-danger", "glyphicon-remove", true);
                                    }
                                });
                            }
                        }, {
                            label: 'Отмена',
                            action: dialogItself => {
                                dialogItself.close();
                            }
                        }
                    ]
                });
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if(xhr.status == 405) {
                    ShowAlert('Ошибка. Формат номера телефона получателя указан не верно', "alert-danger", "glyphicon-remove", true);
                }
                else {
                    ShowAlert('Ошибка формирования кода для выдачи подарка', "alert-danger", "glyphicon-remove", true);
                }
            }
        });
    })

    $('.Bouts').select2({
        minimumInputLength: 1,
        allowClear: true,
        language: "ru",
        initSelection: function(element, callback) {
            if (element.val() == '')
                callback({id: '', text: '-- Не выбрано --'});
            else
                callback({id: element.val(), text: GetBoutName()});
        },
        ajax: {
            url: rootPath + '/api/WebMobileVocabulary/GetBouts',
            dataType: 'json',
            quietMillis: 250,
            data: function(term, page) {
                return {
                    query: term,
                };
            },
            results: function(data, page) {
                var res = [];
                for (var i = 0; i < data.length; i++) {
                    if (data[i].id) {
                        res.push({ id: data[i].id, text: data[i].name });
                    }
                }

                return {
                    results: res
                };
            },
            cache: true
        }
    });

    function GetBoutName(){
        return $('#BoutName').val();
    }

    $(".massCancel").on("click", function (){
        BootstrapDialog.show({
            title: "Подтвердить действие",
            message: "Вы действительно хотите сбросить все подарки?",
            buttons: [
                {
                    label: "Сбросить",
                    cssClass: "btn-danger",
                    action: dialogItself => {
                        $.ajax({
                            type: "get",
                            url: rootPath + "/GiftReserved/GiftMassCancel",
                        })
                            .done((result) => {
                                if (result.HasError) {
                                    BootstrapDialog.show({
                                        title: "Ошибка",
                                        message: result.ErrorMessage
                                    });
                                }
                            })
                            .fail(() => {
                                BootstrapDialog.show({
                                    title: "",
                                    message: "Ошибка"
                                });
                            })
                        dialogItself.close();
                    }
                }, {
                    label: "Отмена",
                    action: dialogItself => {
                        dialogItself.close();
                    }
                }
            ]
        });
    })
});



