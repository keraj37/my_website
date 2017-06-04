function send(tabPressed, cmd = null) {
    url = "/Projects/MandelbrotSet";
    if (tabPressed)
        url += "?tab=true";
    $.ajax({
        type: "POST",
        url: url,
        data: '{cmd: "' + (cmd == null ? $("#console-input").val() : cmd) + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (!tabPressed) $('#console-input').val("");
            if (response.fillInput != null) {
                $('#console-input').val(response.fillInput);
            }
            $('#console-input').focus();
            $('#mb_img').attr('src', 'data:image/png;base64,' + response.image);
        },
        failure: function (response) {
            alert("Sorry, something went wrong");
        },
        error: function (response) {
            alert("Sorry, something went wrong");
        },
        beforeSend: function () {
            $('#loadingimage').css("display", "block");
        },
        complete: function () {
            $('#loadingimage').css("display", "none");
        },
    });
}

$(document).ready(function () {
    $('#execute').click(send);
    $('#console-input').keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode == 13) {
            send(false);
        }
        else if (keyCode == 9) {
            send(true);
        }
    });

    send(false, "mb zoom 1 step 1 k 50");
})

