//Mostrar datos de usuario si está firmado
$(document).ready(function () {
    var cuenta_personal = readCookie("cliente");
    var username = readCookie("nombreCliente");

    if (cuenta_personal != null) {
        $.ajax({
            type: 'GET',
            url: '/Clientes/BuscarClienteById/' + cuenta_personal,
            data: { cuenta_personal },
            complete: function () {
               
            },
            success: function (result) {
                if (result != null) {
                    var resultClientes = JSON.parse(result);
                    
                    document.getElementById('nombre_cliente').value = resultClientes.nombre_cliente;
                    document.getElementById('apePat_cliente').value = resultClientes.apellido_paterno;
                    document.getElementById('apeMat_cliente').value = resultClientes.apellido_materno;
                    document.getElementById('tel_cliente').value = resultClientes.telefono;
                    document.getElementById('email_cliente').value = resultClientes.email_cliente;                   
                }               
            }
        });
    }
});

$(document).ready(function () {
    $("#chkDomiciolio").change(function () {
        if (this.checked) {
            document.getElementById('divDomicilio').style.display = 'block';

            var s = ' <label id="ServDomiLbl" style="color:#C10230">';
            s += '<strong> En el horario seleccionado en su cita el asesor acudirá a su domicilio </strong>';
            s += '</label>';
            $('#ServDomiLbl').html(s);         
          
        } else {
            document.getElementById('divDomicilio').style.display = 'none';

            var s = ' <label id="ServDomiLbl">';
            s += '';
            s += '</label>';
            $('#ServDomiLbl').html(s);  
        }
    });

    
});
$(document).ready(function () {
        document.getElementById('divChkSerMan').style.display = 'none'; 
        document.getElementById('divChkFallas').style.display = 'none';
        document.getElementById('divChkEspecificos').style.display = 'none';

    $("#chkSerMan").change(function () {
        if (this.checked) {
            document.getElementById('divChkSerMan').style.display = 'block';
        } else {
            document.getElementById('divChkSerMan').style.display = 'none';                     
        }
    });

    $("#chkFallas").change(function () {
        if (this.checked) {
            document.getElementById('divChkFallas').style.display = 'block';
        } else {
            document.getElementById('divChkFallas').style.display = 'none';
        }
    });

    $("#chkEspecificos").change(function () {
        if (this.checked) {
            document.getElementById('divChkEspecificos').style.display = 'block';
        } else {
            document.getElementById('divChkEspecificos').style.display = 'none';
        }
    });


});



//Mostrar divs efecto Wizard
$(document).ready(function () {
    OcultarDivs();
    document.getElementById('divAgencias').style.display = 'block';
        
    //Ocultar divs de los iconos de Agencia 
    document.getElementById('divAgenciasIcon0').style.display = 'block';
    document.getElementById('divAgenciasIconMovil0').style.display = 'block';

    //Ocultar divs de los iconos de los vehiculo
    document.getElementById('divVehiculoIcon0').style.display = 'block';
    document.getElementById('divVehiculoIconMovil0').style.display = 'block';
       
    //Ocultar divs de los iconos de la cita
    document.getElementById('divCitaIcon0').style.display = 'block';
    document.getElementById('divCitaIconMovil0').style.display = 'block';

    //Ocultar divs de los iconos del servicio
    //document.getElementById('divServicioIcon0').style.display = 'block';
    //document.getElementById('divServicioIconMovil0').style.display = 'block';

    //Muestra div de los iconos de Servicios especificos
    document.getElementById('divEspecificoIcon0').style.display = 'block';
    document.getElementById('divEspecificoIconMovil0').style.display = 'block';    

    //Ocultar divs de los iconos de la confirmación
    document.getElementById('divConfirmarIcon0').style.display = 'block';
    document.getElementById('divConfirmarIconMovil0').style.display = 'block';
});

function OcultarDivs() {
   //Ocultar divs 
    document.getElementById('divAgencias').style.display = 'none';
    document.getElementById('divVehiculo').style.display = 'none';
    //document.getElementById('divServicio').style.display = 'none';    
    document.getElementById('divCita').style.display = 'none';
    document.getElementById('divEspecifico').style.display = 'none';    
    document.getElementById('divConfirmar').style.display = 'none';

    //llena el titulo en el movil de la pantalla en la q estámos.
    cambiarTitleMovi("Agencia y cita");
  
}


