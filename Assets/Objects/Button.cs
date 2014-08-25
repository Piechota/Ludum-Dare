using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public float time = 0.5f;
	public float doortime = 1;

	public Move moveButton;
	public Move door;

    public Vector3 doorSpeed;

	public void OnTriggerEnter()
	{
        door.speed = doorSpeed;

		moveButton.Activate();
		Destroy( moveButton, time);

		door.Invoke("Activate", time);
		Destroy(door, time + doortime);

		Destroy(this);
	}
}
