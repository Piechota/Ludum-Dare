﻿using UnityEngine;
using System.Collections;

public class Telekinesis : Spell {

    TelekinesisObject selected;
    Vector2 startMouse = Vector2.zero;
    Vector3 oldPlayerPosition;
    Vector3 playerDeltaPosition { get { return player.transform.position - oldPlayerPosition; } }

    Vector3 offset;
    float oldPlayerSpeed;
    

    //Noise directionNoise = new Noise(0);

    public Telekinesis(GameObject player) : base(player) { }

    public override void SpellUpdate()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Input.GetMouseButtonUp(0) && selected)
        {
            selected.isInForce = false;
            selected.rigidbody.useGravity = true;
            selected.rigidbody.freezeRotation = false;

            playerMC.movementSpeed = oldPlayerSpeed;

            selected = null;
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            if (selected == null)
            {
                if (Physics.Raycast(ray, out hit, playerMC.maxDistance))
                {
                    if (hit.collider.GetComponent<TelekinesisObject>())
                    {
                        selected = hit.collider.GetComponent<TelekinesisObject>();
                        selected.UseForce();

                        startMouse = Vector2.zero;

                        oldPlayerPosition = player.transform.position;

                        offset = selected.transform.InverseTransformPoint(hit.point);

                        oldPlayerSpeed = playerMC.movementSpeed;
                        playerMC.movementSpeed /= (selected.rigidbody.mass / 100);
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
                
                selected.rigidbody.velocity = Vector3.zero;

                if (playerDeltaPosition.magnitude != 0)
                {
                    selected.transform.position = selected.transform.position + playerDeltaPosition;
                    oldPlayerPosition = player.transform.position;
                }

                Vector3 direction = Vector3.zero;

                float angle = startMouse.x / (playerMC.rotateDrag * selected.rigidbody.mass);
                angle = (angle > playerMC.maxObjectRotateSpeed) ? playerMC.maxObjectRotateSpeed : angle;
                selected.transform.RotateAround(player.transform.position, Vector3.up, angle);

                

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject != player.gameObject && hit.collider.gameObject != selected.gameObject)
                    {
                        selected.transform.RotateAround(player.transform.position, Vector3.up, -angle);
                        startMouse = Vector2.zero;
                    }
                }

                if(Input.GetMouseButton(1))
                {
                        direction = selected.transform.position - player.transform.position;
                        direction.y = 0.0f;
                        direction.Normalize();
                }
                else
                {
                    angle = startMouse.y / (playerMC.rotateDrag * selected.rigidbody.mass);
                    angle = (angle > playerMC.maxObjectRotateSpeed) ? playerMC.maxObjectRotateSpeed : angle;
                    selected.transform.RotateAround(player.transform.position, player.transform.right, -angle);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if ((hit.collider.gameObject != player.gameObject && hit.collider.gameObject != selected.gameObject) ||
                            (new Vector2(hit.point.x, hit.point.z) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude < playerMC.minDistance ||
                            (hit.point - player.transform.position).magnitude > playerMC.maxDistance)
                        {
                            selected.transform.RotateAround(player.transform.position, player.transform.right, angle);
                            startMouse = Vector2.zero;
                        }
                    }
                }

                direction *= startMouse.y / (playerMC.moveDrag * selected.rigidbody.mass * selected.rigidbody.mass);

                direction = (direction.magnitude > playerMC.maxObjectSpeed) ? (direction.normalized * playerMC.maxObjectSpeed) : direction;

                Vector3 selectedPosition = selected.transform.position + direction;

                Vector3 oldObjectPosition = selected.transform.position;
                selected.rigidbody.MovePosition(selectedPosition);
                

                if (Physics.Raycast(ray, out hit))
                {
                    if ((hit.collider.gameObject != player.gameObject && hit.collider.gameObject != selected.gameObject)||
                        (new Vector2(hit.point.x, hit.point.z) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude < playerMC.minDistance ||
                        (hit.point - player.transform.position).magnitude > playerMC.maxDistance)
                    {
                        selected.transform.position = oldObjectPosition;
                        startMouse = Vector2.zero;
                    }
                }

                Vector3 playerLookAt = selected.transform.TransformPoint(offset) - player.transform.position;
                playerLookAt.y = 0.0f;
                player.transform.LookAt(playerLookAt + player.transform.position);
                Camera.main.transform.LookAt(selected.transform.TransformPoint(offset));
                // + new Vector3(Mathf.Sin(randomDirection),  Mathf.Cos(Mathf.PI/2 - , 0)
            }
        }
    }
}
