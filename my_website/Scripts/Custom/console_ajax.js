﻿function send(tabPressed) {
    consoleUrl = "/Projects/Console";
    if (tabPressed)
        consoleUrl += "?tab=true";
    $.ajax({
        type: "POST",
        url: consoleUrl,
        data: '{cmd: "' + $("#console-input").val() + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var consoleOutput = $("#console-output").text() + response.content;
            $("#console-output").text(consoleOutput);
            if (response.redirectToAction != null) {
                t1 = window.setTimeout(function () { window.location = "/Projects/" + response.redirectToAction; }, 1000);
            }
            if (!tabPressed) $('#console-input').val("");
            if (response.fillInput != null) {
                $('#console-input').val(response.fillInput);
            }
            $('#console-input').focus();
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
            $("#console-output").animate({ scrollTop: $(document).height() }, "slow");
        },
    });
}

$(document).ready(function () {
    $("#console-output").animate({ scrollTop: $(document).height() }, "slow");
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

