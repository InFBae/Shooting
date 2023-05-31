using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;

    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDir;
    private float moveSpeed;
    private float ySpeed = 0;
    private bool isGround;
    private bool isWalking;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGround = GroundCheck();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (moveDir.magnitude == 0)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f);
        }
        else if (isWalking)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, 0.5f);

        }


        //controller.Move(moveDir * moveSpeed * Time.deltaTime);  월드 기준 움직임
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

        animator.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
        animator.SetFloat("ZSpeed", moveDir.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Speed", moveSpeed);
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }

    private void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (isGround && ySpeed < 0)
        {
            ySpeed = 0;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnJump(InputValue inputValue)
    {
        if(isGround)
        {
            ySpeed = jumpSpeed;
        }       
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.6f); ;
    }

    private void OnWalk(InputValue inputValue)
    {
        isWalking = inputValue.isPressed;
    }
}