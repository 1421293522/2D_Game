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
        Init();
    }
    void Update()
    {
        
        
    }

    void Dash()
    {
        mRigidBody.AddForce(10 * mDirection, ForceMode2D.Impulse);
    }

    public override void Idle()
    {
        base.Idle();
        mAnimator.SetFloat("MoveSpeed", 0f);
    }

    public override void Move()
    {
        base.Move();
        mAnimator.SetFloat("MoveSpeed", 1f);
    }
    public override void Shoot()
    {
        mAnimator.SetTrigger("Shoot");
        if (Time.time - mShootTimer > mShootRate)
        {
            base.Shoot();
            mShootTimer = Time.time;
        }
    }
}