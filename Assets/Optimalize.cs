using UnityEngine;
using System.Collections;

public class Optimalize : MonoBehaviour
{
    public GameObject things;
    private bool done = false;
    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        
        if (col.tag == "Player" && !done)
        {
            Debug.Log("asd");
            things.SetActive(!things.active);
            done = true;
        }
            

    }
    // Update is called once per frame
    void Update()
    {

    }
}
