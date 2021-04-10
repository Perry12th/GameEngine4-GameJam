using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorState
{
    NATURAL,
    GROWTH,
    TINY,
    HOVER,
    THUNDER
}

[RequireComponent(typeof(Rigidbody))]
public class ColorMatrixController : MonoBehaviour
{
    [Header("Components")]
    Rigidbody rigidBody;
    [SerializeField]
    ParticleSystem effects;

    [Header("MartixStats")]
    [SerializeField]
    float GrowthSizeRatio;
    public float TinySizeRatio { get; private set; } = 0.2f;
    [SerializeField]
    float hoverDistance;
    [SerializeField]
    float hoverStrength;
    [SerializeField]
    float hoverDampening;
    [SerializeField]
    float lastHitDistance;
    [SerializeField]
    float growthTime;
    [SerializeField]
    float timer;
    [SerializeField]
    float shrinkTime;
    Vector3 normalScale;
    bool isChanging;
    [SerializeField]
    ColorState state = ColorState.NATURAL;
    private Transform spawnTransform;
    
    // Start is called before the first frame update
    void Awake()
    {
        normalScale = transform.localScale;
        rigidBody = GetComponent<Rigidbody>();
        state = ColorState.NATURAL;
        spawnTransform = transform;
    }

    public void FixedUpdate()
    {
        if (state == ColorState.HOVER || state == ColorState.GROWTH && isChanging)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, hoverDistance))
            {
                float forceAmount = HooksLawDampen(hit.distance);

                rigidBody.AddForceAtPosition(Vector3.up * forceAmount, transform.position);
            }
            else
            {
                lastHitDistance = hoverDistance * 1.1f;
            }
        }

        effects.transform.up = Vector3.up;
    }

    private float HooksLawDampen(float hitDistance)
    {
        float forceAmount = hoverStrength * (hoverDistance - hitDistance) + (hoverDampening * (lastHitDistance - hitDistance));
        forceAmount = Mathf.Max(0f, forceAmount);
        lastHitDistance = hitDistance;

        return forceAmount;
    }

    private IEnumerator Grow()
    {
        Vector3 startScale = transform.localScale;
        Vector3 growToScale = Vector3.zero;

        switch (state)
        {
            case (ColorState.NATURAL):
                growToScale = normalScale;
                break;
            case (ColorState.GROWTH):
                growToScale = normalScale * GrowthSizeRatio;
                break;
        }

        do
        {
            transform.localScale = Vector3.Lerp(startScale, growToScale, timer / growthTime);
            timer += Time.deltaTime;
            yield return null;
        } while (timer < growthTime);

        transform.localScale = growToScale;
        isChanging = false;
        timer = 0;
        StopCoroutine(Grow());
    }

    private IEnumerator Shrink()
    {
        Vector3 startScale = transform.localScale;
        Vector3 shrinkToScale = Vector3.zero;

        switch (state)
        {
            case (ColorState.NATURAL):
                shrinkToScale = normalScale;
                break;
            case (ColorState.TINY):
                shrinkToScale = normalScale * TinySizeRatio;
                break;
        }

        do
        {
            transform.localScale = Vector3.Lerp(startScale, shrinkToScale, timer / shrinkTime);
            timer += Time.deltaTime;
            yield return null;
        } while (timer < shrinkTime);

        transform.localScale = shrinkToScale;
        isChanging = false;
        timer = 0;
        StopCoroutine(Shrink());
    }

    public ColorState getState() 
    {
        return state;
    }
    public void ChangeColorMartix(ColorState incomingState)
    {
        if (!isChanging && incomingState != state)
        {
            var main = effects.main;
            switch (incomingState)
            {
                case (ColorState.NATURAL):
                    main.startColor = Color.white;
                    switch (state)
                    {
                        case (ColorState.GROWTH):
                            state = incomingState;
                            StartCoroutine(Shrink());
                            break;
                        case (ColorState.TINY):
                            state = incomingState;
                            StartCoroutine(Grow());
                            break;
                        case (ColorState.HOVER):
                            state = incomingState;
                            rigidBody.freezeRotation = false;
                            break;  
                        default:
                            state = incomingState;
                            break;
                            
                    }
                    break;
                //case (ColorState.THUNDER):
                //    main.startColor = Color.yellow;
                //    break;
                case (ColorState.GROWTH):
                    main.startColor = Color.green;
                    state = incomingState;
                    StartCoroutine(Grow());
                    break;
                case (ColorState.TINY):
                    main.startColor = Color.red;
                    state = incomingState;
                    StartCoroutine(Shrink());
                    break;
                case (ColorState.HOVER):
                    if (state == ColorState.NATURAL)
                    {
                        main.startColor = Color.blue;
                        state = incomingState;
                        rigidBody.freezeRotation = true;
                    }
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            transform.position = spawnTransform.position + new Vector3(0, 3, 0);
            transform.rotation = spawnTransform.rotation;
            ChangeColorMartix(ColorState.NATURAL);
        }
    }
}