function SiguienteAgencias() {
    if (ValidarAgencia()) {
        OcultarDivs();
        document.getElementById('divVehiculo').style.display = 'block';

        document.getElementById('divVehiculoIcon0').style.display = 'none';
        document.getElementById('divVehiculoIconMovil0').style.display = 'none';

        document.getElementById('divVehiculoIcon1').style.display = 'block';
        document.getElementById('divVehiculoIconMovil1').style.display = 'block';

        AgenciaSeleccionada();

        cambiarTitleMovi("Registro");
    }
}

function AtrasVehiculo() {
    OcultarDivs();
    document.getElementById('divAgencias').style.display = 'block';

    document.getElementById('divVehiculoIcon1').style.display = 'none';
    document.getElementById('divVehiculoIconMovil1').style.display = 'none';
    document.getElementById('divVehiculoIcon0').style.display = 'block';
    document.getElementById('divVehiculoIconMovil0').style.display = 'block';

    document.getElementById('divEspecificoIcon1').style.display = 'none';
    document.getElementById('divEspecificoIconMovil1').style.display = 'none';
    document.getElementById('divEspecificoIcon0').style.display = 'block';
    document.getElementById('divEspecificoIconMovil0').style.display = 'block';

    cambiarTitleMovi("Agencia y Cita");
}

function SiguienteVehiculo() {
    if (ValidarRegistro()){

    OcultarDivs();
    document.getElementById('divCita').style.display = 'block';

    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';

    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';

        KmSeleccionado();

        cambiarTitleMovi("Vehículo");
    }
}

function AtrasServicio() {
    OcultarDivs();
    document.getElementById('divVehiculo').style.display = 'block';

    //document.getElementById('divServicioIcon1').style.display = 'none';
    //document.getElementById('divServicioIconMovil1').style.display = 'none';
    //document.getElementById('divServicioIcon0').style.display = 'block';
    //document.getElementById('divServicioIconMovil0').style.display = 'block';

    document.getElementById('divCitaIcon0').style.display = 'block';
    document.getElementById('divCitaIconMovil0').style.display = 'block';
    document.getElementById('divCitaIcon1').style.display = 'none';
    document.getElementById('divCitaIconMovil1').style.display = 'none';

    cambiarTitleMovi("Registro");
}


function SiguienteServicio() {
    if (ValidarVehiculo()) {

    OcultarDivs();
        document.getElementById('divEspecifico').style.display = 'block';
        
    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

        document.getElementById('divEspecificoIcon0').style.display = 'none';
        document.getElementById('divEspecificoIconMovil0').style.display = 'none';
        document.getElementById('divEspecificoIcon1').style.display = 'block';
        document.getElementById('divEspecificoIconMovil1').style.display = 'block';
           
    KmSeleccionado();
        ServicioSeleccionado(); 
        cambiarTitleMovi("Servicio");
      
    }
}
function SiguienteFalla() {
    OcultarDivs();
    document.getElementById('divEspecifico').style.display = 'block';

    document.getElementById('divEspecificoIcon0').style.display = 'none';
    document.getElementById('divEspecificoIconMovil0').style.display = 'none';
    document.getElementById('divEspecificoIcon1').style.display = 'block';
    document.getElementById('divEspecificoIconMovil1').style.display = 'block';

    cambiarTitleMovi("Servicios Específicos");
}
function AtrasFalla() {
    OcultarDivs();
    document.getElementById('divCita').style.display = 'block';

    document.getElementById('divEspecificoIcon1').style.display = 'none';
    document.getElementById('divEspecificoIcon0').style.display = 'block';
    document.getElementById('divEspecificoIconMovil1').style.display = 'none';
    document.getElementById('divEspecificoIconMovil0').style.display = 'block';

    cambiarTitleMovi("Vehículo");
}

function AtrasCita() {
    OcultarDivs();
    document.getElementById('divVehiculo').style.display = 'block';

    document.getElementById('divCitaIcon1').style.display = 'none';
    document.getElementById('divCitaIconMovil1').style.display = 'none';

    document.getElementById('divCitaIcon0').style.display = 'block';
    document.getElementById('divCitaIconMovil0').style.display = 'block';
    cambiarTitleMovi("Servicios Específicos");
}

