using UnityEngine;
using System.Collections;

public class PullRotation : MonoBehaviour {

    public Transform target;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = target.rotation;
	}
}
