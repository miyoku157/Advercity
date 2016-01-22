using UnityEngine;
using System.Collections;

public class ResourcesManager : MonoBehaviour
{
    public int HP;
    // Use this for initialization
    void Start()
    {
        HP = 200;
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    
	void GetSensorPositionData(out Vector3 a_position)
	{
			a_position = gameObject.transform.position;

	}
}
