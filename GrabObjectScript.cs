using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectScript : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;

    [SerializeField] private float rayDistance;

    private GameObject grabbedObject;
    private int layerIndex;

    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Object");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        //Debug.Log(hitInfo.collider.gameObject.layer);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {

            if (Input.GetKey(KeyCode.F) && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.parent = grabPoint;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;

              
            }
            else if (Input.GetKey(KeyCode.D))
            {
                grabbedObject.transform.parent = null;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
               
                grabbedObject = null;


            }

        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);

    }
}
