var context;

window.onload = function () {
    draw_canvas = document.getElementById("draw_canvas");
    draw_canvas.width = 500;
    draw_canvas.height = 500;
    context = draw_canvas.getContext('2d');

    // 선 굵기
    context.lineWidth = 4;
    context.strokeStyle = "black";

    context.fillStyle = '#fff';
    context.fillRect(0, 0, draw_canvas.width, draw_canvas.height);

    draw_canvas.addEventListener("mousemove", function (e) { move(e) });
    draw_canvas.addEventListener("mousedown", function (e) { down(e) });
    draw_canvas.addEventListener("mouseup", function (e) { up(e) });
    draw_canvas.addEventListener("mouseout", function (e) { out(e) });
}

// 처음 마우스가 눌려진 좌표
var startX = 0, startY = 0;
// 드래깅
var painting = false;

// 선 그리기
function draw(curX, curY) {
    context.beginPath();
    context.moveTo(startX, startY);
    context.lineTo(curX, curY);
    context.stroke();
}

function move(e) {
    if (!painting) { return; }
    var curX = e.offsetX, curY = e.offsetY;
    draw(curX, curY);

    startX = curX; startY = curY;
}

function down(e) {
    startX = e.offsetX;
    startY = e.offsetY;
    painting = true;
}
function up(e) { painting = false; }
function out(e) { painting = false; }