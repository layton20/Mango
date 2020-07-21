$(document).ready(function () {
    $('#createLocation').click(function () {
        var url = $('#createLocation').data('url');

        $.get(url, function (data) {
            $('#modalDialog').html(data);
            $('#modalRoot').modal('show');
        });
    });
});