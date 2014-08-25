using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public Vector3 speed = new Vector3( -0.25f,0,0);
	AudioSource sound1;

	public void Activate()
	{
		enabled = true;
        sound1.Play();
	}

	void Start()
	{
		enabled = false;
        sound1 = GetComponent<AudioSource>();
	}

	void FixedUpdate () {
		transform.Translate (speed * Time.deltaTime,Space.World);
	}

    void OnDestroy()
    {
        if (sound1 != null)
            sound1.Stop();
    }
}
