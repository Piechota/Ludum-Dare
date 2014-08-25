using UnityEngine;
using System.Collections;

public class Dialogs2 : MonoBehaviour {

    public AudioClip dialog;

    public GameObject nextDialog;

    public bool playOnTrigger = true;

    void Start()
    {
        collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider player)
    {
        if ((player.gameObject != GameController.Instance.player) || !playOnTrigger)
            return;

        PlayDialog();
    }

    public void PlayDialog()
    {
        GameController.Instance.player.audio.PlayOneShot(dialog);
        if (nextDialog)
            nextDialog.SetActive(true);
        gameObject.SetActive(false);
    }
}
