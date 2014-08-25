using UnityEngine;
using System.Collections;

public class OnCollisionShow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().enabled = false;
	}
	void OnTriggerEnter()
	{
		GetComponent<MeshRenderer> ().enabled = true;
	}
}
