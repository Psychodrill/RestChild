function confirmStateButton(formSelector, actionSelector, actionCode, buttonName, description, commentSelector) {
    if (!actionCode) {
        $(actionSelector).val('');
        $(formSelector).submit();
        return;
    }
    var $content;
    BootstrapDialog.show({
        title: 'Подтвердить действие',
        message: function (dialog) {
            var fn = doT.template($("#stateDialogBody").html());
            $content = $(fn({ name: ((description) ? description : 'Вы действительно хотите ' + buttonName.toLowerCase() + '?'), needComment: commentSelector }));
            return $content;
        },
        buttons: [
            {
                label: buttonName,
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    $(actionSelector).val(actionCode);
                    if (commentSelector) {
                        $(commentSelector).val(dialogItself.$modalContent.find('.stateDialogComment').val());
                    }
                    $(formSelector).submit();
                    dialogItself.close();
                }
            }, {
                label: 'Отмена',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }
        ]
    });
}
//# sourceMappingURL=StateScript.js.map