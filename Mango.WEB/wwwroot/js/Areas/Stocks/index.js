$(document).ready(function () {
    $('.editLocation').tooltip({ delay: 250 });
    $('.deleteLocation').tooltip({ delay: 250 });

    $('.createLocation').click(function () {
        loadModalAjax($('.createLocation').data('url'), null);
    });

    $('.editLocation').click(function () {
        loadModalAjax($('.editLocation').data('url'), `LocationUID=${$(this).data('locationuid')}`);
    });

    $('.deleteLocation').click(function () {
        loadModalAjax($('.deleteLocation').data('url'), `LocationUID=${$(this).data('locationuid')}`);
    });

    $('#modalRoot').on("submit", "#formCreateLocation", function (e) {
        e.preventDefault();
        var form = $(this);
        postModalFormAjax(form, "Successfully added Kitchen");
    });

    $('#modalRoot').on("submit", "#formEditLocation", function (e) {
        e.preventDefault();
        var form = $(this);
        postModalFormAjax(form, "Successfully edited Kitchen");
    });

    function loadModalAjax(url, queryString) {
        if (queryString) {
            url += `?${queryString}`;
        }

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
    };

    function postModalFormAjax(form, successMessage) {
        $.ajax({
            method: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (response) {
                if (response == 0) {
                    window.location.href = encodeURI(`/Stock/Stock?SuccessMessage=${successMessage ? successMessage : 'Success'}`);
                } else {
                    $('#modalDialog').html(response);
                }
            }
        })
    }
});