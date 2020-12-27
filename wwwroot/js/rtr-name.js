$(document).ready(function () {
    $('#provinsi').change(function () {
        var selectedProvinsi = $(this).find("option:selected").text();
        $('#rtr-nama').val("RTRW Provinsi " + selectedProvinsi);
    });
    $('#kabupaten-kota').change(function () {
        var selectedProvinsi = $('#provinsi').find("option:selected").text();
        var selectedKabKota = $(this).find("option:selected").text();
        $('#rtr-nama').val("RTRW Provinsi " + selectedProvinsi + " " + selectedKabKota);
    });
});
