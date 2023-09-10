//function verificarAutenticacion() {
//    var estadoAutenticacion = getCookie('usuarioAutenticado');
//    if (estadoAutenticacion) {
//        if (estadoAutenticacion == "0") {
//            console.log("El usuario está autenticado. Valor de la cookie: " + estadoAutenticacion);
//        } else {
//            console.log("El usuario no está autenticado. Valor de la cookie: " + estadoAutenticacion);
//        }
//    } else {
//        console.log("La cookie de autenticación no está disponible.");
//    }
//}



//document.addEventListener("DOMContentLoaded", function () {
//    try {
//        var nombreInput = document.querySelector('.input-nombre');
//        var emailInput = document.querySelector('.input-email');
//        var curpInput = document.querySelector('.input-curp');
//        var departamentoInput = document.querySelector('.input-departamento');

//        // Si los campos ya tienen datos, salir del script
//        if (nombreInput.value && emailInput.value && curpInput.value && departamentoInput.value) {
//            return;
//        }

//        var nombre = getCookie("nombreUsuario");
//        var email = getCookie("emailUsuario");
//        var curp = getCookie("curpUsuario");
//        var departamento = getCookie("departamentoUsuario");

//        // Llenar los inputs con los datos obtenidos
//        nombreInput.value = nombre;
//        emailInput.value = email;
//        curpInput.value = curp;
//        departamentoInput.value = departamento;

//        alert("Datos de cookies almacenados en los inputs con éxito.");
//    } catch (error) {
//        alert("Error en el script: " + error.message);
//    }
//});




function irvideo() {
    var radioButtons = document.querySelectorAll("input[name='videoRadio']");
    var videoSelected = false; // Variable para comprobar si algún video ha sido seleccionado

    for (var i = 0; i < radioButtons.length; i++) {
        if (radioButtons[i].checked) {
            var selectedVideoUrl = radioButtons[i].value;
            location.href = 'Videos.aspx?videoUrl=' + encodeURIComponent(selectedVideoUrl);
            videoSelected = true;
            break; // Importante: Salir del bucle después de encontrar el radio button seleccionado
        }
    }

    if (!videoSelected) {
        alert("Por favor, selecciona un video antes de continuar.");
    }
}