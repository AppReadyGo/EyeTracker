


$(document).ready(function () {
    var step = $('#sample_code');
    var width = step.width();
    var height = step.height();
    var offset = step.offset();
    $('#overlay').css({ width: width, height: height + 3, top: offset.top, left: offset.left });
});