function SiguienteCita() {
    OcultarDivs();
    document.getElementById('divConfirmar').style.display = 'block';
    
    document.getElementById('divConfirmarIcon0').style.display = 'none';
    document.getElementById('divConfirmarIconMovil0').style.display = 'none';

    document.getElementById('divConfirmarIcon1').style.display = 'block';
    document.getElementById('divConfirmarIconMovil1').style.display = 'block';
         
    PreConfirmacion();
    cambiarTitleMovi("Confirmación");
}

function AtrasEspecificos() {
    OcultarDivs();
    document.getElementById('divCita').style.display = 'block';

    document.getElementById('divEspecificoIcon1').style.display = 'none';
    document.getElementById('divEspecificoIconMovil1').style.display = 'none';
    document.getElementById('divEspecificoIcon0').style.display = 'block';
    document.getElementById('divEspecificoIconMovil0').style.display = 'block';

    cambiarTitleMovi("Vehículo");
}


function Confirmar() {

}


function PreConfirmacion() {

    
    document.getElementById('preVisializacion').style.display = 'none';

    //2019-08-20
    var parts = $("#fechaAgendamiento").val().split('-');
    var fecha = new Date(parts[0], parts[1] - 1, parts[2]);

    var lblDia = '<label id="lblConfirmDia" > ';
    lblDia += parts[2].toString();
    lblDia += ' </label >';

    var weekday = new Array(7);
    weekday[0] = "Domingo";
    weekday[1] = "Lunes";
    weekday[2] = "Martes";
    weekday[3] = "Miercoles";
    weekday[4] = "Jueves";
    weekday[5] = "Viernes";
    weekday[6] = "Sabado";

    var n = weekday[fecha.getDay()];
    var lblDiaSeman = '<label id="lblConfirmNomDia" > ';
        lblDiaSeman += n;
        lblDiaSeman += ' </label >';

    var Month = new Array(12);
    Month[1] = "Enero";
    Month[2] = "Febrero";
    Month[3] = "Marzo";
    Month[4] = "Abril";
    Month[5] = "Mayo";
    Month[6] = "Junio";
    Month[7] = "Julio";
    Month[8] = "Agosto";
    Month[9] = "Septiembre";
    Month[10] = "Octubre";
    Month[11] = "Noviembre";
    Month[12] = "Diciembre";

    var m = Month[fecha.getMonth()+1];
    var lblConfirmMes = '<label id="lblConfirmMes" > ';
    lblConfirmMes += m;
    lblConfirmMes += ' </label >';

    //var agenciaId = document.getElementById("servicios");
    var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));
    //<(agencia);
    var agenciaIdText = agencia[0].nombre_agencia;
    //var agenciaIdText = document.getElementById("servicios").value;//agenciaId.options[agenciaId.innerText].text; //

    var lblPreConfrAgencia = '<label id="lblPreConfrAgencia" > ';
    lblPreConfrAgencia += 'Agendando cita en ' + agenciaIdText;
    lblPreConfrAgencia += ' </label >';

    var marcaVehiculo = document.getElementById("marcaVehiculo");
    var marcaVehiculoText = marcaVehiculo.options[marcaVehiculo.selectedIndex].text;
    var id_familiavehiculo = document.getElementById("id_familiavehiculo");
    var id_familiavehiculoText = id_familiavehiculo.options[id_familiavehiculo.selectedIndex].text;
    var anho_vehiculo = $('#anho_vehiculo').val();
    var id_familiavehiculo = document.getElementById("id_familiavehiculo");
    var id_familiavehiculoText = id_familiavehiculo.options[id_familiavehiculo.selectedIndex].text;
    var servicios = document.getElementById("servicios");
    var serviciosText = servicios.options[servicios.selectedIndex].text;
    var horaAgendamiento = document.getElementById("horaAgendamiento");
    var horaAgendamientoText = horaAgendamiento.options[horaAgendamiento.selectedIndex].text;

    var lblPreConfrVehi = '<label id="lblPreConfrVehi" > ';
    lblPreConfrVehi += marcaVehiculoText + ' ' + id_familiavehiculoText + ' ' + anho_vehiculo;
    lblPreConfrVehi += ' </label >';

    var lblPreConfrServ = '<label id="lblPreConfrServ" > ';
    lblPreConfrServ += 'Servicio de ' + serviciosText + ' km ';
    lblPreConfrServ += ' </label >';

    var lblPreConfrHr = '<label id="lblPreConfrHr" > ';
    lblPreConfrHr += horaAgendamientoText;
    lblPreConfrHr += ' </label >';

    var lblPreConfFalla = '<label id="lblPreConfFalla">' + $('#fallas_mec').val() + '</label>';     


    var PreConfServEspecif = '<div>';
    if (document.getElementById('chkEspecificos').checked) {
        var checksVal = $('[name="checks[]"]:checked').map(function () {
            return this.value;
        }).get();
        if (checksVal.length != 0) {
            checksVal.forEach(x => {
                PreConfServEspecif += x + '<br /> ';
            });
        }
    }
    PreConfServEspecif += '</div > ';

   
    $('#lblPreConfFalla').html(lblPreConfFalla);
    $('#PreConfServEspecif').html(PreConfServEspecif);


    $('#lblConfirmDia').html(lblDia);
    $('#lblConfirmNomDia').html(lblDiaSeman);
    $('#lblConfirmMes').html(lblConfirmMes);

    $('#lblPreConfrAgencia').html(lblPreConfrAgencia);
    $('#lblPreConfrVehi').html(lblPreConfrVehi);
    $('#lblPreConfrServ').html(lblPreConfrServ);
    $('#lblPreConfrHr').html(lblPreConfrHr);
    
    var nombreCompleto = $("#nombre_cliente").val() + ' ' + $("#apePat_cliente").val() + ' ' + $("#apeMat_cliente").val();
    var z = '<label id = "lblConfirmNombre"> ' + nombreCompleto + ' </label>';
     $('#lblConfirmNombre').html(z);
    
}
    
