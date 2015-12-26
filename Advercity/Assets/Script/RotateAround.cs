using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour{
	
	public float speed = 5.0f;
	public GameObject terrain;
	public GameObject xRotator;
	private float terrainHeight;
	private float t = 0.0f;
	private Vector2 mouseClickPosition;
	
	private void moveRight(){
		if(Input.mousePosition.x > Screen.width * 0.95f || Input.GetKey(KeyCode.RightArrow)){
			this.transform.Translate(Vector3.right * this.speed * Time.deltaTime);
			this.getTerrainHeight();
		}
	}
	
	private void moveLeft(){
		if(Input.mousePosition.x < Screen.width * 0.05f || Input.GetKey(KeyCode.LeftArrow)){
			this.transform.Translate(-Vector3.right * this.speed * Time.deltaTime);
			this.getTerrainHeight();
		}
	}
	
	private void moveForward(){
		if(Input.mousePosition.y < Screen.height * 0.05f || Input.GetKey(KeyCode.DownArrow)){
			this.transform.Translate(-Vector3.up * this.speed * Time.deltaTime);
			this.getTerrainHeight();
		}
	}
	
	private void moveBackward(){
		if(Input.mousePosition.y > Screen.height * 0.95f || Input.GetKey(KeyCode.UpArrow)){
			this.transform.Translate(Vector3.up * this.speed * Time.deltaTime);
			this.getTerrainHeight();
		}
	}
	
	private void getTerrainHeight(){
		RaycastHit hit;
		this.terrain.layer = 0;
		// Get terrain y value at current position.
		if(Physics.Raycast(new Vector3(this.transform.position.x, 300, this.transform.position.z), -Vector3.up, out hit, 500)){
			this.terrainHeight = hit.point.y;
		}
		this.terrain.layer = 2;
	}
	

	
	private void rotateCamera(){
		if(Input.GetMouseButtonDown(1)){
			this.mouseClickPosition = Input.mousePosition;
		}
		if(Input.GetMouseButton(1)){
			Vector2 delta = this.mouseClickPosition - (Vector2)Input.mousePosition;
			this.mouseClickPosition = Input.mousePosition;
			this.transform.Rotate(Vector3.up * -this.speed * delta.x * Time.deltaTime);
			this.xRotator.transform.Rotate(Vector3.right * this.speed * delta.y * Time.deltaTime);
			this.xRotator.transform.rotation =Quaternion.Euler( ClampAngle(this.xRotator.transform.rotation.eulerAngles.x, 0.0f, 40.0f),this.xRotator.transform.rotation.eulerAngles.y,this.xRotator.transform.rotation.eulerAngles.z);
		}
	}
	
	private float ClampAngle(float angle, float min, float max) {
		if (angle<90 || angle>270){       // if angle in the critic region...
			if (angle>180) angle -= 360;  // convert all angles to -180..+180
			if (max>180) max -= 360;
			if (min>180) min -= 360;
		}   
		angle = Mathf.Clamp(angle, min, max);
		if (angle<0) angle += 360;  // if angle negative, convert to 0..360
		return angle;
	}
	
	public void Update(){
		if(!Input.GetMouseButton(1)){
			this.moveRight();
			this.moveLeft();
			this.moveForward();
			this.moveBackward();
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	
	public void LateUpdate(){
		this.rotateCamera();
	}
}