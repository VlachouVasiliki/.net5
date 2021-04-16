// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.js-checkout-balance').on('click',
    (event) => {
        debugger;
        event.preventDefault();
        let cardNumber = $('.js-card-number').val();
        let expirationMonth = $('.js-expiration-month').val();
        let expirationYear = $('.js-expiration-year').val();
        let amount = $('.js-amount').val();

        console.log(`${cardNumber}`);

        let data = JSON.stringify({
            cardNumber: cardNumber,
            expirationMonth: expirationMonth,
            expirationYear: expirationYear,
            amount: amount
        });

        // ajax call
        let result = $.ajax({
            url: `/card/checkout`,
            method: 'POST',
            contentType: 'application/json',
            data: data
        }).done(response => {
            console.log('Update was successful');

            $('.search-form').hide();
            $('.alert-success').toggleClass('d-none');

        }).fail(failure => {
            // fail
            console.log('Update failed');
            $('.alert-danger').toggleClass('d-none');
        });

    });
