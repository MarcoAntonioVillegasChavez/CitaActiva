﻿/*'#contactMail').click(function () {

    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;

    if (regex.test($('#contactMail').val().trim())) {
        
    } else {
        alert('La direccón de correo no es válida');
    }
});*/
///////////////////////////////////////////////////////////////////////////////////////////
/*
jQuery(function ($) {
    $(".btn").click(function () {

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
        })

    });
});
*/

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