jQuery(function ($) {
    $(document).ready(function () {
        $("#marcaVehiculo").on("change", function () {
            var marcaVehiculo = $("#marcaVehiculo option:selected").val();

            $.blockUI({
                message: 'Por favor espere un momento...', css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
            $.ajax({
                type: 'GET',
                url: '/Citas/FamiiasVehiculo/' + marcaVehiculo,
                data: { marcaVehiculo },
                complete: function () {
                    $.unblockUI();
                },
                success: function (result) {
                    resultMarcaVehiculo = JSON.parse(result);
                    var s = '<option value="-1" selected="selected">Selecciona el modelo de  tu Vehiculo</option>';
                    for (var i = 0; i < resultMarcaVehiculo.length; i++) {
                        s += '<option value="' + resultMarcaVehiculo[i].id_familiavehiculo + '">' + resultMarcaVehiculo[i].descripcion + '</option>';
                    }
                    $('#id_familiavehiculo').html(s);
                }
            });
        });
    });

    //Llena el DropDownList de los servicios de transparencia y muestra el kilometraje 
    $(document).ready(function () {
        $("#tipo_combustible").on("change", function () {
            var anhoVehiculo = $("#anho_vehiculo").val();
            var familiaVehiculo = $("#id_familiavehiculo option:selected").val();
            var tipoCombustible = $("#tipo_combustible option:selected").val();

            $.blockUI({
                message: 'Por favor espere un momento...', css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
            $.ajax({
                type: 'GET',
                url: '/Servicios/GetServicios/' + anhoVehiculo + '/' + familiaVehiculo + '/' + tipoCombustible,
                data: { anhoVehiculo, familiaVehiculo, tipoCombustible },
                complete: function () {
                    $.unblockUI();
                },
                success: function (result) {
                    console.log(result);
                    resultServicios = JSON.parse(result);
                    console.log(resultServicios);
                    var s = '<option value="-1" selected="selected">Selecciona el kilometraje de  tu Vehiculo</option>';
                    for (var i = 0; i < resultServicios.length; i++) {
                        s += '<option value="' + resultServicios[i].id_servicio + '">' + resultServicios[i].kilometraje + '</option>';
                    }
                    $('#servicios').html(s);
                }
            });
        });
    });

    //Se obtienen los horarios de servicio con base en la agencia seleccionada.
    $(document).ready(function () {
        $("#fechaAgendamiento").on("change", function () {
            var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));
            
            var agenciaId = agencia[0].id_agencia.toString();
            //console.log(agenciaId);
            if (agenciaId != "") {
                $.blockUI({
                    message: 'Por favor espere un momento...', css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#000',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .5,
                        color: '#fff'
                    }
                });
                $.ajax({
                    type: 'GET',
                    url: '/Appointment/Receptionist/' + agenciaId,
                    data: { agenciaId },
                    complete: function () {
                        CosultarHorarios();

                       //
                    },
                    success: function (result) {
                        var resultSchedule = (JSON.parse(result))
                        if (resultSchedule != null) {
                           
                            localStorage.setItem("scheduleId", resultSchedule[0].scheduleId)
                        }
                        //else {
                        //    BloquearPlannedData();
                        //    BloquearPlannedTime();

                        //    toastr.warning("No se encontraron horarios para esta agencia.");
                        //}
                    }
                });
            } else {
                //BloquearPlannedData();
                //BloquearPlannedTime();
            }
        });
    });


    //Se llena el Horario con base en la agencia seleccionada.
    $(function () {
        var today, datepicker;
        today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
        datepicker = $('#fechaAgendamiento').datepicker({
            uiLibrary: 'bootstrap4',
            format: 'dd-mm-yyyy',
            weekStartDay: 1,
            minDate: today
        });
    });
    
    //$(document).ready(function () {
    //    $("#fechaAgendamiento").on("change", function () {
    function CosultarHorarios() {
        var scheduleId = localStorage.getItem("scheduleId")

        $.ajax({
            type: 'GET',
            url: '/Appointment/Schedule/' + scheduleId,
            data: { scheduleId },
            complete: function () {

                //console.log(localStorage.getItem("Min"))
                //console.log(localStorage.getItem("Max"))

                if (localStorage.getItem("Min") != "" && localStorage.getItem("Max") != "") {

                    var parts = $("#fechaAgendamiento").val().split('-');
                    var fecha = new Date(parts[2], parts[1] - 1, parts[0]);
                    var workshopId = $("#agenciaId option:selected").val();

                    $.ajax({
                        type: 'GET',
                        url: '/Appointment/AllowTimes/' + workshopId + '/' + localStorage.getItem("Min") + "/" + localStorage.getItem("Max") + "/" + fecha + "/0",
                        complete: function () {

                            localStorage.removeItem("Min");
                            localStorage.removeItem("Max");
                        },
                        success: function (resultAllowTimes) {
                            var result = (JSON.parse(resultAllowTimes))

                            var s = '<option value="-1" selected="selected">Selecciona un horario.</option>';
                            for (var i = 0; i < result.length; i++) {
                                s += '<option value="' + result[i].hora + '">' + result[i].horario + '</option>';
                            } $('#horaAgendamiento').html(s);

                        }
                    });
                } else {
                    toastr.warning("Ya no se puede elegir un horario de servicio en el dia seleccionado");
                    var s = '<option value="-1" selected="selected">Selecciona un horario.</option>';
                    $('#horaAgendamiento').html(s);
                }

             
                $.unblockUI();                        
            },
            success: function (resultS) {
                var resultSchedule = JSON.parse(resultS);
                var parts = $("#fechaAgendamiento").val().split('-');
                var fecha = new Date(parts[2], parts[1] - 1, parts[0]);

                //console.log(fecha.getDay());
                //console.log(resultSchedule);


                $.each(resultSchedule, function (index, value) {
                    var diaSemana = fecha.getDay();

                    if (diaSemana == 0) {
                        diaSemana = 7;
                    }

                    //console.log(diaSemana);
                    //console.log(value.weekday);

                    if (diaSemana == value.weekday) {

                        var year = viewBagYear;
                        var month = viewBagMonth;

                        if (month < 10) {
                            month = "0" + month
                        }
                        var day = viewBagDay;
                        if (day < 10) {
                            day = "0" + day
                        }

                        var FechaHoyAnho = parts[2].toString();
                        var FechaHoyMes = parts[1].toString();
                        var FechaHoyDia = parts[0].toString();

                        var FechaHoy = FechaHoyAnho + "-" + FechaHoyMes + "-" + FechaHoyDia
                        var date = year + "-" + month + "-" + day
                        if (FechaHoy == date) {

                            var hr = new Date();
                            var min = hr.getMinutes()
                            if (min < 10) {
                                min = "0" + min
                            }

                            var hrHoy = viewBagHora;// (hr.getHours() + 2) + ":" + min + ":" + hr.getSeconds();
                            var hrHoySplit = hrHoy.split(":")
                            var valueBeginning = value.beginning.split(":")
                            var valueEnding = value.ending.split(":")

                            var valoraHr = new Date(year, month - 1, day, hrHoySplit[0], hrHoySplit[1]);

                            var valorBeginning = new Date(year, month - 1, day, valueBeginning[0], valueBeginning[1]);
                            var valorEnding = new Date(year, month - 1, day, valueEnding[0], valueEnding[1]);

                            if (valoraHr >= valorEnding) {
                                localStorage.setItem("Min", "")
                                localStorage.setItem("Max", "")

                            } else {
                                if (valoraHr < valorBeginning) {
                                    localStorage.setItem("Min", value.beginning)
                                } else {
                                    localStorage.setItem("Min", hrHoy)
                                }
                                localStorage.setItem("Max", value.ending)
                            }
                        } else {
                            localStorage.setItem("Min", value.beginning)
                            localStorage.setItem("Max", value.ending)
                        }
                    }
                });
            }
        });
    }
    //    });
    //});

    $(document).ready(function () {
        $("#btnSigCita").on("click", function () {
            var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));
            var agenciaId = agencia[0].id_agencia.toString();

            var id_familiavehiculo = document.getElementById("id_familiavehiculo");
            var id_familiavehiculoText = id_familiavehiculo.options[id_familiavehiculo.selectedIndex].text;

            var servicios = document.getElementById("servicios");
            var serviciosText = servicios.options[servicios.selectedIndex].text;

            var plannedData = new Object();
            plannedData.plannedDate = $("#fechaAgendamiento").val();
            plannedData.plannedTime = $("#horaAgendamiento  option:selected").val();            

            var labours = new Object();
            labours.id = $("#servicios option:selected").val();
            labours.description = serviciosText;
            labours.plannedHours = 2;

            var nombreCompleto = $("#nombre_cliente").val() + ' ' + $("#apePat_cliente").val() + ' ' + $("#apeMat_cliente").val();

            var cliente = readCookie("cliente");
            var username = readCookie("nombreCliente");

            var invitadoInd = 1;
            if (cliente == null) {
                invitadoInd = 0;
            } 
           

            var cita = new Object();
            cita.workshopId = agenciaId;
            cita.contactName = nombreCompleto;
            cita.contactMail = $("#email_cliente").val(); 
            cita.contactPhone = $("#tel_cliente").val();
            cita.needReplacementCar = false; 
            cita.clientReceived = false
            cita.pickUpVehicle = false;//$("#").val();
            cita.vehiclePlate = $("#PlacaVehiculo").val();
            cita.vehicleYear = $("#anho_vehiculo").val();
            cita.brandId = $("#marcaVehiculo option:selected").val();
            cita.versionId = $("#id_familiavehiculo option:selected").val();;
            cita.version = id_familiavehiculoText;
            cita.plannedData = plannedData;
            cita.labours = labours;
            cita.invitadInd = invitadoInd;

            //var arrayLabours = []; //[{ id: 1 }, { description: 2 }]
            //arrayLabours.push(labours);

            console.log(cita);
            //console.log(plannedData);
            //console.log(labours);

            $.blockUI({
                message: 'Por favor espere un momento...', css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
            $.ajax({
                type: 'POST',
                url: '/Citas/Agendamiento/' + cita + '/' + plannedData + '/' + labours,
                data: { cita, plannedData, labours },
                complete: function () {
                    $.unblockUI();

                    SiguienteCita();
                },
                success: function (result) {
                  
                    console.log(result);

                    var s = '<label id="lblNoCita">' + result + '</label>';                     
                    $('#lblNoCita').html(s);
                }
            });
        });
    });
   
});
