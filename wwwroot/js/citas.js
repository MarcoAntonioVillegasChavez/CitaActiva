//Mostrar datos de usuario si está firmado
$(document).ready(function () {
    var cuenta_personal = readCookie("cliente");
    var username = readCookie("nombreCliente");

    //console.log(cuenta_personal);
    //console.log(username);

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
                    //console.log(resultClientes);
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

    //Ocultar divs de los iconos de los servicios
    document.getElementById('divServicioIcon0').style.display = 'block';
    document.getElementById('divServicioIconMovil0').style.display = 'block';

    //Ocultar divs de los iconos de la cita
    document.getElementById('divCitaIcon0').style.display = 'block';
    document.getElementById('divCitaIconMovil0').style.display = 'block';
    //Ocultar divs de los iconos de la confirmación
    document.getElementById('divConfirmarIcon0').style.display = 'block';
    document.getElementById('divConfirmarIconMovil0').style.display = 'block';
});

function OcultarDivs() {
   //Ocultar divs 
    document.getElementById('divAgencias').style.display = 'none';
    document.getElementById('divVehiculo').style.display = 'none';
    document.getElementById('divServicio').style.display = 'none';
    document.getElementById('divCita').style.display = 'none';
    document.getElementById('divConfirmar').style.display = 'none';
}


function SiguienteAgencias() {
    OcultarDivs();
    document.getElementById('divVehiculo').style.display = 'block';

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';

    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

    AgenciaSeleccionada();
}

function AtrasVehiculo() {
    OcultarDivs();
    document.getElementById('divAgencias').style.display = 'block';

    document.getElementById('divVehiculoIcon1').style.display = 'none';
    document.getElementById('divVehiculoIconMovil1').style.display = 'none';

    document.getElementById('divVehiculoIcon0').style.display = 'block';
    document.getElementById('divVehiculoIconMovil0').style.display = 'block';
}

function SiguienteVehiculo() {
    OcultarDivs();
    document.getElementById('divServicio').style.display = 'block';

    document.getElementById('divServicioIcon0').style.display = 'none';
    document.getElementById('divServicioIconMovil0').style.display = 'none';

    document.getElementById('divServicioIcon1').style.display = 'block';
    document.getElementById('divServicioIconMovil1').style.display = 'block';

    KmSeleccionado();

}

function AtrasServicio() {
    OcultarDivs();
    document.getElementById('divVehiculo').style.display = 'block';

    document.getElementById('divServicioIcon1').style.display = 'none';
    document.getElementById('divServicioIconMovil1').style.display = 'none';

    document.getElementById('divServicioIcon0').style.display = 'block';
    document.getElementById('divServicioIconMovil0').style.display = 'block';
}


function SiguienteServicio(){
    OcultarDivs();
    document.getElementById('divCita').style.display = 'block';

    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';

    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';
    
}

function AtrasCita() {
    OcultarDivs();
    document.getElementById('divServicio').style.display = 'block';

    document.getElementById('divCitaIcon1').style.display = 'none';
    document.getElementById('divCitaIconMovil1').style.display = 'none';

    document.getElementById('divCitaIcon0').style.display = 'block';
    document.getElementById('divCitaIconMovil0').style.display = 'block';

}
function SiguienteCita() {
    OcultarDivs();
    document.getElementById('divConfirmar').style.display = 'block';
    
    document.getElementById('divConfirmarIcon0').style.display = 'none';
    document.getElementById('divConfirmarIconMovil0').style.display = 'none';

    document.getElementById('divConfirmarIcon1').style.display = 'block';
    document.getElementById('divConfirmarIconMovil1').style.display = 'block';
         
    PreConfirmacion();
}

function AtrasConfirmar() {
    OcultarDivs();
    document.getElementById('divCita').style.display = 'block';

    document.getElementById('divConfirmarIcon1').style.display = 'none';
    document.getElementById('divConfirmarIconMovil1').style.display = 'none';

    document.getElementById('divConfirmarIcon0').style.display = 'block';
    document.getElementById('divConfirmarIconMovil0').style.display = 'block';
}


function Confirmar() {

}