function AgenciaSeleccionada() {

    var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));
console.log(agencia);
    var agenciaText = agencia[0].nombre_agencia;//document.getElementById("places").value();
    //console.log(agenciaText);
    var s = '<label id="lblAgenciaSeleccionada" > ';
    s += 'Agendando cita en ' + agenciaText;
    s += ' </label >';

    $('#lblAgenciaSeleccionada').html(s);

    var s1 = '<label id="lblAgenciaSeleccionadaMovil" > ';
    s1 += 'Agendando cita en ' + agenciaText;
    s1 += ' </label >';
    $('#lblAgenciaSeleccionadaMovil').html(s1);

    if (document.getElementById('chkDomiciolio').checked) {
        var s2 = '<label id="lblServDomi">';
        s2 += 'Servicio a Domicilio';
        s2 += ' </label >';
        $('#lblServDomi').html(s2);

        var s3 = '<label id="lblServDomiMovil">';
        s3 += 'Servicio a Domicilio';
        s3 += ' </label >';
        $('#lblServDomiMovil').html(s3);
    }

 
  
}
function KmSeleccionado() {    
    var idFamiliavehiculo = document.getElementById("id_familiavehiculo");
    var FamiliavehiculoText = idFamiliavehiculo.options[idFamiliavehiculo.selectedIndex].text;

    var tipoCombustible = document.getElementById("tipo_combustible");
    var tipoCombustibleText = tipoCombustible.options[tipoCombustible.selectedIndex].text;

    var serviciosId = document.getElementById("servicios");
    var serviciosText = serviciosId.options[serviciosId.selectedIndex].text;

    
    var a = '<label id="FamiliavehiculoSeleccionado" > ';
    a += FamiliavehiculoText + ' ' + $('#anho_vehiculo').val();
    a += ' </label >';
    $('#FamiliavehiculoSeleccionado').html(a);

    var a1 = '<label id="FamiliavehiculoSeleccionadoMovil" > ';
    a1 += FamiliavehiculoText + ' ' + $('#anho_vehiculo').val();
    a1 += ' </label >';
    $('#FamiliavehiculoSeleccionadoMovil').html(a1);
       
    var b = '<label id="CombustibleSeleccionado" > ';
    b += tipoCombustibleText;
    b += ' </label >';
    $('#CombustibleSeleccionado').html(b);

  var b1 = '<label id="CombustibleSeleccionadoMovil" > ';
    b1 += tipoCombustibleText;
    b1 += ' </label >';
    $('#CombustibleSeleccionadoMovil').html(b1);

    var c = '<label id="VinRecibido" > ';
    c += 'VIN / Placa ' + $('#PlacaVehiculo').val();
    c += ' </label >';
    $('#VinRecibido').html(c);

    var c1 = '<label id="VinRecibidoMovil" > ';
    c1 += 'VIN / Placa ' + $('#PlacaVehiculo').val();
    c1 += ' </label >';
    $('#VinRecibidoMovil').html(c1);

    var s = '<label id="lblKm" > ';
    s += ' El Servicio y mantenimiento  son basados sobre el kilometraje estimado de ' + serviciosText + ' kilómetros';
    s += ' </label >';
    $('#lblKm').html(s);

   
}

