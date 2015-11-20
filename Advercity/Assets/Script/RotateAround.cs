using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{
	public Transform target;
	Transform newTarget;

	public float distance;
	public float xSpeed = 2.5f;
	public float ySpeed = 2.5f;
	
	public float yMinLimit = -89f;
	public float yMaxLimit = 89f;
	
	public float distanceMin = 0.5f;
	public float distanceMax = 15f;
	
	public float smoothTime = 2f;
	
	float rotationYAxis = 0.0f;
	float rotationXAxis = 0.0f;
	
	float rotatingVelocityX = 0.0f;
	float rotatingVelocityY = 0.0f;

	bool isChangingTarget = false;
	bool simpleClick = false;

	float timer;
	float delay = 0.5f;

	RaycastHit hit;
	Ray ray;

	Vector3 AimedPosition;
	Quaternion AimedRotation;
	Transform StartingPosition;

	float totalDistancetoGo = 0.0f;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start()
	{
		Vector3 angles = transform.eulerAngles;
		rotationYAxis = angles.y;
		rotationXAxis = angles.x;
		
		// Make the rigid body not change rotation
		if(GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
	
	void LateUpdate()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(!simpleClick)
			{
				simpleClick = true;
				timer = Time.time;
			}
			else
			{
				simpleClick = false;

				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if(Physics.Raycast(ray, out hit, 500))
				{
					newTarget = hit.transform.gameObject.GetComponent<Transform>();
					AimedPosition = new Vector3(0f, 0f, 0f);
					rotationXAxis = ClampAngle (rotationXAxis, yMinLimit, yMaxLimit);
					
					Quaternion toRotation2 = Quaternion.Euler (rotationXAxis, rotationYAxis, 0);
					Quaternion rotation2 = toRotation2;

					Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
					Vector3 position2 = rotation2 * negDistance + newTarget.position;

					AimedPosition = position2;
					StartingPosition = transform;
					isChangingTarget = true;
				}
			}
		}

		if(simpleClick)
		{
			// if the time now is delay seconds more than when the first click started. 
			if((Time.time - timer) > delay)
			{
				simpleClick = false;
			}
		}

		if (target && !isChangingTarget)
		{
			if (Input.GetMouseButton(0))
			{
				rotatingVelocityX += xSpeed * Input.GetAxis ("Mouse X") * distance * 0.02f;
				rotatingVelocityY += ySpeed * Input.GetAxis ("Mouse Y") * distance * 0.02f;
			}
			
			rotationYAxis += rotatingVelocityX;
			rotationXAxis -= rotatingVelocityY;

			rotationXAxis = ClampAngle (rotationXAxis, yMinLimit, yMaxLimit);

			Quaternion toRotation = Quaternion.Euler (rotationXAxis, rotationYAxis, 0);
			Quaternion rotation = toRotation;

			distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 3, distanceMin, distanceMax);


			// Empecher la caméra de se placer derrière un objet
			/*RaycastHit hit;
			if (Physics.Linecast(target.position, transform.position, out hit))
			{
				distance -= hit.distance;
			}*/

			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			transform.rotation = rotation;
			transform.position = position;

			rotatingVelocityX = Mathf.Lerp(rotatingVelocityX, 0, Time.deltaTime * smoothTime);
			rotatingVelocityY = Mathf.Lerp(rotatingVelocityY, 0, Time.deltaTime * smoothTime);
		}
		else if(isChangingTarget)
		{
			SmoothTranslation(StartingPosition, AimedPosition);
		}
	}
	
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	private bool SmoothTranslation(Transform start, Vector3 targetPosition)
	{
		float distanceToGo = Vector3.Distance(transform.position, targetPosition);

		if(totalDistancetoGo == 0.0f)
		{
			totalDistancetoGo = Vector3.Distance(start.position, targetPosition);
		}
		else
		{
			if(distanceToGo > totalDistancetoGo / 100 * 10)
			{
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1.0f);
			}
			else if(distanceToGo > 0)
			{
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * totalDistancetoGo/8);
			}
			else
			{
				totalDistancetoGo = 0.0f;
				isChangingTarget = false;
				target = newTarget;
			}
		}

		return false;
	}
}
