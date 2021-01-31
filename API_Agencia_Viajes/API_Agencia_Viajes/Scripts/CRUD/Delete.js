
function Fun_Viajeros_Delete(IDViajero,Nombre) {
  
    var confir = confirm("Esta a punto de Eliminar a el Viajero " + Nombre + ", al eliminarlo se perderan los viajes que tenga asociados ¿Está seguro que desea eliminar definitivamente a este viajero");
    if (confir) {

        $.ajax
        ({
            data: JSON.stringify({
                id: IDViajero,
            }),
            contentType: 'application/json; charset=utf-8',
            url: "/API_Agencia_Viajes/Viajeros/Delete",
            type: "POST",
            dataType: 'json',
            success: function (data) {
                if (data.Validar) {
                    alert("Viajero eliminado Correctamente");
                    location.reload();
                }
                else {
                    alert(data.Mensaje);
                }
                
            },
            error: function (data) {
                alert("Ocurrió un error al intentar eliminar a el viajero.");
            }
        });
    }
}

function Fun_Viajes_Disponibles_Delete (IDViaje,Codigo,Origen,Destino)
{

    var confir = confirm("Esta a punto de Eliminar el Viaje " + Codigo + " con Origen en "+ Origen + " y destino en " + Destino +", al eliminarlo los viajeros que tengan dicho viaje se perderan ¿Está seguro que desea eliminar definitivamente este Viaje");
    if (confir) {

        $.ajax
        ({
            data: JSON.stringify({
                id: IDViaje,
            }),
            contentType: 'application/json; charset=utf-8',
            url: "/API_Agencia_Viajes/Viajes_Disponibles/Delete",
            type: "POST",
            dataType: 'json',
            success: function (data) {
                if (data.Validar) {
                    alert("Viaje eliminado Correctamente");
                    location.reload();
                }
                else {
                    alert(data.Mensaje);
                }

            },
            error: function (data) {
                alert("Ocurrió un error al intentar eliminar el viaje.");
            }
        });
    }
}

function Fun_Viajes_Delete(IDViajero_Viaje,Nombre,Origen,Destino) {

    var confir = confirm("Esta a punto de Eliminar a el viaje seleccionado (" +Origen +"-"+ Destino+") del Viajero "+Nombre+", ¿Está seguro que desea eliminar definitivamente a este viaje?");
    if (confir) {

        $.ajax
        ({
            data: JSON.stringify({
                id: IDViajero_Viaje,
            }),
            contentType: 'application/json; charset=utf-8',
            url: "/API_Agencia_Viajes/SolicitarViaje/Delete",
            type: "POST",
            dataType: 'json',
            success: function (data) {
                if (data.Validar) {
                    alert("Viaje eliminado Correctamente");
                    location.reload();
                }
                else {
                    alert(data.Mensaje);
                }

            },
            error: function (data) {
                alert("Ocurrió un error al intentar eliminar el viaje.");
            }
        });
    }
}

