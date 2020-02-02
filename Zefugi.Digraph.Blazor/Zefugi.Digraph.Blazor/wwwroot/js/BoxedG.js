function getBBox(obj) {
    var b = obj.getBBox();
    var str = b.x + "," + b.y + "," + b.width + "," + b.height;
    return str;
}