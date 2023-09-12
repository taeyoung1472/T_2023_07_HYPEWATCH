using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestPlayerMovementController : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float jumpForce;

    protected Rigidbody rb;

    private float distanceToFeet;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        distanceToFeet = GetComponent<Collider>().bounds.extents.y;
    }

    protected virtual void Update()
    {
        Move();
        Jump();
    }
    protected virtual void Move()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput *= movementSpeed * Time.deltaTime;
        transform.Translate(new Vector3(movementInput.x, 0f, movementInput.y));
    }
    protected virtual void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    protected bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToFeet * 0.1f);
    }
    
}
