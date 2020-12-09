function UpdateMonth() {
    $('#month-status > li').removeClass('month-link-active');
    var target = '#month-status li:nth-child(' + $('#month-status').attr('aria-month') + ')';
    $(target).addClass('month-link-active');

    var monthNameSource = '#month-status li:nth-child(' + $('#month-status').attr('aria-month') + ') a';
    $('#nama-bulan').text($(monthNameSource).text());
}

function AjaxLinSekPerSub() {
    AjaxLintasSektor(
        $('#StatusYear').val(),
        $('#month-status').attr('aria-month'),
        $('#month-status').attr('aria-isrdtr'));
    AjaxPersetujuanSubstansi(
        $('#StatusYear').val(),
        $('#month-status').attr('aria-month'),
        $('#month-status').attr('aria-isrdtr'));
}

function AjaxLintasSektor(tahunLinSek, bulanLinSek, isRdtrLinSek) {
    $.getJSON('../Ajax/ListLintasSektor',
        { tahun: tahunLinSek, bulan: bulanLinSek, isRdtr: isRdtrLinSek }, function (data) {
            var items = '';
            $('#lintas-sektor .month-data-text ul').empty();

            $.each(data, function (i, data) {
                items += DisplayRtr('#', i + 1, data);
            });

            $('#lintas-sektor .month-data-text ul').html(items);
        });
}

function AjaxPersetujuanSubstansi(tahunPerSub, bulanPerSub, isRdtrPerSub) {
    $.getJSON('../Ajax/ListPersetujuanSubstansi',
        { tahun: tahunPerSub, bulan: bulanPerSub, isRdtr: isRdtrPerSub }, function (data) {
            var items = '';
            $('#persetujuan-substansi .month-data-text ul').empty();

            $.each(data, function (i, data) {
                items += DisplayRtr('#', i + 1, data);
            });

            $('#persetujuan-substansi .month-data-text ul').html(items);
        });
}

function DisplayRtr(link, index, rtr) {
    var linkText = index + '. ' + rtr.displayNamaProvinsiKabupatenKota;
    var rtrInfo = rtr.displayNama + '<br />' + rtr.displayTanggalDokumen;

    return '<li><a href="' + link + '">' + linkText + '</a><p>' + rtrInfo + '</p></li>';
}

$(document).ready(function () {
    UpdateMonth();
    AjaxLinSekPerSub();

    $('#StatusYear').change(function () {
        AjaxLinSekPerSub();
    });

    $('#month-status li a').click(function () {
        $('#month-status').attr('aria-month', $(this).attr('aria-value'));
        AjaxLinSekPerSub();
        UpdateMonth();
    });

    $('#type-rtrw').click(function () {
        $('#type-rtrw').addClass('month-type-active');
        $('#type-rdtr').removeClass('month-type-active');
        $('#month-status').attr('aria-isrdtr', 0);
        AjaxLinSekPerSub();
    });

    $('#type-rdtr').click(function () {
        $('#type-rdtr').addClass('month-type-active');
        $('#type-rtrw').removeClass('month-type-active');
        $('#month-status').attr('aria-isrdtr', 1);
        AjaxLinSekPerSub();
    });
});
