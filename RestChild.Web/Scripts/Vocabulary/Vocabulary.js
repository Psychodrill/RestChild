$(function () {
	var uploadHandler = rootPath + 'UploadImage.ashx';
	var downloadHandler = rootPath + 'DownloadImage.ashx';

	var cx = 0;
	var cy = 0;
	var cx2 = 0;
	var cy2 = 0;

	function loadImage(input) {
		$('#imgForCrop').removeAttr("style");
		if (input.files && input.files[0]) {
			var reader = new FileReader();

			reader.onload = function (e) {

				$('#imgForCrop').attr('src', e.target.result);
			}

			reader.readAsDataURL(input.files[0]);
		}
	}

	function updatePreview(c) {
		cx = c.x;
		cy = c.y;
		cx2 = c.x2;
		cy2 = c.y2;
	};

	$('#imageCropDialog').on('shown.bs.modal', function () {
		var imgPreview = $('#imgPreview');
		cx = 0;
		cy = 0;
		cx2 = 0;
		cy2 = 0;
		$('#imgForCrop').removeClass('hidden');
		$('#imgForCrop').Jcrop({
			aspectRatio: imgPreview.width() / imgPreview.height(),
			boxWidth: $('#imageCropDialogBody').width(),
			onChange: updatePreview,
			onSelect: updatePreview
		});
	});

	$('#imageCropDialog').on('hidden.bs.modal', function () {
		$('#imgForCrop').addClass('hidden');
		$('#imgForCrop').data('Jcrop').destroy();
	});

	function onImageCropDialogSaveClick() {
		$('#loading').addClass('loading');
		var data = new FormData();
		data.append('file-' + 0, $('#photo')[0].files[0]);
		data.append('x', Math.round(cx));
		data.append('y', Math.round(cy));
		data.append('x2', Math.round(cx2));
		data.append('y2', Math.round(cy2));
		$.ajax({
			url: uploadHandler,
			data: data,
			cache: false,
			contentType: false,
			processData: false,
			type: 'POST',
			success: function (data) {
				$('#PhotoUrl, .photoUrl').val(data);
				$('#imgPreview').attr('src', downloadHandler + '/' + data);
				$('#imageCropDialog').modal('hide');
				$('#editImageBtn').removeClass('hidden');
				$('#loading').removeClass('loading');
			},
			error: function() {
				alert('Ошибка сохранения');
				$('#loading').removeClass('loading');
			}
		});
	}

	function onImageCropDialogCancelClick() {

	}

	function showEditDialog() {
		$('#imageCropDialog').modal({ backdrop: false });
		$('#photoUpdated').val(true);
	}

	$('#photo').change(function () {
		loadImage(this);
		showEditDialog();
		$('#btnRemoveFile').removeClass('hidden');
	});

	$('#btnRemoveFile').click(() => {
		$('#imgPreview').attr('src', null);
		$('#imgForCrop').attr('src', null);
		$('#PhotoUrl, .photoUrl').val('');
		$('#editImageBtn').addClass('hidden');
		$('#btnRemoveFile').addClass('hidden');
	});

	$('#editImageBtn').click(function () {
		showEditDialog();
	});

	$('#imageCropDialogSave').click(onImageCropDialogSaveClick);
	$('#imageCropDialogCancel').click(onImageCropDialogCancelClick);
	$("#PriceBasePlace").numeric({ decimal: ",", negative: true });
	$("#PriceAddonPlace").numeric({ decimal: ",", negative: true });
})
