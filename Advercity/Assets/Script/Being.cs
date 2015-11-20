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
		protected NavMeshAgent agent;
		// Use this for initialization
		virtual protected void Start () {
			agent = GetComponent<NavMeshAgent> ();
		}
		
		// Update is called once per frame
		virtual protected void Update () {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				
			} else if (Input.GetKeyDown (KeyCode.Mouse1)) {
				Vector3 pos= getMousePosition();
				if(pos!=Vector3.zero){
					move(pos);
				}
			}
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
		virtual protected Vector3 getMousePosition(){
			RaycastHit Hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out Hit, 100)) {// modifier la distance finalel
				if(Hit.collider.tag=="Map"){
					return Hit.point;
				}
			}
			return Vector3.zero;
		}
		virtual public void move(Vector3 position){
			agent.SetDestination (position);
		}
	}
}