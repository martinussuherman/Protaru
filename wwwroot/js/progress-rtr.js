function UpdateProgressRtrwnT51(data) {
    $('#progress-rtrwn-t51 .row:eq(0) .progress-score').html(data.penyusunanMateriTeknis);
    $('#progress-rtrwn-t51 .row:eq(0) .progress-bar').attr(
        'style',
        'width: ' + data.penyusunanMateriTeknis / data.total * 100 + '%;');

    $('#progress-rtrwn-t51 .row:eq(1) .progress-score').html(data.penyepakatanTpak);
    $('#progress-rtrwn-t51 .row:eq(1) .progress-bar').attr(
        'style',
        'width: ' + data.penyepakatanTpak / data.total * 100 + '%;');

    $('#progress-rtrwn-t51 .row:eq(2) .progress-score').html(data.harmonisasiKemenkumham);
    $('#progress-rtrwn-t51 .row:eq(2) .progress-bar').attr(
        'style',
        'width: ' + data.harmonisasiKemenkumham / data.total * 100 + '%;');

    $('#progress-rtrwn-t51 .row:eq(3) .progress-score').html(data.pembahasanSekretariat);
    $('#progress-rtrwn-t51 .row:eq(3) .progress-bar').attr(
        'style',
        'width: ' + data.pembahasanSekretariat / data.total * 100 + '%;');

    $('#progress-rtrwn-t51 .row:eq(4) .progress-score').html(data.penetapanPresiden);
    $('#progress-rtrwn-t51 .row:eq(4) .progress-bar').attr(
        'style',
        'width: ' + data.penetapanPresiden / data.total * 100 + '%;');
}

function UpdateProgressRtrwnT52(data) {
    $('#progress-rtrwn-t52 .row:eq(0) .progress-score').html(data.kajianPk);
    $('#progress-rtrwn-t52 .row:eq(0) .progress-bar').attr(
        'style',
        'width: ' + data.kajianPk / data.total * 100 + '%;');

    $('#progress-rtrwn-t52 .row:eq(1) .progress-score').html(data.penyusunanPk);
    $('#progress-rtrwn-t52 .row:eq(1) .progress-bar').attr(
        'style',
        'width: ' + data.penyusunanPk / data.total * 100 + '%;');

    $('#progress-rtrwn-t52 .row:eq(2) .progress-score').html(data.penyusunanMateriTeknis);
    $('#progress-rtrwn-t52 .row:eq(2) .progress-bar').attr(
        'style',
        'width: ' + data.penyusunanMateriTeknisRtrwnT52 / data.total * 100 + '%;');

    $('#progress-rtrwn-t52 .row:eq(3) .progress-score').html(data.penyepakatanTpak);
    $('#progress-rtrwn-t52 .row:eq(3) .progress-bar').attr(
        'style',
        'width: ' + data.penyepakatanTpakRtrwnT52 / data.total * 100 + '%;');

    $('#progress-rtrwn-t52 .row:eq(4) .progress-score').html(data.harmonisasiKemenkumham);
    $('#progress-rtrwn-t52 .row:eq(4) .progress-bar').attr(
        'style',
        'width: ' + data.harmonisasiKemenkumhamRtrwnT52 / data.total * 100 + '%;');

    $('#progress-rtrwn-t52 .row:eq(5) .progress-score').html(data.pembahasanSekretariat);
    $('#progress-rtrwn-t52 .row:eq(5) .progress-bar').attr(
        'style',
        'width: ' + data.pembahasanSekretariatRtrwnT52 / data.total * 100 + '%;');

    $('#progress-rtrwn-t52 .row:eq(6) .progress-score').html(data.penetapanPresiden);
    $('#progress-rtrwn-t52 .row:eq(6) .progress-bar').attr(
        'style',
        'width: ' + data.penetapanPresidenRtrwnT52 / data.total * 100 + '%;');
}

