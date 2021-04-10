using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutTargetController : MonoBehaviour
{
    [SerializeField]
    TargetPuzzleContorller targetPuzzleContorller;
    [SerializeField]
    GameObject targetingObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetingObject)
        {
            targetPuzzleContorller.OnTargetHit();
        }
    }
}
