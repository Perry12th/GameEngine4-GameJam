using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    GameObject doorWay;
    [SerializeField]
    bool isOpen = false;
    [SerializeField]
    Vector3 openPositon;
    [SerializeField]
    Vector3 closedPositon;
    [SerializeField]
    float toggleTime;
    [SerializeField]
    float timer;
    private void Awake()
    {
        closedPositon = doorWay.transform.localPosition;
        timer = 0;
    }
    public IEnumerator ToggleDoor(bool open)
    {
        Vector3 startPosition = doorWay.transform.localPosition;
        Vector3 endPosition = open ? openPositon : closedPositon;
        isOpen = open;
        do
        {
            doorWay.transform.localPosition = Vector3.Lerp(startPosition, endPosition, timer / toggleTime);
            timer += Time.deltaTime;
            yield return null;
        } while (timer < toggleTime);

        doorWay.transform.localPosition = endPosition;
        timer = 0;
        StopCoroutine(ToggleDoor(open));
    }
}
