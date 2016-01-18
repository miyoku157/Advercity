using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Synapse.Runtime;
namespace AssemblyCSharp
{
    public abstract class Being : MonoBehaviour
    {
        protected int idGroup;
        protected Brain brain;
        protected NavMeshAgent agent;

		public bool isCollecting = false;
		public bool isAttacking = false;
        protected Being target;
		public int strenght=70;
		public int stamina;
		public int intellect;
		public int perception=10;
		public int charisma;
		public int scope=3;
		protected Item[][] Inventaire;

        // Use this for initialization
        virtual protected void Start()
        {
			Inventaire= new Item[2][];
			Inventaire[0]=new equipment[2];
			Inventaire[1]=new Item[20];
            agent = GetComponent<NavMeshAgent>();
			//to delete
			stamina = 100;
        }
        // Update is called once per frame
        virtual protected void Update()
        {
			if (stamina < 0) {
				GameObject.Destroy(gameObject);
			}
        }

		virtual public bool resolver(int stat1)
		{
			int alea=Random.Range (0, 100);
			if (alea < stat1) {
				return true;
			}
			return false;
		}
        virtual public bool resolver(int stat1, int stat2)
        {

            return false;
        }
		//Deal damage based on the weapon. Will not be heritate by hazard script
		virtual public void dealDamage(Being target){
			int Bstrength = strenght / 10;
			int basdamage = 0;
			if (Inventaire [0] [0] == null) {
				basdamage = Random.Range (2, 4);

			} else {
				equipment temp=(equipment)Inventaire[0][0];
				basdamage=Random.Range(2+temp.damage,4+temp.damage);
			}
			basdamage += Bstrength;
			target.stamina -= basdamage;
		}
		virtual public void move(Vector3 position){
			GetComponent<NavMeshAgent> ().SetDestination (position);
		}
		virtual public void launchAttack(GameObject Target){
			StartCoroutine ("attack", Target);		
		}

		// Attack coroutine, allow to attack or move to the target just by right clicking
		virtual protected IEnumerator attack(GameObject Gobject)
		{
			Being stat = null;
			if (gameObject.GetComponent<Being>() != null)
			{
				stat = gameObject.GetComponent<Being>();
			}
			while (Gobject!=null&&isAttacking)
			{
				if (Vector3.Distance(Gobject.transform.position, this.transform.position) > scope)
				{
					move(Gobject.transform.position);
				}
				else
				{
					if(resolver(stat.strenght)){
						this.dealDamage(Gobject.GetComponent<Being>());
						//add attack animation here
					}
				}
				yield return new WaitForSeconds(0.5f);
			}
		}
		virtual public void launchCollect(GameObject Gobject){
			StartCoroutine ("collect",Gobject);
		}
		virtual protected IEnumerator collect(GameObject Gobject){
			bool isFull = true;
			while (Gobject!=null&&isCollecting) {
				//animation collect
				if (Vector3.Distance(Gobject.transform.position, this.transform.position) >3)
				{
					move(Gobject.transform.position);
				}
				else{
					for(int i=0;i<20;i++){
						if(Inventaire[1][i]!=null){
							if(Gobject.tag=="Tree"&&isFull){
								isFull=false;
								Inventaire[1][i]=new Resources("Bois","UI/Wood_icon",5);
							}else if(Gobject.tag=="Metal"&&isFull){
								Inventaire[1][i]=new Resources("Métal","UI/17",5);
								isFull=false;
							}else if (Gobject.tag=="Food"&&isFull){
								Inventaire[1][i]=new Resources("Nourriture","UI/food_chicken_thig-512",5);
								isFull=false;
							}else if(Gobject.tag=="Water"&&isFull){
								Inventaire[1][i]=new Resources("Eau","UI/Water-drops1",5);
								isFull=false;
							}else if(isFull){
								Inventaire[1][i]=new Resources("Xenonium","UI/large",2);
								isFull=false;
							}
							if(isFull){
								this.isCollecting=false;
							}
						}
					}
					Gobject.transform.parent.GetComponent<ResourcesManager>().HP-=5;
				}
				yield return new WaitForSeconds(5);

			}
		}


        virtual protected void GetSensorPositionData(out Vector3 a_position)
        {
            if (this != null) {
				a_position = gameObject.transform.position;
			} else {
				a_position=Vector3.zero;
			}
        }
    }
}