$("#menu-home").addClass("active");

$.getJSON(
  'api/Progress/HomeSummary',
  null,
  function (data) {
    $('#summary-progress').html(data.progress);
    $('#summary-done').html(data.done);
  });

$.getJSON(
  'api/Dashboard/RtrByJenisAsync',
  null,
  function (data) {
    var ctx = $('#rtr-by-jenis');
    var chart = new Chart(ctx, {
      // The type of chart we want to create
      type: 'pie',

      // The data for our dataset
      data: {
        labels: data.label,
        datasets: [{
          label: 'RTR Berdasarkan Jenis',
          backgroundColor: [
            'aqua',
            'brown',
            'chocolate',
            'deeppink',
            'forestgreen',
            'goldenrod'
          ],
          borderColor: 'lightgray',
          data: data.data
        }]
      },

      // Configuration options go here
      options: {}
    });
  });

var tahunUpdated = 2020;

$.getJSON(
  'api/Dashboard/RtrUpdatedAsync',
  { tahun: tahunUpdated },
  function (data) {
    var ctx = $('#rtr-updated');
    var chart = new Chart(ctx, {
      // The type of chart we want to create
      type: 'line',

      // The data for our dataset
      data: {
        labels: data.label,
        datasets: [{
          label: 'Summary RTR Yang Sudah Diupdate',
          backgroundColor: 'hotpink',
          borderColor: 'lightgray',
          data: data.data
        }]
      },

      // Configuration options go here
      options: {}
    });
  });
  
var tahunCreated = 2020;

$.getJSON(
  'api/Dashboard/RtrCreatedAsync',
  { tahun: tahunCreated },
  function (data) {
    var ctx = $('#rtr-created');
    var chart = new Chart(ctx, {
      // The type of chart we want to create
      type: 'line',

      // The data for our dataset
      data: {
        labels: data.label,
        datasets: [{
          label: 'Summary RTR Yang Baru Dibuat',
          backgroundColor: 'khaki',
          borderColor: 'lightgray',
          data: data.data
        }]
      },

      // Configuration options go here
      options: {}
    });
  });