function ServicioSeleccionado(){
    //var kitSeleccionado = localStorage.getItem("radioValue");
    
    //var a = '<label id = "lblServicioSeleccionado">';
    //a += kitSeleccionado;
    //a += '</label>';
    //$('#lblServicioSeleccionado').html(a);
    
    //var a1 = '<label id = "lblServicioSeleccionadoMovil">';
    //a1 += kitSeleccionado;
    //a1 += '</label>';
    //$('#lblServicioSeleccionadoMovil').html(a1);


    //var serviciosId = document.getElementById("kilometrajeVehiculo");
    //var serviciosText = serviciosId.options[serviciosId.selectedIndex].text;

    
    var b = '<label id = "lblServicioSeleccionado">';
    b += '' + $('#kilometrajeVehiculo').val() + ' km';
    b += '</label>';
    $('#lblKmSeleccionado').html(b);

    var b1 = '<label id = "lblServicioSeleccionado">';
    b1 += '' + $('#kilometrajeVehiculo').val() + 'km';
    b1 += '</label>';
    $('#lblKmSeleccionadoMovil').html(b1);

    
}


// Wizard de imagenes
//Agencias
function ClickAgencias() {   
        OcultarDivs();

        document.getElementById('divAgencias').style.display = 'block';
    ApagarIconos();

    cambiarTitleMovi('Agencia y cita');
    
}
//Registro
function ClickVehiculos() {
    //alert('click vehiculor');
    OcultarDivs();
    document.getElementById('divVehiculo').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

    //SiguienteAgencias();
    cambiarTitleMovi("Registro");
    //SiguienteVehiculo();
}
//Vehiculo
function ClikCitas() {
    //alert('click citas');
    OcultarDivs();
    document.getElementById('divCita').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

    
    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';
    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';


    cambiarTitleMovi("Vehículo");

}
//Fallas
function ClickServicios() {
   
    OcultarDivs();
    //document.getElementById('divServicio').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';
    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';

    //document.getElementById('divServicioIcon0').style.display = 'none';
    //document.getElementById('divServicioIconMovil0').style.display = 'none';
    //document.getElementById('divServicioIcon1').style.display = 'block';
    //document.getElementById('divServicioIconMovil1').style.display = 'block';

    cambiarTitleMovi("Falla");
   
}
//Servicios Especificos
function ClickEspecificos() {
    OcultarDivs();
    document.getElementById('divEspecifico').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';


    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';
    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';

    //document.getElementById('divServicioIcon0').style.display = 'none';
    //document.getElementById('divServicioIconMovil0').style.display = 'none';
    //document.getElementById('divServicioIcon1').style.display = 'block';
    //document.getElementById('divServicioIconMovil1').style.display = 'block';

    document.getElementById('divEspecificoIcon0').style.display = 'none';
    document.getElementById('divEspecificoIconMovil0').style.display = 'none';
    document.getElementById('divEspecificoIcon1').style.display = 'block';
    document.getElementById('divEspecificoIconMovil1').style.display = 'block';

    cambiarTitleMovi("Servicio");
}
//Confirma
function ClickConfirmar() {
    //alert('click confirmar');
    OcultarDivs();
    document.getElementById('divConfirmar').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';


    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';
    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';

    //document.getElementById('divServicioIcon0').style.display = 'none';
    //document.getElementById('divServicioIconMovil0').style.display = 'none';
    //document.getElementById('divServicioIcon1').style.display = 'block';
    //document.getElementById('divServicioIconMovil1').style.display = 'block';
    
    document.getElementById('divEspecificoIcon0').style.display = 'none';
    document.getElementById('divEspecificoIconMovil0').style.display = 'none';
    document.getElementById('divEspecificoIcon1').style.display = 'block';
    document.getElementById('divEspecificoIconMovil1').style.display = 'block';

    document.getElementById('divConfirmarIcon0').style.display = 'none';
    document.getElementById('divConfirmarIconMovil0').style.display = 'none';
    document.getElementById('divConfirmarIcon1').style.display = 'block';
    document.getElementById('divConfirmarIconMovil1').style.display = 'block';

    cambiarTitleMovi("Confirmación");

    document.getElementById('previ').style.display = 'none';
}

