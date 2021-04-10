using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    GameObject objectToDisplay;
    [SerializeField]
    Transform objectDisplayPoint;
    [SerializeField]
    BoxCollider Collider;
    public bool displayingObject = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToDisplay && !displayingObject)
        {
            ColorMatrixController matrixController = other.gameObject.GetComponent<ColorMatrixController>();
            if (matrixController.getState() == ColorState.TINY)
            {
                SetDisplayObject();
            }
        }
    }

    public void SetDisplayObject()
    {
        objectToDisplay.transform.position = objectDisplayPoint.position;
        ColorMatrixController matrixController = objectToDisplay.GetComponent<ColorMatrixController>();
        if (matrixController.getState() != ColorState.TINY)
        {
            objectToDisplay.transform.localScale = objectToDisplay.transform.localScale * matrixController.TinySizeRatio;
        }
        matrixController.enabled = false;
        objectToDisplay.GetComponent<Rigidbody>().useGravity = false;
        objectToDisplay.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        objectToDisplay.GetComponentInChildren<ParticleSystem>().Stop();
        displayingObject = true;
    }
}
