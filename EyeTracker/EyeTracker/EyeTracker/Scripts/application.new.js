
function showOverlay(elem) {
    if (appId == 0) {
        var width = elem.outerWidth() + 5;
        var height = elem.outerHeight();
        var offset = elem.offset();
        $('#overlay').css({ width: width, height: height, top: offset.top, left: offset.left });
        $('#overlay').show();
    }
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
    if (appId == 0) {
        showOverlay($('#sample_code'));
    } else {
        $('#overlay').hide();
        $('#action_pnl').hide();
        $('#Type').attr('disabled', 'disabled');
    }
    $('#Type').change(function () {
        switchType($(this).val());
    });
    function screenRemoveBtnClick() {
        var li = $(this).parent();
        li.remove();
    }
    function screenImgBtnClick() {
        var width = $(this).attr('width');
        var height = $(this).attr('height');
        var css = null;
        if (width > height) {
            css = { width: 200 };
        } else {
            css = { height: 200 };
        }
        $('#img_preview img').css(css);
        $('#img_preview img').attr('src', screenImgURL + appId + '/' + width + '/' + height);
        $('#img_preview').show();
    }
    $('#screens_list .remove-btn').click(screenRemoveBtnClick);
    $('#screens_list .img-lnk').click(screenImgBtnClick);
    $('#screens_list .img-lnk').fancybox();

    $('#add_screen_btn').click(function () {
        var width = $('#screen_width').val() * 1;
        var height = $('#screen_height').val() * 1;
        if (width == 0 || height == 0) {
            $('#screen_error').text('Width or height is wrong format');
        } else {
            /*
            $("#loading").ajaxStart(function () {
            $(this).show();
            }).ajaxComplete(function () {
            $(this).hide();
            });
            */
            $.ajaxFileUpload({
                url: addScreenURL + appId + '/?Width=' + width + '&Height=' + height,
                secureuri: false,
                fileElementId: 'screen_img',
                dataType: 'json',
                success: function (data, status) {
                    if (data.HasError) {
                        $('#screen_error').text('Something wrong happen, please contact to administrator.');
                    } else {
                        var remBtn = $('<a class="remove-btn">&nbsp</a>');
                        remBtn.click(screenRemoveBtnClick);
                        var imgBtn = $('<a class="img-lnk" src="' + screenImgURL + appId + '/' + width + '/' + height + '/screen.jpg">' + width + 'X' + height + '</a>');
                        var li = $('<li></li>');
                        li.append(remBtn);
                        li.append(imgBtn);
                        $('#screens_list').append(li);
                        $('#screen_width').val('');
                        $('#screen_height').val('');
                        $('#screen_img').val('');
                        imgBtn.fancybox();
                    }
                },
                error: function (data, status, e) {
                    $('#screen_error').text('Something wrong happen, please contact to administrator.');
                }
            });
        }
    });
    $('#create_lnk').click(function () {
        var desc = $('#Description').val();
        var type = $('#Type').val();
        $('#Description').removeClass('input-validation-error');
        $('.validation-summary-errors').remove();
        $('.field-validation-error').remove();
        if (desc.length == 0) {
            $('#Description').addClass('input-validation-error');
            $('#tbody').prepend('<tr><td colspan="2" class="validation-summary-errors"><span>Account creation was unsuccessful. Please contact to administrator.</span></td></tr>');
            $('#Description').parent().append('<span class="field-validation-error">The Description field is required.</span>');
            showOverlay($('#sample_code'));
        }
        else {
            showOverlay($('#sample_code'));
            $.ajax({
                url: newAppURL,
                dataType: 'json',
                type: 'POST',
                data: { "PortfolioId": $('#PortfolioId').val(),"Description": desc, "Type": type },
                success: function (json) {
                    if (json.HasError) {
                        $('#tbody').prepend('<tr><td colspan="2" class="validation-summary-errors"><span>Account creation was unsuccessful. Please contact to administrator.</span></td></tr>');
                    } else {
                        $('#overlay').hide();
                        $('#action_pnl').hide();
                        $('#web_code').val($('#web_code').val().replace('**-****-******', json.code));
                        $('#android_code').val($('#android_code').val().replace('**-****-******', json.code));
                        $('.property-id').text(json.code);
                        $('#Type').attr('disabled', 'disabled');
                        appId = json.appId;
                        $('#Id').val(appId);
                        $('#edit_form').attr("action", $('#edit_form').attr("action") + appId);
                    }
                    showOverlay($('#sample_code'));
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
    $('#Type option[value=1]').attr('disabled', null);
    $('#Type option[value=3]').attr('selected', true);
    //--
    switchType($('#Type').val());
});