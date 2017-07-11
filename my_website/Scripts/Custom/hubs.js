﻿var generalHub = $.connection.GeneralHub;

function updateWebCamStream(image) {
    var src = 'data:image/jpeg;base64,' + image;
    $('#webcam').attr("src", src);
}

function sendSpreadMessage() {
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

    generalHub.client.spreadMessage = function (msg) {
        $('#visitors').append('<li>New message: <strong>' + msg + '</strong></li>');
    };

    generalHub.client.addNewMessageToPage = addNewMessageToPage;
    generalHub.client.updateWebCamStreamAll = function (image) {
        //$('#visitors').append('<li>Image to <strong>' + 'ALL' + '</strong></li>');
        updateWebCamStream(image);
    };
    generalHub.client.updateWebCamStreamGroup = function (image) {
        //$('#visitors').append('<li>Image to <strong>' + 'GROUP' + '</strong></li>');
        updateWebCamStream(image);
    };
    generalHub.client.updateWebCamStreamUser = function (image) {
        //$('#visitors').append('<li>Image to <strong>' + 'USER' + '</strong></li>');
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

        sendSpreadMessage();
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}