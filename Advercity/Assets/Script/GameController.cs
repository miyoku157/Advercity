using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace AssemblyCSharp
{
    public class GameController : MonoBehaviour
    {
        static GameObject controller;
        public static List<Being>[] Units;
        public static GameObject selecttarget;
        public static GameObject oldSelectTarget;
        GameObject attObj;
        GameObject oldObject = null;
        // Use this for initialization
        void Awake()
        {
            attObj = null;
            selecttarget = null;
            controller = this.gameObject;
            StartCoroutine(checkObject());

            //Temporaire
            Units = new List<Being>[2];
            Units[0] = new List<Being>();
            Units[1] = new List<Being>();
            Units[0].Add(GameObject.Find("ninja").GetComponent<Being>());
            Units [1].Add (GameObject.Find ("Cube (1)").GetComponent<Being>());
        }

        // Update is called once per frame
        void Update()
        {

            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    selecttarget = getMousePosition();
                    if (selecttarget != GameController.controller)
                    {
                        if (selecttarget != null)
                        {
                            if (oldSelectTarget != null)
                            {
								if(oldSelectTarget.tag=="Being"){
                                	oldSelectTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("_Outline", 0.005f);
								}
								}
                            oldSelectTarget = selecttarget;
							if(oldSelectTarget.tag=="Being"){
                            	oldSelectTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_OutlineColor", Color.green);
                            	oldSelectTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("_Outline", 0.005f);
							}
							selecttarget = null;
                        }
                        //to do
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attObj = getMousePosition();
                    if (attObj != null && oldSelectTarget != null)
                    {
                        if (attObj != controller && !oldSelectTarget.GetComponent<Being>().isAttacking && attObj != oldSelectTarget && attObj.tag == "Being")
                        {
                            oldSelectTarget.GetComponent<Being>().isAttacking = true;
                            oldSelectTarget.GetComponent<Being>().isCollecting = false;
                            oldSelectTarget.GetComponent<Being>().launchAttack(attObj);

                        }
                        else if (attObj.layer == LayerMask.NameToLayer("Resources") && !oldSelectTarget.GetComponent<Being>().isCollecting)
                        {
                            oldSelectTarget.GetComponent<Being>().isCollecting = true;
                            oldSelectTarget.GetComponent<Being>().isAttacking = false;
                            oldSelectTarget.GetComponent<Being>().launchCollect(attObj);

                        }
                        else if (controller != null && attObj.layer != LayerMask.NameToLayer("Resources"))
                        {
                            if (oldSelectTarget.GetComponent<Being>())
                            {
                                oldSelectTarget.GetComponent<Being>().isAttacking = false;
                                oldSelectTarget.GetComponent<Being>().isCollecting = false;
                                oldSelectTarget.GetComponent<Being>().move(controller.transform.position);
                                //todo
                            }
                        }
                    }
                }
            }
        }
        private IEnumerator checkObject()
        {
            RaycastHit Hit;
            for (;;)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (oldObject != null)
                {
                    if (oldObject.layer == LayerMask.NameToLayer("Resources"))
                    {
                        GameObject parent = oldObject.transform.parent.gameObject;
                        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
                        int lenght = renderers.GetLength(0);
                        for (int i = 0; i < lenght; i++)
                        {
                            renderers[i].material.color = Color.white;
                        }
                    }
                    else {
                        if (oldObject != oldSelectTarget)
                        {
                            oldObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("_Outline", 0.000f);
                        }
                    }
                }
                if (Physics.Raycast(ray, out Hit, 100))
                {// modifier la distance finalel

                    if (Hit.collider.gameObject.layer == LayerMask.NameToLayer("Resources"))
                    {
                        GameObject parent = Hit.collider.transform.parent.gameObject;
                        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
                        int lenght = renderers.GetLength(0);
                        for (int i = 0; i < lenght; i++)
                        {
                            renderers[i].material.color = Color.red;
                        }
                        oldObject = Hit.collider.gameObject;

                    }
                    else if (Hit.collider.tag != "Map")
                    {

                        if (Hit.collider.gameObject != oldSelectTarget)
                        {
                            Hit.collider.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_OutlineColor", Color.red);
                            Hit.collider.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("_Outline", 0.005f);
                            oldObject = Hit.collider.gameObject;
                        }
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        public static GameObject getMousePosition()
        {
            RaycastHit Hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out Hit, 1000))
            {// modifier la distance finale
                if (Hit.collider.tag == "Map")
                {
                    GameController.controller.transform.position = Hit.point;
                    return GameController.controller.gameObject;
                }
                else if (Hit.collider.tag != "Ennemy")
                {
                    return Hit.collider.gameObject;
                }
            }
            return null;
        }

    }
}
