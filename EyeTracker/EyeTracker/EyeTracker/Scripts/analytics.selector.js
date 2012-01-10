
function removeItem() {
    var item = $(checked[i]);
    var text = item.parent().text();
    var id = item.val();
    popup.next().append('<li><a itemid="' + id + '" class="remove-btn">&nbsp;</a>' + text + '</li>');
    item.parent().parent().remove();
}

$(function () {
    $('.selector .link').click(function () {
        var popup = $(this).next();
        popup.toggle();
    });
    $('.selector .cancel-btn').click(function () {
        var popup = $(this).parent().parent().hide();
        popup.find('input:checked').attr('checked', false);
    });
    $('.selector .apply-btn').click(function () {
        var popup = $(this).parent().parent().hide();
        var checked = popup.find('input:checked');
        for (var i = 0; i < checked.length; i++) {
            var item = $(checked[i]);
            var text = item.parent().text();
            var id = item.val();
            var idex = item.parent().attr('itemindex');
            var newItem = $('<li><a itemid="' + id + '" itemindex="' + idex + '" class="remove-btn">&nbsp;</a>' + text + '</li>');
            var selitems = popup.next().find('a');
            for (var j = 0; j < selitems.length; j++) {
                var selitem = selitems[j];
                if ((selitem.attr('itemindex') * 1) > idex) {
                    newItem.insertBefore(selitem.parent());
                }
            }
            newItem.find('.remove-btn').click(removeItem);
            item.parent().parent().remove();
        }
    });
    $('.selector .remove-btn').click(removeItem);
});
