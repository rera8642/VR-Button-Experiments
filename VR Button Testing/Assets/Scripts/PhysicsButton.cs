using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadzone = 0.025f;
    [SerializeField] private Transform upperLimit;
    [SerializeField] private AudioSource sound;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed;
    public UnityEvent onReleased;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > upperLimit.position.y) {
            transform.position = upperLimit.position;
        }
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;
        if (Mathf.Abs(value) < deadzone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);
    }

    void Pressed()
    {
        isPressed = true;
        Debug.Log("pressed");
        sound.Play();
        onPressed.Invoke();
    }

    void Released()
    {
        isPressed = false;
        Debug.Log("released");
        onReleased.Invoke();
    }
}
