using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    static GameObject controller;
    GameObject selecttarget;
    GameObject oldObject = null;
    // Use this for initialization
    void Start()
    {
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
            getMousePosition();
            if (selecttarget != null && controller != null)
            {
                if (selecttarget.GetComponent<NavMeshAgent>())
                {
                    selecttarget.GetComponent<NavMeshAgent>().SetDestination(controller.transform.position);
                    //todo
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
            Debug.Log("Raycast");
            if (Hit.collider.tag == "Map")
            {
                Debug.Log("Map");
                GameController.controller.transform.position = Hit.point;
                return GameController.controller.gameObject;
            }
            else if (Hit.collider.tag == "Item")
            {
                return Hit.collider.gameObject;
            }
            else if (Hit.collider.tag == "Being")
            {
                return Hit.collider.gameObject;
            }
        }
        return null;
    }

}

