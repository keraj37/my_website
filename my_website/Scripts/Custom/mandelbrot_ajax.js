function send(tabPressed, cmd = null, imgNum = "01") {
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
            $('#mb_img' + imgNum).attr('src', 'data:image/png;base64,' + response.image);
            if (imgNum == "01") {
                $('#mb_last_cmd').html();
            }
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

    send(false, "mb width 700 height 700 zoom 1 step 1 k 50 xmin -2 xmax 2 ymin -2 ymax 2", "01");
    send(false, "mb ymin -0.6 ymax -0.5 xmin -0.6 xmax -0.5 k 400", "02");
})

