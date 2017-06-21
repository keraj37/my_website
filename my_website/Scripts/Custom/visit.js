function sendMessageVisit() {
    generalHub.server.logVisit();
}

$(function () {
    generalHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected visit from: <strong>' + ip + '</strong></li>');
    };

    if (typeof triggerAndNewMessageToPage !== 'undefined' && triggerAndNewMessageToPage)
        generalHub.client.addNewMessageToPage = triggerAndNewMessageToPage;

    //$.connection.hub.logging = true;
    $.connection.hub.start().done(function () {
        sendMessageVisit();
    });
});
