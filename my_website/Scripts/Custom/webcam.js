function updateWebCamStream(image) {
    var src = 'data:image/png;base64,' + image;
    $('#webcam').attr("src", src);
}