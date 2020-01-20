class SvgPanZoomBehaviour {
    constructor(svgTarget) {
        this.svgTarget = true;
        this.zoom = true;
        this.pan = true;
        this._isDragging = false;
        this._dragOffsetX = 0;
        this._dragOffsetY = 0;

        this.svgTarget.addEventListener('mousedown', this.mouseDown);
        this.svgTarget.addEventListener('mouseup', this.mouseDown);
        this.svgTarget.addEventListener('mousemove', this.mouseMove);
    }

    mouseDown() {
        this._isDragging = true;
        this._dragOffsetX = event.clientX;
        this._dragOffsetY = event.clientY;
        vb = this._svgTarget.getAttribute('viewBox');
        console.log(vb);
    }

    mouseUp() {
        this._isDragging = false;
    }

    mouseMove() {
        if (isDragging) {
            let deltaX = event.clientX - this._dragOffsetX;
            let deltaY = event.clientY - this._dragOffsetY;

            //viewBoxX += deltaX;
            //viewBoxY += deltaY;

            this._dragOffsetX = event.clientX;
            this._dragOffsetY = event.clientY;

            //view.setAttribute("viewBox", -viewBoxX + " " + -viewBoxY + " " + viewBoxZ + " " + (viewBoxZ / viewBoxYZDivider));
        }
    }
}