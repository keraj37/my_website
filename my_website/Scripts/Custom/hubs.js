var generalHub = $.connection.GeneralHub;
var webCamHub = $.connection.WebCamHub;

function updateWebCamStream(image) {
    var src = 'data:image/jpeg;base64,' + image;
    $('#webcam').attr("src", src);
}

function sendMessageVisit() {
    generalHub.server.logVisit();
}

function addNewMessageToPage(name, message) {
    $('#discussion').append('<li><strong>' + htmlEncode(name)
        + '</strong>: ' + htmlEncode(message) + '</li>');
}

function sendMessageChat() {
    generalHub.server.sendChatMessage($('#displayname').val(), $('#message').val());
    $('#message').val('').focus();
}

$(function () {
    $('#message').focus();

    generalHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected request from: <strong>' + ip + '</strong></li>');
    };

    generalHub.client.addNewMessageToPage = addNewMessageToPage;
    webCamHub.client.updateWebCamStreamAll = function (image) {
        $('#visitors').append('<li>Image to <strong>' + 'ALL' + '</strong></li>');
        updateWebCamStream(image);
    };
    webCamHub.client.updateWebCamStreamGroup = function (image) {
        $('#visitors').append('<li>Image to <strong>' + 'GROUP' + '</strong></li>');
        updateWebCamStream(image);
    };
    webCamHub.client.updateWebCamStreamUser = function (image) {
        $('#visitors').append('<li>Image to <strong>' + 'USER' + '</strong></li>');
        updateWebCamStream(image);
    };

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

        sendMessageVisit();
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
