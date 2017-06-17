var wasZoomIn = false;

function send(tabPressed, cmd = null) {
    wasZoomIn = false;
    url = "/Projects/MandelbrotSet";
    if (tabPressed)
        url += "?tab=true";
    $.ajax({
        type: "POST",
        url: url,
        data: '{cmd: "' + (cmd == null ? $("#console-input").val() : cmd) + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (!tabPressed && !wasZoomIn) $('#console-input').val("");
            if (response.fillInput != null) {
                $('#console-input').val(response.fillInput);
            }
            $('#console-input').focus();
            if (response.image != null) {
                initImage('data:image/png;base64,' + response.image);
            }

            lastX = response.lastX;
            lastY = response.lastY;
            lastWidth = response.lastWidth;
        },
        failure: function (response) {
            alert("Sorry, something went wrong");
        },
        error: function (response) {
            alert("Sorry, something went wrong");
        },
        beforeSend: function () {
            $('#loadingimage').css("display", "block");
        },
        complete: function () {
            $('#loadingimage').css("display", "none");
        },
    });
}

$(document).ready(function () {
    $('#execute').click(send);
    $('#console-input').keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode == 13) {
            setCookie("__lastcmd", $("#console-input").val(), 100);
            send(false);
        }
        else if (keyCode == 9) {
            send(true);
        }
        else if (keyCode == 38) {
            var cmd = getCookie("__lastcmd");
            if (cmd != "") {
                $('#console-input').val(cmd);
            } 
        }
    });    
})

var lastPixelWidth = 700;

var lastX = -2;
var lastY = -2;
var lastWidth = 4;

var canvas = document.getElementById('mb_canvas');
var ctx = canvas.getContext('2d');
var rect = {};
var drag = false;
var imageObj = null;

function initImage(src) {
    imageObj = new Image();
    imageObj.onload = function () { ctx.drawImage(imageObj, 0, 0); };
    imageObj.src = src;
}

function init() {
    canvas.addEventListener('mousedown', mouseDown, false);
    canvas.addEventListener('mouseup', mouseUp, false);
    canvas.addEventListener('mousemove', mouseMove, false);
}

function mouseDown(e) {
    rect.startX = e.pageX - this.offsetLeft;
    rect.startY = e.pageY - this.offsetTop;
    drag = true;
}

function mouseUp() { drag = false; procesRectangle()}

function mouseMove(e) {
    if (drag) {
        ctx.clearRect(0, 0, lastPixelWidth, lastPixelWidth);
        ctx.drawImage(imageObj, 0, 0);
        rect.w = (e.pageX - this.offsetLeft) - rect.startX;
        rect.h = (e.pageY - this.offsetTop) - rect.startY;

        if (rect.w > rect.h) rect.h = rect.w;
        else if (rect.w < rect.h) rect.w = rect.h;

        ctx.strokeStyle = 'white';
        ctx.strokeRect(rect.startX, rect.startY, rect.w, rect.h);
    }
}

function procesRectangle() {
    var lastWidthToPixels = lastWidth / lastPixelWidth;
    var currentWidthToPixels = rect.w * lastWidthToPixels;
    var currentXToPixels = rect.startX * lastWidthToPixels;
    var currentYToPixels = (lastPixelWidth - rect.startY) * lastWidthToPixels;
    var currentXMin = lastX + currentXToPixels;
    var currentYMax = lastY + currentYToPixels;
    var currentXMax = currentXMin + currentWidthToPixels;
    var currentYMin = currentYMax - currentWidthToPixels;

    var k = parseInt($("#k-input").val());
    var kString = '';
    if (k != "NaN")
        kString = " k " + k;
    var fullCommand = "mb power 2 power2 0.7 xmin " + currentXMin + " xmax " + currentXMax + " ymin " + currentYMin + " ymax " + currentYMax + " light 0.6" + kString;
    send(false, fullCommand);
    wasZoomIn = true;

    $('#console-input').val(fullCommand);
}

init();