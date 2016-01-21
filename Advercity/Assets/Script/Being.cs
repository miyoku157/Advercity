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
        public GameObject city;
        public bool isCollecting = false;
        public bool isAttacking = false;
        public bool isGoing = false;
        public bool isoccupy = false;
        protected Being target;
        public int strenght = 70;
        public int stamina;
        public int intellect;
        public int perception = 10;
        public int charisma;
        public int scope = 3;
        public Item[][] Inventaire;

        // Use this for initialization
        virtual protected void Start()
        {
            Inventaire = new Item[2][];
            Inventaire[0] = new equipment[2];
            Inventaire[1] = new Item[20];
            agent = GetComponent<NavMeshAgent>();
            //to delete
            stamina = 100;
            city = GameObject.Find("Centre ville");
        }
        // Update is called once per frame
        virtual protected void Update()
        {
            if (isCollecting || isAttacking || isGoing)
            {
                isoccupy = true;
            }
            else {
                isoccupy = false;
            }
            if (stamina < 0)
            {
                GameObject.Destroy(gameObject);
            }
        }

        virtual public bool resolver(int stat1)
        {
            int alea = Random.Range(0, 100);
            if (alea < stat1)
            {
                return true;
            }
            return false;
        }
        virtual public bool resolver(int stat1, int stat2)
        {

            return false;
        }
        //Deal damage based on the weapon. Will not be heritate by hazard script
        virtual public void dealDamage(Being target)
        {
            int Bstrength = strenght / 10;
            int basdamage = 0;
            if (Inventaire[0][0] == null)
            {
                basdamage = Random.Range(2, 4);

            }
            else {
                equipment temp = (equipment)Inventaire[0][0];
                basdamage = Random.Range(2 + temp.damage, 4 + temp.damage);
            }
            basdamage += Bstrength;
            target.stamina -= basdamage;
        }
        virtual public void move(Vector3 position)
        {
            isGoing = true;
            isAttacking = false;
            isCollecting = false;
            GetComponent<NavMeshAgent>().SetDestination(position);
        }
        virtual public void launchAttack(GameObject Target)
        {
            StartCoroutine("attack", Target);

        }

        // Attack coroutine, allow to attack or move to the target just by right clicking
        virtual protected IEnumerator attack(GameObject Gobject)
        {
            isGoing = false;
            isAttacking = true;
            isCollecting = false;
            Being stat = null;
            if (gameObject.GetComponent<Being>() != null)
            {
                stat = gameObject.GetComponent<Being>();
            }
            while (Gobject != null && isAttacking)
            {
                if (Vector3.Distance(Gobject.transform.position, this.transform.position) > scope)
                {
                    move(Gobject.transform.position);
                }
                else
                {
                    if (resolver(stat.strenght))
                    {
                        this.dealDamage(Gobject.GetComponent<Being>());
                        //add attack animation here
                    }
                }
                yield return new WaitForSeconds(0.5f);
                if (Gobject == null)
                {
                    isAttacking = false;
                }
            }
        }
        virtual public void launchCollect(GameObject Gobject)
        {
            StartCoroutine("collect", Gobject);
        }
        virtual protected IEnumerator collect(GameObject Gobject)
        {
            isGoing = false;
            isAttacking = false;
            isCollecting = true;
            while (Gobject != null && isCollecting)
            {
                bool isFull = true;
                //animation collect
                while (isGoing)
                {
                    yield return new WaitForSeconds(1);
                }
                if (Vector3.Distance(Gobject.transform.position, this.transform.position) > 3)
                {
                    move(Gobject.transform.position);
                }
                else {
                    int hp = Gobject.transform.parent.GetComponent<ResourcesManager>().HP;
                    hp -= 5;
                    for (int i = 0; i < 20; i++)
                    {
                        if (Inventaire[1][i] == null && isFull)
                        {
                            if (Gobject.tag == "Tree")
                            {
                                isFull = false;
                                Inventaire[1][i] = new Resources("Bois", "UI/Wood_icon", 1, 5);
                            }
                            else if (Gobject.tag == "Metal")
                            {
                                Inventaire[1][i] = new Resources("Métal", "UI/17", 1, 5);
                                isFull = false;
                            }
                            else if (Gobject.tag == "Food")
                            {
                                Inventaire[1][i] = new Resources("Nourriture", "UI/food_chicken_thig-512", 1, 5);
                                isFull = false;
                            }
                            else if (Gobject.tag == "Water")
                            {
                                Inventaire[1][i] = new Resources("Eau", "UI/Water-drops1", 1, 5);
                                isFull = false;
                            }
                            else if (Gobject.tag == "Xenonium")
                            {
                                Inventaire[1][i] = new Resources("Xenonium", "UI/large", 1, 2);
                                isFull = false;
                            }

                        }
                    }
                    Gobject.transform.parent.GetComponent<ResourcesManager>().HP = hp;
                    if (isFull || hp <= 0)
                    {
                        isGoing = true;
                        move(city.transform.position);
                    }

                }
                yield return new WaitForSeconds(0.5f);

            }
        }


        virtual protected void GetSensorPositionData(out Vector3 a_position)
        {
            if (this != null)
            {
                a_position = gameObject.transform.position;
            }
            else {
                a_position = Vector3.zero;
            }
        }
    }
}