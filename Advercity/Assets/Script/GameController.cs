using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace AssemblyCSharp
{
    public class GameController : MonoBehaviour
    {
        static GameObject controller;
        public static List<Being>[] Units;
		public static List<ResourcesManager> ressources;
        public static GameObject selecttarget;
        public static GameObject oldSelectTarget;
		public static Vector3[] spot;

        GameObject attObj;
        GameObject oldObject = null;
        // Use this for initialization
        void Awake()
        {
			spot = new Vector3[3];
			spot[0] = new Vector3(125, 41, 625);
			spot[1] = new Vector3(650, 38, 583);
			spot[2] = new Vector3(704.5f, 49.4f, 100);
			Units=new List<Being>[3];
			for (int i=0; i<3; i++) {
				Units[i]=new List<Being>();
			}
			int alea = Random.Range (0, 3);
			List<int> iteger = new List<int> ();
			iteger.Add(0);
			iteger.Add (1);
				iteger.Add(2);
			int idcamp = 0;
			GameObject Batiment = Instantiate<GameObject> (UnityEngine.Resources.Load<GameObject> ("Prefabs/HQ"));
			Batiment.GetComponent<Building_manager> ().Idcamp = idcamp;
			idcamp++;
			Batiment.transform.position = spot [alea] + new Vector3 (10, 0, 0);
			GameObject player = Instantiate<GameObject> (UnityEngine.Resources.Load<GameObject> ("Prefabs/Player"));
			player.transform.position = spot [alea];
			NavMeshHit hit;
			if (NavMesh.SamplePosition (player.transform.position, out hit, 1.0f, NavMesh.AllAreas)) {
				player.transform.position=hit.position;
			}
			player.AddComponent<NavMeshAgent> ();
			player.GetComponent<NavMeshAgent> ().speed = 12;
			player.GetComponent<Being> ().city = Batiment;
			GameController.Units [0].Add (player.GetComponent<Being> ());
			GameObject[] compagnon = new GameObject[2];
			for (int i = 0; i < 2; i++) {
				compagnon [i] = Instantiate<GameObject> (UnityEngine.Resources.Load<GameObject> ("Prefabs/ninja"));
				compagnon [i].transform.position = spot [alea] - i * new Vector3 (10, 0, 10) + new Vector3 (5, 0, 5);
				compagnon [i].GetComponent<Being> ().city = Batiment;
				GameController.Units [0].Add (compagnon [i].GetComponent<Being> ());
			}
			iteger.Remove (alea);
			for (int i = 0; i < 2; i++) {
				 alea= Random.Range(0,2);
				if(iteger.Count==0){
					alea=iteger[0];
				}else{
				alea=iteger[alea];
				}
				Batiment = Instantiate<GameObject> (UnityEngine.Resources.Load<GameObject> ("Prefabs/HQ"));
				Batiment.transform.position = spot [alea];
				Batiment.GetComponent<Building_manager> ().Idcamp = idcamp;
				idcamp++;
				for (int j = 0; j < 4; j++) {
					GameObject ennemy = Instantiate<GameObject> (UnityEngine.Resources.Load<GameObject> ("Prefabs/Enemy"));
					ennemy.transform.position = spot [alea] + Mathf.Pow (-1, j) * new Vector3 (5 + j, 0, 5 + j);
					ennemy.GetComponent<Being> ().city = Batiment;
					GameController.Units [i + 1].Add (ennemy.GetComponent<Being> ());
				}
				
			}
            attObj = null;
            selecttarget = null;
			ressources = new List<ResourcesManager> ();
            controller = this.gameObject;
            StartCoroutine(checkObject());

            //Temporaire
            /*Units = new List<Being>[2];
            Units[0] = new List<Being>();
            Units[1] = new List<Being>();
            Units[0].Add(GameObject.Find("player").GetComponent<Being>());
			Units[0].Add(GameObject.Find("ninja").GetComponent<Being>());
			Units[0].Add(GameObject.Find("ninja (1)").GetComponent<Being>());
			Units[1].Add(GameObject.Find("Enemy (1)").GetComponent<Being>());
			Units[1].Add(GameObject.Find("Enemy (2)").GetComponent<Being>());
			Units[1].Add(GameObject.Find("Enemy").GetComponent<Being>());
			ressources.Add (GameObject.Find ("Forest").GetComponent<ResourcesManager>());
			ressources.Add (GameObject.Find ("Forest (1)").GetComponent<ResourcesManager>());
			ressources.Add (GameObject.Find ("Metal").GetComponent<ResourcesManager>());
			ressources.Add (GameObject.Find ("Water").GetComponent<ResourcesManager>());
			ressources.Add (GameObject.Find ("Crystal (1)").GetComponent<ResourcesManager>());
			ressources.Add (GameObject.Find ("Crystal").GetComponent<ResourcesManager>());
*/
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
						{if(selecttarget.layer==LayerMask.NameToLayer("Units")){
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
                        }
                        //to do
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attObj = getMousePosition();
                    if (attObj != null && oldSelectTarget != null)
                    {
                        if (attObj != controller && !oldSelectTarget.GetComponent<Being>().isAttacking && attObj != oldSelectTarget && attObj.tag == "Ennemy")
                        {
                            
                            oldSelectTarget.GetComponent<Being>().launchAttack(attObj);

                        }
                        else if (attObj.layer == LayerMask.NameToLayer("Resources") && !oldSelectTarget.GetComponent<Being>().isCollecting)
                        {
                            
                           
                            oldSelectTarget.GetComponent<Being>().launchCollect(attObj);

                        }
                        else if (controller != null && attObj.layer != LayerMask.NameToLayer("Resources"))
                        {
                            if (oldSelectTarget.GetComponent<Being>())
                            {
                                
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
						MeshRenderer[] renderM;
						if(renderers.Length==0){
							renderM=parent.GetComponentsInChildren<MeshRenderer>();
							int lenght = renderM.Length;
							for (int i = 0; i < lenght; i++)
							{
								renderM[i].material.color = Color.red;
							}
						}else{
	                        int lenght = renderers.GetLength(0);
	                        for (int i = 0; i < lenght; i++)
	                        {
	                            renderers[i].material.color = Color.red;
	                        }
						}
                        oldObject = Hit.collider.gameObject;

                    }
                    else if (Hit.collider.tag != "Map")
                    {

                        if (Hit.collider.gameObject != oldSelectTarget)
                        {
							if(Hit.collider.transform.childCount>1){
                            Hit.collider.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_OutlineColor", Color.red);
                            Hit.collider.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("_Outline", 0.005f);
                            oldObject = Hit.collider.gameObject;
							}
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
                else
                {
                    return Hit.collider.gameObject;
                }
            }
            return null;
        }

    }
}