function ApagarIconos() {
    
    document.getElementById('divVehiculoIcon0').style.display = 'block';
    document.getElementById('divVehiculoIconMovil0').style.display = 'block';
    document.getElementById('divVehiculoIcon1').style.display = 'none';
    document.getElementById('divVehiculoIconMovil1').style.display = 'none';

    //document.getElementById('divServicioIcon0').style.display = 'block';
    //document.getElementById('divServicioIconMovil0').style.display = 'block';
    //document.getElementById('divServicioIcon1').style.display = 'none';
    //document.getElementById('divServicioIconMovil1').style.display = 'none';

    document.getElementById('divCitaIcon0').style.display = 'block';
    document.getElementById('divCitaIconMovil0').style.display = 'block';
    document.getElementById('divCitaIcon1').style.display = 'none';
    document.getElementById('divCitaIconMovil1').style.display = 'none';

    document.getElementById('divEspecificoIcon0').style.display = 'block';
    document.getElementById('divEspecificoIconMovil0').style.display = 'block';
    document.getElementById('divEspecificoIcon1').style.display = 'none';
    document.getElementById('divEspecificoIconMovil1').style.display = 'none';

    document.getElementById('divConfirmarIcon0').style.display = 'block';
    document.getElementById('divConfirmarIconMovil0').style.display = 'block';
    document.getElementById('divConfirmarIcon1').style.display = 'none';
    document.getElementById('divConfirmarIconMovil1').style.display = 'none';

    document.getElementById('previ').style.display = 'block';
}

function readCookie(name) {

    var nameEQ = name + "=";
    var ca = document.cookie.split(';');

    for (var i = 0; i < ca.length; i++) {

        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) {
            return decodeURIComponent(c.substring(nameEQ.length, c.length));
        }
    }
    return null;

}

