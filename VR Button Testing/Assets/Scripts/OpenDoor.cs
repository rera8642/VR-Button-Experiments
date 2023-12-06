using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;

    public void Open()
    {
        Debug.Log("Open");
        animator.SetBool("Open", true);
    }
}
