$(document).ready(function () {
    $('#provinsi').removeAttr('disabled');
    $('#kabupaten-kota').removeAttr('disabled');

    $('#provinsi').change( function () {
        $.getJSON('../api/Table/KabupatenKota', 
            { kodeProvinsi: $('#provinsi').val()}, function(data){
            var items='';
            $('#kabupaten-kota').empty();
            $.each(data, function(i, kabupatenKota){
                items+='<option value="' + kabupatenKota.value + '">' + kabupatenKota.text + '</option>';
            });
            $('#kabupaten-kota').html(items); 
        });
    });
});