function UpdateProgressPulauKsnKpnT51(data) {
    $('#progress-pulau-t51 .row:eq(0) .progress-score').html(data.penyusunanMateriTeknis);
    $('#progress-pulau-t51 .row:eq(0) .progress-bar').attr(
        'style',
        'width: ' + data.penyusunanMateriTeknis / data.total * 100 + '%;');

    $('#progress-pulau-t51 .row:eq(1) .progress-score').html(data.penyepakatanDaerah);
    $('#progress-pulau-t51 .row:eq(1) .progress-bar').attr(
        'style',
        'width: ' + data.penyepakatanDaerah / data.total * 100 + '%;');

    $('#progress-pulau-t51 .row:eq(2) .progress-score').html(data.penyepakatanTpak);
    $('#progress-pulau-t51 .row:eq(2) .progress-bar').attr(
        'style',
        'width: ' + data.penyepakatanTpak / data.total * 100 + '%;');

    $('#progress-pulau-t51 .row:eq(3) .progress-score').html(data.harmonisasiKemenkumham);
    $('#progress-pulau-t51 .row:eq(3) .progress-bar').attr(
        'style',
        'width: ' + data.harmonisasiKemenkumham / data.total * 100 + '%;');

    $('#progress-pulau-t51 .row:eq(4) .progress-score').html(data.pembahasanSekretariat);
    $('#progress-pulau-t51 .row:eq(4) .progress-bar').attr(
        'style',
        'width: ' + data.pembahasanSekretariat / data.total * 100 + '%;');

    $('#progress-pulau-t51 .row:eq(5) .progress-score').html(data.penetapanPresiden);
    $('#progress-pulau-t51 .row:eq(5) .progress-bar').attr(
        'style',
        'width: ' + data.penetapanPresiden / data.total * 100 + '%;');
}

function UpdateProgressPulauKsnKpnT52(data) {
    $('#progress-pulau-t52 .row:eq(0) .progress-score').html(data.kajianPk);
    $('#progress-pulau-t52 .row:eq(0) .progress-bar').attr(
        'style',
        'width: ' + data.kajianPk / data.total * 100 + '%;');

    $('#progress-pulau-t52 .row:eq(1) .progress-score').html(data.penyusunanPk);
    $('#progress-pulau-t52 .row:eq(1) .progress-bar').attr(
        'style',
        'width: ' + data.penyusunanPk / data.total * 100 + '%;');

    $('#progress-pulau-t52 .row:eq(2) .progress-score').html(data.penyusunanMateriTeknisPulauT52);
    $('#progress-pulau-t52 .row:eq(2) .progress-bar').attr(
        'style',
        'width: ' + data.penyusunanMateriTeknisPulauT52 / data.total * 100 + '%;');

    $('#progress-pulau-t52 .row:eq(3) .progress-score').html(data.penyepakatanDaerahPulauT52);
    $('#progress-pulau-t52 .row:eq(3) .progress-bar').attr(
        'style',
        'width: ' + data.penyepakatanDaerahPulauT52 / data.total * 100 + '%;');

    $('#progress-pulau-t52 .row:eq(4) .progress-score').html(data.penyepakatanTpakPulauT52);
    $('#progress-pulau-t52 .row:eq(4) .progress-bar').attr(
        'style',
        'width: ' + data.penyepakatanTpakPulauT52 / data.total * 100 + '%;');

    $('#progress-pulau-t52 .row:eq(5) .progress-score').html(data.harmonisasiKemenkumhamPulauT52);
    $('#progress-pulau-t52 .row:eq(5) .progress-bar').attr(
        'style',
        'width: ' + data.harmonisasiKemenkumhamPulauT52 / data.total * 100 + '%;');

    $('#progress-pulau-t52 .row:eq(6) .progress-score').html(data.pembahasanSekretariatPulauT52);
    $('#progress-pulau-t52 .row:eq(6) .progress-bar').attr(
        'style',
        'width: ' + data.pembahasanSekretariatPulauT52 / data.total * 100 + '%;');

    $('#progress-pulau-t52 .row:eq(7) .progress-score').html(data.penetapanPresidenPulauT52);
    $('#progress-pulau-t52 .row:eq(7) .progress-bar').attr(
        'style',
        'width: ' + data.penetapanPresidenPulauT52 / data.total * 100 + '%;');
}

