using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace AssemblyCSharp{
public class IHM : MonoBehaviour
{
	public static Vector3[] spot;
		private GameObject caracterPanel;
	// Use this for initialization
	void Start ()
	{
		spot = new Vector3[3];
		spot [0] = new Vector3 (150, 150, 625);
		spot [1] = new Vector3 (650, 140, 583);
		spot [2] = new Vector3 (700, 150, 100);
			caracterPanel = GameObject.Find ("ImageCadre");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public void credits(){
			Application.LoadLevel ("Credits");	
	}
	public void launchGame(){
			Application.LoadLevel ("Main");
			int alea=Random.Range (0, 3);
			GameObject player=Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject> ("Prefabs/Player_character"));
			player.transform.position = spot [alea];
			GameController.Units [0].Add (player.GetComponent<Being> ());
			GameObject[] compagnon = new GameObject[2];
			for (int i=0;i<0;i++){
				compagnon[i]=Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/Compagnon"));
				compagnon[i].transform.position=spot[alea]-i*new Vector3(10,0,10)+new Vector3(5,0,5);
				GameController.Units[0].Add(compagnon[i].GetComponent<Being>());
			}
			GameObject Batiment= Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/Caravane"));
			Batiment.transform.position = spot [alea] + new Vector3 (15, 0, 0);
			for (int i=0;i<2;i++){
				int precedent=alea;
				alea=(precedent+1+Random.Range(0,2))%3;
				Batiment=Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/Caravane"));
				Batiment.transform.position=spot[alea];
				for (int j=0; j<4;j++){
					GameObject ennemy= Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/Ennemy"));
					ennemy.transform.position=spot[alea]+Mathf.Pow(-1,j)*new Vector3(5+j,0,5+j);
					GameController.Units[i+1].Add(ennemy.GetComponent<Being>());
				}

			}
	}
		public void closeCompagnon(){
			caracterPanel.SetActive(false);
		}
	public void openCompagnon(){
			if (caracterPanel.activeSelf) {
				caracterPanel.SetActive(false);
			} else {
				caracterPanel.SetActive(true);
				caracterPanel.transform.GetChild(2).GetComponent<Text>().text="Force : "+ GameController.Units[0][0].strenght; 
				caracterPanel.transform.GetChild(3).GetComponent<Text>().text="Endurance : "+ GameController.Units[0][0].stamina; 
				caracterPanel.transform.GetChild(4).GetComponent<Text>().text="Intelligence : "+ GameController.Units[0][0].intellect; 
				caracterPanel.transform.GetChild(5).GetComponent<Text>().text="Perception : "+ GameController.Units[0][0].perception; 
				caracterPanel.transform.GetChild(6).GetComponent<Text>().text="Charisme : "+ GameController.Units[0][0].charisma; 
				caracterPanel.transform.GetChild(7).GetComponent<Text>().text="Portée : "+ GameController.Units[0][0].scope; 

			}
			}
	public void openInventaire(){
	}
	public void openVille(){
	}
}

}