using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Modified from PickUpScript by JonDevTutorials
    // https://github.com/JonDevTutorial/PickUpTutorial/blob/main/PickUpScript.cs

    [SerializeField] private GameObject player; // Player object
    [SerializeField] private Transform holdPos; // Position at which object is held
    [SerializeField] private float throwForce = 700.0f; //force at which the object is thrown at
    [SerializeField] private float pickupDistance = 3.0f;

    private GameObject heldObj; 
    private Rigidbody heldObjRB;
    private Outline heldObjOutline;

    void Update()
    {
        // This is terribly inefficient, feel free to change it :)
        foreach (var obj in HoldableObject.Objects)
        {
            var d = (player.transform.position - obj.transform.position).sqrMagnitude;
            if (d < pickupDistance)
            {
                obj.GetComponent<Outline>().enabled = true;
            }
            else
            {
                obj.GetComponent<Outline>().enabled = false;
            }
        }

            if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            if (heldObj == null) //if currently not holding anything
            {
                float dist = 100.0f;
                HoldableObject targetObj = null;
                foreach (var obj in HoldableObject.Objects)
                {
                    var d = (player.transform.position - obj.transform.position).sqrMagnitude;
                    if (d < pickupDistance)
                    {
                        if (d < dist)
                        {
                            targetObj = obj;
                            dist = d;
                        }
                    }
                }

                if (targetObj != null)
                {
                    PickUpObject(targetObj.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            if (Input.GetKeyDown(KeyCode.Mouse0)) //Mouse 0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                ThrowObject();
            }

        }
    }

    void PickUpObject(GameObject pickup)
    {   
        if (pickup.GetComponent<Rigidbody>())
        {
            heldObj = pickup;
            heldObjRB = pickup.GetComponent<Rigidbody>();
            heldObjOutline = pickup.GetComponent<Outline>();
            heldObjOutline.enabled = false;
            heldObjRB.isKinematic = true;
            heldObjRB.transform.parent = holdPos.transform; //parent object to holdposition
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRB.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }
    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRB.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRB.AddForce(holdPos.transform.forward * throwForce);
        heldObj = null;
    }
}
