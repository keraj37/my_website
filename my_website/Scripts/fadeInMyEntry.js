$(document).ready(function () {
    $('.myentry').each(function (index, elem) {
        $(this).hide().fadeIn(500 + Math.random() * 2000);
    })
});