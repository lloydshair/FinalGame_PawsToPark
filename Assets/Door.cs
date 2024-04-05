using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour

{
    public bool isOpen;
    public Animator animator;
    public void OpenDoor()
    {
        if (!isOpen )
        {
            isOpen = true;
            Debug.Log("Door is now open...");
            animator.SetBool("isOpen", isOpen);
        }
    }
}
