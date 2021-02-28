using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    DoorController door;
    [SerializeField]
    Animator animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crystal"))
        {
            ColorMatrixController matrixController = collision.gameObject.GetComponent<ColorMatrixController>();
            if (matrixController.getState() == ColorState.GROWTH)
            {
                StartCoroutine(door.ToggleDoor(true));
                Destroy(collision.gameObject);
            }
            
        }
    }
}
