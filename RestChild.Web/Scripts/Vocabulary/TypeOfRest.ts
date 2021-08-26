/// <reference path="../typings/jquery.fileupload/jquery.fileupload.d.ts" />

$(() => {
	$('.fileinput-rest-rules-button').each((i, e) => {
		var realName = '';
		var parent = $(e).closest('.form-group');
		var fu = $(e).fileupload({
			url: rootPath + '/UploadRulesOfRestHandler.ashx',
			dataType: 'json',
			pasteZone: null,
			dropZone: null,
			maxChunkSize: 1000000,
			beforeSend: (xhr, data) => {
				xhr.setRequestHeader("X-FileName", realName);
			},
			submit: (e, data) => {
			},
			always: (e, data) => {
			},
			done: (e, data) => {
				if (data.result != null && data.result.length > 0) {
					var realname = data.result[0].realname;
					parent.find('.UrlToRulesOfRest').val(realname);
					parent.find('.remove-rest-rules').removeClass("hidden");
					parent.find('.fileinput-rest-rules-button').addClass('hidden');
					parent.find('input[type=hidden]').val(realname);
					var href = parent.find('.href-file');
					var fileExt = realname != null ? realname.substring(realname.lastIndexOf('.')) : '';
					href.attr('href', '/UploadRulesOfRestHandler.ashx?f=' + realname + '&t=Правила' + fileExt);
					href.removeClass('hidden');
				}
				realName = '';
			}
		});

		fu.on('fileuploadchunkdone', (e, data) => {
			$.each(data.result, (index, file) => {
				realName = file.realname;
			});
		});
	});

	$('.remove-rest-rules').click((e) => {
		var parent = $(e.target).closest('.form-group');
		parent.find('.remove-rest-rules').addClass("hidden");
		parent.find('.fileinput-rest-rules-button').removeClass('hidden');
		parent.find('input[type=hidden]').val('');
		var href = parent.find('.href-file');
		href.attr('href', null);
		href.addClass('hidden');
	});
});
