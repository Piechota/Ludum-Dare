using UnityEngine;
using System.Collections;

public class OnCollisionDestroy : MonoBehaviour {
	
	void OnTriggerEnter()
	{
		Destroy (gameObject);
	}
}
