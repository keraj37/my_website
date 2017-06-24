function sendMessageVisit() {
    generalHub.server.logVisit();
}

$(function () {
    generalHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected request from: <strong>' + ip + '</strong></li>');
    };

    if (typeof triggerAndNewMessageToPage !== 'undefined' && triggerAndNewMessageToPage)
        generalHub.client.addNewMessageToPage = triggerAndNewMessageToPage;

    if (typeof updateWebCamStream !== 'undefined' && updateWebCamStream)
        generalHub.client.updateWebCamStream = updateWebCamStream;

    //$.connection.hub.logging = true;
    $.connection.hub.start().done(function () {
        sendMessageVisit();
    });
});
