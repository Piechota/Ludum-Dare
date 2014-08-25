using UnityEngine;
using System.Collections;

public class EndGameEnable : MonoBehaviour {

    private bool run = false;
	// Use this for initialization
	void Start () {
	}
    void Update()
    {
        if (run)
            return;

        GameController.Instance.playerBright.active = false;
        GameController.Instance.playerDark.active = false;

        GameController.Instance.endCamera.active = true;
    }
}
