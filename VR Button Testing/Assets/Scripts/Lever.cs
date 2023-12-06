using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public UnityEvent onPressed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 2) {
            Pressed();
        }
    }

    void Pressed()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        onPressed.Invoke();
    }
}
