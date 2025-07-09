using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet Behavior
// Two status: Flying and Hit
// Three behavior: Move Forward, Hit something, Destroyed
[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehavior : MonoBehaviour
{
    // Status

    protected Rigidbody2D mRigidBody = null;
    public enum BulletStatus
    {
        Flying, Crash, Destroyed
    };

    // information about Bullet should include its speed
    public float mSpeed = 10f;
    public float mFlyingDuration = 3f;
    public float mHitDuration = 1f;
    protected BulletStatus mBulletStatus = BulletStatus.Flying;
    protected float mStatusTime = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Moving forward at a constant speed
    public void Move()
    {
        mRigidBody.velocity = mSpeed * transform.right;
    }

    // Hit something, the wall or humanoid
    public void Hit()
    {
        
        mRigidBody.velocity = Vector3.zero;
        
    }

    // Destroyed it self
    public void Destroy()
    {
        Debug.Log("Hit");
        Destroy(transform.gameObject);
    }

}
