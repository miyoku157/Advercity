using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Synapse.Runtime;
namespace AssemblyCSharp
{
    public abstract class Being : MonoBehaviour
    {
        public static List<Being>[] Units;
        protected int idGroup;
        protected Brain brain;
        protected NavMeshAgent agent;
		protected bool isAttacking = false;
        protected Being target;
        protected float strenght;
        protected float stamina;
        protected float intellect;
        protected float perception;
        protected float charisma;
        protected float scope;

        // Use this for initialization
        virtual protected void Start()
        {
            agent = GetComponent<NavMeshAgent>();
			//to delete
			stamina = 100;
        }
        virtual protected IEnumerator Iainit()
        {
            while (Application.isPlaying && brain != null)
            {
                AIUpdate();
                yield return new WaitForSeconds(1);
            }
        }
        void AIUpdate()
        {
            if (brain.Process() == false)
            {
                target = null;
            }
        }
        // Update is called once per frame
        virtual protected void Update()
        {

        }

        virtual public bool resolver()
        {
            return true;
        }
		
		virtual public void move(Vector3 position){
			isAttacking = false;
			GetComponent<NavMeshAgent> ().SetDestination (position);
		}
		virtual public void launchAttack(GameObject Target){
			isAttacking = true;
			StartCoroutine ("attack", Target);		
		}


		// changer coroutine en vrai fonction tout en essayant de garder maitrise du temps régler le scope aussi
		virtual protected IEnumerator attack(GameObject Gobject)
		{
			Being stat = null;
			if (gameObject.GetComponent<Being>() != null)
			{
				stat = gameObject.GetComponent<Being>();
			}
			while (stat.stamina > 0&&isAttacking)
			{
				if (Vector3.Distance(Gobject.transform.position, this.transform.position) < scope)
				{
					move(Gobject.transform.position);
				}
				else
				{
					//add attack animation here
					resolver();
				}
				yield return new WaitForSeconds(0.1f);
			}
		}



        virtual protected void GetSensorPositionData(out Vector3 a_position)
        {
            a_position = gameObject.transform.position;
        }
    }
}