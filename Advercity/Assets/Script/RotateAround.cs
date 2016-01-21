using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour{
	
	public float speed = 5.0f;
	private float terrainHeight;
	private float dist=13.0f;
	private bool zoom = false;
	public void Start(){
		this.transform.position = GameObject.Find ("ninja").transform.position + new Vector3 (0, 50, 0);
	}
	private void moveRight(){
		if(Input.mousePosition.x > Screen.width * 0.98f || Input.GetKey(KeyCode.RightArrow)){
				this.transform.Translate(Vector3.right * this.speed * Time.deltaTime,Space.World);
		}
	}
	
	private void moveLeft(){
		if(Input.mousePosition.x < Screen.width * 0.02f || Input.GetKey(KeyCode.LeftArrow)){
			this.transform.Translate(-Vector3.right * this.speed * Time.deltaTime,Space.World);
		}
	}
	
	private void moveForward(){
		if(Input.mousePosition.y < Screen.height * 0.02f || Input.GetKey(KeyCode.DownArrow)){
			this.transform.Translate(-Vector3.forward * this.speed * Time.deltaTime,Space.World);
		}
	}
	
	private void moveBackward(){
		if(Input.mousePosition.y > Screen.height * 0.98f || Input.GetKey(KeyCode.UpArrow)){
			this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime,Space.World);
		}
	}
	private void getTerrainHeight(){
		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, this.transform.forward,out hit, 500,~((1<<9)|(1<<10)))) {
			this.terrainHeight=hit.point.y;
		}
	}
	private void adjustToTerrainHeight(){
		if (terrainHeight-dist  != this.terrainHeight) {
			this.transform.position = new Vector3 (this.transform.position.x, dist + terrainHeight, this.transform.position.z);
		}
		}
	private void zoomin(){
		if (!zoom) {
			this.transform.position = this.transform.position - new Vector3 (0, 20, 0);
			zoom=true;
			dist+=20;
		} else {
			this.transform.position = this.transform.position + new Vector3 (0, 20, 0);
			zoom=false;
			dist-=20;
		}
	}
	public void Update(){
		this.moveRight ();
		this.moveLeft ();
		this.moveForward ();
		this.moveBackward ();
		getTerrainHeight ();
		adjustToTerrainHeight ();
		this.transform.position = new Vector3 (Mathf.Clamp (transform.position.x, 5, 795), transform.position.y, Mathf.Clamp (transform.position.z, 5, 795));
		if (Input.GetKeyDown (KeyCode.Space)) {
			zoomin();
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	

}