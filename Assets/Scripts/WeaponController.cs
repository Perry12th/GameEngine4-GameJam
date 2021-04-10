using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    float fireRange;
    [SerializeField]
    float fireRate;
    [SerializeField]
    ColorState weaponState;
    [SerializeField]
    Material weaponMaterial;
    [SerializeField]
    TextMeshProUGUI weaponStateText;
    [SerializeField]
    LayerMask hittableItems;
    [SerializeField]
    bool readyToFire;
    [SerializeField]
    float thunderForce;
    [SerializeField]
    AudioSource fireSound;
    [SerializeField]
    AudioSource switchSound;

    // Start is called before the first frame update
    void Awake()
    {
        weaponStateText.text = "GrowthMode";
        weaponStateText.color = Color.green;
        weaponMaterial.SetColor("_EmissionColor", Color.green);
        weaponState = ColorState.GROWTH;
        readyToFire = true;
    }


    public void OnFire(InputAction.CallbackContext value)
    { 
        if (value.started && readyToFire && isActiveAndEnabled)
        {
            StartCoroutine(Fire());
        }
    }


    private IEnumerator Fire()
    {
        readyToFire = false;
        fireSound.Play();
        RaycastHit hit;
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.TransformDirection(Vector3.forward), out hit, fireRange, hittableItems))
        {
            ColorMatrixController matrixController = hit.transform.gameObject.GetComponent<ColorMatrixController>();
            if (matrixController)
            {
                if (weaponState == ColorState.THUNDER)
                {
                    Rigidbody rigidbody = hit.transform.gameObject.GetComponent<Rigidbody>();
                    Vector3 direction = Vector3.Normalize(hit.point - firePoint.transform.position);
                    rigidbody.AddForceAtPosition(direction * thunderForce, hit.point);
                }
                matrixController.ChangeColorMartix(weaponState);
            }
        }

        yield return new WaitForSeconds(fireRate);
        readyToFire = true;
    }

    public void Switch2Green(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            switchSound.Play();
            weaponStateText.text = "GrowthMode";
            weaponStateText.color = Color.green;
            weaponMaterial.SetColor("_EmissionColor", Color.green);
            weaponState = ColorState.GROWTH;
        }
    }

    public void Switch2Red(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            switchSound.Play();
            weaponStateText.text = "ShrinkMode";
            weaponStateText.color = Color.red;
            weaponMaterial.SetColor("_EmissionColor", Color.red);
            weaponState = ColorState.TINY;
        }
    }

    public void Switch2Blue(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            switchSound.Play();
            weaponStateText.text = "HoverMode";
            weaponStateText.color = Color.blue;
            weaponMaterial.SetColor("_EmissionColor", Color.blue);
            weaponState = ColorState.HOVER;
        }
    }

    public void Switch2Yellow(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            switchSound.Play();
            weaponStateText.text = "ThunderMode";
            weaponStateText.color = Color.yellow;
            weaponMaterial.SetColor("_EmissionColor", Color.yellow);
            weaponState = ColorState.THUNDER;
        }
    }

    public void Switch2White(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            switchSound.Play();
            weaponStateText.text = "NaturalMode";
            weaponStateText.color = Color.white;
            weaponMaterial.SetColor("_EmissionColor", Color.white);
            weaponState = ColorState.NATURAL;
        }
    }
}
