using UnityEngine;
using System.Collections;
using Synapse.Runtime;
namespace AssemblyCSharp
{
	public abstract class Being : MonoBehaviour {
		protected Brain brain;
		protected Being target;
		protected float strenght;
		protected float stamina;
		protected float intellect;
		protected float perception;
		protected float charisma;
		protected float scope;
		protected NavMeshAgent agent;
		// Use this for initialization
		virtual protected void Start () {
			agent = GetComponent<NavMeshAgent> ();

		}
		virtual protected IEnumerator Iainit(){
			brain = new SynapseLibrary_Test.Deplacement.Move (this);
			while(Application.isPlaying&&brain!=null){
				AIUpdate();
				yield return new WaitForSeconds(1);
			}	
		}
		void AIUpdate()
		{
			if(brain.Process() == false)
			{
				target = null;
			}
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

		virtual protected Vector3 getMousePosition(){
			RaycastHit Hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out Hit, 100)) {// modifier la distance finalel
				if(Hit.collider.tag=="Map"){
					return Hit.point;
				}else if(Hit.collider.tag=="Item"){
				}else if (Hit.collider.tag=="Ennemy"){
				}
			}
			return Vector3.zero;
		}
		virtual protected void move(Vector3 position){
			agent.SetDestination (position);
		}

		virtual protected IEnumerator attack(GameObject Gobject){
			Being stat;
			if (gameObject.GetComponent<Being> () != null) {
				stat= gameObject.GetComponent<Being> ();
			}
				while(stat.stamina>0){
					if(Vector3.Distance(Gobject.transform.position,this.transform.position)<scope){
						move(Gobject.transform.position);
					}else{
						//add attack animation here
						resolver();
					}
					yield return new WaitForSeconds(0.1);
				}
			}


		virtual protected object[] GetLayerboxData()
		{
			return Enemy.Instance.ToArray();
		}
		virtual protected void GetSensorPositionData(out Vector3 a_position)
		{
			a_position = gameObject.transform.position;
		}
		virtual protected void DesireFollowCallback(object a_collectible)
		{		
			target = a_collectible as Enemy;
		}
		
		virtual protected void DesireCoolCallback()
		{
			target = null;
		}
	}
}