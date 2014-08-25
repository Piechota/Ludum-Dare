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
        if ((player.gameObject != GameController.Instance.playerBright && player.gameObject != GameController.Instance.playerDark) || !playOnTrigger)
            return;

        PlayDialog();
    }

    public void PlayDialog()
    {

        if (GameController.Instance.playerBright.active == true)
            GameController.Instance.playerBright.audio.PlayOneShot(dialog);
        if (GameController.Instance.playerDark.active == true)
            GameController.Instance.playerDark.audio.PlayOneShot(dialog);

        if (nextDialog)
            nextDialog.SetActive(true);
        gameObject.SetActive(false);
    }
}
