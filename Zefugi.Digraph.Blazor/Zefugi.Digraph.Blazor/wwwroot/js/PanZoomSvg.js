var pzsIsDragging = false;

function pzsMouseDown() {
    pzsIsDragging = true;
    console.log("panZoomSvg_mouseDown");
}

function pzsMouseUp() {
    pzsIsDragging = false;
    console.log("panZoomSvg_mouseUp");
}

function pzsMouseMove() {
    if (pzsIsDragging) {

        console.log("panZoomSvg_mouseMove");
    }
}

function pzsMouseWheel() {
    console.log("panZoomSvg_mouseWheel");
}