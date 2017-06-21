function sendMessageVisit() {
    generalHub.server.logVisit();
}

function triggerSpreadVisit(ip) {
    $('#visitors').append('<li>Detected visit from: <strong>' + ip + '</strong></li>');
};

$(function () {
    $.connection.hub.start().done(function () {
        sendMessageVisit();
    });
});
