function sendMessageVisit() {
    generalHub.server.logVisit();
}

connectionReadyListners.push(function () {
    sendMessageVisit();
});

documentReadyListners.push(function () {
    generalHub.client.spreadVisit = function (ip) {
        $('#visitors').append('<li>Detected visit from: <strong>' + ip + '</strong></li>');
    };
});