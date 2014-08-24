using UnityEngine;
using System.Collections;

public class MjölnirPushPlate : MonoBehaviour {

	Vector3 defaultPos;
	public Vector3 displace;

	public GameObject myDoor;
	Vector3 myDoorBasePosition;
	public Vector3 myDoorDisplace;

    public AudioSource doorSound;
    public GameObject target;

	bool moving;
	float progress;

	void Start ()
	{
        doorSound = GetComponent<AudioSource>();
		progress = 0;
		defaultPos = transform.position;
		myDoorBasePosition = myDoor.transform.position;
		moving = false;
		enabled = false;
	}

	void OnTriggerEnter(Collider collider)
	{
        Debug.Log("wlazl");

        if (collider.gameObject != target)
            return;

		enabled = true;
		moving = true;
	}
	void OnTriggerExit()
	{
        Debug.Log("wylazl"); 

        moving = false;
	}

	void Update()
	{
		if ( (moving == true) && progress <1 && target.GetComponent<TelekinesisObject>() && !target.GetComponent<TelekinesisObject>().isInForce)
		{
            if (!doorSound.isPlaying)
                doorSound.Play();
			progress += Time.deltaTime / 2;
		}
        else if (((moving != true) || (target.GetComponent<TelekinesisObject>() && target.GetComponent<TelekinesisObject>().isInForce)) && progress > 0)
        {
            if (!doorSound.isPlaying)
                doorSound.Play();
            progress -= Time.deltaTime;
        }
        else {
            doorSound.Stop();
        }
		transform.position = defaultPos + progress * displace;
		myDoor.transform.position = myDoorBasePosition + progress * myDoorDisplace;
	}
}
