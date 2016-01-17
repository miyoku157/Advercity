using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Building_manager : MonoBehaviour
{
	protected List<float[]> ressources;
    // Use this for initialization
	//1:bois,2Métal, 3Bouffe,4 eau,5 Extrateresre
    void Start()
    {
		ressources= new List<float[]>();
		for (int i=0; i<3; i++) {
			ressources.Add(new float[5]);
		}
		GameObject label = GameObject.Find ("LabelBois");
		label.GetComponent<Text> ().text = "Bois : " + ressources [0] [0];
		label = GameObject.Find ("LabelMetal");
		label.GetComponent<Text> ().text = "Métal : " + ressources [0] [1];
		label = GameObject.Find ("LabelFood");
		label.GetComponent<Text> ().text = "Food : " + ressources [0] [2];
		label = GameObject.Find ("LabelEau");
		label.GetComponent<Text> ().text = "Eau : " + ressources [0] [3];
		label = GameObject.Find ("LabelXenonium");
		label.GetComponent<Text> ().text = "Xenonium : " + ressources [0] [4];
    }

    // Update is called once per frame
    void Update()
    {

    }
}

