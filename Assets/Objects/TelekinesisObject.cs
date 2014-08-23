using UnityEngine;
using System.Collections;

public class TelekinesisObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rigidbody rigid;
        if (!(rigid = GetComponent<Rigidbody>()))
            rigid = gameObject.AddComponent<Rigidbody>();

        rigid.freezeRotation = true;
	}
}
