using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnReload(InputValue inputValue)
    {
        animator.SetTrigger("Reload");
    }

    private void OnFire(InputValue inputValue)
    {
        animator.SetTrigger("Fire");
    }
}
