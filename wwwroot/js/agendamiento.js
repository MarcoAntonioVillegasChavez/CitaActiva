jQuery(function ($) {
    $(document).ready(function () {
        // $("#marcaVehiculo").on("change", function () {
        var codigoQis = "NI";// $("#marcaVehiculo option:selected").val();

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
            url: '/Citas/FamiiasVehiculo/' + codigoQis,
            data: { codigoQis },
            contentType: "application/json",
            complete: function () {
                $.unblockUI();
            },
            success: function (result) {
                resultMarcaVehiculo = JSON.parse(result);
                var s = '<option value="-1" selected="selected">Selecciona el modelo de  tu Vehículo</option>';
                for (var i = 0; i < resultMarcaVehiculo.length; i++) {
                    s += '<option value="' + resultMarcaVehiculo[i].id_familia + '">' + resultMarcaVehiculo[i].nombre_familia + '</option>';
                }
                $('#id_familiavehiculo').html(s);
            }
            //   });
        });
    });


    ////Llena el DropDownList de los servicios de transparencia y muestra el kilometraje 
    $(document).ready(function () {
            //var familiaVehiculo = document.getElementById("id_familiavehiculo");
            //var familiaVehiculoText = familiaVehiculo.options[familiaVehiculo.selectedIndex].text;

            ////var tipoCombustible = $("#tipo_combustible option:selected").val();
            //var tipoCombustible = document.getElementById("tipo_combustible");
            //var tipoCombustibleText = tipoCombustible.options[tipoCombustible.selectedIndex].text;


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
                url: '/Servicios/GetServicios/',
                data: {  },
                complete: function () {
                    $.unblockUI();
                },
                success: function (result) {
                    resultServicios = JSON.parse(result);
                    var s = '<option value="-1" selected="selected">Selecciona el servicio correspondiente  a tu kilometraje</option>';
                    for (var i = 0; i < resultServicios.length; i++) {
                        s += '<option value="' + resultServicios[i].codigo_qis + '">' + resultServicios[i].kilometraje_maximo + '</option>';
                    }
                    $('#servicios').html(s);
                }
            });
    });
       

    //Se obtienen los horarios de servicio con base en la agencia seleccionada.
    $(document).ready(function () {
        $("#fechaAgendamiento").on("change", function () {
            var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));

            var agenciaId = agencia[0].id_agencia.toString();
                      
            document.getElementById('divServicioDomicilioAlert').style.display = 'none';
            document.getElementById('divServicioDomicilioAlert2').style.display = 'none';
            document.getElementById('divServicioDomicilio').style.display = 'block';
            document.getElementById('chkDomiciolio').checked = false;

            var s = ' <label id="ServDomiLbl"></label>';
            $('#ServDomiLbl').html(s);  

            if (agencia[0].activa) {
                document.getElementById('divServicioDomicilioAlert').style.display = 'none';
                document.getElementById('divServicioDomicilioAlert2').style.display = 'none';
                document.getElementById('divServicioDomicilio').style.display = 'block';              
                                
            } else {
                document.getElementById('divServicioDomicilioAlert').style.display = 'none';
                document.getElementById('divServicioDomicilioAlert2').style.display = 'block';
                document.getElementById('divServicioDomicilio').style.display = 'none';
            }


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
                    },
                    success: function (result) {
                        var resultSchedule = (JSON.parse(result))
                        if (resultSchedule != null) {

                            localStorage.setItem("scheduleId", resultSchedule[0].scheduleId)
                        }
                    }
                });
            } else {

            }
        });
    });


    //Se llena el Horario con base en la agencia seleccionada.
    $(function () {
        var today, datepicker;
        today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
        datepicker = $('#fechaAgendamiento').datepicker({
            uiLibrary: 'bootstrap4',
            format: 'yyyy-mm-dd',
            weekStartDay: 0,
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

             //   console.log(localStorage.getItem("Min"));
             //   console.log(localStorage.getItem("Max"));

				if (localStorage.getItem("Min") != "" && localStorage.getItem("Max") != "") {
					if (localStorage.getItem("Min") == "00:00:00" && localStorage.getItem("Max") == "00:01:00") {
						toastr.warning("No se encontró un horario en este dia, para esta agencia.");
						var s = '<option value="-1" selected="selected">Selecciona un horario.</option>';
						$('#horaAgendamiento').html(s);
					}else{
                    var fecha = $("#fechaAgendamiento").val();

                    var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));
                    var idAgencia = agencia[0].id_agencia.toString();


                    $.ajax({
                        type: 'GET',
                        url: '/Appointment/AllowTimes/' + idAgencia + '/' + localStorage.getItem("Min") + "/" + localStorage.getItem("Max") + "/" + fecha + "/0",
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
				}
				}else {
                    toastr.warning("Ya no se puede elegir un horario de servicio en el dia seleccionado");
                    var s = '<option value="-1" selected="selected">Selecciona un horario.</option>';
                    $('#horaAgendamiento').html(s);
                }


                $.unblockUI();
            },
            success: function (resultS) {
                var resultSchedule = JSON.parse(resultS);
                console.log(resultSchedule);
				 if (resultSchedule != null) {
					
                var parts = $("#fechaAgendamiento").val().split('-');
                var fecha = new Date(parts[0], parts[1] - 1, parts[2]);

                //console.log(fecha.getDay());
                //console.log(fecha);


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

                        var FechaHoyAnho = parts[0].toString();
                        var FechaHoyMes = parts[1].toString();
                        var FechaHoyDia = parts[2].toString();

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
			} else {
                    toastr.warning("No se encontráron horarios disponibles para esta fecha");
                    var s = '<option value="-1" selected="selected">Selecciona un horario.</option>';
			$('#horaAgendamiento').html(s);
			}
            }
        });
    }


    $(document).ready(function () {
        $("#btnSigCita").on("click", function () {

            if (ValidarAgencia() && ValidarRegistro() && ValidarVehiculo() && validarServicio()) {

                var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));
                var agenciaId = agencia[0].id_agencia.toString();
                var agenciaNom = agencia[0].nombre_agencia.toString();

                var id_familiavehiculo = document.getElementById("id_familiavehiculo");
                var id_familiavehiculoText = id_familiavehiculo.options[id_familiavehiculo.selectedIndex].text;

                var servicios = document.getElementById("servicios");
                var serviciosText = servicios.options[servicios.selectedIndex].text;

                var plannedData = new Object();
                plannedData.plannedDate = $("#fechaAgendamiento").val();
                plannedData.plannedTime = $("#horaAgendamiento  option:selected").val();

                var nombreCompleto = $("#nombre_cliente").val() + ' ' + $("#apePat_cliente").val() + ' ' + $("#apeMat_cliente").val();

                //            Se declara el arrayLabours
                var arrayLabours = [];
                /*
             */
                var comments = " ";
                if (document.getElementById('chkDomiciolio').checked == true) {

                    var calleCliente = $("#calleCliente").val();
                    var colonia = $("#colonia").val();
                    var num_ext = $("#num_ext").val();
                    var num_int = $("#num_int").val();
                    var ciudad = $("#ciudad").val();
                    var cp = $("#cp").val();

                    comments = 'Servicio a Domicilio en: ' + calleCliente + ' No.' + num_ext + ' No Int. ' + num_int + ' Colonia ' + colonia + ' CP ' + cp + ' Ciudad ' + ciudad;
                }

                //Se agrega servicios de Mantenimiento.
                if (document.getElementById('chkSerMan').checked) {
                    var serviciosId = document.getElementById("servicios");

                    if (serviciosId.selectedIndex != 0 ) {
                        var serviciosText = serviciosId.options[serviciosId.selectedIndex].text;
                        var labours = new Object();
                        labours.id = arrayLabours.length + 1;
                        labours.description = 'Servicio de transparencia de ' + serviciosText + ' km';
                        labours.plannedHours = 2;
                        labours.operatorId = 0;
                        labours.teamId = 0;
                        labours.tipo = 1;
                        arrayLabours.push(labours);
                    }
                }
                //se agrega fallas mecanicas a arraylabours
                if (document.getElementById('chkFallas').checked) {
                    var fallas_mec = $('#fallas_mec').val();
                    if (fallas_mec != "") {
                    var labours = new Object();
                    labours.id = arrayLabours.length + 1;
                    labours.description = 'Falla mecanica: ' + fallas_mec;
                    labours.plannedHours = 2;
                    labours.operatorId = 0;
                    labours.teamId = 0;
                    labours.tipo = 1;
                    arrayLabours.push(labours);
                    }
                }

             
                //se agrega servicios especificos a arraylabours

                if (document.getElementById('chkEspecificos').checked) {
                    var checksVal = $('[name="checks[]"]:checked').map(function () {
                        return this.value;
                    }).get();
                    if (checksVal.length != 0) {

                        checksVal.forEach(x => {
                            var labours = new Object();
                            labours.id = arrayLabours.length + 1;
                            labours.description = x;
                            labours.plannedHours = 2;
                            labours.operatorId = 0;
                            labours.teamId = 0;
                            labours.tipo = 1;
                            arrayLabours.push(labours);
                        });

                    }
                }
                //console.log(arrayLabours);

                var cita = new Object();
                cita.workshopId = agenciaId;
                cita.workShopName = agenciaNom;
                cita.contactName = nombreCompleto;
                cita.contactMail = $("#email_cliente").val();
                cita.contactPhone = $("#tel_cliente").val();
                cita.needReplacementCar = false;
                cita.clientReceived = false
                cita.pickUpVehicle = false;//$("#").val();
                cita.vehiclePlate = $("#PlacaVehiculo").val();
                cita.vehicleYear = $("#anho_vehiculo").val();
                cita.brandId = $("#marcaVehiculo option:selected").val();
                cita.versionId = $("#id_familiavehiculo option:selected").val();
                cita.mileage = $("#kilometrajeVehiculo").val();
                cita.version = id_familiavehiculoText;
                cita.plannedData = plannedData;
                cita.labours = arrayLabours;
                cita.comments = comments;
                //cita.cuenta_personal = cliente;

                //console.log(cita);


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
                    url: '/Citas/Agendamiento/' + cita,
                    data: { cita },
                    complete: function () {
                        $.unblockUI();

                        SiguienteCita();
                    },
                    success: function (result) {
                        console.log(result);
                        document.getElementById('previ').style.display = 'none';

                        var a = '  <label id="lblNoCita" style="color:#C10230"> <strong> ' + result + '</strong></label>';
                        $('#lblNoCita').html(a);

                        var s = '<h6> <label id="lblNoCita" style="color:#C10230"> <strong> En breve se pondrá en contacto un asesor de la agencia contigo, para dar seguimiento a tu cita. </strong></label></h6> <br />';
                        s += ' <b><label style="color:#C10230">Para cualquier cambio o cancelación en su cita, favor de contactar al tel: 800 711 2886<label></b>';
                        $('#lblMnsg').html(s);

                        $('#divAgenciasNormal').removeAttr('onclick');
                        $('#divAgenciasMovil').removeAttr('onclick');
                        $('#divVehiculoNormal').removeAttr('onclick');
                        $('#divVehiculoMovil').removeAttr('onclick');
                        $('#divCitaNormal').removeAttr('onclick');
                        $('#divCitaMovil').removeAttr('onclick');
                        $('#divEspecificoNormal').removeAttr('onclick');
                        $('#divEspecificoMovil').removeAttr('onclick');
                        $('#divConfirmarNormal').removeAttr('onclick');
                        $('#divConfirmarMovil').removeAttr('onclick');

                    }
                });
            }
        });
    });

    //$(document).ready(function () {
    //	$("#btnSigVehiculos").on("click", function () {


    //		$.blockUI({
    //			message: 'Por favor espere un momento...', css: {
    //				border: 'none',
    //				padding: '15px',
    //				backgroundColor: '#000',
    //				'-webkit-border-radius': '10px',
    //				'-moz-border-radius': '10px',
    //				opacity: .5,
    //				color: '#fff'
    //			}
    //		});
    //		$.ajax({
    //			type: 'GET',
    //			url: '/Kits/GetKits',
    //			data: {  },
    //			complete: function () {

    //                   $.unblockUI();

    //                   $(".radio").on('click', function (event) {
    //                       event.stopPropagation();
    //                       event.stopImmediatePropagation();

    //                       var value = $(this).attr('value');
    //                       localStorage.setItem("radioValue", value);                       
    //                   });

    //                   $(".cardShow").on('click', function (event) {
    //                       event.stopPropagation();
    //                       event.stopImmediatePropagation();

    //                       var str = $(this).attr('id')
    //                       var idKitConcepto = str.split("-");

    //                       var paq = $(this).attr('paq');
    //                       var title = $(this).attr('title');
    //                       var color = $(this).attr('color');

    //                       $.ajax({
    //                           type: 'GET',
    //                           url: '/Kits/GetKitConcepto/' + idKitConcepto[1],
    //                           data: { idKitConcepto },
    //                           complete: function () {

    //                           },
    //                           success: function (result) {
    //                               resultActividades = JSON.parse(result)
    //                               console.log(resultActividades);
    //                               //console.log(localStorage.getItem("idKitConcepto-" + idKitConcepto[1]));



    //                               var b = '<div class="modal-header" style="background-color:' + color + '; color: white">';
    //                               b += '<h5 class="modal-title"  id="exampleModalLabel">' + paq + '</h5>';
    //                               b += '<button type="button" class="close float-right" data-dismiss="modal" aria-label="Close">';
    //                               b += '<span aria-hidden="true" style="color: white">&times;</span>';
    //                               b += '</button>';
    //                               b += '</div>';
    //                               jQuery("#modalHeader").html(b);

    //                               var a = '<div id="modalBody" class="modal-body" style="padding-top:0px">';                               
    //                               a += '  <div style="width: 100%; height: 400px; overflow-y: scroll;">';
    //                               for (var i = 0; i < resultActividades[0].actividades.length; i++) {
    //                                   if (resultActividades[0].actividades[i].activo == 1) {
    //                                       a += '<p>' + resultActividades[0].actividades[i].actividades + '</p>';
    //                                   }
    //                               }                               
    //                               for (var i = 0; i < resultActividades[0].actividades.length; i++) {
    //                                   if (resultActividades[0].actividades[i].activo == 0) {
    //                                       a += '<p> <input type="checkbox" name="checks[]" text="' + resultActividades[0].actividades[i].actividades + '" value="' + resultActividades[0].actividades[i].id_actividad + '"  >   ' + resultActividades[0].actividades[i].actividades + '</input></p>';
    //                                   }
    //                               }
    //                               a += '  </div > ';
    //                               a += '</div > ';
    //                               jQuery("#modalBody").html(a);

    //                               var arr = localStorage.getItem("idKitConcepto-" + idKitConcepto[1])
    //                               if (arr != null) {
    //                                   var split = arr.split(",");
    //                                   for (var i = 0; i < split.length; i++) {

    //                                       $("#modalBody input").each(function () {

    //                                           if (this.value == split[i]) {
    //                                               this.checked = true;
    //                                           }
    //                                       })
    //                                   }
    //                               }

    //                               var c = '<div id="modalSubHeader" class="modal-body" style="text-align:center; padding-bottom:0px">';
    //                               c += '<Label>' + title + '</Label>';
    //                               c += '</div>';
    //                               jQuery("#modalSubHeader").html(c);
    //                               jQuery("#ModalDetalle").modal('show');

    //                               $('[name="checks[]"]').click(function () {
    //                                   var checksVal = $('[name="checks[]"]:checked').map(function () {
    //                                       return this.value;
    //                                   }).get();

    //                                   var checksText = $('[name="checks[]"]:checked').map(function () {
    //                                       return $(this).attr('text');
    //                                   }).get();

    //                                   localStorage.setItem("idKitConcepto-" + idKitConcepto[1], checksVal);
    //                                   localStorage.setItem('AdicionalesSeleccionados', checksText);

    //                                   //console.log(localStorage.getItem("idKitConcepto-" + idKitConcepto[1]));
    //                                   //console.log(localStorage.getItem("AdicionalesSeleccionados"));
    //                               });


    //                           }
    //                       });

    //                   });

    //			},
    //			success: function (result) {
    //				resultKits = JSON.parse(result);
    //				//console.log(resultKits);

    //                   var s = '<div id="accordion">';
    //                   for (var i = 0; i < resultKits.length; i++) {
    //                       s += '<div class="card">';
    //                       s += '  <div class="card-header" style="background-color:' + resultKits[i].color + '; color: white"  id="heading' + i + '" data-toggle="collapse" data-target="#collapse' + i + '" aria-expanded="true" aria-controls="collapse"' + i + '">';
    //                       s += '   <h6>' + resultKits[i].descripcion + '</h6>';
    //                       s += '  </div>';
    //                       if (i == 0) {
    //                           s += '  <div id="collapse' + i + '" class="collapse show" aria-labelledby="heading' + i + '" data-parent="#accordion">';
    //                       } else {
    //                           s += '  <div id="collapse' + i + '" class="collapse" aria-labelledby="heading' + i + '" data-parent="#accordion">';
    //                       }

    //                       s += '<div style="width: 100%; height: 200px; overflow-y: scroll;">';
    //                      // s += '<div class="card-columns pt-2 align-items-center">';

    //                       for (var k = 0; k < resultKits[i].conceptos.length; k++) {
    //                           if (resultKits[i].conceptos[k].tipo == 1) {
    //                               s += '';
    //                               s += ' <div class="card border-light cardShow" id="card-' + resultKits[i].conceptos[k].id_kit_concepto + '" paq="' + resultKits[i].descripcion + '" title = "' + resultKits[i].conceptos[k].concepto + '" color = "' + resultKits[i].color + '" style="padding:0; color: ' + resultKits[i].color + ';"> ';
    //                               s += '     <div class="card-body" style="padding:0"> ';
    //                               s += '         <p class="card-text"  > <ul> <li>' + resultKits[i].conceptos[k].concepto + ' </li></ul> </p> ';
    //                               s += '     </div> ';
    //                               s += ' </div> ';
    //                           }
    //                         }

    //                       s += '</div>';            
    //                       //s += '</div>';            

    //                      s += '  <input type="radio" class="radio" id="radio-' + i + '" value="' + resultKits[i].descripcion + '" name="paquetes" >Paquete Seleccionado</input>';
    //                      s += '  </div>';
    //                      s += '</div>';
    //			   }
    //                   s += '</div>';
    //				$('#accordion').html(s);
    //			}
    //           });
    //	});
    //});

    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/ServicioEspecifico/GetServicioEspecifico',
            data: {},
            complete: function () {
                $('[name="checks[]"]').click(function () {
                    var checksVal = $('[name="checks[]"]:checked').map(function () {
                        return this.value;
                    }).get();

                });
            },
            success: function (result) {
                resultEspecificos = JSON.parse(result)
                //console.log(resultEspecificos);

                var s = '<div id="divServicioEspecificohtml">';
                for (var i = 0; i < resultEspecificos.length; i++) {
                    s += '<label><input type="checkbox" name="checks[]" id="cbox"' + i + ' value="' + resultEspecificos[i].descripcion + '">  ' + resultEspecificos[i].descripcion + ' </label><br>';
                }
                s += '</div >';
                $('#divServicioEspecificohtml').html(s);
            }
        });

    });

    $(document).ready(function () {
        $("#contact-tab").on("click", function () {
            $.ajax({
                type: 'GET',
                url: '/ServicioAdicional/GetServicioAdicional',
                data: {},
                complete: function () {
                },
                success: function (result) {
                    resultAdicionales = JSON.parse(result);
                    // console.log(resultAdicionales);

                    var s = '<div id="adicionales">';
                    s += '<table>';
                    s += '<tr>';
                    s += '<td>';
                    s += '</td>';
                    s += '</tr>';
                    s += '</table>';

                    for (var i = 0; i < resultAdicionales.length; i++) {

                        s += '<div class="card border-light mb-3" style = "max-width: 18rem;" onclick = "changeColor(this);" > ';
                        s += '    <div class="card-body">';
                        s += '        <p class="card-text"> ' + resultAdicionales[i].descripcion + '  </p > ';
                        s += '    </div>';
                        s += '</div>';
                    }
                    s += 'Los precios de adicionales, están sujetos a evaluación en el distribuidor';
                    s += '</div>';
                    $('#adicionales').html(s);
                }
            });
        });
    });


    //
    $(document).ready(function () {
        $("#zonas").on("change", function () {
            var idZona = $("#zonas option:selected").val();

            // console.log(idZona);

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
                url: '/Agencias/GetAgenciasByIdZona/' + idZona,
                data: { idZona },
                contentType: "application/json",
                complete: function () {
                    $.unblockUI();
                },
                success: function (result) {
                    resultAgencias = JSON.parse(result);
                    //  console.log(resultAgencias);

                    clearResults();
                    document.getElementById('divFechaAgendamiento').style.display = 'none';
                    document.getElementById('divServicioDomicilio').style.display = 'block';
                    document.getElementById('chkDomiciolio').checked = false;

                    document.getElementById('fechaAgendamiento').value = "";
                    document.getElementById('horaAgendamiento').selectedIndex = 0;
                    document.getElementById('divServicioDomicilioAlert').style.display = 'none'
                    document.getElementById('divServicioDomicilioAlert2').style.display = 'none';

                    document.getElementById('divDomicilio').style.display = 'none'

                    var s = ' <label id="ServDomiLbl"></label>';
                    $('#ServDomiLbl').html(s);  

                    resultAgencias.forEach(function (resultAgencias, i) {

                        var Id = resultAgencias.id_agencia;
                        //  console.log(Id);

                        var markerLetter = String.fromCharCode('A'.charCodeAt(0) + (i % 26));
                        var markerIcon = '/img/maps.JPG';


                        var tr = document.createElement('tr');
                        tr.style.backgroundColor = (i % 2 === 0 ? '#F0F0F0' : '#FFFFFF');
                        tr.onclick = function () {
                            // google.maps.event.trigger(markers[i], 'click');
                        };
                        var iconTd = document.createElement('td');
                        var nameTd = document.createElement('td');
                        var btnTd = document.createElement('td');

                        var btn = document.createElement('button');
                        btn.setAttribute('class', 'btn button');
                        btn.textContent = 'Seleccionar';
                        //btn.innerHtml = '<i class="fa fa-check-circle-o"></i>';
                        //document.body.appendChild(btn);

                        btn.onclick = function () {
                            $.ajax({
                                type: 'GET',
                                url: '/Agencias/GetAgenciasByPlaceId/' + Id,
                                data: { Id },
                                complete: function () {
                                    // $.unblockUI();
                                    //SiguienteAgencias();

                                    var agenciaInfo = JSON.parse(localStorage.getItem("AgenciaInfo"));
                                    if (agenciaInfo[0].activa == false) {
                                        document.getElementById('divServicioDomicilio').style.display = 'none';
                                        document.getElementById('chkDomiciolio').checked = false;
                                        document.getElementById('divServicioDomicilioAlert').style.display = 'none';
                                        document.getElementById('divServicioDomicilioAlert2').style.display = 'block';
                                    }

                                    document.getElementById('divFechaAgendamiento').style.display = 'block';

                                    $("#horaAgendamiento").on("change", function () {

                                        //console.log(localStorage.getItem("AgenciaInfo"));
                                        document.getElementById('divServicioDomicilioAlert').style.display = 'none';
                                        document.getElementById('divServicioDomicilioAlert2').style.display = 'none';

                                        var fechaAgendamientoCompleta = new Date($('#fechaAgendamiento').val() + ' ' + document.getElementById('horaAgendamiento').value);
                                        fechaAgendamientoCompleta.setDate(fechaAgendamientoCompleta.getDate());

                                        var fechaHoy = new Date();
                                        var y = fechaHoy.getFullYear();
                                        var m = fechaHoy.getMonth() ;
                                        var d = fechaHoy.getDate();
                                        
                                        var yA = fechaAgendamientoCompleta.getFullYear();
                                        var mA = fechaAgendamientoCompleta.getMonth() ;
                                        var dA = fechaAgendamientoCompleta.getDate();

                                        var f = new Date(y, m, d);
                                        var fA = new Date(yA, mA, dA);
                                        var fAR = new Date(y, m, f.getDate() + 1);

                                        //si la agencia tiene habilitada la propiedad de agendamiento a domicilio
                                        if (agenciaInfo[0].activa) {
                                            console.log("puede hacer agendamiento a domicilio");
                                            //si se agenda la cita para el dia de hoy, no muestra servicio a domicilio
                                            if (f.getTime() == fA.getTime()) {
                                                document.getElementById('divServicioDomicilio').style.display = 'none';
                                                document.getElementById('chkDomiciolio').checked = false;

                                                if (agenciaInfo[0].activa) {
                                                    document.getElementById('divServicioDomicilioAlert').style.display = 'block';
                                                    document.getElementById('divServicioDomicilioAlert2').style.display = 'none';
                                                } else {
                                                    document.getElementById('divServicioDomicilioAlert').style.display = 'none';
                                                    document.getElementById('divServicioDomicilioAlert2').style.display = 'block';
                                                }

                                                //si la cita es para mañana, hoy la puedo agendar antes de las 18:00 hrs
                                            } else if (fAR.getTime() == fA.getTime()) {
                                                if (new Date().getHours() >= 18) {
                                                    document.getElementById('divServicioDomicilio').style.display = 'none';
                                                    document.getElementById('chkDomiciolio').checked = false;

                                                    document.getElementById('divServicioDomicilioAlert').style.display = 'block'

                                                } else {
                                                    document.getElementById('divServicioDomicilio').style.display = 'block';
                                                }
                                            } else {
                                                document.getElementById('divServicioDomicilio').style.display = 'block';
                                            }
                                        } else {
                                            document.getElementById('divServicioDomicilio').style.display = 'none';
                                            document.getElementById('chkDomiciolio').checked = false;
                                            document.getElementById('divServicioDomicilioAlert2').style.display = 'block';
                                        }

                                        //console.log(fAR.getTime());

                                        //console.log(fA.getTime()); 

                                        //console.log(new Date().getHours());

                                        //console.log("fecha de hoy");
                                        //console.log(f);
                                        
                                        //console.log("fecha agendamiento");
                                        //console.log(fA);

                                        //console.log("fecha siguiente");
                                        //console.log(fAR);
                                        
                                    });
                                },
                                success: function (result) {
                                    localStorage.setItem("AgenciaInfo", result);
                                    var res = JSON.parse(result);

                                    clearResults();


                                    var s = '<table style="width:100%">';
                                    s += '<tr>';
                                    s += '<td>';
                                    s += 'Agencia seleccionada: <strong>' + res[0].nombre_agencia + '</strong>';
                                    s += '</td>';
                                    s += '</tr>';
                                    s += '</table>';
                                    s += '<br />';
                                    $('#results').html(s);

                                    console.log(localStorage.getItem("AgenciaInfo"));
                                }
                            });
                        }

                        var icon = document.createElement('img');
                        icon.src = markerIcon;
                        icon.setAttribute('class', 'placeIcon');
                        icon.setAttribute('className', 'placeIcon');

                        var tr = document.createElement('tr');
                        iconTd.appendChild(icon);
                        btnTd.appendChild(btn);
                        tr.appendChild(iconTd);
                        nameTd.appendChild(document.createTextNode(resultAgencias.nombre_agencia));
                        tr.appendChild(nameTd);

                        tr.appendChild(btnTd);
                        document.getElementById('results').appendChild(tr);
                    });



                }
            });
        });


    });

    function clearResults() {
        var results = document.getElementById('results');
        while (results.childNodes[0]) {
            results.removeChild(results.childNodes[0]);
        }
    }

});
