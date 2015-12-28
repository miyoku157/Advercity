using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour{
	
	public float speed = 5.0f;
	public GameObject terrain;
	public GameObject xRotator;
	private float terrainHeight;
	private Vector2 mouseClickPosition;
	
	private void moveRight(){
		if(Input.mousePosition.x > Screen.width * 0.95f || Input.GetKey(KeyCode.RightArrow)){
			this.transform.Translate(Vector3.right * this.speed * Time.deltaTime);
		}
	}
	
	private void moveLeft(){
		if(Input.mousePosition.x < Screen.width * 0.05f || Input.GetKey(KeyCode.LeftArrow)){
			this.transform.Translate(-Vector3.right * this.speed * Time.deltaTime);
		}
	}
	
	private void moveForward(){
		if(Input.mousePosition.y < Screen.height * 0.05f || Input.GetKey(KeyCode.DownArrow)){
			this.transform.Translate(-Vector3.up * this.speed * Time.deltaTime);
		}
	}
	
	private void moveBackward(){
		if(Input.mousePosition.y > Screen.height * 0.95f || Input.GetKey(KeyCode.UpArrow)){
			this.transform.Translate(Vector3.up * this.speed * Time.deltaTime);
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
		this.moveRight();
		this.moveLeft();
		this.moveForward();
		this.moveBackward();
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	

}