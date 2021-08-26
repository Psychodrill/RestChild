$(function () {
    $('select').select2({ dropdownAutoWidth: true });
    $('.refused').on('click', function (e) {
        var $t = $(e.target).closest('button');
        BootstrapDialog.show({
            title: 'Подтвердить действие',
            message: function () {
                return $('<span>Вы уверены что хотите зафиксировать отказ от подарка?</span>');
            },
            buttons: [
                {
                    label: 'Подтвердить',
                    cssClass: 'btn-danger',
                    action: function (dialogItself) {
                        dialogItself.close();
                        $.ajax({
                            url: rootPath + '/api/NewBout/GiftCancel?uid=' + $t.attr('dataid'),
                            type: 'POST',
                            success: function () {
                                ShowAlert("Был зафиксирован отказ от выдачи подарка.", "alert-success", "glyphicon-ok", true);
                                var interval = setInterval(function () {
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
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }
            ]
        });
    });
    $('.agree').on('click', function (e) {
        var $t = $(e.target).closest('button');
        $.ajax({
            url: rootPath + '/api/NewBout/GiftRequestToIssued?uid=' + $t.attr('dataid'),
            type: 'POST',
            success: function () {
                var $content;
                BootstrapDialog.show({
                    title: 'Подтвердить действие',
                    message: function () {
                        var body = '\t<div>\n' +
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
                            action: function (dialogItself) {
                                var code = $content.find('.accountCode').val();
                                if (!code) {
                                    ShowAlert('Укажите код пользователя приложения для установки связи', "alert-danger", "glyphicon-remove", true);
                                    return;
                                }
                                dialogItself.close();
                                $.ajax({
                                    url: rootPath + '/api/NewBout/GiftIssued?uid=' + $t.attr('dataid') + '&code=' + code,
                                    type: 'POST',
                                    success: function () {
                                        ShowAlert("Был зафиксирован факт выдачи подарка.", "alert-success", "glyphicon-ok", true);
                                        var interval = setInterval(function () {
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
                            action: function (dialogItself) {
                                dialogItself.close();
                            }
                        }
                    ]
                });
            },
            error: function () {
                ShowAlert('Ошибка формирования кода для выдачи подарка', "alert-danger", "glyphicon-remove", true);
            }
        });
    });
});
//# sourceMappingURL=list.js.map