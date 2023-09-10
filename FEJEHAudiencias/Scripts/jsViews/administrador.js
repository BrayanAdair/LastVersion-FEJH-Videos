
document.addEventListener("DOMContentLoaded", function () {
    mostrarContenido('contenido-datos', document.querySelector('.activeColorSidebar'));
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



// funcion para los steps de SUBIR VIDEO
$(document).ready(function () {

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    $(".next").click(function () {

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $('.radio-group .radio').click(function () {
        $(this).parent().find('.radio').removeClass('selected');
        $(this).addClass('selected');
    });

    $(".submit").click(function () {
        return false;
    })

});
// FUNCION TOOGLE DESAPARECER DESPUES DE MANDARTE AL UNA PAGINA DE ADMINISTRADOR
document.addEventListener("DOMContentLoaded", function () {
    // Obtén todos los enlaces del menú lateral
    var links = document.querySelectorAll("#sidebarMenu .list-group-item");

    // Escucha el clic en cada enlace
    links.forEach(function (link) {
        link.addEventListener("click", function () {
            // Cierra el menú lateral
            var sidebar = document.getElementById("sidebarMenu");
            var bootstrapCollapse = new bootstrap.Collapse(sidebar, {
                toggle: false, // Evita que el menú se cierre automáticamente
            });
            bootstrapCollapse.hide(); // Oculta manualmente el menú
        });
    });
});



////////////////////////
//Eliminar evento readload del boton
let btnBuscarCausa = document.getElementById('BuscarButton').addEventListener('click', (e) => {
    e.preventDefault();
});
