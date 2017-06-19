function sendMessageVisit() {
    generalHub.server.logVisit();
}

$(function () {
    generalHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected visit from: <strong>' + ip + '</strong></li>');
    };
    $.connection.hub.start().done(function () {
        sendMessageVisit();
    });
});
