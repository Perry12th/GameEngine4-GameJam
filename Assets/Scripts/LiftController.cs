using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
    [SerializeField]
    Transform newSpawnTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<MovementController>().startingPoint = newSpawnTransform.position;
        }
    }
}
