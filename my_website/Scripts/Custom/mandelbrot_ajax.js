﻿function send(tabPressed, cmd = null, imgNum = "01") {
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
            if (response.image != null) $('#mb_img' + imgNum).attr('src', 'data:image/png;base64,' + response.image);
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
            setCookie("__lastcmd", $("#console-input").val(), 100);
            send(false);
        }
        else if (keyCode == 9) {
            send(true);
        }
        else if (keyCode == 38) {
            var cmd = getCookie("__lastcmd");
            if (cmd != "") {
                $('#console-input').val(cmd);
            } 
        }
    });    
})
