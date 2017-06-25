function updateWebCamStream(image) {
    var src = 'data:image/jpeg;base64,' + image;
    $('#webcam').attr("src", src);
}