using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    public GameObject playerBright, playerDark;
    public Queue<GameObject> DesertSpawns;
    public Queue<GameObject> NilfheimSpaws;

    private static Object locker;
    private static GameController _instance;

    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    // Use this for initialization
    void Start()
    {
        if (!GetComponent<GUIText>())
            gameObject.AddComponent<GUIText>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchText(string text)
    {
        guiText.text = text;
    }

    public void SpawnPlayer(bool isNilfheim)
    {
        playerBright.SetActive(false);
        playerDark.SetActive(false);

        if (isNilfheim)
        {
            SpawnAtPlayerAtPoint(NilfheimSpaws.Dequeue(), playerDark);
        }
        else
        {
            SpawnAtPlayerAtPoint(DesertSpawns.Dequeue(), playerDark);
        }
    }

    private void SpawnAtPlayerAtPoint(GameObject spawner, GameObject player)
    {
        player.transform.position = spawner.transform.position;
        player.SetActive(true);
    }
}
