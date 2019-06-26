

function Validar(contactName, contactMail, contactPhone, brandId, versionId, vehicleYear, vehiclePlate, labours, workshopId, plannedDate, plannedTime) {
    contactName = document.getElementById(contactName);    
    if (contactName.value == "") {
        toastr.warning("El campo Nombre esta vacio");
        contactName.focus();
        return false;
    }

    contactMail = document.getElementById(contactMail);
    if (contactMail.value == "") {
        toastr.warning("El campo Email esta vacio");
        contactMail.focus();
        return false;
    }
    else {
        var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
        if (!regex.test($('#contactMail').val().trim())) {
            
        //} else {
            toastr.warning('La direccón de correo no es válida');
            contactMail.focus();
            return false;
        }
    }
    contactPhone = document.getElementById(contactPhone);
    if (contactPhone == "") {
        toastr.warning("El campo Telefono esta vacio");
        contactPhone.focus();
        return false;
    } else {
        var expresionRegular1 = /^([0-9]+){9}$/;
        var expresionRegular2 = /\s/;

        if (expresionRegular2.test(contactPhone.value)) {
            toastr.warning('error existen espacios en blanco en el campo Telefono');
            contactPhone.focus();
            return false;
        }
        else if (!expresionRegular1.test(contactPhone.value)) {
            toastr.warning('Numero de telefono incorrecto');
            contactPhone.focus();
            return false;
        }    
    }
    brandId = document.getElementById(brandId);
    if (brandId.value == "-1") {
        toastr.warning('Por favor seleccione una marca.');
        brandId.focus();
        return false;
    }
    versionId = document.getElementById(versionId);
    if (versionId.value == "-1") {
        toastr.warning('Por favor seleccione un Modelo.');
        versionId.focus();
        return false;
    }
    vehicleYear = document.getElementById(vehicleYear);
    if (vehicleYear.value == "") {
        toastr.warning("El campo Año esta vacio");
        vehicleYear.focus();
        return false;
    } else {
        var year = new Date().getFullYear()
        var yearMax = year + 1
        var yearMin = yearMax - 15

        if (vehicleYear.value > yearMax) {
            toastr.warning("El año de tu automovil no debe de ser mayor a " + yearMax);
            vehicleYear - focus();
            return false;
        }
        if (vehicleYear.value < yearMin) {
            toastr.warning("El año de tu automovil no debe de ser menor a " + yearMin);
            vehicleYear - focus();
            return false;
        }
    }


    /*vehiclePlate = document.getElementById(vehiclePlate);
    if (vehiclePlate.value == "") {
        toastr.warning("El campo Placa / VIN esta vacio");
        vehiclePlate.focus();
        return false;
    }*/
    labours = document.getElementById(labours);
    if (labours.value == "-1") {
        toastr.warning('Por favor seleccione un Paquete de Servicio.');
        labours.focus();
        return false;
    }
    workshopId = document.getElementById(workshopId);
    if (workshopId.value == "-1") {
        toastr.warning('Por favor seleccione un Taller.');
        workshopId.focus();
        return false;
    }
    plannedDate = document.getElementById(plannedDate);
    if (plannedDate.value == "") {
        toastr.warning("Por favor seleccione una Fecha");
        plannedDate.focus();
        return false;
    }
    plannedTime = document.getElementById(plannedTime);
    if (plannedTime.value == "-1") {
        toastr.warning('Por favor seleccione un Horario.');
        plannedTime.focus();
        return false;
    }
         
}
function ValidarHome(idHome) {
    idHome = document.getElementById(idHome);
    if (idHome.value == "") {
        toastr.warning("Por favor agregue el id de su cita agendada con anterioridad.");
        idHome.focus();
        return false;
    }
}



////////////////////////////////////////////////////////////////////

function DeshabilitarControles() {
    $('#contactName').attr('disabled', false);
    $('#contactMail').attr('disabled', false);
    $('#contactPhone').attr('disabled', false);
    $('#brandId').attr('disabled', false);
    $('#versionId').attr('disabled', false);
    $('#vehicleYear').attr('disabled', false);
    $('#vehiclePlate').attr('disabled', false);
    $('#labours').attr('disabled', false);
    $('#workshopId').attr('disabled', false);
}


