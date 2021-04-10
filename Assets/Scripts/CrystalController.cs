using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField]
    MeshCollider meshCollider;
    [SerializeField]
    Rigidbody Rigidbody;
    [SerializeField]
    Transform startingLocation;
    [SerializeField]
    Transform endingLocation;
    [SerializeField]
    AltarRoomController AltarRoomController;
    public bool isOnAltar;

    public void Start()
    {
        startingLocation = transform;
        isOnAltar = false;
    }

    public void SetAtStartingLocation()
    {
        transform.position = startingLocation.position;
        isOnAltar = false;
    }

    public void SetAtFinishLocation()
    {
        transform.position = endingLocation.position;
        isOnAltar = true;
        meshCollider.isTrigger = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isOnAltar)
        {
            SetAtFinishLocation();
            AltarRoomController.OnCrystalGained();
        }
    }
}
