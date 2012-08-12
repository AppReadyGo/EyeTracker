
$(document).ready(function () {
    $('#predefinedSizes').change(function () {
        var val = $(this).val().split('X');
        $('#Width').val(val[0]);
        $('#Height').val(val[1]);
    });
    $('#predefinedPathes').change(function () {
        var val = $(this).val();
        $('#Path').val(val);
    });
});