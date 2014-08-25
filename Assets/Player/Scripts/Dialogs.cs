using UnityEngine;
using System.Collections;

public class Dialogs : MonoBehaviour {

    public Queue dialogs;
    public Queue texts;

    private bool isWorking = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!isWorking)
            return;

        if (audio.isPlaying)
            return;

        if(texts.Count == 0)
        {
            GameController.Instance.SwitchText(string.Empty);
            Destroy(gameObject);
        }

        GameController.Instance.audio.PlayOneShot(dialogs.Dequeue() as AudioClip);
        GameController.Instance.SwitchText(texts.Dequeue() as string);
	}

    public void TurnOn()
    {
        isWorking = true;
    }
}
