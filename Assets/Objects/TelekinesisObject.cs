using UnityEngine;
using System.Collections;

public class TelekinesisObject : MonoBehaviour {

    public bool isInForce = false;

	// Use this for initialization
	void Start () {
        Rigidbody rigid;
        if (!(rigid = GetComponent<Rigidbody>()))
            rigid = gameObject.AddComponent<Rigidbody>();

        rigid.freezeRotation = true;
	}
}
