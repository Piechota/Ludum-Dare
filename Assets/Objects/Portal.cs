using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
    public bool nilfheim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider player)
    {
        if (player.tag != "Player")
            return;

        GameController.Instance.SpawnPlayer(nilfheim);
    }
}
