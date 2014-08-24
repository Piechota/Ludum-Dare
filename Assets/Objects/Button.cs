using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public float time = 0.5f;
	public float doortime = 1;

	public Move moveButton;
	public Move door;

	public void Activate()
	{
		moveButton.Activate();
		Destroy( moveButton, time);

		door.Invoke("Activate", time);
		Destroy(door, time + doortime);

		Destroy(this);
	}
}
