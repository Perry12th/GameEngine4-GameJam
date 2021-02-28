﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float walkSpeed = 10.0f;
    [SerializeField]
    float playerGravity = -30.0f;
    [SerializeField]
    float jumpHeight = 3.0f;
    Vector3 velocity;
    Vector2 moveDelta;

    [Header("Ground Check")]
    [SerializeField]
    LayerMask groundMask;
    [SerializeField]
    float groundLimit;
    RaycastHit groundCheck;
    public bool isGrounded;

    [Header("Character Controller")]
    CharacterController controller;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out groundCheck, groundLimit, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // gravity
        velocity.y += playerGravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // movement
        Vector3 movement = transform.right * moveDelta.x + transform.forward * moveDelta.y;
        controller.Move(movement * walkSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveDelta = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (isGrounded && value.started)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * playerGravity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
    }
}