using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBotController : MonoBehaviour
{
    [SerializeField]
    DoorController door;
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
       ColorMatrixController matrixController = collision.gameObject.GetComponent<ColorMatrixController>();

        if (matrixController)
        {
            StartCoroutine(door.ToggleDoor(true));
        }

        
    }
}
