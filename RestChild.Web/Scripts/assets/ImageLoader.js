function initPhotoUploader(uploadHandler, downloadHandler) {
    uploadHandler = uploadHandler || rootPath + 'UploadImage.ashx';
    downloadHandler = downloadHandler || rootPath + 'DownloadImage.ashx';
    var cx = 0;
    var cy = 0;
    var cx2 = 0;
    var cy2 = 0;
    var fileName = '';
    function loadImage(input) {
        $('#imgForCrop').removeAttr("style");
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            fileName = input.files[0].name;
            reader.onload = function (e) {
                var result = e.target.result;
                $('#imgForCrop').attr('src', result);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }
    function updatePreview(c) {
        cx = c.x;
        cy = c.y;
        cx2 = c.x2;
        cy2 = c.y2;
    }
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
        var file;
        file = $('#photo')[0];
        data.append('file-' + 0, file.files[0]);
        data.append('x', Math.round(cx).toString());
        data.append('y', Math.round(cy).toString());
        data.append('x2', Math.round(cx2).toString());
        data.append('y2', Math.round(cy2).toString());
        $.ajax({
            url: uploadHandler,
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data) {
                $('.photoUrl').val(data);
                $('.photoName').val(fileName);
                $('#imgPreview').attr('src', downloadHandler + '/' + data);
                $('#imageCropDialog').modal('hide');
                $('#editImageBtn').removeClass('hidden');
                $('#loading').removeClass('loading');
            },
            error: function () {
                alert('Ошибка сохранения');
                $('#loading').removeClass('loading');
            }
        });
    }
    function onImageCropDialogCancelClick() {
    }
    function showEditDialog() {
        $('#imageCropDialog').modal({ backdrop: false });
    }
    $('#photo').change(function () {
        loadImage(this);
        showEditDialog();
        $('#btnRemoveFile').removeClass('hidden');
    });
    $('#btnRemoveFile').click(function () {
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
}
//# sourceMappingURL=ImageLoader.js.map