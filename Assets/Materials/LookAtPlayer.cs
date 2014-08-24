using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

    public Transform playerTransform;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.LookRotation(playerTransform.position - this.transform.position);
        Debug.DrawRay(this.transform.position, playerTransform.position - this.transform.position);
            //.SetLookRotation(playerTransform.position - this.transform.position);
	}
}
