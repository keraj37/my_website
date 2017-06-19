﻿function sendMessageChat() {
    generalHub.server.sendChatMessage($('#displayname').val(), $('#message').val());
    $('#message').val('').focus();
}

$(function () {
    generalHub.client.addNewMessageToPage = function (name, message) {
        $('#discussion').append('<li><strong>' + htmlEncode(name)
            + '</strong>: ' + htmlEncode(message) + '</li>');
    };
    $('#displayname').val(prompt('Enter your name:', ''));
    $('#message').focus();
    $.connection.hub.start().done(function () {
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
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}