// Variables globales para el control del temporizador y el tiempo de inactividad
var tiempoInactivo = 100000000000000000; // 10 minutos (600,000 milisegundos)
var temporizadorInactivo;

function redireccionarALogin() {
    // Cambiar la URL a la página de login
    window.location.href = "Logout.aspx";
}

function reiniciarTemporizador() {
    // Si ya existe un temporizador, lo limpiamos y lo reiniciamos
    if (temporizadorInactivo) {
        clearTimeout(temporizadorInactivo);
    }
    temporizadorInactivo = setTimeout(redireccionarALogin, tiempoInactivo);
}

// Eventos de interacción para reiniciar el temporizador
document.onclick = reiniciarTemporizador;
document.onkeypress = reiniciarTemporizador;
document.onmousemove = reiniciarTemporizador; // Agregar onmousemove

// Iniciar el temporizador al cargar la página
reiniciarTemporizador();
