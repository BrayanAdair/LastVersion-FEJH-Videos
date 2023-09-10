document.addEventListener("DOMContentLoaded", function () {
    mostrarContenido('contenido-magistrados', document.querySelector('.activeColorSidebar'));
});

function mostrarContenido(contenidoId, elemento) {
    var contenidos = document.querySelectorAll('.contenido-pantalla');
    for (var i = 0; i < contenidos.length; i++) {
        contenidos[i].style.display = 'none';
    }

    var contenido = document.getElementById(contenidoId);
    if (contenido) {
        contenido.style.display = 'block';
    }

    var enlaces = document.querySelectorAll('.list-group-item');
    for (var i = 0; i < enlaces.length; i++) {
        enlaces[i].classList.remove('active');
    }

    elemento.classList.add('active');

}

