using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    public GameObject playerBright, playerDark;
    public List<GameObject> DesertSpawns;
    public List<GameObject> NilfheimSpaws;

    public GameObject endCamera;

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
            GameObject nilfheim = NilfheimSpaws[0];
            NilfheimSpaws.Remove(nilfheim);
            SpawnAtPlayerAtPoint(nilfheim, playerDark);
        }
        else
        {
            GameObject desert = DesertSpawns[0];
            DesertSpawns.Remove(desert);
            SpawnAtPlayerAtPoint(desert, playerBright);
        }
    }

    private void SpawnAtPlayerAtPoint(GameObject spawner, GameObject player)
    {

        player.transform.position = spawner.transform.position;
        player.transform.LookAt(player.transform.position + spawner.transform.forward);
        player.rigidbody.velocity = Vector3.zero;
        player.SetActive(true);
    }
}
