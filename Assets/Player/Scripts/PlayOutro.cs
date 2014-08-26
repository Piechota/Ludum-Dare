using UnityEngine;
using System.Collections;

public class PlayOutro : MonoBehaviour {

    private bool wasPlayeng = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (audio.isPlaying)
            return;

        if(wasPlayeng)
        {
            Application.Quit();
            return;
        }

        wasPlayeng = true;
        audio.Play();
	}
}
