function calculaRFC() {
    function quitaArticulos(palabra) {
        return palabra.replace("DEL ", "").replace("LAS ", "").replace("DE ",
            "").replace("LA ", "").replace("Y ", "").replace("A ", "");
    }
    function esVocal(letra) {
        if (letra == 'A' || letra == 'E' || letra == 'I' || letra == 'O'
            || letra == 'U' || letra == 'a' || letra == 'e' || letra == 'i'
            || letra == 'o' || letra == 'u')
            return true;
        else
            return false;
    }
    nombre = $("#nombre_cliente").val().toUpperCase();
    apellidoPaterno = $("#apellido_paterno").val().toUpperCase();
    apellidoMaterno = $("#apellido_materno").val().toUpperCase();
    fecha = $("#fecha_nacimiento").val();
    var rfc = "";
    apellidoPaterno = quitaArticulos(apellidoPaterno);
    apellidoMaterno = quitaArticulos(apellidoMaterno);
    rfc += apellidoPaterno.substr(0, 1);
    var l = apellidoPaterno.length;
    var c;
    for (i = 0; i < l; i++) {
        c = apellidoPaterno.charAt(i);
        if (esVocal(c)) {
            rfc += c;
            break;
        }
    }
    rfc += apellidoMaterno.substr(0, 1);
    rfc += nombre.substr(0, 1);
    rfc += fecha.substr(8, 10);
    rfc += fecha.substr(3, 5).substr(0, 2);
    rfc += fecha.substr(0, 2);
    // rfc += "-" + homclave;
    $("#rfc").val(rfc);
}