
function showOverlay() {
    var step = $('#sample_code');
    var width = step.width();
    var height = step.height();
    var offset = step.offset();
    $('#overlay').css({ width: width, height: height + 3, top: offset.top, left: offset.left });
}

$(document).ready(function () {
    if (appId == 0) {
        showOverlay();
    } else {
        $('#overlay').hide();
        $('#create_lnk').hide();
    }
    $('#create_lnk').click(function () {
        var desc = $('#Description').val();
        var type = $('#Type').val();
        if (desc.length == 0) {
            $('#Description').addClass('input-validation-error');
            $('#app_config').prepend('<div class="validation-summary-errors"><span>Account creation was unsuccessful. Please correct the errors and try again.</span></div>');
            $('#description_error').empty().append('<span class="field-validation-error">The Description field is required.</span>');
            showOverlay();
        }
        else {
            $('#Description').removeClass('input-validation-error');
            $('.validation-summary-errors').remove();
            $('#description_error').empty();
            showOverlay();
            $.ajax({
                url: newAppURL,
                dataType: 'json',
                type: 'POST',
                data: { "Description": desc, "Type": type },
                success: function (json) {
                    if (json.HasError) {
                        $('#app_config').prepend('<div class="validation-summary-errors"><span>Account creation was unsuccessful. Please contact to administrator.</span></div>');
                    } else {
                        $('#overlay').hide();
                        $('#create_lnk').hide();
                        $('#code').html($('#code').html().replace('**-******-***', json.Value));
                        $('#property').text($('#property').text().replace('**-******-***', json.Value));
                    }
                }
            });
        }
    });
    $('#done_lnk').click(function () {
        var desc = $('#Description').val();
        if (desc.length == 0) {
            $('#Description').addClass('input-validation-error');
            $('#app_config').prepend('<div class="validation-summary-errors"><span>Account creation was unsuccessful. Please correct the errors and try again.</span></div>');
            $('#description_error').empty().append('<span class="field-validation-error">The Description field is required.</span>');
        } else {
            $('#application_form')[0].submit();
        }
    });
    //Disable not exsists types
    $('#Type option[value!=3]').attr('disabled', true);
    $('#Type option[value=1]').attr('disabled', null);
    $('#Type option[value=3]').attr('selected', true);
    //--
});