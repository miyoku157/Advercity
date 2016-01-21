using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace AssemblyCSharp{
public class IHM : MonoBehaviour
{
	public static Vector3[] spot;
		private GameObject caracterPanel;
		private GameObject inventory;
		private GameObject downWeapon;
		private GameObject downArmor;
		private int equip;
	// Use this for initialization
	void Start ()
	{
		spot = new Vector3[3];
		spot [0] = new Vector3 (150, 150, 625);
		spot [1] = new Vector3 (650, 140, 583);
		spot [2] = new Vector3 (700, 150, 100);
		caracterPanel = GameObject.Find ("ImageCadrePersonnage");
		inventory = GameObject.Find ("ImageCadreInventaire");
		caracterPanel.SetActive (false);
		inventory.SetActive (false);
		downWeapon=inventory.transform.GetChild(6).gameObject;
		downArmor=inventory.transform.GetChild(7).gameObject;

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
			int idcamp = 0;
			GameObject Batiment= Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/HQ"));
			Batiment.GetComponent<Building_manager> ().Idcamp = idcamp;
			idcamp++;
			Batiment.transform.position = spot [alea] + new Vector3 (15, 0, 0);
			GameObject player=Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject> ("Prefabs/Player_character"));
			player.transform.position = spot [alea];
			player.GetComponent<Being> ().city = Batiment;
			GameController.Units [0].Add (player.GetComponent<Being> ());
			GameObject[] compagnon = new GameObject[2];
			for (int i=0;i<0;i++){
				compagnon[i]=Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/Compagnon"));
				compagnon[i].transform.position=spot[alea]-i*new Vector3(10,0,10)+new Vector3(5,0,5);
				compagnon[i].GetComponent<Being>().city=Batiment;
				GameController.Units[0].Add(compagnon[i].GetComponent<Being>());
			}

			for (int i=0;i<2;i++){
				int precedent=alea;
				alea=(precedent+1+Random.Range(0,2))%3;
				Batiment=Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/HQ"));
				Batiment.transform.position=spot[alea];
				Batiment.GetComponent<Building_manager> ().Idcamp = idcamp;
				idcamp++;
				for (int j=0; j<4;j++){
					GameObject ennemy= Instantiate<GameObject>(UnityEngine.Resources.Load<GameObject>("Prefabs/Ennemy"));
					ennemy.transform.position=spot[alea]+Mathf.Pow(-1,j)*new Vector3(5+j,0,5+j);
					ennemy.GetComponent<Being>().city=Batiment;
					GameController.Units[i+1].Add(ennemy.GetComponent<Being>());
				}

			}
	}
		public void closeCompagnon(){
			caracterPanel.SetActive(false);
		}
		public void closeInventaire(){
			inventory.SetActive (false);
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
			if (!inventory.activeSelf) {
				Item[][] inv;
				if(GameController.oldSelectTarget.GetComponent<Being>()==null){
					inv = GameController.Units [0] [0].Inventaire;
				}else{
					inv =GameController.oldSelectTarget.GetComponent<Being>().Inventaire;
				}
				inventory.SetActive (true);
				readInventaire (inv);
			} else {
				for(int i=1;i<=20;i++){
					GameObject.Find("Text ("+i+")").GetComponent<Text>().text="";
				}
				downArmor.transform.GetChild(0).GetComponent<Text>().text="";
				downWeapon.transform.GetChild(0).GetComponent<Text>().text="";
				inventory.SetActive(false);


			}
		}
	private void readInventaire(Item[][] inv){
			int j = 1;
			for(int i=0;i<2;i++){
				j=1;
				foreach(Item element in inv[i]){
					Text obj=GameObject.Find("Text ("+j+")").GetComponent<Text>();
					if(element!=null){
						if(element.type==0){
							AssemblyCSharp.equipment temp=(AssemblyCSharp.equipment)element;
							if(temp.typeEquip==0){
								downWeapon.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(temp.name));
							}else{
								downArmor.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(temp.name));
							}
							obj.text="Equipement : "+ temp.name+" Dommage : "+ temp.damage+ " Portée : "+ temp.bonusScope ;
						}else{
							AssemblyCSharp.Resources temp=(AssemblyCSharp.Resources)element;
							obj.text="Ressource : "+ temp.name+" Quantité : "+ temp.nb;
						}
						j+=1;
					}
					else{
						obj.text="";
					}
				}
			}
		}
	public void changeArmor(int equip){
			string name=downArmor.GetComponent<Dropdown>().options[equip].text;
			downArmor.transform.GetChild (0).GetComponent<Text> ().text = name;
		}
	public void changeWeapon(int equip){
			string name=downWeapon.GetComponent<Dropdown>().options[equip].text;
			downWeapon.transform.GetChild (0).GetComponent<Text> ().text = name;
		}
	public void openVille(){
	}
		public void selectPlayer(){
			Camera.main.transform.position = GameController.Units [0] [0].transform.position+new Vector3(0,40,0);
			GameController.oldSelectTarget=GameController.Units [0] [0].gameObject;
			GameController.oldSelectTarget.GetComponent<Renderer>().material.SetFloat("_Outline",0.005f);
		}
}

}