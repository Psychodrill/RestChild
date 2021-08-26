$(function () {
    $('.unhideitem').on('click', function (e) {
        var $t = $(e.target).closest('.block-base');
        var block = $t.find('.blockitems');
        if (block.is('.hidden')) {
            block.removeClass('hidden');
        }
        else {
            block.addClass('hidden');
        }
    });
});
//# sourceMappingURL=childUniqe.js.map