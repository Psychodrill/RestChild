
function saveForm() {
	var $memo = <any>$('#memo');
	$('.description').val($memo.code());
	$('#boutJournalForm').submit();
}
$(() => {
	var data = ($('.description').val());
	var $memo = <any>$('#memo');
	$memo.summernote({
		lang: 'ru-RU',
		toolbar: [
			['style', ['style']],
			['font', ['bold', 'italic', 'underline', 'clear']],
			['fontname', ['fontname']],
			['color', ['color']],
			['para', ['ul', 'ol', 'paragraph']],
			['height', ['height']],
			['table', ['table']],
			['insert', ['link', 'hr']]
		],
		maxHeight: 300,
		height: 300
	});

	$memo.code(data);

	$('.datetime').inputmask("d.m.y h:s", {
		placeholder: "дд.мм.гггг чч:мм",
		clearIncomplete: true
	}).focusout((e) => {
		var now = moment().startOf('day');
		var val = moment($(e.target).val(), 'DD.MM.YYYY HH:mm');
		if (now.diff(val, 'days') < 0) {
			$(e.target).val('');
		}
	});
	$('.datetime').datetimepicker({ showTodayButton: true, format: 'DD.MM.YYYY HH:mm', maxDate: new Date() });
});

$(() => {
	var fileRowFn = doT.template($('#fileRowTemplate').html());
	function processRowsTask(body, startIndex) {
		var childs = body.find('tr');
		var regExp = new RegExp("\[[0-9]+\].", "g");
		for (var i = 0; i < childs.length; i++) {
			$(childs[i]).find('input').each(function () {
				if ($(this).attr('name')) {
					$(this).attr('name', $(this).attr('name').replace(regExp, '[' + (startIndex + i).toString() + '].'));
				}
			});
		}

		if (childs.length === 1) {
			body.find('.empty-row').show();

		} else {
			body.find('.empty-row').hide();
		}
	}

	function removeFile() {
		var $row = $(this).parent().parent();
		var $tbody = $row.parent();
		$row.remove();
		processRowsTask($tbody, 0);
	}

	$('.request-file-remove').click(removeFile);

	$('.fileinput-button').each(function () {
		var realName = '';
		var fu = $(this).fileupload({
			url: rootPath + '/UploadBoutJournal.ashx',
			dataType: 'json',
			pasteZone: null,
			dropZone: null,
			maxChunkSize: 1000000,
			beforeSend: (xhr) => {
				xhr.setRequestHeader("X-FileName", realName);
			},
			submit: (e) => {
				var target = $(e.target);
				var parent = $(target.parent().parent().parent()[0]);
				parent.find('.file-upload-div').addClass('hidden');
				parent.find('.file-uploading-div').removeClass('hidden');
			},
			always: (e) => {
				var target = $(e.target);
				var parent = $(target.parent().parent().parent()[0]);
				parent.find('.file-upload-div').removeClass('hidden');
				parent.find('.file-uploading-div').addClass('hidden');
			},
			done: (e, data) => {
				realName = '';
				$.each(data.result, (index, file) => {
					var target = $(e.target);
					var parent = $(target.parent().parent().parent()[0]);
					var tbody = parent.find('table tbody');
					var $table = parent.find('table');
					var entity = {
						fileTypesIndex: parent.find(".index-hidden").val(),
						fileTitle: file.name,
						fileName: file.realname,
						tableName: $table.attr('tablename'),
						fileIndex: parent.find('table tbody tr').length
					};

					var row = $(fileRowFn(entity));
					row.find('.request-file-remove').click(removeFile);
					tbody.append(row);
					processRowsTask(tbody, 0);
				});
			}
		});

		fu.on('fileuploadchunkdone', (e, data) => {
			$.each(data.result, (index, file) => {
				realName = file.realname;
			});
		});
	});
});
