/*function mostrarError() {
    alert(mensajeDeError);
}*/

async function manejarErrorApi(resp) {
    let mensajeError = '';

    if (resp.status === 400) {
        mensajeError = await resp.text();
    } else if (resp.status === 404) {
        mensajeError = recursoNoEncontrado;
    } else {
        mensajeError = errorInesperado;
    }

    mostrarMensajeError(mensajeError);
}

function mostrarMensajeError(mensaje) {
    Swal.fire({
        icon: 'error',
        title: 'Error...',
        text: mensaje
    });

}