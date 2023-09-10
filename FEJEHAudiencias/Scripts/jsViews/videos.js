document.getElementById('video').oncontextmenu = function (e) {
    e.preventDefault();
};

$(document).ready(function () {
    var video = document.getElementById("video");
    var source = video.querySelector("source#mp4");

    source.src = decodeURIComponent('<%= VideoUrl %>');
    video.load();
});
