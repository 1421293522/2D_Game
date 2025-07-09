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
    private int mHealth = 100;
    private const int kMaxHealth = 100;
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

    public new void Shoot()
    {
        if (Time.time - mShootTimer > mShootRate)
        {
            base.Shoot();
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            int damage = collision.gameObject.GetComponent<BulletBehavior>().getDamage();
            if (damage > 0)
            {
                mHealth -= damage;
            }
        }
        else if (collision.CompareTag("BloodPackage"))
        {
            //Debug.Log("+10");
            BloodPackage bp = collision.GetComponent<BloodPackage>();
            bp.Delete();
            if(mHealth+10 > kMaxHealth) { mHealth = kMaxHealth; } 
            else { mHealth = mHealth + 10; }
        }
    }

    public int getHealth()
    {
        return mHealth;
    }
}