using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
	public class GameController : MonoBehaviour
	{
	    static GameObject controller;
	    GameObject selecttarget;
		GameObject attObj;
	    GameObject oldObject = null;
	    // Use this for initialization
	    void Start()
	    {
			attObj = null;
	        selecttarget = null;
	        controller = this.gameObject;
	        StartCoroutine(checkObject());
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        if (Input.GetKeyDown(KeyCode.Mouse0))
	        {
	            selecttarget = getMousePosition();
	            if (selecttarget != null)
	            {
	                //to do
	            }
	        }
	        else if (Input.GetKeyDown(KeyCode.Mouse1))
	        {
	            attObj=getMousePosition();
				if(selecttarget != null)
				{
					if(attObj != controller)
					{
						// Modifier attack pour qu'il créer une coroutine dans Being
						selecttarget.GetComponent<Being>().launchAttack(attObj);
					}
					else if (controller != null)
		            {
		                if (selecttarget.GetComponent<Being>())
		                {
							selecttarget.GetComponent<Being>().move(controller.transform.position);
		                    //todo
		                }
		            }
				}
	        }

	    }
	    private IEnumerator checkObject()
	    {
	        RaycastHit Hit;
	        for (; ; )
	        {
	            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	            if (oldObject != null)
	            {
	                oldObject.GetComponent<Renderer>().material.color = Color.white;
	            }
	            if (Physics.Raycast(ray, out Hit, 100))
	            {// modifier la distance finalel
	                if (Hit.collider.tag != "Map")
	                {
	                    Hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
	                    oldObject = Hit.collider.gameObject;
	                }
	            }
	            yield return new WaitForSeconds(0.15f);
	        }
	    }
	    public static GameObject getMousePosition()
	    {
	        RaycastHit Hit;
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out Hit, 1000))
	        {// modifier la distance finalel
	            if (Hit.collider.tag == "Map")
	            {
	                GameController.controller.transform.position = Hit.point;
	                return GameController.controller.gameObject;
	            }
	            else
	            {
	                return Hit.collider.gameObject;
	            }
	        }
	        return null;
	    }

	}
}
