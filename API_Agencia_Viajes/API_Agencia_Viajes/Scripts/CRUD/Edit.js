$(document).ready(function () { });

function Edit(Control, ID) // Ingresa el Nombre del Controlador para redirigir al metodo adecuado 
{
    var div = $("#Modal"); // este es el div id, debe estar al final de cada vista donde llamemos a esta función
    div.empty(); // limpia lo que había anteriormente

    switch (Control) {

        case "Viajeros":
            div.load("/API_Agencia_Viajes/Viajeros/Edit?id=" + ID); // seleciona la vista que se mostrará en el popup
            popupDiv(div, "Editar Viajero", 350, 220); // describe parámetros que recibirá la función popupDiv
            div.dialog('open'); // permite que al hacer click en el botón, se abra el popup
            break; 
        case "Viajes_Disponibles":
            div.load("/API_Agencia_Viajes/Viajes_Disponibles/Edit?id=" + ID);
            popupDiv(div, "Editar Viajero", 350, 220);
            div.dialog('open'); 
            break;
        case "SolicitarViaje":
            div2.load("/SAT_Estandar/Configuracion/Edit?id_configuracion=" + ID);
            popupDiv(div2, "Editar Configuración", 450, 500);
            div2.dialog('open');
            break;
        default:
            location.reload();
            break;
    }
}

function popupDiv(div, titleDiv, w, h) // se crea una función llamada popupDiv que recibe parámetros
{
    div.dialog
    ({
        autoOpen: false, // se usa para que el popup tenga tiempo para cargar completamente
        width: 'auto', // asigna el ancho con el parámetro que recibió
        title: titleDiv, // asigna el título con el parámetro que recibió
        height: 'auto', // asigna el alto con el parámetro que recibió
        modal: true, // evita que se pueda hacer click en algo que esté debajo del popup
        stack: true, // permite que exista un popup sobre otro
        draggable: true, // permite que se pueda arrastrar el popup
        resizable: false, // permite que se pueda ajustar el tamaño del popup
        show: { effect: "drop" }, //ejecuta el efecto para mostrar el popup
        hide: { effect: "fade" }, //ejecuta el efecto para cerrar el popup
        position: { my: 'center-120', at: 'top+200' },
        buttons: // crea los botones en jquery
        {
    }
});
}



function Fun_Viajeros_Edit(IDViajero) {

    var Nombre_ = $("#Nombre").val();
    var Direccion_ = $("#Direccion").val();
    var Telefono_ = $("#Telefono").val();

    if (Nombre_ != "" && Direccion_ != "" && Telefono_ != "") {
        $.ajax
        ({
            data: JSON.stringify({
                id:IDViajero,
                nombre: Nombre_,
                direccion: Direccion_,
                telefono: Telefono_
            }),
            contentType: 'application/json; charset=utf-8',
            url: "/API_Agencia_Viajes/Viajeros/Edit",
            type: "POST",
            dataType: 'json',
            success: function (data) {
                if (data.Validar) {
                    alert("Se ha modificado los datos del Viajero Exitosamente");
                    location.reload();
                }
                else {
                    alert(data.Mensaje);
                }

            },
            error: function (data) {
                alert("Ocurrió un error al intentar actualizar la información viajero");
            }
        });
    }
    else {
        if (Direccion_ == "") {
            $("#Direccion").addClass("Resaltar_Campo")
        }
        else {
            $("#Direccion").removeClass("Resaltar_Campo")
        }
        if (Nombre_ == "") {
            $("#Nombre").addClass("Resaltar_Campo")
        } else {
            $("#Nombre").removeClass("Resaltar_Campo")
        }
        if (Telefono_ == "") {
            $("#Telefono").addClass("Resaltar_Campo")
        } else {
            $("#Telefono").removeClass("Resaltar_Campo")
        }
        alert("Por favor Introduzca los datos requeridos")

    }
}

