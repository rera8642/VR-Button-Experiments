using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{

    public Transform target;
    public float rotationMultiplier;

    private Collider[] handColliders;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
    }

    public void EnableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = true;
        }
    }

    public void EnableHandColliderDelay(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }

    public void DisableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegrees = angleInDegrees * rotationAxis + 90 * rotationMultiplier * transform.forward;

        rb.angularVelocity = (rotationDifferenceInDegrees * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
