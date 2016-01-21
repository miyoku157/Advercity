using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Synapse.Runtime;
public class Building_manager : MonoBehaviour
{
	protected float[] ressources;
	Text LabelBois;
	Text LabelMetal;
	Text LabelFood;
	Text LabelEau;
	Text LabelXeno;
	private Brain  synapseBrain;
	public int Idcamp=1;
	public int[] visitedMark;
	private GameObject[] mark;
    // Use this for initialization
	//1:bois,2Métal, 3Bouffe,4 eau,5 Extrateresre
    void Start()
    {
		ressources= new float[5];
		LabelBois=GameObject.Find ("LabelBois").GetComponent<Text>();
		LabelBois.text = "Bois : " + ressources [0];
		LabelMetal = GameObject.Find ("LabelMetal").GetComponent<Text> ();
		LabelMetal.text = "Métal : " + ressources [1];
		LabelFood = GameObject.Find ("LabelFood").GetComponent<Text>();
		LabelFood.text = "Nourriture : " + ressources[2];
		LabelEau = GameObject.Find ("LabelEau").GetComponent<Text>();
		LabelEau.text = "Eau : " + ressources [3];
		LabelXeno = GameObject.Find ("LabelXenonium").GetComponent<Text>();
		LabelXeno.text = "Xenonium : " + ressources [4];
		mark = new GameObject[11];
		visitedMark = new int[11];
		for (int i=0; i<11; i++) {
			mark[i]=GameObject.Find("mark1 ("+i+")");
		}
		StartCoroutine ("StartAI");
    }
	IEnumerator StartAI(){
		synapseBrain = new SynapseLibrary_IA.Building.MissResources (this) ;
		while (Application.isPlaying&& synapseBrain!=null){
			AIUpdate();
			yield return new WaitForSeconds(1);
		}
	}
	protected object[] GetLayeremptyData(){
		Object[] tab = new Object[1];
		tab [0] = this;
		return tab;
	}

	private void AIUpdate(){
		if (synapseBrain.Process () == false) {
		}
	}
	protected void DesirePickResourcesCallback(){
		foreach(AssemblyCSharp.Being temp in AssemblyCSharp.GameController.Units[Idcamp]){
		int alea=Random.Range(0,11);
		bool visited= false;
		for(int j=0;j<11;j++){
			if(alea==visitedMark[j]){
				visited=true;
			}
		}
		if(!visited){
			if(!temp.isoccupy){
					temp.move(mark[alea].transform.position);
				}
			}
		}
			
	}
	protected void GetSensorWaterData(out float water){
		water =ressources[3] ;
	}
	protected void GetSensorFoodData(out float food){
		food =ressources[2] ;
	}
	protected void GetSensorWoodData(out float wood){
		wood =ressources[0] ;
	}
	protected void GetSensorMetalData(out float metal){
		metal =ressources[1] ;
	}
	protected void GetSensorXenoniumData(out float xeno){
		xeno =ressources[4] ;
	}
    // Update is called once per frame
    void Update()
    {

    }
	void OnCollisionEnter(Collision coll){
		AssemblyCSharp.Item[] temp = coll.gameObject.GetComponent<AssemblyCSharp.Being> ().Inventaire [1];
		for (int i=0; i<20; i++) {
			if(temp[i]!=null){
				if(temp[i].type==1){
					AssemblyCSharp.Resources res= (AssemblyCSharp.Resources)temp[i];
					if(res.name=="Bois"){
						ressources[0]+=res.nb;
						LabelBois.text = "Bois : " + ressources [0];
					}else if(res.name=="Métal"){
						ressources [1]+=res.nb;
						LabelMetal.text = "Métal : " + ressources [1];
					}else if (res.name=="Nourriture"){
						ressources [2]+=res.nb;
						LabelFood.text = "Food : " + ressources [2];

					}else if (res.name=="Eau"){
						ressources [3]+=res.nb;
						LabelEau.text = "Eau : " + ressources [3];

					}else{
						ressources [4]+=res.nb;
						LabelXeno.text = "Xenonium : " + ressources [4];

					}
				}else{
				
				}
			}
		}
		for(int j=0;j<20;j++){
			coll.gameObject.GetComponent<AssemblyCSharp.Being> ().Inventaire [1][j]=null;
		}
		coll.gameObject.GetComponent<AssemblyCSharp.Being>().isGoing=false;
	}
}

