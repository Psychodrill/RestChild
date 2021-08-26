$(() => {
    $('.unhideitem').on('click', (e) => {
        let $t = $(e.target).closest('.block-base');
        let block = $t.find('.blockitems');
        if (block.is('.hidden')) {
            block.removeClass('hidden');
        } else {
            block.addClass('hidden');
        }
    });
});
