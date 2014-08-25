using UnityEngine;
using System.Collections;

public class TelekinesisObject : MonoBehaviour {

    public bool isInForce = false;

    private float yMove = 0.0f;
    private float upSpeed = 0.3f;
    private float yHeight = 0.1f;

    private AudioSource hitSound;
    private float speed;
	// Use this for initialization
	void Start () {
        if (!GetComponent<Rigidbody>())
            gameObject.AddComponent<Rigidbody>();

        hitSound = GetComponent<AudioSource>();
	}
    void Update()
    {
        if (!isInForce || yMove >= yHeight)
            return;

        Vector3 up = Vector3.up;

        up *= Time.deltaTime * yHeight / upSpeed;
        yMove += up.y;
        transform.position += up;

        speed = rigidbody.velocity.magnitude;
    }
    public void UseForce()
    {
        rigidbody.useGravity = false;
        rigidbody.freezeRotation = true;
        rigidbody.isKinematic = false;
        isInForce = true;
        yMove = 0.0f;
    }

    private void PlaySound()
    {
        if (!hitSound)
            return;

        hitSound.Play();
    }
    private void StopSound()
    {
        if (!hitSound)
            return;

        hitSound.Stop();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isInForce || rigidbody.velocity.magnitude == 0 || collision.gameObject.tag == "Player")
            return;

        StopSound();
        PlaySound();
    }
}
