using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
	public abstract class Being : MonoBehaviour {
		protected float strenght;
		protected float stamina;
		protected float intellect;
		protected float perception;
		protected float charisma;
		// Use this for initialization
		virtual protected void Start () {
		
		}
		
		// Update is called once per frame
		virtual protected void Update () {
		
		}
		virtual protected bool resolver(){
			return true;
		}
		virtual protected GameObject getTarget(){
			RaycastHit Hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out Hit, 100)) {// modifier la distance finalel
				if(Hit.collider.tag=="Being"){
					return Hit.transform.gameObject;
				}
			}
			return null;
		}
	}
}