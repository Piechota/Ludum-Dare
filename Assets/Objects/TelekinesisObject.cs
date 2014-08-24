using UnityEngine;
using System.Collections;

public class TelekinesisObject : MonoBehaviour {

    public bool isInForce = false;

    private float yMove = 0.0f;
    private float upSpeed = 0.3f;
    private float yHeight = 0.1f;
	// Use this for initialization
	void Start () {
        Rigidbody rigid;
        if (!(rigid = GetComponent<Rigidbody>()))
            rigid = gameObject.AddComponent<Rigidbody>();
	}
    void Update()
    {
        if (!isInForce || yMove >= yHeight)
            return;

        Vector3 up = Vector3.up;

        up *= Time.deltaTime * yHeight / upSpeed;
        yMove += up.y;
        transform.position += up;
    }
    public void UseForce()
    {
        rigidbody.useGravity = false;
        rigidbody.freezeRotation = true;
        isInForce = true;
        yMove = 0.0f;
    }
}
