using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

    public Transform playerTransform;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(new Vector3(playerTransform.position.x, this.transform.position.y, playerTransform.position.z));
        Debug.DrawRay(this.transform.position, playerTransform.position - this.transform.position);
            //.SetLookRotation(playerTransform.position - this.transform.position);
	}
}
