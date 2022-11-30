using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    RaycastHit hit;
    public float hand_Distance;
    public float distance;
    public bool useInteract = false;
    public LayerMask whatIsTarget;

    private Vector3 rayOrigin;
    private Vector3 rayDir;
    private Transform moveTarget;
    private Color originColor;

    private float targetDistance;

    // Update is called once per frame
    void Update()
    {
        rayOrigin 
            = gameObject.transform.position + 
            gameObject.transform.forward * hand_Distance;
        rayDir = gameObject.transform.forward * distance;

        Ray ray = new Ray(rayOrigin, rayDir);

        Debug.DrawRay(rayOrigin, rayDir, Color.green);
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, distance, whatIsTarget))
            {
                GameObject hitTarget = hit.collider.gameObject;
                originColor = hit.collider.GetComponent<Renderer>().material.color;

                hitTarget.GetComponent<Renderer>().material.color = Color.yellow;

                moveTarget = hitTarget.transform;
                targetDistance = hit.distance;

                useInteract = true;

                //Debug.Log(hit.collider.gameObject.name);
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            if(moveTarget != null)
            {
                moveTarget.GetComponent<Renderer>().material.color = originColor;
            }
            moveTarget = null;
            useInteract = false;

        }

        if (moveTarget != null)
        {
            moveTarget.position = ray.origin + ray.direction * targetDistance;
        }

    }
}
