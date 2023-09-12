using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenjiMovementController : TestPlayerMovementController
{
    private int jumpCount = 0;
    protected override void Update()
    {
        base.Update();

        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }
    protected override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount == 0)
            {
                rb.velocity = Vector3.up * jumpForce;

                if (IsGrounded())
                {
                    jumpCount = 1;
                }
                else
                {
                    jumpCount = 2;
                }
            }
            else if (jumpCount == 1)
            {
                rb.velocity = Vector3.up * jumpForce;
                jumpCount = 2;
            }
        }
    }
    
}
