$(document).ready(function () {
    $('.btn-delete').click(function () {
        var uploadElement = $(this).parent().siblings('.custom-file');
        uploadElement.removeClass('d-none');
        uploadElement.children('.file-path').val('');
        $(this).parent().addClass('d-none');
    });
});

function process(button, namaRtr)
{
    var fileData = $(button).siblings('.dokumen-file').prop('files')[0];
    var progressElement = $(button).parent().siblings('.progress');
    var progressBar = progressElement.children('.progress-bar');
    var linkElement = $(button).parent().siblings('.file-info');
    var formData = new FormData();

    formData.append('__RequestVerificationToken', 
        $('.bg-page-detail input[name=__RequestVerificationToken]').val());
    formData.append('UploadFile', fileData);
    formData.append('NamaRtr', namaRtr);

    $.ajax({
        url: '../../Ajax/UploadFile',
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: formData,                         
        method: 'POST',
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress",
                function(event)
                {
                    var percent = event.loaded / event.total * 100;
                    progressElement.removeClass('d-none');
                    progressBar.css('width', percent + '%');
                    progressBar.attr('aria-valuenow', percent);
                },
                false
            );

            return xhr;
        },
        success: function(response) {
            var base = '../..';
            var result = response.replace(new RegExp('"', 'g'), '');
            $(button).siblings('.file-path').val(result);
            linkElement.children('.file-link').attr('href', base + result);
            linkElement.removeClass('d-none');
            progressElement.addClass('d-none');
            progressBar.css('width', '0%');
            progressBar.attr('aria-valuenow', 0);
            $(button).parent().addClass('d-none');
        }
    });
}
