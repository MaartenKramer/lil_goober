using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public InputActionReference moveInput;
    public InputActionReference rotateInput;

    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float rotateSpeed = 12f;

    Vector3 velocity;
    public float gravity = -20f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        
    }

    void Update()
    {
        CheckGround();
        RotatePlayer();
        MovePlayer();
        ApplyGravity(gravity);
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // checks if player is on ground

        if (isGrounded && velocity.y < 0) // resets velocity when player is grounded
        {
            velocity.y = -2f;
        }
    }

    private void RotatePlayer()
    {
        float rotationAmount = rotateInput.action.ReadValue<float>();
        float rotation = rotationAmount * rotateSpeed * Time.deltaTime;

        transform.Rotate(0, rotation, 0);
    }

    private void MovePlayer()
    {
        float moveAmount = moveInput.action.ReadValue<float>();
        Vector3 direction = new Vector3(0f, 0f, moveAmount).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.eulerAngles.y;

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity(float grav)
    {
        velocity.y += grav * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
