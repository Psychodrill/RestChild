$('.fileinput-memo-button').each(function () {
    var realName = '';
    var fu = $(this).fileupload({
        url: '/UploadHotelFile.ashx',
        dataType: 'json',
        pasteZone: null,
        dropZone: null,
        maxChunkSize: 1000000,
        beforeSend: function (xhr, data) {
            xhr.setRequestHeader("X-FileName", realName);
        },
        submit: function (e, data) {
        },
        always: function (e, data) {
        },
        done: function (e, data) {
            realName = '';
            var $p = $(e.target).parent().parent();
            if (data.result != null && data.result.length > 0) {
                var realname = data.result[0].realname;
                $p.find('.memolink').val(realname);
                $p.find('.remove-memo').removeClass("hidden");
                $p.find('.fileinput-memo-button').addClass('hidden');
                var href = $p.find('.href-file');
                var fileExt = realname != null ? realname.substring(realname.lastIndexOf('.')) : '';
                href.attr('href', '/UploadHotelFile.ashx?f=' + realname + '&t=Памятка' + fileExt);
                href.removeClass('hidden');
            }
        }
    });
    fu.on('fileuploadchunkdone', function (e, data) {
        $.each(data.result, function (index, file) {
            realName = file.realname;
        });
    });
});
$('.remove-memo').click(function () {
    $('.remove-memo').addClass("hidden");
    $('.fileinput-memo-button').removeClass('hidden');
    $('.memolink').val('');
    var href = $('.href-file');
    href.attr('href', null);
    href.addClass('hidden');
});
//# sourceMappingURL=MemoFile.js.map