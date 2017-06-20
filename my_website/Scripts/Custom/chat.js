function sendMessageChat() {
    generalHub.server.sendChatMessage($('#displayname').val(), $('#message').val());
    $('#message').val('').focus();
}

connectionReadyListners.push(function () {
    $('#sendmessage').click(function () {
        sendMessageChat();
    });

    $('#message').keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode == 13) {
            sendMessageChat();
        }
    });
});

documentReadyListners.push(function () {
    generalHub.client.addNewMessageToPage = function (name, message) {
        $('#discussion').append('<li><strong>' + htmlEncode(name)
            + '</strong>: ' + htmlEncode(message) + '</li>');
    };
    $('#displayname').val(prompt('Enter your name:', ''));
    $('#message').focus();
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}