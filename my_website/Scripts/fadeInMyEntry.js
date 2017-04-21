$(document).ready(function () {
    $('.myentry').each(function (index, elem) {
        $(this).hide().fadeIn(500 + Math.random() * 4000);
        /*
        $(this).animate({
            border: 10
        }, 5000, function () {
            // Animation complete.
        });
        */
    })
});