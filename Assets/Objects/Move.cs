using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public Vector3 speed = new Vector3( -0.25f,0,0);
	public AudioClip sound1;
	public void Activate()
	{
		enabled = true;
		audio.PlayOneShot (sound1);
	}

	void Start()
	{
		enabled = false;
	}

	void FixedUpdate () {
		transform.Translate (speed * Time.deltaTime);
	}
}
