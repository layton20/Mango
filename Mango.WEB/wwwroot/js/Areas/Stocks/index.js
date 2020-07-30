$(document).ready(function () {
    //UnassignedStocks script
    $('#tooltipKitchens').tooltip();
    $('#tooltipUnassignedStocks').tooltip();
    $('#tableUnassignedStocks').DataTable();

    // Locations
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
        postModalFormAjax(form);
    });
    $('#modalRoot').on("submit", "#formEditLocation", function (e) {
        e.preventDefault();
        var form = $(this);
        postModalFormAjax(form);
    });

    // Stocks
    $('.createStock').click(function () {
        loadModalAjax($('.createStock').data('url'), null);
    });
    $('.editStock').click(function () {
        loadModalAjax($('.editStock').data('url'), `StockUID=${$(this).data('stockuid')}`);
    });
    $('.deleteStock').click(function () {
        loadModalAjax($('.deleteStock').data('url'), `StockUID=${$(this).data('stockuid')}`);
    });

    $('#modalRoot').on("submit", "#formCreateStock", function (e) {
        e.preventDefault();
        var form = $(this);
        postModalFormAjax(form);
    });
    $('#modalRoot').on("submit", "#formEditStock", function (e) {
        e.preventDefault();
        var form = $(this);
        postModalFormAjax(form);
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

    function postModalFormAjax(form) {
        $.ajax({
            method: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    window.location.href = encodeURI(`/Stock/Stock?SuccessMessage=${response.success}`);
                } else if (response.error) {
                    window.location.href = encodeURI(`/Stock/Stock?SuccessMessage=${response.error}`);
                }
                else {
                    $('#modalDialog').html(response);
                }
            }
        })
    }
});