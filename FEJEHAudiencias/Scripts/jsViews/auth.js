window.onload = function () {
    var autenticado = getCookie('usuarioAutenticado');
    if (!autenticado || autenticado != '0') {
        window.location.href = "Login.aspx";
    }
}

//CODIGO JOSUE MODAL INICIO
document.addEventListener("DOMContentLoaded", function () {
    mostrarInfoCookies();
});

// Función para mostrar la información de las cookies 

function mostrarInfoCookies() {
    // Obtener los valores de las cookies
    var departamentoUsuario = getCookie('departamentoUsuario');
    var nombreUsuario = getCookie('nombreUsuario');
    var curpUsuario = getCookie('curpUsuario');
    var emailUsuario = getCookie('emailUsuario');


    // Actualizar los elementos de la modal con los valores de las cookies
    document.getElementById('nombreUsuarioS').innerText = departamentoUsuario;
    document.getElementById('distrito').innerText = nombreUsuario;
    document.getElementById('cargo').innerText = curpUsuario;
    document.getElementById('curpUsuario').innerText = emailUsuario;

}

// Función para obtener el valor de una cookie por su nombre
function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(";").shift());
}
//CODIGO JOSUE MODAL FINAL


var temporizadorSesion;

function iniciarTemporizadorInactividad() {
    // Si el temporizador ya está corriendo, lo limpiamos para reiniciar el conteo
    if (temporizadorSesion) {
        clearTimeout(temporizadorSesion);
    }

    // Establecemos el temporizador para cerrar sesión después de 20 minutos
    temporizadorSesion = setTimeout(function () {
        cerrarSesion();
    }, 20 * 60  * 1000); // 20 minutos
}

function cerrarSesion() {
    // Aquí colocas lo que necesitas para cerrar sesión
    deleteCookie('usuarioAutenticado');
    window.location.href = "Login.aspx";
}

// Eventos para detectar actividad del usuario
document.addEventListener('mousemove', iniciarTemporizadorInactividad);
document.addEventListener('keydown', iniciarTemporizadorInactividad);

// Iniciamos el temporizador la primera vez
iniciarTemporizadorInactividad();

