//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
    public class Hazard : Being
    {
		bool inside=false;
        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }
		void OnTriggerEnter(Collider coll){
			inside = true;
			if (coll.GetComponent<Being> ()!=null) {
				StartCoroutine ("DestroyUnit", coll.gameObject);
			}
		}
		void OnTriggerExit(){
			inside = false;
		}
		IEnumerator DestroyUnit(GameObject obj){
			while (inside) {
				obj.GetComponent<Being> ().stamina -= 25;
				yield return new WaitForSeconds (1);
			}
		}
    }
}