function PreConfirmacion() {
    var parts = $("#fechaAgendamiento").val().split('-');
    var fecha = new Date(parts[2], parts[1] - 1, parts[0]);

    var lblDia = '<label id="lblConfirmDia" > ';
    lblDia += parts[0].toString();
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

    var m = Month[fecha.getMonth()];
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
    lblPreConfrServ += 'Servicio de ' + serviciosText + ' Paquete ';
    lblPreConfrServ += ' </label >';

    var lblPreConfrHr = '<label id="lblPreConfrHr" > ';
    lblPreConfrHr += horaAgendamientoText;
    lblPreConfrHr += ' </label >';

    $('#lblConfirmDia').html(lblDia);
    $('#lblConfirmNomDia').html(lblDiaSeman);
    $('#lblConfirmMes').html(lblConfirmMes);

    $('#lblPreConfrAgencia').html(lblPreConfrAgencia);
    $('#lblPreConfrVehi').html(lblPreConfrVehi);
    $('#lblPreConfrServ').html(lblPreConfrServ);
    $('#lblPreConfrHr').html(lblPreConfrHr);
    

}
    
function AgenciaSeleccionada() {
    
    //var agenciaId = document.getElementById("agenciaId");
    //var agenciaIdText = agenciaId.options[agenciaId.selectedIndex].text;
    //var Valores = "Agendando cita en " + $("#agenciaId option:selected").val() ;

    var agencia = JSON.parse(localStorage.getItem("AgenciaInfo"));
    console.log(agencia);
    var agenciaText = agencia[0].nombre_agencia;//document.getElementById("places").value();

    var s = '<label id="lblGeneral" > ';
    s += 'Agendando cita en ' + agenciaText;
    s += ' </label >';

    $('#lblGeneral').html(s);
  
}
function KmSeleccionado() {    
    var serviciosId = document.getElementById("servicios");
    var serviciosText = serviciosId.options[serviciosId.selectedIndex].text;

    var s = '<label id="lblKm" > ';
    s += ' El Servicio y Mantenimiento  son basados sobre el kilometraje estimado de ' + serviciosText + ' kilómetros';
    s += ' </label >';

    $('#lblKm').html(s);
    
}

// Wizard de imagenes
function ClickAgencias() {
    //alert('click agencias');
    OcultarDivs();

    document.getElementById('divAgencias').style.display = 'block';
    ApagarIconos();
}

function ClickVehiculos() {
    //alert('click vehiculor');
    OcultarDivs();
    document.getElementById('divVehiculo').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';
}

function ClickServicios() {
    //alert('click servicios');
    OcultarDivs();
    document.getElementById('divServicio').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

    document.getElementById('divServicioIcon0').style.display = 'none';
    document.getElementById('divServicioIconMovil0').style.display = 'none';
    document.getElementById('divServicioIcon1').style.display = 'block';
    document.getElementById('divServicioIconMovil1').style.display = 'block';
}

function ClikCitas() {
    //alert('click citas');
    OcultarDivs();
    document.getElementById('divCita').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

    document.getElementById('divServicioIcon0').style.display = 'none';
    document.getElementById('divServicioIconMovil0').style.display = 'none';
    document.getElementById('divServicioIcon1').style.display = 'block';
    document.getElementById('divServicioIconMovil1').style.display = 'block';

    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';
    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';
}

function ClickConfirmar() {
    //alert('click confirmar');
    OcultarDivs();
    document.getElementById('divConfirmar').style.display = 'block';
    ApagarIconos();

    document.getElementById('divVehiculoIcon0').style.display = 'none';
    document.getElementById('divVehiculoIconMovil0').style.display = 'none';
    document.getElementById('divVehiculoIcon1').style.display = 'block';
    document.getElementById('divVehiculoIconMovil1').style.display = 'block';

    document.getElementById('divServicioIcon0').style.display = 'none';
    document.getElementById('divServicioIconMovil0').style.display = 'none';
    document.getElementById('divServicioIcon1').style.display = 'block';
    document.getElementById('divServicioIconMovil1').style.display = 'block';

    document.getElementById('divCitaIcon0').style.display = 'none';
    document.getElementById('divCitaIconMovil0').style.display = 'none';
    document.getElementById('divCitaIcon1').style.display = 'block';
    document.getElementById('divCitaIconMovil1').style.display = 'block';

    document.getElementById('divConfirmarIcon0').style.display = 'none';
    document.getElementById('divConfirmarIconMovil0').style.display = 'none';
    document.getElementById('divConfirmarIcon1').style.display = 'block';
    document.getElementById('divConfirmarIconMovil1').style.display = 'block';
}


