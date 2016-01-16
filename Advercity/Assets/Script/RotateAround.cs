using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour{
	
	public float speed = 5.0f;
	private float terrainHeight;
	private float t=0.0f;
	private void moveRight(){
		Vector3 pos=transform.position;
		if(Input.mousePosition.x > Screen.width * 0.95f || Input.GetKey(KeyCode.RightArrow)){
				this.transform.Translate(Vector3.right * this.speed * Time.deltaTime,Space.World);
		}
	}
	
	private void moveLeft(){
		if(Input.mousePosition.x < Screen.width * 0.05f || Input.GetKey(KeyCode.LeftArrow)){
			this.transform.Translate(-Vector3.right * this.speed * Time.deltaTime,Space.World);
		}
	}
	
	private void moveForward(){
		if(Input.mousePosition.y < Screen.height * 0.05f || Input.GetKey(KeyCode.DownArrow)){
			this.transform.Translate(-Vector3.forward * this.speed * Time.deltaTime,Space.World);
		}
	}
	
	private void moveBackward(){
		if(Input.mousePosition.y > Screen.height * 0.95f || Input.GetKey(KeyCode.UpArrow)){
			this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime,Space.World);
		}
	}
	private void getTerrainHeight(){
		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, this.transform.forward,out hit, 500)) {
			this.terrainHeight=hit.point.y;
		}
	}
	private void adjustToTerrainHeight(){
		if (terrainHeight-13  != this.terrainHeight) {
			this.transform.position = new Vector3 (this.transform.position.x, 13 + terrainHeight, this.transform.position.z);
		}
		}
	public void Update(){
		this.moveRight();
		this.moveLeft();
		this.moveForward();
		this.moveBackward();
		getTerrainHeight ();
		adjustToTerrainHeight ();
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	

}