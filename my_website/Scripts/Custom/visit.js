var visitHub = $.connection.VisitorsHub;

function sendMessage() {
    visitHub.server.logVisit();
}

$(function () {
    visitHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected visit from: <strong>' + ip + '</strong></li>');
    };
    $.connection.hub.start().done(function () {
        sendMessage();
    });
});
