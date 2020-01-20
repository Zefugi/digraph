let view = document.getElementById('view');
let viewRect = view.getBoundingClientRect();

const MOUSE_EVENT_PRIMARY_PHASE = 2;

let viewBoxX = 0;
let viewBoxY = 0;
let viewBoxZ = 100 * (viewRect.width / 100);
let viewBoxMinZ = viewBoxZ;
let viewBoxMaxZ = viewBoxZ * 3;
let viewBoxYZDivider = viewRect.width / viewRect.height;
const VIEWBOX_INCREMENTS = viewBoxZ / 10;

let isDragging = false;
let dragOffsetX, dragOffsetY;

function mouseDown()
{
    // Validate that the current target is the view/svg element.
    if(event.eventPhase != MOUSE_EVENT_PRIMARY_PHASE)
        return;
    if(event.currentTarget.id != "view")
        return;

    // Store the current state.
    isDragging = true;
    dragOffsetX = event.clientX;
    dragOffsetY = event.clientY;
}

function mouseUp()
{
    isDragging = false;
}

function mouseMove()
{
    if(isDragging)
    {
        let deltaX = event.clientX - dragOffsetX;
        let deltaY = event.clientY - dragOffsetY;

        viewBoxX += deltaX;
        viewBoxY += deltaY;

        dragOffsetX = event.clientX;
        dragOffsetY = event.clientY;

        view.setAttribute("viewBox", -viewBoxX + " " + -viewBoxY + " " + viewBoxZ + " " + (viewBoxZ / viewBoxYZDivider));
    }
}

function mouseWheel()
{
    if(event.deltaY < 0)
    {
        viewBoxZ -= VIEWBOX_INCREMENTS;
        if(viewBoxZ < viewBoxMinZ)
        {
            viewBoxZ = viewBoxMinZ;
            return;
        }
    }
    else if(event.deltaY > 0)
    {
        viewBoxZ += VIEWBOX_INCREMENTS;
        if(viewBoxZ > viewBoxMaxZ)
        {
            viewBoxZ = viewBoxMaxZ;
            return;
        }
    }

    /* Get the mouse pointer coordinates relative to the view/svg object. */
    viewX = (event.clientX - viewRect.left) * (viewBoxZ / viewBoxMinZ);
    viewY = (event.clientY - viewRect.top) * (viewBoxZ / viewBoxMinZ);
    
    view.setAttribute("viewBox", -viewBoxX + " " + -viewBoxY + " " + viewBoxZ + " " + (viewBoxZ / viewBoxYZDivider));
}


class Node
{
    constructor (svgRoot, title, x, y)
    {
        this.svgRoot = svgRoot;
        this.title = title;
        this.x = x;
        this.y = y;

        this.svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    
        this.nodeBG = document.createElementNS("http://www.w3.org/2000/svg", "rect");
        this.svg.appendChild(this.nodeBG);
    
        this.nodeTitle = document.createElementNS("http://www.w3.org/2000/svg", "text");
        this.nodeTitle.textContent = this.title;
        this.svg.appendChild(this.nodeTitle);
    
        svgRoot.appendChild(this.svg);
    
        this.update();

        this.nodeBG.addEventListener('mousedown', this.mouseDown);
        this.nodeTitle.addEventListener('mousedown', this.mouseDown);
    }

    update()
    {
        this.svg.setAttributeNS(null, "x", this.x);
        this.svg.setAttributeNS(null, "y", this.y);

        this.nodeBG.setAttributeNS(null, "width", 100);
        this.nodeBG.setAttributeNS(null, "height", 100);
        this.nodeBG.setAttributeNS(null, "fill", "#529fca");

        this.nodeTitle.setAttributeNS(null, "x", 10);
        this.nodeTitle.setAttributeNS(null, "y", 20);
        this.nodeTitle.setAttributeNS(null, "stroke", "white");
        this.nodeTitle.textContent = this.title;

        this.nodeBG.setAttributeNS(null, "width", 20 + this.nodeTitle.getBBox().width);
    }

    selectStart()
    {
            return false;
    }

    mouseDown(e)
    {
        console.log("Down : " + e.currentTarget);
    }
}

var node = new Node(view, "Node Text", 50, 50);
