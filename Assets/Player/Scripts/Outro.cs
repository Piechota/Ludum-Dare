using UnityEngine;
using System.Collections;

public class Outro : MonoBehaviour {

    public GameObject outro;

	void OnTriggerEnter(Collider player)
    {
        if (player.tag != "Player")
            return;

        outro.SetActive(true);

        gameObject.SetActive(false);
    }
}
