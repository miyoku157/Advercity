var speed = 1.0;
var moveSize = Vector3.one;
private var basePosition : Vector3;

function Start ()
{
	basePosition = transform.localPosition;	
}

function Update ()
{
	var offset = Random.GetVector3(speed);
	offset -= Vector3 (0.5, 0.5, 0.5);
	offset = Vector3.Scale(moveSize, offset);
	transform.localPosition = offset + basePosition;
}

@script AddComponentMenu("Noise/Transform Position")