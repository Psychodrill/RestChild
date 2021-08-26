$(() => {
    $('.select2').select2();
    let $formType = $('#formType');

    $('#btnExcel').on('click', ()=>{
        let subUrl = $formType.val();
        window.location.replace(rootPath + subUrl.substr(1) + '?yearOfRestId=' + $('#year').val());
    });
});
