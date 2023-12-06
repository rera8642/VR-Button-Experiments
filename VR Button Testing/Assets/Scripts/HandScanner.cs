using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandScanner : MonoBehaviour
{
    [SerializeField] private LayerMask handLayer;
    [SerializeField] private float holdSeconds;
    [SerializeField] private AudioSource audioSource;

    private float secendsHeld;
    private bool isOn = false;
    private bool canStart = true;

    public UnityEvent onPressed;

    private void OnCollisionStay(Collision collision)
    {
        if (canStart) {
            if ((handLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            {
                if (!isOn)
                {
                    audioSource.Play();
                    isOn = true;
                }
                secendsHeld += Time.deltaTime;
                if (secendsHeld >= holdSeconds)
                {
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                    canStart = false;
                    Pressed();
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((handLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            audioSource.Stop();
            isOn = false;
            secendsHeld = 0;
        }
    }

    void Pressed()
    {
        onPressed.Invoke();
    }
}
