using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float jumpForce = 3.0f;
    public float airControl = 0.0f;
    public float heightTest = 0.5f;

    private Spell activeSpell;

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
	}

    void FixedUpdate()
    {
        
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            Screen.showCursor = false;

        Vector3 direction = Vector3.zero;

        if ((Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.A)) &&
            IsOnGround())
            rigidbody.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += this.transform.forward;
        if (Input.GetKey(KeyCode.S))
            direction -= this.transform.forward;
        if (Input.GetKey(KeyCode.D))
            direction += this.transform.right;
        if (Input.GetKey(KeyCode.A))
            direction -= this.transform.right;
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKey(KeyCode.F))
            GameController.Instance.SwitchText("dziala");
        if (Input.GetKey(KeyCode.E))
            GameController.Instance.SwitchText(string.Empty);

        direction.Normalize();

        direction *= (IsOnGround() ? movementSpeed : airControl);
        rigidbody.velocity += direction;

        Vector3 XZSpeed = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z);
        if(XZSpeed.magnitude > movementSpeed)
        {
            XZSpeed.Normalize();
            XZSpeed *= movementSpeed;
            XZSpeed.y = rigidbody.velocity.y;
            rigidbody.velocity = XZSpeed;
        }
        
        //Rotating
        transform.Rotate(Vector3.up, rotationSpeed * Input.GetAxis("Mouse X"));
        Camera.main.transform.localEulerAngles = new Vector3(-rotationSpeed * Input.GetAxis("Mouse Y") + Camera.main.transform.localEulerAngles.x, 0);

        //Jumping
        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            Jump();
        }

        if (activeSpell != null)
            activeSpell.SpellUpdate();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeSpell = new Telekinesis(gameObject);
            Debug.Log("Telekinesis");
        }
	}

    private void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    public bool IsOnGround()
    {
        CapsuleCollider cc = GetComponent<CapsuleCollider>();

        cc.enabled = false;
        bool returned = Physics.Raycast(new Ray(transform.position, Vector3.down), cc.height * transform.localScale.x / 2 + heightTest);
        cc.enabled = true;

        return returned;
    }
}
