using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] float reloadTime;
    [SerializeField] Rig aimRig;
    [SerializeField] WeaponHolder weaponHolder;

    private Animator animator;
    private bool isReloading;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponHolder = GetComponentInChildren<WeaponHolder>();
    }
    private void OnReload(InputValue inputValue)
    {
        if (isReloading)
        {
            return;
        }
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        animator.SetTrigger("Reload");
        isReloading = true;
        aimRig.weight = 0;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        aimRig.weight = 1;

    }
    private void OnFire(InputValue inputValue)
    {
        if (isReloading) { return; }
        Fire();
    }

    private void Fire()
    {
        animator.SetTrigger("Fire");
        weaponHolder.Fire();
    }
}
