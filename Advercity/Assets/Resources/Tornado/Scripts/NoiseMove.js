var speed = 1.0;
var moveSize = Vector3.one;
var basePosition : Vector3;

function Start()
{
	basePosition = transform.localPosition;	
}

function Update()
{
    var offset = new Vector3();
	offset -= Vector3 (0.5, 0.5, 0.5);
	offset = Vector3.Scale(moveSize, offset);
	transform.localPosition = offset + basePosition;
}