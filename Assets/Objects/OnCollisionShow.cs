using UnityEngine;
using System.Collections;

public class OnCollisionShow : MonoBehaviour {

    public GameObject forDelete;

	// Use this for initialization
	void Start () {
        forDelete.SetActive(false);
	}
	void OnTriggerEnter()
	{
        if(forDelete)
        forDelete.SetActive(true);
    }
}
