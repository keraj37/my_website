function sendMessageVisit() {
    generalHub.server.logVisit();
}

function updateWebCamStream(image) {
    var src = 'data:image/jpeg;base64,' + image;
    if ($('#webcam'))
        $('#webcam').attr("src", src);
}

$(function () {
    generalHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected request from: <strong>' + ip + '</strong></li>');
    };

    if (typeof triggerAndNewMessageToPage !== 'undefined' && triggerAndNewMessageToPage)
        generalHub.client.addNewMessageToPage = triggerAndNewMessageToPage;

    if (typeof updateWebCamStream !== 'undefined' && updateWebCamStream)
        webCamHub.client.updateWebCamStream = updateWebCamStream;

    //$.connection.hub.logging = true;
    $.connection.hub.start().done(function () {
        sendMessageVisit();
    });
});
