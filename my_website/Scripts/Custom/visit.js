var visitHub = $.connection.VisitorsHub;

function sendMessageVisit() {
    visitHub.server.logVisit();
}

$(function () {
    visitHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected visit from: <strong>' + ip + '</strong></li>');
    };
    $.connection.hub.start().done(function () {
        sendMessageVisit();
    });
});
