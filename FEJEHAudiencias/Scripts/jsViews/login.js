var dragFile = document.getElementById('drag-file'); //input flie - certificado
var nameFile = document.getElementById('name-file');
var FCertificado = document.getElementById('FCertificado');

// Agregar controladores de eventos para el drag and drop
dragFile.addEventListener('dragstart', function (e) {
    e.dataTransfer.setData('text/plain', 'file');
});

dragFile.addEventListener('dragover', function (e) {
    e.preventDefault();
});

dragFile.addEventListener('drop', function (e) {
    e.preventDefault();
    var files = e.dataTransfer.files;
    if (files.length === 1) {
        var fileName = files[0].name;
        nameFile.textContent = fileName;
    }
});

// Agregar controlador de evento para el botón de archivo
FCertificado.addEventListener('change', function (e) {
    var files = FCertificado.files;
    if (files.length > 0) {
        var fileName = files[0].name;
        nameFile.textContent = fileName;
    }
});

//drag and drop for clave privada
var dragFileClv = document.getElementById('drag-file-2'); // input file cleve privada
var nameFileClv = document.getElementById('name-file-2');
var FLlavePrivada = document.getElementById('FLlavePrivada');

dragFileClv.addEventListener('dragstart', function (e) {
    e.dataTransfer.setData('text/plain', 'file');
});

dragFileClv.addEventListener('dragover', function (e) {
    e.preventDefault();
});

dragFileClv.addEventListener('drop', function (e) {
    var filesClv = e.dataTransfer.files;
    if (filesClv.length === 1) {
        var fileNameClv = filesClv[0].name;
        nameFileClv.textContent = fileNameClv;
    }
});

// Agregar controlador de evento para el botón de archivo
FLlavePrivada.addEventListener('change', function (e) {
    var filesClv = FLlavePrivada.files;
    if (filesClv.length > 0) {
        var fileNameClv = filesClv[0].name;
        nameFileClv.textContent = fileNameClv;
    }
});

// buttom's hover in dragBorder's hover class
var dragBorder = document.querySelector('.dragBorder');
// Obtener el elemento <label> dentro del elemento con la clase "dragBG"
var label = dragBorder.querySelector('.dragBG label');
// Agregar el evento de "mouseover" al elemento "dragBorder"
dragBorder.addEventListener('mouseover', function () {
    // Aplicar los estilos al elemento <label> al hacer hover en "dragBorder"
    label.style.backgroundColor = '#3983c5';
    label.style.color = '#fff';
});

// Agregar el evento de "mouseout" al elemento "dragBorder"
dragBorder.addEventListener('mouseout', function () {
    // Restablecer los estilos del elemento <label> al quitar el hover en "dragBorder"
    label.style.backgroundColor = '';
    label.style.color = '';
});

function disableEnterKey(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        return false;
    }
}


//ir a pestaña multimedia 
var buttonIrTecnicoMultimedia = document.getElementById("irLoginMultimedia").addEventListener('click', () => {
    window.location.href = "Loginmultimedia.aspx";
});