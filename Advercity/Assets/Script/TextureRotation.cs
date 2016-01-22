using UnityEngine;
using System.Collections;

public class TextureRotation : MonoBehaviour
{
	private Vector2 offset = Vector2.zero;
	private float speed  = 0.5f;
	private Renderer rend;

	void Start ()
	{
		rend = gameObject.GetComponent<Renderer> ();
	}

	void Update ()
	{
		offset.y = (offset.y + speed * Time.deltaTime) % 1;
		rend.material.mainTextureOffset = offset;
	}
}