function UpdateProgressT51(data) {
    $('#progress-t51 .row:eq(0) .progress-score').html(data.penyusunan);
    $('#progress-t51 .row:eq(0) .progress-bar').attr(
        'style',
        'width: ' + data.penyusunan / data.total * 100 + '%;');
    $('#progress-t51 .row:eq(1) .progress-score').html(data.rekomendasiGubernur);
    $('#progress-t51 .row:eq(1) .progress-bar').attr(
        'style',
        'width: ' + data.rekomendasiGubernur / data.total * 100 + '%;');
    $('#progress-t51 .row:eq(2) .progress-score').html(data.persetujuanSubstansi);
    $('#progress-t51 .row:eq(2) .progress-bar').attr(
        'style',
        'width: ' + data.persetujuanSubstansi / data.total * 100 + '%;');
    $('#progress-t51 .row:eq(3) .progress-score').html(data.perda);
    $('#progress-t51 .row:eq(3) .progress-bar').attr(
        'style',
        'width: ' + data.perda / data.total * 100 + '%;');
}

function UpdateProgressT52(data) {
    $('#progress-t52 .row:eq(0) .progress-score').html(data.prosesPK);
    $('#progress-t52 .row:eq(0) .progress-bar').attr(
        'style',
        'width: ' + data.prosesPK / data.total * 100 + '%;');
    $('#progress-t52 .row:eq(1) .progress-score').html(data.revisi);
    $('#progress-t52 .row:eq(1) .progress-bar').attr(
        'style',
        'width: ' + data.revisi / data.total * 100 + '%;');
    $('#progress-t52 .row:eq(2) .progress-score').html(data.rekomendasiGubernur);
    $('#progress-t52 .row:eq(2) .progress-bar').attr(
        'style',
        'width: ' + data.rekomendasiGubernur / data.total * 100 + '%;');
    $('#progress-t52 .row:eq(3) .progress-score').html(data.persetujuanSubstansi);
    $('#progress-t52 .row:eq(3) .progress-bar').attr(
        'style',
        'width: ' + data.persetujuanSubstansi / data.total * 100 + '%;');
    $('#progress-t52 .row:eq(4) .progress-score').html(data.perda);
    $('#progress-t52 .row:eq(4) .progress-bar').attr(
        'style',
        'width: ' + data.perda / data.total * 100 + '%;');
}

