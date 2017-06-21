var generalHub = $.connection.GeneralHub;

$(function () {
    if (typeof triggerAndNewMessageToPage !== 'undefined' && triggerAndNewMessageToPage)
        generalHub.client.addNewMessageToPage = triggerAndNewMessageToPage;

    if (typeof triggerSpreadVisit !== 'undefined' && triggerSpreadVisit)
        generalHub.client.spreadVisit = triggerSpreadVisit;
});
