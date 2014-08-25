using UnityEngine;
using System.Collections;

public class OnCollisionShow : MonoBehaviour {
    MeshRenderer mr;
	// Use this for initialization
	void Start () {
        if (!(mr = GetComponent<MeshRenderer>()))
            mr = GetComponentInChildren<MeshRenderer>();

        if (!mr)
            return;

		mr.enabled = false;

        if(mr.collider)
            mr.collider.enabled = false;
	}
	void OnTriggerEnter()
	{
        if (!mr)
            return;

        mr.enabled = true;
        if (mr.collider)
            mr.collider.enabled = true;
    }
}
