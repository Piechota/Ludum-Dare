using UnityEngine;
using System.Collections;

public class Telekinesis : Spell {

    TelekinesisObject selected;
    Vector2 startMouse = Vector2.zero;
    Vector3 oldPlayerPosition;
    Vector3 playerDeltaPosition { get { return player.transform.position - oldPlayerPosition; } }

    float minDistance = 0.1f;
    float maxDistance = 2.5f;
    float maxObjectSpeed = 3.0f;
    float maxObjectRotateSpeed = 5.0f;

    //Noise directionNoise = new Noise(0);

    public Telekinesis(GameObject player) : base(player) { }

    public override void SpellUpdate()
    {
        if (Input.GetMouseButtonUp(0) && selected)
        {
            selected.isInForce = false;
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

                        selected.isInForce = true;

                        oldPlayerPosition = player.transform.position;
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
                //if (Input.GetKeyDown(KeyCode.W) ||
                //    Input.GetKeyDown(KeyCode.S) ||
                //    Input.GetKeyDown(KeyCode.D) ||
                //    Input.GetKeyDown(KeyCode.A))
                //{
                //    selected.transform.position = new Vector3(player.transform.position.x + distanceToObject.x,
                //                                               selected.transform.position.y,
                //                                               player.transform.position.z + distanceToObject.y);
                //}
               
                
                selected.rigidbody.velocity = Vector3.zero;

                if (playerDeltaPosition.magnitude != 0)
                {
                    selected.transform.position = selected.transform.position + playerDeltaPosition;
                    oldPlayerPosition = player.transform.position;
                }

                Vector3 direction = Vector3.zero;

                float angle = startMouse.x / (100 * selected.rigidbody.mass);
                angle = (angle > maxObjectRotateSpeed) ? maxObjectRotateSpeed : angle;
                selected.transform.RotateAround(player.transform.position, Vector3.up, angle);

                RaycastHit hit;
                if (Physics.Raycast(player.transform.position, selected.transform.position - player.transform.position, out hit))
                {
                    if (hit.collider.gameObject != player.gameObject && hit.collider.gameObject != selected.gameObject)
                    {
                        selected.transform.RotateAround(player.transform.position, Vector3.up, -angle);
                    }
                }

                
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
                

                if (Physics.Raycast(player.transform.position, selectedPosition - player.transform.position, out hit))
                {
                    if (hit.collider.gameObject == player.gameObject || hit.collider.gameObject == selected.gameObject)
                    {
                        selectedPosition = ((selectedPosition - player.transform.position).magnitude < minDistance) ? selected.transform.position : selectedPosition;
                        selectedPosition = ((selectedPosition - player.transform.position).magnitude > maxDistance) ? selected.transform.position : selectedPosition;
                    }
                    else
                    {
                        selectedPosition = selected.transform.position;
                    }
                }

                //selected.transform.position = new Vector3(player.transform.position.x + distanceToObject.x,
                //                                           selected.transform.position.y,
                //                                           player.transform.position.z + distanceToObject.y);

                selected.rigidbody.MovePosition(selectedPosition);

                Vector3 playerLookAt = selected.transform.position - player.transform.position;
                playerLookAt.y = 0.0f;
                player.transform.LookAt(player.transform.position + playerLookAt);
                Camera.main.transform.LookAt(selected.transform.position);
                // + new Vector3(Mathf.Sin(randomDirection),  Mathf.Cos(Mathf.PI/2 - , 0)
            }
        }
    }
}
