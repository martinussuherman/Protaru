$("#menu-home").addClass("active");

function UpdateHomeSummaryDaerahNasional(data) {
    const now = new Date();
    const dateOptions = { dateStyle: 'full' };
    const timeOptions = { timeZoneName: 'short' };

    $('#summary-info .text-info').html(
        'Status : ' + now.toLocaleDateString('id-ID', dateOptions) + '/' +
        now.toLocaleTimeString('id-ID', timeOptions));
    // $('#summary-info .text-info').html('Status : ' + data.info);
    $('#summary-info a[progress="nasional"]').html(data.progressNasional);
    $('#summary-info a[done="nasional"]').html(data.doneNasional);
    $('#summary-info a[progress="rtrw"]').html(data.progressRtrw);
    $('#summary-info a[done="rtrw"]').html(data.doneRtrw);
    $('#summary-info a[progress="rdtr"]').html(data.progressRdtr);
    $('#summary-info a[done="rdtr"]').html(data.doneRdtr);
}

function UpdateHomeSummary(data) {
    $('#summary-progress').html(data.progress);
    $('#summary-done').html(data.done);
}


// $(function(){
//   $('#menu-daerah').hover(function() {
//       $(this).addClass('show');
//       $('#menu-daerah .dropdown-menu').addClass('show');
//   },
//   function() {
//       $(this).removeClass('show');
//       $('#menu-daerah .dropdown-menu').removeClass('show');
//   });
// });