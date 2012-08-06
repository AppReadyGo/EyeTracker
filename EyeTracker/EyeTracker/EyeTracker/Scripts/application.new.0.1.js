
function showOverlay(elem) {
    var width = elem.outerWidth() + 5;
    var height = elem.outerHeight();
    var offset = elem.offset();
    $('#overlay').css({ width: width, height: height, top: offset.top, left: offset.left });
    $('#overlay').show();
}

function centerElement(parent,elem) {
    var offset = elem.offset();
    var l = offset.left + (parent.outerWidth() / 2) - (elem.outerWidth() / 2);
    var t = offset.top + (parent.outerHeight() / 2) - (elem.outerHeight() / 2);
    elem.css({ top: t, left: l });
}

function switchType(type) {
    $('#screens').hide();
    $('#sample_web_code').hide();
    $('#sample_android_code').hide();
    switch (type*1) {
        case 1: //Web
            $('#sample_web_code').show();
            break;
        case 2: //Web Mobile
            break;
        case 3: //Android
            $('#sample_android_code').show();
            $('#screens').show();
            break;
        case 4: //iPhone
            break;
        case 5: //Windows
            break;
    }
    showOverlay($('#sample_code'));
}

$(document).ready(function () {
    showOverlay($('#sample_code'));
    $('#Type').change(function () {
        switchType($(this).val());
    });

    $('#create_lnk').click(function () {
        var desc = $('#Description').val();
        var type = $('#Type').val();
        $('#Description').removeClass('input-validation-error');
        $('.validation-summary-errors').remove();
        $('.field-validation-error').remove();
        if (desc.length == 0) {
            $('#Description').addClass('input-validation-error');
            $('#Description').parent().append('<span class="field-validation-error">The Description field is required.</span>');
            showOverlay($('#sample_code'));
        }
        else {
            showOverlay($('#sample_code'));
            $.ajax({
                url: newAppURL,
                dataType: 'json',
                type: 'POST',
                data: { "PortfolioId": $('#PortfolioId').val(), "Description": desc, "Type": type },
                success: function (json) {
                    if (json.HasError) {
                        $('#tbody').prepend('<tr><td colspan="2" class="validation-summary-errors"><span>Account creation was unsuccessful. Please contact to administrator.</span></td></tr>');
                    } else {
                        $('#overlay').hide();
                        $('#action_pnl').hide();
                        $('#web_code').val($('#web_code').val().replace('**-****-******', json.code));
                        $('.property-id').text(json.code);
                        $('#Type').attr('disabled', 'disabled');
                        appId = json.appId;
                        $('#Id').val(appId);
                        var lnk = $('#properties_lnk');
                        lnk.attr('href', lnk.attr('href').replace('{appId}', appId));
                        $('#Id').closest("form").attr("action", "/Application/Edit/" + appId);
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
            $('#Description_error').empty().append('<span class="field-validation-error">The Description field is required.</span>');
        } else {
            $('#application_form')[0].submit();
        }
    });

    $('#save_btn').click(function () {
    });

    //Disable not exsists types
    $('#Type option[value!=3]').attr('disabled', true);
    $('#Type option[value=3]').attr('selected', true);
    //--
    switchType($('#Type').val());
});