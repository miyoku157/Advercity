using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Building_manager : MonoBehaviour
{
	protected List<float[]> ressources;
	Text LabelBois;
	Text LabelMetal;
	Text LabelFood;
	Text LabelEau;
	Text LabelXeno;
    // Use this for initialization
	//1:bois,2Métal, 3Bouffe,4 eau,5 Extrateresre
    void Start()
    {
		ressources= new List<float[]>();
		for (int i=0; i<3; i++) {
			ressources.Add(new float[5]);
		}
		LabelBois=GameObject.Find ("LabelBois").GetComponent<Text>();
		LabelBois.text = "Bois : " + ressources [0] [0];
		LabelMetal = GameObject.Find ("LabelMetal").GetComponent<Text> ();
		LabelMetal.text = "Métal : " + ressources [0] [1];
		LabelFood = GameObject.Find ("LabelFood").GetComponent<Text>();
		LabelFood.text = "Food : " + ressources [0] [2];
		LabelEau = GameObject.Find ("LabelEau").GetComponent<Text>();
		LabelEau.text = "Eau : " + ressources [0] [3];
		LabelXeno = GameObject.Find ("LabelXenonium").GetComponent<Text>();
		LabelXeno.text = "Xenonium : " + ressources [0] [4];
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
						ressources [0] [0]+=res.nb;
						LabelBois.text = "Bois : " + ressources [0] [0];
					}else if(res.name=="Métal"){
						ressources [0] [1]+=res.nb;
						LabelMetal.text = "Métal : " + ressources [0] [1];
					}else if (res.name=="Nourriture"){
						ressources [0] [2]+=res.nb;
						LabelFood.text = "Food : " + ressources [0] [2];

					}else if (res.name=="Eau"){
						ressources [0] [3]+=res.nb;
						LabelEau.text = "Eau : " + ressources [0] [3];

					}else{
						ressources [0] [4]+=res.nb;
						LabelXeno.text = "Xenonium : " + ressources [0] [4];

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

