$(document).ready(function () {

});

function Create(Control) // Ingresa el Nombre del Controlador para redirigir al metodo adecuado
{
    var div = $("#Modal"); // este es el div id, debe estar al final de cada vista donde llamemos a esta función
    div.empty(); // limpia lo que había anteriormente

    switch (Control) {

        case "Viajeros":
            div.load("/API_Agencia_Viajes/Viajeros/Create"); // seleciona la vista que se mostrará en el popup
            popupDiv(div,"Registrar Viajero", 350, 220); // describe parámetros que recibirá la función popupDiv
            div.dialog('open'); // permite que al hacer click en el botón, se abra el popup
            break;
        case "Viajes_Disponibles":
            div.load("/API_Agencia_Viajes/Viajes_Disponibles/Create"); 
            popupDiv(div, "Registrar Viaje", 350, 220); 
            div.dialog('open'); 
            break;
        case "SolicitarViaje":
            div.load("/API_Agencia_Viajes/SolicitarViaje/Create");
            popupDiv(div, "Solicitar Viaje", 350, 220);
            div.dialog('open');
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
        show: { effect: "drop" },
        hide: { effect: "fade" },
        position: { my: 'center-120', at: 'top+200' }, //Centro el Modal en pantalla
        buttons: // crea los botones en jquery
        {}
    });
}

function Fun_Viajeros_Create() {

    var Cedula_ = $("#Cedula").val();
    var Nombre_ = $("#Nombre").val();
    var Direccion_ = $("#Direccion").val();
    var Telefono_ = $("#Telefono").val();

    if (Cedula_ != "" && Nombre_ != "" && Telefono_ != "") { //Verifico si los campos estan vacios
        $.ajax
            ({
                data: JSON.stringify({
                    cedula: Cedula_,
                    nombre: Nombre_,
                    direccion: Direccion_,
                    telefono: Telefono_

                }),
                contentType: 'application/json; charset=utf-8',
                url: "/API_Agencia_Viajes/Viajeros/Create",
                type: "POST",
                dataType: 'json',
                success: function (data) {

                    if (data.Validar) {
                        alert("Se ha registrado el Viajero Exitosamente");
                        location.reload();
                    }
                    else {
                        alert(data.Mensaje);//Muestra el error enviado del contralador
                    }

                },
                error: function (data) {
                    alert("Ocurrió un error al intentar registrar al viajero");
                }
            });
    } else {
                if (Cedula_ == "") {
                    $("#Cedula").addClass("Resaltar_Campo") //Agrego la clase para resaltar el campo vacio
                }
                else {
                    $("#Cedula").removeClass("Resaltar_Campo") // Remuevo la clase para dejar de resaltar el campo lleno
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

function BuscarCedula()
{
    var Cedula_ = $("#Cedula").val();
    
    if (Cedula_ != "") {

        $.ajax
            ({
                data: JSON.stringify({
                    Cedula: Cedula_
                }),
                contentType: 'application/json; charset=utf-8',
                url: "/API_Agencia_Viajes/SolicitarViaje/BuscarViajero",
                type: "POST",
                dataType: 'json',
                success: function (data) {

                    if (data.Validar) {
                        alert(data.Mensaje)
                        $("#ID").val(data.ID)
                    }
                    else {
                        alert(data.Mensaje);//Muestra el error enviado del contralador
                    }

                },
                error: function (data) {
                    alert("Ocurrió un error al intentar buscar al viajero");
                }
            });
    }
}

function Fun_SolicitarViaje_Create() {

    var Cedula_ = $("#Cedula").val();
    var Viaje_ = $("#Viajes").val();
    var ID_ = $("#ID").val();

    
    

    if (Cedula_ != "" && Viaje_ != "" && Viaje_ != "-- Seleccione --") { //Verifico si los campos estan vacios
        $.ajax
            ({
                data: JSON.stringify({
                    IDViajero:ID_,
                    Cedula: Cedula_,
                    IDViaje: Viaje_
                }),
                contentType: 'application/json; charset=utf-8',
                url: "/API_Agencia_Viajes/SolicitarViaje/Create",
                type: "POST",
                dataType: 'json',
                success: function (data) {

                    if (data.Validar) {
                        alert("Se ha registrado este viaje al viajero");
                        location.reload();
                    }
                    else {
                        alert(data.Mensaje);//Muestra el error enviado del contralador
                    }

                },
                error: function (data) {
                    alert("Ocurrió un error al intentar registrar al viajero");
                }
            });
    } else {
        if (Cedula_ == "") {
            $("#Cedula").addClass("Resaltar_Campo") //Agrego la clase para resaltar el campo vacio
        }
        else {
            $("#Cedula").removeClass("Resaltar_Campo") // Remuevo la clase para dejar de resaltar el campo lleno
        }
        if (Viaje_ == "" || Viaje_ == "-- Seleccione --") {
            $("#Viajes").addClass("Resaltar_Campo")
        } else {
            $("#Viajes").removeClass("Resaltar_Campo")
        }
        alert("Por favor Introduzca los datos requeridos")
    }
}






