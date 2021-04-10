using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupController : MonoBehaviour
{
    [SerializeField]
    float pickUpRange = 5;
    [SerializeField]
    Transform holdParent;
    [SerializeField]
    float moveForce;
    private GameObject heldObj;
    [SerializeField]
    GameObject weaponSocket;
    [SerializeField]
    GameObject crosshairUI;
    [SerializeField]
    LayerMask pickupLayer;
    [SerializeField]
    AudioSource pickSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObj != null)
        {
            MoveObject();
        }
        else
        {
            weaponSocket.SetActive(true);
            crosshairUI.SetActive(true);
        }
    }

    public void OnPickup(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, pickupLayer))
                {
                    PickupObject(hit.transform.gameObject);
                    pickSound.Play();
                }
            }
            else
            {
                DropObject();
            }
        }

        
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject (GameObject pickObj)
    {
        ColorMatrixController martixObject = pickObj.GetComponent<ColorMatrixController>();
        if (martixObject && martixObject.getState() != ColorState.HOVER)
        {
            Rigidbody objRig = martixObject.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;
            objRig.freezeRotation = true;

            objRig.transform.parent = holdParent;
            heldObj = pickObj;

            weaponSocket.SetActive(false);
            crosshairUI.SetActive(false);
        }
    }

    void DropObject ()
    {
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        heldObj.GetComponent<Rigidbody>().useGravity = true;
        heldRig.drag = 1;
        heldRig.freezeRotation = false;

        heldRig.transform.parent = null;
        heldObj = null;

        weaponSocket.SetActive(true);
        crosshairUI.SetActive(true);
    }
}