function changeColor(x) {
    if (x.style.background == "rgb(197, 34, 34)") {
        x.style.background = "#fff";
        x.style.color = "#000000";
    } else {
        x.style.background = "#C52222";
        x.style.color = "#fff";
    }
    return false;
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// GOOGLE PLACES
///////////////////////////////////////////////////////////////////////////////////////////////////////////////


//var map, places;
//var markers = [];
//var countryRestrict = { 'country': 'mx' };
//var MARKER_PATH = 'https://developers.google.com/maps/documentation/javascript/images/marker_green';

//function ActivarPlacesSearch() {
//    map = new google.maps.Map(document.getElementById('map'), {

//        mapTypeControl: false,
//        panControl: false,
//        zoomControl: false,
//        streetViewControl: false
//    });

//    var input = document.getElementById('places');
//    var autocomplete = new google.maps.places.Autocomplete(input, {
//        //types: ['(cities)']
//        types: ['(regions)'],
//        componentRestrictions: countryRestrict
//    });

//    places = new google.maps.places.PlacesService(map);

//    //Inicio AutocompleteService

//    google.maps.event.addListener(autocomplete, 'place_changed', function () {

//        var place = autocomplete.getPlace();
//        if (place.geometry) {
//            //console.log(place);
//            var displaySuggestions = function (predictions, status) {
               
//                if (status != google.maps.places.PlacesServiceStatus.OK) {
//                    clearResults()
//                    alert('No se encontraron resultados');
//                    return;
//                }
//                map.panTo(place.geometry.location);
//                map.setZoom(15);
//                clearResults();
//                clearMarkers();

//                predictions.forEach(function (predictions, i) {
                    
//                    var placeId = predictions.place_id;
//                    var markerLetter = String.fromCharCode('A'.charCodeAt(0) + (i % 26));
//                    var markerIcon = MARKER_PATH + markerLetter + '.png';

//                    var tr = document.createElement('tr');
//                    tr.style.backgroundColor = (i % 2 === 0 ? '#F0F0F0' : '#FFFFFF');
//                    tr.onclick = function () {
//                        // google.maps.event.trigger(markers[i], 'click');
//                    };
//                    var iconTd = document.createElement('td');
//                    var nameTd = document.createElement('td');
//                    var btnTd = document.createElement('td');

//                    var btn = document.createElement('button');
//                    btn.setAttribute('class', 'btn button');
//                    btn.textContent = 'Seleccionar';

//                    btn.onclick = function () {
//                        $.ajax({
//                            type: 'GET',
//                            url: '/Agencias/GetAgenciasByPlaceId/' + placeId,
//                            data: { placeId },
//                            complete: function () {
//                                // $.unblockUI();
//                                //SiguienteAgencias();

//                                document.getElementById('divFechaAgendamiento').style.display = 'block';
                               

//                             },
//                            success: function (result) {
//                               localStorage.setItem("AgenciaInfo", result);
//                                //console.log(localStorage.getItem("AgenciaInfo"));
//                            }
//                        });
//                    }

//                    var icon = document.createElement('img');
//                    icon.src = markerIcon;
//                    icon.setAttribute('class', 'placeIcon');
//                    icon.setAttribute('className', 'placeIcon');

//                    var tr = document.createElement('tr');
//                    iconTd.appendChild(icon);
//                    btnTd.appendChild(btn);
//                    tr.appendChild(iconTd);
//                    nameTd.appendChild(document.createTextNode(predictions.structured_formatting.main_text));
//                    tr.appendChild(nameTd);

//                    tr.appendChild(btnTd);
//                    document.getElementById('results').appendChild(tr);
//                });
//            };

//            var marcaVehiculo = document.getElementById("marcaVehiculo");
//            var marcaVehiculoText = marcaVehiculo.options[marcaVehiculo.selectedIndex].text;

//            var service = new google.maps.places.AutocompleteService();
//            service.getQueryPredictions({ input: marcaVehiculoText + ' Autocom ' + place.vicinity, types: ['establishment'] }, displaySuggestions);

//        } else {
//            document.getElementById('places').placeholder = 'Escribe tu ciudad';
//        }


//    });
//}

//function clearResults() {
//    var results = document.getElementById('results');
//    while (results.childNodes[0]) {
//        results.removeChild(results.childNodes[0]);
//    }
//}
//function clearMarkers() {
//    for (var i = 0; i < markers.length; i++) {
//        if (markers[i]) {
//            markers[i].setMap(null);
//        }
//    }
//    markers = [];
//}

function ValidarAgencia() {
    var fechaAgendamiento = $("#fechaAgendamiento").val();
    if (fechaAgendamiento == "") {
        toastr.warning("Por favor selecciona una fecha.");
        return false;
    }
    var marcaVehiculo = document.getElementById("horaAgendamiento");
    if (marcaVehiculo.selectedIndex == 0) {
        toastr.warning("Por favor selecciona un horario.");
        return false;
    }
    return true;
}
function ValidarRegistro() {
    var nombre_cliente = $("#nombre_cliente").val();
    if (nombre_cliente == "") {
        toastr.warning("Por favor escriba su nombre.");
        return false;
    }
    var apePat_cliente = $("#apePat_cliente").val();
    if (apePat_cliente == "") {
        toastr.warning("Por favor escriba su apellido paterno.");
        return false;
    }
    var apeMat_cliente = $("#apeMat_cliente").val();
    if (apeMat_cliente == "") {
        toastr.warning("Por favor escriba su apellido materno.");
        return false;
    }
    var tel_cliente = $("#tel_cliente").val();
    if (tel_cliente == "") {
        toastr.warning("Por favor escriba su telefono.");
        return false;
    }else {
        var expresionRegular1 = /^([0-9]+){9}$/;
        var expresionRegular2 = /\s/;

        if (expresionRegular2.test(tel_cliente.value)) {
            toastr.warning('error existen espacios en blanco en el campo Telefono');
            return false;
        }
        else if (expresionRegular1.test(tel_cliente.value)) {
            toastr.warning('Número de teléfono incorrecto');
            return false;
        }    
    }
    var email_cliente = $("#email_cliente").val();
    if (email_cliente == "") {
        toastr.warning("Por favor escriba su direccion de correo.");
        return false;
    } else {
        var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
        if (!regex.test($('#email_cliente').val().trim())) {
            toastr.warning('La direccón de correo no es válida');
            
            return false;
        }
    }
       
    
    if (document.getElementById('divDomicilio').style.display == 'block') {
       
        var calleCliente = $("#calleCliente").val();
        if (calleCliente == "") {
            toastr.warning("Por favor escriba la calle de su domicilio.");
            return false;
        }
        var colonia = $("#colonia").val();
        if (colonia == "") {
            toastr.warning("Por favor escriba la colonia de su domicilio.");
            return false;
        }
        var colonia = $("#colonia").val();
        if (colonia == "") {
            toastr.warning("Por favor escriba la colonia de su domicilio.");
            return false;
        }
        var num_ext = $("#num_ext").val();
        if (num_ext == "") {
            toastr.warning("Por favor escriba el número de su domicilio.");
            return false;
        }        
        var ciudad = $("#ciudad").val();
        if (ciudad == "") {
            toastr.warning("Por favor escriba la ciudad de su domicilio.");
            return false;
        }
        var cp = $("#cp").val();
        if (cp == "") {
            toastr.warning("Por favor escriba el Código postal de su domicilio.");
            return false;
        } else {
            var expresionRegular1 = /^([0-9]+){9}$/;
            var expresionRegular2 = /\s/;

            if (expresionRegular2.test(cp.value)) {
                toastr.warning('error existen espacios en blanco en el campo Código postal');
                return false;
            }
            else if (expresionRegular1.test(cp.value)) {
                toastr.warning('Código postal incorrecto');
                return false;
            }
        }
    }
    return true;
}
function ValidarVehiculo() {

    id_familiavehiculo = document.getElementById('id_familiavehiculo');
    if (id_familiavehiculo.value == "-1") {
        toastr.warning('Por favor seleccione su vehículo.');
        return false;
    }
    var anho_vehiculo = $("#anho_vehiculo").val();
    if (anho_vehiculo == "") {
        toastr.warning("Por favor escriba el año de su vehículo.");
        return false;
    } else {
        var expresionRegular1 = /^([0-9]+){9}$/;
        var expresionRegular2 = /\s/;

        if (expresionRegular2.test(anho_vehiculo.value)) {
            toastr.warning('error existen espacios en blanco en el campo año del vehículo');
            return false;
        }
        else if (expresionRegular1.test(anho_vehiculo.value)) {
            toastr.warning('Año incorrecto');
            return false;
        }
        var d = new Date();
        var n = d.getFullYear();
        if (anho_vehiculo > n + 1) {
            toastr.warning('Año incorrecto');
            return false;
        }
    }
    tipo_combustible = document.getElementById('tipo_combustible');
    if (tipo_combustible.value == "-1") {
        toastr.warning('Por favor seleccione el combustible de su vehículo.');
        return false;
    }
    
    var kilometrajeVehiculo = $("#kilometrajeVehiculo").val();
    if (kilometrajeVehiculo == "") {
        toastr.warning("Por favor escriba el kilometraje de su vehículo.");
        return false;
    } else {
        var expresionRegular1 = /^([0-9]+){9}$/;
        var expresionRegular2 = /\s/;

        if (expresionRegular2.test(kilometrajeVehiculo.value)) {
            toastr.warning('error existen espacios en blanco en el campo Kilometraje');
            return false;
        }
        else if (expresionRegular1.test(kilometrajeVehiculo.value)) {
            toastr.warning('Kilometraje incorrecto');
            return false;
        }
    }

    return true;
}

function validarServicio() {
    if (document.getElementById('chkSerMan').checked) {
        var servicios = document.getElementById('servicios');
        if (servicios.value == "-1") {
            toastr.warning('Por favor seleccione el servicio que realizará a su vehículo.');
            return false;
        }
    }
    if (document.getElementById('chkFallas').checked) {
        var fallas_mec = $('#fallas_mec').val();
        if (fallas_mec == "") {
            toastr.warning('Por favor describa la falla de su vehículo.');
            return false;
        }
    }
    if (document.getElementById('chkEspecificos').checked) {
        var checksVal = $('[name="checks[]"]:checked').map(function () {
            return this.value;
        }).get();

        console.log(typeof (checksVal));

        if (checksVal.length == 0) {
            toastr.warning('Por favor seleccione los servicios especificos que se le realizarán a su vehículo.');
            return false;
        }
    }
    return true;
}


function cambiarTitleMovi(modulo) {
    var s = ' <div class="col-md-6" style="padding-top:15px">';
    s += '<H4><strong><label style="color:#C10230">'+ modulo +'</label></H4></strong>';
    s += '<di>';
    $('#titleMovil').html(s);
}