$(document).ready(function () {
    $('#rtrwn-t51-progress').click(function () {
        $.getJSON(
            'api/Progress/RtrwnT51',
            {},
            function (data) {
                UpdateProgressRtrwnT51(data);
            });

        $('#progress-rtrwn-t51 .col-sm-2 a img').unwrap();
        $('#progress-rtrwn-t51 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrwnT51/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrwn-t52-progress').click(function () {
        $.getJSON(
            'api/Progress/RtrwnT52',
            {},
            function (data) {
                UpdateProgressRtrwnT52(data);
            });

        $('#progress-rtrwn-t52 .col-sm-2 a img').unwrap();
        $('#progress-rtrwn-t52 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrwnT52/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrpulau-t51-progress').click(function () {
        $.getJSON('Ajax/ProgressPulauKsnKpnT51',
            { jenisRtr: 6 }, function (data) {
                UpdateProgressPulauKsnKpnT51(data);
            });

        $('#rtrpulau-t51-progress').addClass('active-progress');
        $('#rtrksn-t51-progress').removeClass('active-progress');
        $('#rtrkpn-t51-progress').removeClass('active-progress');
        $('#progress-pulau-t51 .col-sm-2 a img').unwrap();
        $('#progress-pulau-t51 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrPulauT51/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrksn-t51-progress').click(function () {
        $.getJSON('Ajax/ProgressPulauKsnKpnT51',
            { jenisRtr: 8 }, function (data) {
                UpdateProgressPulauKsnKpnT51(data);
            });

        $('#rtrksn-t51-progress').addClass('active-progress');
        $('#rtrpulau-t51-progress').removeClass('active-progress');
        $('#rtrkpn-t51-progress').removeClass('active-progress');
        $('#progress-pulau-t51 .col-sm-2 a img').unwrap();
        $('#progress-pulau-t51 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrKsnT51/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrkpn-t51-progress').click(function () {
        $.getJSON('Ajax/ProgressPulauKsnKpnT51',
            { jenisRtr: 12 }, function (data) {
                UpdateProgressPulauKsnKpnT51(data);
            });

        $('#rtrkpn-t51-progress').addClass('active-progress');
        $('#rtrpulau-t51-progress').removeClass('active-progress');
        $('#rtrksn-t51-progress').removeClass('active-progress');
        $('#progress-pulau-t51 .col-sm-2 a img').unwrap();
        $('#progress-pulau-t51 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrKpnT51/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrpulau-t52-progress').click(function () {
        $.getJSON('Ajax/ProgressPulauKsnKpnT52',
            { jenisRtr: 7 }, function (data) {
                UpdateProgressPulauKsnKpnT52(data);
            });

        $('#rtrpulau-t52-progress').addClass('active-progress');
        $('#rtrksn-t52-progress').removeClass('active-progress');
        $('#rtrkpn-t52-progress').removeClass('active-progress');
        $('#progress-pulau-t52 .col-sm-2 a img').unwrap();
        $('#progress-pulau-t52 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrPulauT52/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrksn-t52-progress').click(function () {
        $.getJSON('Ajax/ProgressPulauKsnKpnT52',
            { jenisRtr: 9 }, function (data) {
                UpdateProgressPulauKsnKpnT52(data);
            });

        $('#rtrksn-t52-progress').addClass('active-progress');
        $('#rtrpulau-t52-progress').removeClass('active-progress');
        $('#rtrkpn-t52-progress').removeClass('active-progress');
        $('#progress-pulau-t52 .col-sm-2 a img').unwrap();
        $('#progress-pulau-t52 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrKsnT52/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrkpn-t52-progress').click(function () {
        $.getJSON('Ajax/ProgressPulauKsnKpnT52',
            { jenisRtr: 13 }, function (data) {
                UpdateProgressPulauKsnKpnT52(data);
            });

        $('#rtrkpn-t52-progress').addClass('active-progress');
        $('#rtrpulau-t52-progress').removeClass('active-progress');
        $('#rtrksn-t52-progress').removeClass('active-progress');
        $('#progress-pulau-t52 .col-sm-2 a img').unwrap();
        $('#progress-pulau-t52 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrKpnT52/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rdtr-t51-progress').click(function () {
        $.getJSON(
            'api/Progress/T51',
            { jenisRtr: 1 },
            function (data) {
                UpdateProgressT51(data);
            });

        $('#rdtr-t51-progress').addClass('btn-danger text-white').removeClass('btn-outline-danger');
        $('#rtrw-t51-progress').addClass('btn-outline-success').removeClass('btn-success text-white');
        $('#progress-t51 .col-sm-2 a img').unwrap();
        $('#progress-t51 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RdtrT51/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrw-t51-progress').click(function () {
        $.getJSON(
            'api/Progress/T51',
            { jenisRtr: 3 },
            function (data) {
                UpdateProgressT51(data);
            });

        $('#rdtr-t51-progress').addClass('btn-outline-danger').removeClass('btn-danger text-white');
        $('#rtrw-t51-progress').addClass('btn-success text-white').removeClass('btn-outline-success');
        $('#progress-t51 .col-sm-2 a img').unwrap();
        $('#progress-t51 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrwT51/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rdtr-t52-progress').click(function () {
        $.getJSON(
            'api/Progress/T52',
            { jenisRtr: 2 },
            function (data) {
                UpdateProgressT52(data);
            });

        $('#rdtr-t52-progress').addClass('btn-danger text-white').removeClass('btn-outline-danger');
        $('#rtrw-t52-progress').addClass('btn-outline-success').removeClass('btn-success text-white');
        $('#progress-t52 .col-sm-2 a img').unwrap();
        $('#progress-t52 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RdtrT52/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrw-t52-progress').click(function () {
        $.getJSON(
            'api/Progress/T52',
            { jenisRtr: 4 },
            function (data) {
                UpdateProgressT52(data);
            });

        $('#rdtr-t52-progress').addClass('btn-outline-danger').removeClass('btn-danger text-white');
        $('#rtrw-t52-progress').addClass('btn-success text-white').removeClass('btn-outline-success');
        $('#progress-t52 .col-sm-2 a img').unwrap();
        $('#progress-t52 .col-sm-2').each(function (index) {
            var stage = index + 1;
            $(this).wrapInner('<a href="RtrwT52/SearchResult/ByProgress?stage=' + stage + '"></a>');
        });
    });

    $('#rtrwn-t51-progress').trigger("click");
    $('#rtrwn-t52-progress').trigger("click");

    $('#rtrpulau-t51-progress').trigger("click");
    $('#rtrpulau-t52-progress').trigger("click");

    $('#rtrw-t51-progress').trigger("click");
    $('#rtrw-t52-progress').trigger("click");
});
