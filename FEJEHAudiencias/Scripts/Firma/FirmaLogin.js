function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + encodeURIComponent(value) + expires + "; path=/";
}

function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(";").shift());
}

function deleteCookie(name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT; path=/;';
}

var detallesGlobales = null;


function firmaLogeo() {
    var password = $("[id*='TxtClaveAcceso']").val();
    if (password) {
        firma.validateKeyPairs(password, function (objResult) {
            if (objResult.state == 0) {
                firma.decodeCertificate(true, function (objDetalles) {
                    if (objDetalles.state == 0) {
                        detallesGlobales = objDetalles;

                        // Establece la cookie usuarioAutenticado
                        setCookie('usuarioAutenticado', objDetalles.state, 1);

                        // Guardar detalles del usuario en cookies
                        setCookie('nombreUsuario', objDetalles.subjectName, 1);
                        setCookie('emailUsuario', objDetalles.subjectEmail, 1);
                        setCookie('curpUsuario', objDetalles.subjectCURP, 1);
                        setCookie('departamentoUsuario', objDetalles.issuerDepartament, 1);

                        console.log("Datos almacenados en cookies:");

                        window.location.href = "Busqueda.aspx";
                    } else {
                        console.log("Error en objDetalles:", objDetalles);
                    }
                });
            } else {
                $('#modalUsuario').modal('show');
            }
        });
    } else {
        alert("Falta especificar contraseña");
    }
}