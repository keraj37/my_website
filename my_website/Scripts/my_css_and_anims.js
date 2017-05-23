$(document).ready(function () {
    $('.myentry').each(function (index, elem)
    {
        if ($(this).hasClass("randomsize"))
        {
            $(this).css("font-size", function ()
            {
                return Math.ceil(Math.random() * 19 + 13) + "px";
            });
        }
    })
    
    /*
    $('.myentry-multiple').each(function (index, elem) {
        $(this).hide().fadeIn(500 + Math.random() * 2000);
        $(this).css('background-color', getRandomColor('rgba'));
    })
    */
});

/*
function getRandomColor(format) {
    var rint = Math.floor(0x100000000 * Math.random());
    switch (format) {
        case 'hex':
            return '#' + ('00000' + rint.toString(16)).slice(-6).toUpperCase();
        case 'hexa':
            return '#' + ('0000000' + rint.toString(16)).slice(-8).toUpperCase();
        case 'rgb':
            return 'rgb(' + (rint & 255) + ',' + (rint >> 8 & 255) + ',' + (rint >> 16 & 255) + ')';
        case 'rgba':
            return 'rgba(' + (rint & 255) + ',' + (rint >> 8 & 255) + ',' + (rint >> 16 & 255) + ',' + Math.min(0.9, Math.random() + 0.4) + ')';
        default:
            return rint;
    }
}
*/
