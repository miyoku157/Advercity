using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	Transform target=null;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine (checkObject());
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 destination=Vector3.zero;
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			target=getMousePosition();
			if (target.gameObject != null) {
				//to do
			}
		}else if (Input.GetKeyDown(KeyCode.Mouse1)){
			destination=getMousePosition().position;
			if(destination!=Vector3.zero){
				//todo
			}
		}

	}
	private IEnumerator checkObject(){
		RaycastHit Hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray,out Hit, 100)) {// modifier la distance finalel
			if(Hit.collider.tag!="Map"){
				Hit.collider.gameObject.GetComponent<Renderer>().material.color=Color.red;
			}
		}
		yield return new WaitForSeconds(0.25f);
	}
	public static Transform getMousePosition(){
		RaycastHit Hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray,out Hit, 100)) {// modifier la distance finalel
			if(Hit.collider.tag=="Map"){
				Transform ret=null;
				ret.position=Hit.point;
				return ret;
			}else if(Hit.collider.tag=="Item"){
				return Hit.collider.transform;
			}else if (Hit.collider.tag=="Being"){
				return Hit.collider.transform;
			}
		}
		return null;
	}
	
}

