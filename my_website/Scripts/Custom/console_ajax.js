function send() {
    $.ajax({
        type: "POST",
        url: "/Projects/Console",
        data: '{cmd: "' + $("#console-input").val() + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#console-output").text(response.content);

            if (response.redirectToAction != null) {
                t1 = window.setTimeout(function () { window.location = "/Projects/" + response.redirectToAction; }, 1000);
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
            $('#console-input').val("");
            $('#console-input').focus();
            $('#loadingimage').css("display", "none");
        },
    });
}

$(document).ready(function () {
    $('#execute').click(send);
    $('#console-input').keypress(function (e) {
        if (e.which == 13) {
            send();
        }
    });
})
    
