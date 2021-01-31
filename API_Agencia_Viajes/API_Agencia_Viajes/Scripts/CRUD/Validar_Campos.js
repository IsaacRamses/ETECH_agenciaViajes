


function validarNro(e) { //Solo permite teclear numeros en el campo
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) //Firefox/Opera
    {
        key = e.which;
    }
if (key < 48 || key > 57) {
        if (key == 8) // Detectar . (punto) y backspace (retroceso)
        { return true; }
        else
        { return false; }
    }
    return true;
}


function soloLetras(e) { //Solo permite escribir letras en el campo
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = [8, 37, 39, 46, 6, 44, 45];
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
       // alert('Tecla no aceptada');
        return false;
    }
}