function ApagarIconos() {
    
    document.getElementById('divVehiculoIcon0').style.display = 'block';
    document.getElementById('divVehiculoIconMovil0').style.display = 'block';
    document.getElementById('divVehiculoIcon1').style.display = 'none';
    document.getElementById('divVehiculoIconMovil1').style.display = 'none';

    document.getElementById('divServicioIcon0').style.display = 'block';
    document.getElementById('divServicioIconMovil0').style.display = 'block';
    document.getElementById('divServicioIcon1').style.display = 'none';
    document.getElementById('divServicioIconMovil1').style.display = 'none';

    document.getElementById('divCitaIcon0').style.display = 'block';
    document.getElementById('divCitaIconMovil0').style.display = 'block';
    document.getElementById('divCitaIcon1').style.display = 'none';
    document.getElementById('divCitaIconMovil1').style.display = 'none';

    document.getElementById('divConfirmarIcon0').style.display = 'block';
    document.getElementById('divConfirmarIconMovil0').style.display = 'block';
    document.getElementById('divConfirmarIcon1').style.display = 'none';
    document.getElementById('divConfirmarIconMovil1').style.display = 'none';
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


///////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// GOOGLE PLACES
///////////////////////////////////////////////////////////////////////////////////////////////////////////////


var map, places;
var markers = [];
var countryRestrict = { 'country': 'mx' };
var MARKER_PATH = 'https://developers.google.com/maps/documentation/javascript/images/marker_green';

function ActivarPlacesSearch() {
    map = new google.maps.Map(document.getElementById('map'), {

        mapTypeControl: false,
        panControl: false,
        zoomControl: false,
        streetViewControl: false
    });

    var input = document.getElementById('places');
    var autocomplete = new google.maps.places.Autocomplete(input, {
        //types: ['(cities)']
        types: ['(regions)'],
        componentRestrictions: countryRestrict
    });

    places = new google.maps.places.PlacesService(map);

    //Inicio AutocompleteService

    google.maps.event.addListener(autocomplete, 'place_changed', function () {

        var place = autocomplete.getPlace();
        if (place.geometry) {
            //console.log(place);
            var displaySuggestions = function (predictions, status) {
               
                if (status != google.maps.places.PlacesServiceStatus.OK) {
                    clearResults()
                    alert('No se encontraron resultados');
                    return;
                }
                map.panTo(place.geometry.location);
                map.setZoom(15);

                //console.log(predictions);

                clearResults();
                clearMarkers();

                predictions.forEach(function (predictions, i) {
                    
                    var placeId = predictions.place_id;
                    var markerLetter = String.fromCharCode('A'.charCodeAt(0) + (i % 26));
                    var markerIcon = MARKER_PATH + markerLetter + '.png';

                    var tr = document.createElement('tr');
                    tr.style.backgroundColor = (i % 2 === 0 ? '#F0F0F0' : '#FFFFFF');
                    tr.onclick = function () {
                        // google.maps.event.trigger(markers[i], 'click');
                    };
                    var iconTd = document.createElement('td');
                    var nameTd = document.createElement('td');
                    var btnTd = document.createElement('td');

                    var btn = document.createElement('button');
                    btn.setAttribute('class', 'btn btn-primary');
                    btn.textContent = 'Seleccionar';

                    btn.onclick = function () {
                        $.ajax({
                            type: 'GET',
                            url: '/Agencias/GetAgenciasByPlaceId/' + placeId,
                            data: { placeId },
                            complete: function () {
                                // $.unblockUI();
                                SiguienteAgencias();
                            },
                            success: function (result) {
                               localStorage.setItem("AgenciaInfo", result);
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
                    nameTd.appendChild(document.createTextNode(predictions.structured_formatting.main_text));
                    tr.appendChild(nameTd);

                    tr.appendChild(btnTd);
                    document.getElementById('results').appendChild(tr);
                });
            };

            var marcaVehiculo = document.getElementById("marcaVehiculo");
            var marcaVehiculoText = marcaVehiculo.options[marcaVehiculo.selectedIndex].text;

            var service = new google.maps.places.AutocompleteService();
            service.getQueryPredictions({ input: marcaVehiculoText + ' Autocom ' + place.vicinity, types: ['establishment'] }, displaySuggestions);

        } else {
            document.getElementById('places').placeholder = 'Escribe tu ciudad';
        }


    });
}

function clearResults() {
    var results = document.getElementById('results');
    while (results.childNodes[0]) {
        results.removeChild(results.childNodes[0]);
    }
}
function clearMarkers() {
    for (var i = 0; i < markers.length; i++) {
        if (markers[i]) {
            markers[i].setMap(null);
        }
    }
    markers = [];
}