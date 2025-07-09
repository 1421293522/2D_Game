using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HeroBehavior : HumanoidBehavior
{
    
    private float mDashSpeed = 2f;
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mRigidBody.freezeRotation = true;
    }
    void Update()
    {
        // Test Controller
        mMoveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (mDirection.magnitude != 0)
            Move();
        else
            Idle();

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
        
    }

    void Dash()
    {
        mRigidBody.AddForce(10 * mDirection, ForceMode2D.Impulse);
    }

    public override void Shoot()
    {
        if (Time.time - mShootTimer > mShootRate)
        {
            base.Shoot();
            mShootTimer = Time.time;
        }
    }
}