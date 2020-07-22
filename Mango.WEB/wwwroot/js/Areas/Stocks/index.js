$(document).ready(function () {
    $('.editLocation').tooltip({
        delay: 250
    });

    $('.createLocation').click(function () {
        var url = $('.createLocation').data('url');

        $.get(url, function (data) {
            if (data.message) {
                window.location.href = encodeURI(`/Stock/Stock?ErrorMessage=${data.message}`);
            } else {
                $('#modalDialog').html(data);
                $('#modalRoot').modal('show');

                if ($('#modalRoot').is(':visible')) {
                    var form = jQuery('form', $modal).first();
                    jQuery.validator.unobtrusive.parse(form);
                }
            }
        });
    });

    $('#modalRoot').on("submit", "#formCreateLocation", function (e) {
        e.preventDefault();
        var form = $(this);

        $.ajax({
            method: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (response) {
                if (response == 0) {
                    window.location.href = encodeURI(`/Stock/Stock?SuccessMessage='Successfully added Kitchen'`);
                } else {
                    $('#modalDialog').html(response);
                }
            }
        })
    });
});