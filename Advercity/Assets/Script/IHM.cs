using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace AssemblyCSharp
{
    public class IHM : MonoBehaviour
    {
        private GameObject caracterPanel;
        private GameObject inventory;
        private GameObject downWeapon;
        private GameObject downArmor;
        private int equip;
		private int openInt=0;
        // Use this for initialization
        void Start()
        {
            

            caracterPanel = GameObject.Find("ImageCadrePersonnage");
            inventory = GameObject.Find("ImageCadreInventaire");
			if (caracterPanel != null) {
				caracterPanel.SetActive (false);
				inventory.SetActive (false);
				downWeapon = inventory.transform.GetChild (6).gameObject;
				downArmor = inventory.transform.GetChild (7).gameObject;
			}
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void credits()
        {
            Application.LoadLevel("Credits");
        }
        public void launchGame()
        {
			Application.LoadLevel ("Main");
        }
        public void closeCompagnon()
        {
            caracterPanel.SetActive(false);
        }
        public void closeInventaire()
        {
            inventory.SetActive(false);
        }
        public void openCompagnon()
        {
            for (int i = 0; i < GameController.Units.Length; i++)
            {
                if (GameController.oldSelectTarget == null)
                {
                    openInt = 0;
                }
                else if (GameController.oldSelectTarget.GetComponent<Being>() == GameController.Units[0][i])
                {
                    openInt = i;
                }
            }
				if(caracterPanel.activeSelf){
					caracterPanel.SetActive(false);
				}else{
					caracterPanel.SetActive(true);
					readCharacter();
				}
			
            
        }
		private void readCharacter(){

				caracterPanel.transform.GetChild(3).GetComponent<Text>().text = "Force : " + GameController.Units[0][openInt].strenght;
				caracterPanel.transform.GetChild(4).GetComponent<Text>().text = "Endurance : " + GameController.Units[0][openInt].stamina;
				caracterPanel.transform.GetChild(5).GetComponent<Text>().text = "Intelligence : " + GameController.Units[0][openInt].intellect;
				caracterPanel.transform.GetChild(6).GetComponent<Text>().text = "Perception : " + GameController.Units[0][openInt].perception;
				caracterPanel.transform.GetChild(7).GetComponent<Text>().text = "Charisme : " + GameController.Units[0][openInt].charisma;
				caracterPanel.transform.GetChild(8).GetComponent<Text>().text = "Portée : " + GameController.Units[0][openInt].scope;
		}
        public void openInventaire()
        {
            if (!inventory.activeSelf)
            {
                Item[][] inv;
                if (GameController.oldSelectTarget == null)
                {
                    inv = GameController.Units[0][0].Inventaire;
                }
                else {
                    inv = GameController.oldSelectTarget.GetComponent<Being>().Inventaire;
                }
                inventory.SetActive(true);
				for(int i=0;i<GameController.Units[0].Count;i++){
					if(GameController.oldSelectTarget==null){
						openInt=0;
					}
					else if(GameController.oldSelectTarget.GetComponent<Being>()==GameController.Units[0][i]){
						openInt=i;
					}
				}
                readInventaire(inv);
            }
            else {
                for (int i = 1; i <= 20; i++)
                {
                    GameObject.Find("Text (" + i + ")").GetComponent<Text>().text = "";
                }
                downArmor.transform.GetChild(0).GetComponent<Text>().text = "";
                downWeapon.transform.GetChild(0).GetComponent<Text>().text = "";
                inventory.SetActive(false);


            }
        }
        private void readInventaire(Item[][] inv)
        {
            int j = 1;
            for (int i = 0; i < 2; i++)
            {
                j = 1;
                foreach (Item element in inv[i])
                {
                    Text obj = GameObject.Find("Text (" + j + ")").GetComponent<Text>();
                    if (element != null)
                    {
                        if (element.type == 0)
                        {
                            AssemblyCSharp.equipment temp = (AssemblyCSharp.equipment)element;
                            if (temp.typeEquip == 0)
                            {
                                downWeapon.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(temp.name));
                            }
                            else {
                                downArmor.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(temp.name));
                            }
                            obj.text = "Equipement : " + temp.name + " Dommage : " + temp.damage + " Portée : " + temp.bonusScope;
                        }
                        else {
                            AssemblyCSharp.Resources temp = (AssemblyCSharp.Resources)element;
                            obj.text = "Ressource : " + temp.name + " Quantité : " + temp.nb;
                        }
                        j += 1;
                    }
                    else {
                        obj.text = "";
                    }
                }
            }
        }
		public void plusCharacter(){
			openInt++;
			openInt = openInt % 3;
			readCharacter ();
			
		}
		public void moinsCharacter(){
			openInt--;
			if (openInt < 0) {
				openInt=3;
			}
			readCharacter ();
		}
		public void plusinvCharacter(){
			openInt++;
			openInt = openInt % 3;
			readInventaire(GameController.Units[0][openInt].Inventaire);

		}
		public void moinsinvCharacter(){
			openInt--;
			if (openInt < 0) {
				openInt=3;
			}
			readInventaire(GameController.Units[0][openInt].Inventaire);
		}
        public void changeArmor(int equip)
        {
            string name = downArmor.GetComponent<Dropdown>().options[equip].text;
            downArmor.transform.GetChild(0).GetComponent<Text>().text = name;
        }
        public void changeWeapon(int equip)
        {
            string name = downWeapon.GetComponent<Dropdown>().options[equip].text;
            downWeapon.transform.GetChild(0).GetComponent<Text>().text = name;
        }
		public void returnMenu (){
			Application.LoadLevel ("MenuHome");
		}

        public void openVille()
        {
        }
        public void selectPlayer()
        {
            Camera.main.transform.position = GameController.Units[0][0].transform.position + new Vector3(0, 40, 0);
            GameController.oldSelectTarget = GameController.Units[0][0].gameObject;
            GameController.oldSelectTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("_Outline", 0.005f);
			GameController.oldSelectTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_OutlineColor", Color.green);

		}
    }

}