using UnityEngine;
using System.Collections;

public class Telekinesis : Spell {

    TelekinesisObject selected;
    Vector2 startMouse = Vector2.zero;
    Vector3 distanceToObject;

    float minDistance = 0.5f;
    float maxDistance = 2.5f;
    float maxObjectSpeed = 3.0f;
    float maxObjectRotateSpeed = 5.0f;

    //Noise directionNoise = new Noise(0);

    public Telekinesis(GameObject player) : base(player) { }

    public override void SpellUpdate()
    {
        if (Input.GetMouseButtonUp(0) && selected)
        {
            selected.GetComponent<Rigidbody>().useGravity = true;
            selected = null;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (selected == null)
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    if (hit.collider.GetComponent<TelekinesisObject>())
                    {
                        selected = hit.collider.GetComponent<TelekinesisObject>();
                        selected.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
                        selected.GetComponent<Rigidbody>().useGravity = false;
                        startMouse = Vector2.zero;

                        distanceToObject = selected.transform.position - player.transform.position;
                    }
                    else { /*tu bedzie jakiś dzwiek, particle itp oznaczajace niepowodzenie*/}
                }
            }
        }

        if(Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonDown(1))
            {
                startMouse = Vector2.zero;
            }

            startMouse.x += Input.GetAxis("Mouse X");
            startMouse.y += Input.GetAxis("Mouse Y");

            if(selected)
            {
                //directionNoise.change(0.99f, 0.1f);
                //float randomDirection = directionNoise.get();
                /*if (Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.A))
                    selected.transform.position = player.transform.position + distanceToObject;
                */
                selected.rigidbody.velocity = Vector3.zero;

                Vector3 direction = Vector3.zero;

                float angle = startMouse.x / (100 * selected.rigidbody.mass);
                angle = (angle > maxObjectRotateSpeed) ? maxObjectRotateSpeed : angle;
                selected.transform.RotateAround(player.transform.position, Vector3.up, angle);

                
                if(!Input.GetMouseButton(1))
                {
                        direction = selected.transform.position - player.transform.position;
                        direction.y = 0.0f;
                        direction.Normalize();
                }
                else
                {
                    direction = Vector3.up;
                }
                direction *= startMouse.y / (130 * selected.rigidbody.mass * selected.rigidbody.mass);

                direction = (direction.magnitude > maxObjectSpeed) ? (direction.normalized * maxObjectSpeed) : direction;

                Vector3 selectedPosition = selected.transform.position + direction;
                selectedPosition = ((selectedPosition - player.transform.position).magnitude < minDistance) ? selected.transform.position : selectedPosition;
                selectedPosition = ((selectedPosition - player.transform.position).magnitude > maxDistance) ? selected.transform.position : selectedPosition;
                selected.rigidbody.MovePosition(selectedPosition);

                Vector3 playerLookAt = selected.transform.position - player.transform.position;
                playerLookAt.y = 0.0f;
                player.transform.LookAt(player.transform.position + playerLookAt);
                Camera.main.transform.LookAt(selected.transform.position);
                // + new Vector3(Mathf.Sin(randomDirection),  Mathf.Cos(Mathf.PI/2 - , 0)
                distanceToObject = selected.transform.position - player.transform.position;
            }
        }
    }
}
