


$(document).ready(function () {
    var step = $('#sample_code');
    var width = step.width();
    var height = step.height();
    var offset = step.offset();
    $('#overlay').css({ width: width, height: height + 3, top: offset.top, left: offset.left });

    $('#create_lnk').click(function () {
        var desc = $('#Description').val();
        var type = $('#Type').val();
        $.ajax({
            url: newAppURL,
            dataType: 'json',
            type: 'POST',
            data: { "Description": desc, "Type": type },
            success: function (json) {
            }
        });
    });
    //Disable not exsists types
    $('#Type option[value!=3]').attr('disabled', true);
    $('#Type option[value=3]').attr('selected', true);
    //--
});