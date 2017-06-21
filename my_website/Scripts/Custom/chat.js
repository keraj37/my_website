function sendMessageChat() {
    generalHub.server.sendChatMessage($('#displayname').val(), $('#message').val());
    $('#message').val('').focus();
}

function triggerAndNewMessagetopage(name, message) {
    $('#discussion').append('<li><strong>' + htmlEncode(name)
        + '</strong>: ' + htmlEncode(message) + '</li>');
}

$(function () {    
    $('#displayname').val(prompt('Enter your name:', ''));
    $('#message').focus();

    $.connection.hub.logging = true;

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