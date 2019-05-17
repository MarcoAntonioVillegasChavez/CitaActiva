
/*jQuery(function ($) {
    $('.form-control.time').each(function () {
        var startDate = $(this).data("initial-datetime");
        $(this).datetimepicker({
            minTime: '7:00',
            maxTime: '12:00' ,
            datepicker: false,
            formatTime: 'H:i',
            format: 'H:i'

        });
    });
});

jQuery(function ($) {
    $('.form-control.date').each(function () {
        var startDate = $(this).data("initial-datetime");
        $(this).datetimepicker({
            timepicker: false,
            format: 'Y-m-d',
            startDate: new Date(),
            onChangeDateTime: function (dp, $input) {
               
            }
        });
    });
});


$(document).ready(function () {
    $("#workshopId").on("change", function () {
        var workshopId = $("#workshopId option:selected").val();
        $.ajax({
            type: 'GET',
            url: '/Appointment/Receptionist/' + workshopId,
            data: { workshopId },
            success: function (result) {
                var s = '<option value ="-1"> Selecciona un Recepcionaista</option>';
                for (var i = 0; i < result.length; i++) {
                    s += '<option value="' + result[i].id + '">' + result[i].name + '</option>';
                }
                $("#receptionistId").html(s);
            }
        });

    });
});
/*
$(document).ready(function () {
    $("#receptionistId").on("change", function () {
        //var scheduleId = $("#receptionistId option:selected").val();
        $.ajax({
            type: 'GET',
            url: '/Appointment/Schedule/' + scheduleId,
            data: { workshopId },
            success: function (result) {
               
                /*var s = '<option value ="-1"> Selecciona un Recepcionaista</option>';
                for (var i = 0; i < result.length; i++) {
                    s += '<option value="' + result[i].scheduleId + '">' + result[i].name + '</option>';
                }*//*
                $("#receptionistId").html(s);
            }
        });
    });
});
*/