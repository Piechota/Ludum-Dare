using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour {
    public GameObject playerDesert;

    public AudioClip dialog;
    public float dialogLength = 2;
    public AudioClip impacts;
    public float impactsLength = 2;
    private bool finished = false;

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;

        StartCoroutine(playIntro());
	}
	
	// Update is called once per frame
	void Update () {
        if (finished)
        {
            playerDesert.SetActive(true);
            Debug.Log("Game starts");
            Destroy(this.gameObject);
        }
	}

    IEnumerator playIntro()
    {
        this.audio.Stop();
        this.audio.clip = dialog;
        this.audio.Play();
        yield return new WaitForSeconds(dialogLength);
        this.audio.Stop();
        Debug.Log("First clip finished");

        this.audio.clip = impacts;
        this.audio.Play();        
        yield return new WaitForSeconds(impactsLength);
        this.audio.Stop();
        Debug.Log("Second clip finished");
        finished = true;
    }
}
